namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class PaketUebersichtForm : Form
{
  private readonly List<ManagedTemplate> _alleTemplates;

  public PaketUebersichtForm(List<ManagedTemplate> alleTemplates)
  {
    InitializeComponent();

    _alleTemplates = alleTemplates;
  }

  private void PaketUebersicht_Load(object sender, EventArgs e)
  {
    var pakete = Enum.GetValues<Paket>();

    foreach (var paket in pakete)
    {
      var paketName = Enum.GetName(paket);

      var templatesInPaket = _alleTemplates
        .Where(t => t.Einstufung?.Klassifizierung != null)
        .Where(t => t.Pakete.Contains(paket));

      var zuordnungen = Enum.GetValues<PhishingMailLevelOfDifficulty>()
        .Select(difficulty =>
        {
          var aktuelleAnzahl = (byte)templatesInPaket.Count(t => t.Einstufung.Klassifizierung == difficulty);

          var benoetigteAnzahl = PaketKonfiguration.Konfiguration[paket][difficulty];

          return (difficulty, aktuelleAnzahl, benoetigteAnzahl);
        });

      var dto = new PaketStatusDto { PaketName = paketName, Zuordnungen = zuordnungen };

      ctrl_PaketeLayout.Controls.Add(new PaketStatus(dto));
    }
  }
}
