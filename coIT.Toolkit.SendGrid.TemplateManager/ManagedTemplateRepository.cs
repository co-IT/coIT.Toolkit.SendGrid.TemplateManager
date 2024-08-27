using System.Diagnostics;
using System.Text;
using coIT.Libraries.Sendgrid.Contracts;
using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public class ManagedTemplateRepository
{
  private readonly string _pfad;

  public static ManagedTemplateRepository Erstellen(string pfad)
  {
    return new ManagedTemplateRepository(pfad);
  }

  private ManagedTemplateRepository(string pfad)
  {
    _pfad = pfad;
  }

  public async Task<List<ManagedTemplate>> LadeAusLokalenKopien(CancellationToken ct)
  {
    var templates = new List<ManagedTemplate>();

    if (!Directory.Exists(_pfad))
      return templates;

    var gespeicherteTemplates = Directory.GetFiles(_pfad, "*.json", SearchOption.TopDirectoryOnly);

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

  public async Task AktualisiereLokaleKopien(IEnumerable<ManagedTemplate> templates, CancellationToken ct)
  {
    foreach (var template in templates)
    {
      if (ct.IsCancellationRequested)
        return;

      await AktualisiereLokaleKopie(template, ct);
    }
  }

  public async Task AktualisiereLokaleKopie(ManagedTemplate template, CancellationToken ct)
  {
    var content = JsonConvert.SerializeObject(template);
    await File.WriteAllTextAsync(Filepath(template.SendGridTemplate), content, Encoding.UTF8, ct);
  }

  public async Task AktualisiereLokaleKopie(IEnumerable<SendGridTemplate> templates, CancellationToken ct)
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

  private string Filepath(SendGridTemplate template)
  {
    return Path.Combine(_pfad, Filename(template.TemplateId));
  }

  private static string Filename(string templateId)
  {
    return $"{templateId}.json";
  }

  public void EditiereLokaleKopie(ManagedTemplate template)
  {
    var file = Filepath(template.SendGridTemplate);

    var psi = new ProcessStartInfo("notepad.exe")
    {
      Arguments = file,
      UseShellExecute = false,
      WindowStyle = ProcessWindowStyle.Hidden,
      CreateNoWindow = false,
    };

    Process.Start(psi);
  }
}
