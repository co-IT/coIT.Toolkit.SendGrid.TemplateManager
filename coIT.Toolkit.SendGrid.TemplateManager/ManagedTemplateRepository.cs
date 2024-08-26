using System.Diagnostics;
using System.Text;
using coIT.Libraries.Sendgrid.Contracts;
using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public static class ManagedTemplateRepository
{
  private static readonly string LokalerPfad = "D:\\co-IT.eu GmbH\\Uli Armbruster - Cyber Lounge\\SendGrid\\database";

  static ManagedTemplateRepository()
  {
    if (!Directory.Exists(LokalerPfad))
      Directory.CreateDirectory(LokalerPfad);
  }

  public static async Task<List<ManagedTemplate>> LadeAusLokalenKopien(CancellationToken ct)
  {
    var templates = new List<ManagedTemplate>();

    if (!Directory.Exists(LokalerPfad))
      return templates;

    var gespeicherteTemplates = Directory.GetFiles(LokalerPfad, "*.json", SearchOption.TopDirectoryOnly);

    foreach (var gespeichertesTemplate in gespeicherteTemplates)
    {
      if (ct.IsCancellationRequested)
        return templates;

      var template = await LadeAusLokalerKopie(gespeichertesTemplate, ct);
      templates.Add(template!);
    }

    return templates;
  }

  private static async Task<ManagedTemplate?> LadeAusLokalerKopie(string file, CancellationToken ct)
  {
    if (!File.Exists(file))
      return null;

    var content = await File.ReadAllTextAsync(file, Encoding.UTF8, ct);
    var managedTemplate = JsonConvert.DeserializeObject<ManagedTemplate>(content);
    return managedTemplate;
  }

  public static async Task AktualisiereLokaleKopien(IEnumerable<ManagedTemplate> templates, CancellationToken ct)
  {
    foreach (var template in templates)
    {
      if (ct.IsCancellationRequested)
        return;

      await AktualisiereLokaleKopie(template, ct);
    }
  }

  public static async Task AktualisiereLokaleKopie(ManagedTemplate template, CancellationToken ct)
  {
    var content = JsonConvert.SerializeObject(template);
    await File.WriteAllTextAsync(Filepath(template.SendGridTemplate), content, Encoding.UTF8, ct);
  }

  public static async Task AktualisiereLokaleKopie(IEnumerable<SendGridTemplate> templates, CancellationToken ct)
  {
    foreach (var template in templates)
    {
      if (ct.IsCancellationRequested)
        return;

      var managedTemplate = new ManagedTemplate(template);

      var existing = await LadeAusLokalerKopie(Filepath(template), ct);
      if (existing != null)
      {
        managedTemplate.HtmlContent = string.Empty;
        managedTemplate.PlainContent = string.Empty;
        managedTemplate.Tags = existing.Tags;
        managedTemplate.Pakete = existing.Pakete;
        managedTemplate.Einstufung = existing.Einstufung;
        managedTemplate.Absender = existing.Absender;
        managedTemplate.Archiviert = existing.Archiviert;
      }

      await AktualisiereLokaleKopie(managedTemplate, ct);
    }
  }

  private static string Filepath(SendGridTemplate template)
  {
    return Path.Combine(LokalerPfad, Filename(template.TemplateId));
  }

  private static string Filename(string templateId)
  {
    return $"{templateId}.json";
  }

  public static void EditiereLokaleKopie(ManagedTemplate template)
  {
    var file = Filepath(template.SendGridTemplate);

    var psi = new ProcessStartInfo("notepad.exe")
    {
      Arguments = file,
      UseShellExecute = false,
      WindowStyle = ProcessWindowStyle.Hidden,
      CreateNoWindow = false
    };

    Process.Start(psi);
  }
}