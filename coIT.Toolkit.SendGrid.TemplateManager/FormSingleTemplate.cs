using System.Diagnostics;
using coIT.Libraries.Sendgrid;
using CSharpFunctionalExtensions;
using SendGrid.Helpers.Mail;
using static System.Windows.Forms.LinkLabel;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class FormSingleTemplate : Form
{
  private readonly SendGridService _sendGridService;

  public FormSingleTemplate()
  {
    InitializeComponent();
  }

  public FormSingleTemplate(ManagedTemplate template, SendGridService sendGridService)
    : this()
  {
    _sendGridService = sendGridService;
    Template = template;
  }

  public ManagedTemplate Template { get; }

  public async void ZeigeTemplate()
  {
    await LadeInWebView2(Template.HtmlContent);

    var domains = new HashSet<string>
    {
      "bund-und-laender.online",
      "contact-it.biz",
      "contact-it.info",
      "der-anders-denker.de",
      "deutsche-verwertungsgesellschaft.de",
      "inspire-to-change.com",
      "tech-zeitung.online",
      "why-how-what.info",
      "arbeitnehmer-vertreter.de",
      "bundesgerichtshof-karlsruhe.de",
      "industrie-un-handelskammer.de",
      "kanzlei-sonnenschauer.de",
      "kununun.de",
      "gesundheitlich-arbeiten.de",
      "apothekernetzwerk.de",
      "anton-schneider-gmbh.de",
      "bdi-bund.de",
    };

    ctrlEinstufungPersRelevanz.DataSource = Enum.GetValues(typeof(Bewertungsskala));
    ctrlEinstufungSprachniveau.DataSource = Enum.GetValues(typeof(Bewertungsskala));
    ctrlEinstufungDruck.DataSource = Enum.GetValues(typeof(Bewertungsskala));
    ctrlEinstufungFormatierung.DataSource = Enum.GetValues(typeof(Bewertungsskala));
    ctrlEinstufungUnternehmensinfo.DataSource = Enum.GetValues(typeof(Bewertungsskala));

    ctrlDomains.Items.AddRange(domains.Select(d => $"@{d}").OrderBy(d => d).ToArray());
    ctrlTrackingLinkDomain.Items.AddRange(domains.OrderBy(d => d).ToArray());

    EinstufungAnzeigen();
    AbsenderAnzeigen();
  }

  private void AbsenderAnzeigen()
  {
    if (Template.Absender is null)
      return;

    var adresse = new List<string> { "", "", "" };
    if (!string.IsNullOrWhiteSpace(Template.Absender.Adresse))
      adresse = Template.Absender.Adresse.Split('@', StringSplitOptions.RemoveEmptyEntries).ToList();

    ctrlAbsenderadresse.Text = adresse.FirstOrDefault() ?? string.Empty;
    ctrlDomains.SelectedItem = $"@{adresse.Last() ?? string.Empty}";

    ctrlAbsendername.Text = Template.Absender.Name;
    ctrlCVAlsAbsendername.Checked = Template.Absender.VonCyberVerantwortlichen;
  }

  private void EinstufenAbbrechen(object sender, EventArgs e)
  {
    DialogResult = DialogResult.Abort;
    Close();
  }

  private void EinstufungSpeichern(object sender, EventArgs e)
  {
    if (Template.Einstufung == null)
      Template.Einstufung = new PhishingMailEinstufung();

    Template.Einstufung.AktuellesThema = ctrlEinstufungAktuellesThema.Checked;
    Template.Einstufung.AuthErsterLink = ctrlEinstufungAuthErsterLink.Checked;
    Template.Einstufung.Bilder = ctrlEinstufungBilder.Checked;
    Template.Einstufung.CVNameVerwendet = ctrlEinstufungCVNameVerwendet.Checked;
    Template.Einstufung.VariablesDatum = ctrlEinstufungVariablesDatum.Checked;
    Template.Einstufung.HatLogo = ctrlEinstufungHatLogo.Checked;
    Template.Einstufung.AuthTrackingLink = ctrlEinstufungAuthTrackingLink.Checked;
    Template.Einstufung.PersonalisierteAnrede = ctrlEinstufungPersonalisierteAndrede.Checked;
    Template.Einstufung.BeruflicheRelevanz = ctrlEinstufungBeruflicheRelevanz.Checked;

    Template.Einstufung.Unternehmensinfo = EinstufungFeststellen(ctrlEinstufungUnternehmensinfo);
    Template.Einstufung.Sprachniveau = EinstufungFeststellen(ctrlEinstufungSprachniveau);
    Template.Einstufung.DruckUndAuswirkung = EinstufungFeststellen(ctrlEinstufungDruck);
    Template.Einstufung.PersoenlicheRelevanz = EinstufungFeststellen(ctrlEinstufungPersRelevanz);
    Template.Einstufung.Formatierung = EinstufungFeststellen(ctrlEinstufungFormatierung);

    Template.Einstufung.AuthAbsenderadresse = ctrlEinstufungAuthAbsenderadresse.Checked;
    Template.Einstufung.AuthAnzeigename = ctrlEinstufungAuthAnzeigename.Checked;

    Template.Einstufung.TrackingLinkDomain = ctrlTrackingLinkDomain.SelectedItem.ToString();
    Template.Einstufung.TrackingLinkPfad = ctrlTrackingLinkPfad.Text;

    Template.Absender = AbsenderEinlesen();

    DialogResult = DialogResult.Yes;
    Close();
  }

  private Absender AbsenderEinlesen()
  {
    return new Absender
    {
      Adresse = $"{ctrlAbsenderadresse.Text}{ctrlDomains.SelectedItem}",
      Name = ctrlCVAlsAbsendername.Checked ? string.Empty : ctrlAbsendername.Text,
      VonCyberVerantwortlichen = ctrlCVAlsAbsendername.Checked,
    };
  }

  private static Bewertungsskala EinstufungFeststellen(ListControl comboBox)
  {
    var valide = Enum.TryParse<Bewertungsskala>(
      (comboBox.SelectedValue ?? string.Empty).ToString(),
      out var einstufung
    );

    return valide ? einstufung : Bewertungsskala.Keine;
  }

  private void EinstufungAnzeigen()
  {
    if (Template.Einstufung == null)
      return;

    ctrlEinstufungAktuellesThema.Checked = Template.Einstufung.AktuellesThema;
    ctrlEinstufungAuthErsterLink.Checked = Template.Einstufung.AuthErsterLink;
    ctrlEinstufungBilder.Checked = Template.Einstufung.Bilder;
    ctrlEinstufungCVNameVerwendet.Checked = Template.Einstufung.CVNameVerwendet;
    ctrlEinstufungVariablesDatum.Checked = Template.Einstufung.VariablesDatum;
    ctrlEinstufungHatLogo.Checked = Template.Einstufung.HatLogo;
    ctrlEinstufungPersonalisierteAndrede.Checked = Template.Einstufung.PersonalisierteAnrede;
    ctrlEinstufungBeruflicheRelevanz.Checked = Template.Einstufung.BeruflicheRelevanz;

    ctrlEinstufungAuthTrackingLink.Checked = Template.Einstufung.AuthTrackingLink;
    ctrlTrackingLinkDomain.SelectedItem = Template.Einstufung.TrackingLinkDomain;
    ctrlTrackingLinkPfad.Text = Template.Einstufung.TrackingLinkPfad;

    ctrlEinstufungPersRelevanz.SelectedItem = Template.Einstufung.PersoenlicheRelevanz;
    ctrlEinstufungSprachniveau.SelectedItem = Template.Einstufung.Sprachniveau;
    ctrlEinstufungDruck.SelectedItem = Template.Einstufung.DruckUndAuswirkung;
    ctrlEinstufungFormatierung.SelectedItem = Template.Einstufung.Formatierung;
    ctrlEinstufungUnternehmensinfo.SelectedItem = Template.Einstufung.Unternehmensinfo;

    ctrlEinstufungAuthAbsenderadresse.Checked = Template.Einstufung.AuthAbsenderadresse;
    ctrlEinstufungAuthAnzeigename.Checked = Template.Einstufung.AuthAnzeigename;
  }

  private void ctrlEinstufungCVAlsAbsendename_CheckedChanged(object sender, EventArgs e)
  {
    if (ctrlCVAlsAbsendername.Checked)
    {
      ctrlAbsendername.Visible = false;
      labelAbsendername.Visible = false;
    }
    else
    {
      ctrlAbsendername.Visible = true;
      labelAbsendername.Visible = true;
    }
  }

  private async Task LadeInWebView2(string html)
  {
    await ctrlHtmlAnzeige.EnsureCoreWebView2Async(null);
    ctrlHtmlAnzeige.NavigateToString(html);
  }

  private void btnOeffneEditor_Click(object sender, EventArgs e)
  {
    var url = Template.SendGridTemplate.EditorUri.AbsoluteUri;

    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
  }

  private async void btnSendTemplate_Click(object sender, EventArgs e)
  {
    var form = new SendTemplateForm();
    var dialogResult = form.ShowDialog();

    if (dialogResult != DialogResult.OK)
      return;

    var email = form.Email;

    var message = new SendGridMessage
    {
      From = new EmailAddress("test@co-IT.eu", "co-IT"),
      TemplateId = Template.SendGridTemplate.TemplateId,
      Personalizations = [new Personalization { Tos = [new EmailAddress(email.Address, "Testuser")] }],
    };

    var result = await _sendGridService.SendTemplateAsync(message);

    result.Match(() => MessageBox.Show("Erfolgreich gesendet"), MessageBox.Show);
  }
}
