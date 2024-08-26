namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class PaketStatus : UserControl
{
  public PaketStatus(PaketStatusDto dto)
  {
    InitializeComponent();

    ctrl_PaketName.Text = dto.PaketName;

    SetupTable();
    AddTableHeaderRow();

    foreach (var zuordnung in dto.Zuordnungen)
      AddRow(zuordnung.Difficulty, zuordnung.AktuelleAnzahl, zuordnung.BenoetigteAnzahl);
  }

  private void AddRow(PhishingMailLevelOfDifficulty difficulty, byte aktuelleAnzahl, byte benoetigteAnzahl)
  {
    ctrl_Table.RowCount += 1;
    ctrl_Table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    ctrl_Table.Controls.Add(new Label { Text = Enum.GetName(difficulty) }, 0,
      ctrl_Table.RowCount - 1);

    var hintergrundfarbe = aktuelleAnzahl == benoetigteAnzahl ? Color.LawnGreen : Color.IndianRed;

    var aktuelleAnzahlLabel = new Label { Text = aktuelleAnzahl.ToString(), BackColor = hintergrundfarbe };
    ctrl_Table.Controls.Add(aktuelleAnzahlLabel, 1, ctrl_Table.RowCount - 1);

    var benoetigteAnzahlLabel = new Label { Text = benoetigteAnzahl.ToString(), BackColor = hintergrundfarbe };
    ctrl_Table.Controls.Add(benoetigteAnzahlLabel, 2, ctrl_Table.RowCount - 1);
  }

  private void AddTableHeaderRow()
  {
    ctrl_Table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    var headerFont = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);
    ctrl_Table.Controls.Add(
      new Label { Text = "Schwierigkeit", Font = headerFont }, 0,
      ctrl_Table.RowCount - 1);
    ctrl_Table.Controls.Add(
      new Label { Text = "Ist", Font = headerFont }, 1,
      ctrl_Table.RowCount - 1);
    ctrl_Table.Controls.Add(
      new Label { Text = "Soll", Font = headerFont }, 2,
      ctrl_Table.RowCount - 1);
  }

  private void SetupTable()
  {
    ctrl_Table.RowStyles.Clear();
    ctrl_Table.ColumnCount = 3;
    ctrl_Table.RowCount = 1;
  }
}

public record PaketStatusDto
{
  public string PaketName { get; set; }

  public IEnumerable<(PhishingMailLevelOfDifficulty Difficulty, byte AktuelleAnzahl, byte BenoetigteAnzahl)>
    Zuordnungen { get; set; }
}