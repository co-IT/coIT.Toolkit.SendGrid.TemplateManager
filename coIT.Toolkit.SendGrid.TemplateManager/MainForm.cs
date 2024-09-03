using System.Text;
using coIT.Libraries.ConfigurationManager;
using coIT.Libraries.ConfigurationManager.Cryptography;
using coIT.Libraries.ConfigurationManager.Serialization;
using coIT.Libraries.Sendgrid;
using coIT.Toolkit.SendGrid.TemplateManager.Configurations;
using CSharpFunctionalExtensions;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class MainForm : Form
{
  private const string? ConnectionStringEnvVariableKey = "COIT_TOOLKIT_DATABASE_CONNECTIONSTRING";
  private readonly AesCryptographyService _cryptographyService;

  private readonly EnvironmentManager _environmentManager;
  private readonly SortableBindingList<TabellenEintrag> _tabellenListe = [];
  private List<ManagedTemplate> _alleTemplates = Enumerable.Empty<ManagedTemplate>().ToList();
  private bool _bestandsdatenImportiert;
  private CancellationTokenSource _cts;
  private ManagedTemplateRepository _managedTemplateRepository;

  private ManagedTemplate _selektiertesTemplate;
  private int _selektierteZeile;
  private SendGridConfigurationDataTableRepository _sendGridConfigurationRepository;
  private SendGridService _sendGridService;
  private Func<ManagedTemplate, object> _sortierung = t => t.SendGridTemplate.Name;
  private Dictionary<string, string> _zuordnungen = new(); //Zuordnungen TemplateId <-> InterneId

  public MainForm(CancellationTokenSource cts)
  {
    InitializeComponent();

    _cts = cts;

    var key =
      "eyJJdGVtMSI6InlLdHdrUDJraEJRbTRTckpEaXFjQWpkM3pBc3NVdG8rSUNrTmFwYUgwbWs9IiwiSXRlbTIiOiJUblRxT1RUbXI3ajBCZlUwTEtnOS9BPT0ifQ==";
    _cryptographyService = AesCryptographyService.FromKey(key).Value;
    var serializer = new NewtonsoftJsonSerializer();
    _environmentManager = new EnvironmentManager(_cryptographyService, serializer);
  }

  private void MeldungAnzeigen(string meldung)
  {
    ctrlMeldungen.InvokeIfRequired(() => ctrlMeldungen.Items.Add($"{DateTime.Now:hh:mm:ss} Uhr: {meldung}"));
    ctrlMeldungen.InvokeIfRequired(() =>
    {
      if (ctrlMeldungen.Items.Count == 0)
        return;

      ctrlMeldungen.SelectedIndex = ctrlMeldungen.Items.Count - 1;
    });
  }

  private async Task<Result> LadeTemplatesOhneInhalte(CancellationToken ct)
  {
    MeldungAnzeigen("Lade Templates von SendGrid ohne Inhalte");

    return await Result
      .Success()
      .Bind(() => _sendGridService.GetTemplatesAsync(ct))
      .Tap(templates => _managedTemplateRepository.Update(templates, ct))
      .Map(_ => _managedTemplateRepository.GetAll(ct))
      .Tap(templates => _alleTemplates = templates)
      .Tap(() => MeldungAnzeigen($"{_alleTemplates.Count} Templates gefunden"));
  }

  private async Task<Result> LadeInhalteVonTemplates(CancellationToken ct)
  {
    MeldungAnzeigen("Lade Inhalte asynchron nach.");

    var gesamt = _alleTemplates.Count;
    var verarbeitet = 0;

    var prozente = Enumerable.Range(1, 9).ToDictionary(k => gesamt / 10 * k, v => 100 / 10 * v);
    prozente.Add(gesamt, 100);

    foreach (var template in _alleTemplates)
    {
      var templateId = template.SendGridTemplate.TemplateId;
      var versionId = template.SendGridTemplate.VersionId;

      await _sendGridService
        .GetTemplateContentsAsync(templateId, versionId, ct)
        .Tap(inhalt =>
        {
          template.PlainContent = inhalt.PlainContent;
        })
        .Tap(_ => _managedTemplateRepository.Update(template, ct))
        .TapError(FehlerAnzeigen);

      verarbeitet += 1;
      if (prozente.TryGetValue(verarbeitet, out var prozent))
        MeldungAnzeigen($"{prozent:N0}% der Inhalte geladen");

      if (ct.IsCancellationRequested)
        return Result.Failure("Aktion abgebrochen");

      await Task.Delay(250, ct);
    }

    MeldungAnzeigen("Inhalte erfolgreich nachgeladen.");
    return Result.Success();
  }

  private async void LadeTemplatesVonSendGrid(object sender, EventArgs e)
  {
    await LadeTemplatesOhneInhalte(_cts.Token)
      .Tap(AktualisiereTabelle)
      .Tap(() => LadeInhalteVonTemplates(_cts.Token))
      .Tap(AktualisiereTabelle);

    ctrl_PaketUebersicht.Enabled = true;
  }

  private async void LadeTemplatesAusLokalemSpeicher(object sender, EventArgs e)
  {
    MeldungAnzeigen("Lade Templates aus lokalem Speicher");

    _cts = new CancellationTokenSource();
    _alleTemplates = await _managedTemplateRepository.GetAll(_cts.Token);

    AktualisiereTabelle();

    MeldungAnzeigen("Templates geladen");

    ctrl_PaketUebersicht.Enabled = true;
  }

  private void AktualisiereTabelle()
  {
    ctrlTemplatesListe.InvokeIfRequired(() =>
    {
      var sortiertUndGefiltert = FilterAnwenden()
        //.OrderBy(_sortierung)
        .Select(t => t.ToTabellenEintrag())
        .ToList();

      _tabellenListe.Clear();

      foreach (var tabellenEintrag in sortiertUndGefiltert)
        _tabellenListe.Add(tabellenEintrag);

      _tabellenListe.SortAgain();

      ctrlTemplatesListe.InvokeIfRequired(() =>
      {
        if (
          ctrlTemplatesListe.Rows.Count > 0
          && ctrlTemplatesListe.Rows.Count >= _selektierteZeile
          && _selektierteZeile > 1
        )
          ctrlTemplatesListe.Rows[_selektierteZeile].Selected = true;
      });
    });
  }

  private List<ManagedTemplate> FilterAnwenden()
  {
    IEnumerable<ManagedTemplate> templates = _alleTemplates;
    MeldungAnzeigen($"Templates insgesamt: {templates.Count()}");

    templates = templates.Where(t => !t.Archiviert).ToList();
    MeldungAnzeigen($"ohne archivierte Templates: {templates.Count()}");

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterName.Text.Trim()))
    {
      templates = templates
        .Where(t =>
          t.SendGridTemplate.Name.Contains(
            ctrlTemplateFilterName.Text.Trim(),
            StringComparison.InvariantCultureIgnoreCase
          )
        )
        .ToList();

      MeldungAnzeigen($"nach Namen-Filterung: {templates.Count()}");
    }

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterSubject.Text.Trim()))
    {
      templates = templates
        .Where(t =>
          t.SendGridTemplate.Subject.Contains(
            ctrlTemplateFilterSubject.Text.Trim(),
            StringComparison.InvariantCultureIgnoreCase
          )
        )
        .ToList();
      MeldungAnzeigen($"nach Subject-Filterung: {templates.Count()}");
    }

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterInhalt.Text.Trim()))
    {
      templates = templates
        .Where(t =>
          t.PlainContent.Contains(ctrlTemplateFilterInhalt.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)
        )
        .ToList();
      MeldungAnzeigen($"nach Inhalt-Filterung: {templates.Count()}");
    }

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterTags.Text.Trim()))
    {
      templates = templates
        .Where(t =>
          t.Tags.Any(tag =>
            tag.Contains(ctrlTemplateFilterTags.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)
          )
        )
        .ToList();
      MeldungAnzeigen($"nach Tag-Filterung: {templates.Count()}");
    }

    if (ctrlFilterPaket.SelectedItem != null)
    {
      templates = templates.Where(t => t.Pakete.Any(paket => paket == (Paket)ctrlFilterPaket.SelectedItem)).ToList();
      MeldungAnzeigen($"nach Paket-Filterung: {templates.Count()}");
    }

    templates = ctrlFilterStatus.Text switch
    {
      "eingestuft" => templates.Where(t => t.Einstufung is not null).ToList(),
      "einzustufen" => templates.Where(t => t.Einstufung is null).ToList(),
      _ => templates.ToList(),
    };

    MeldungAnzeigen($"nach Status-Filter: {templates.Count()}");

    MeldungAnzeigen($"insgesamt nach Filterung: {templates.Count()}");
    return templates.ToList();
  }

  private void FehlerAnzeigen(string fehler)
  {
    ctrlMeldungen.InvokeIfRequired(() => MessageBox.Show(fehler, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error));
  }

  private void TabelleFormatieren()
  {
    ctrlTemplatesListe.AllowUserToAddRows = false;
    ctrlTemplatesListe.AllowUserToDeleteRows = false;
    ctrlTemplatesListe.AllowUserToOrderColumns = true;
    ctrlTemplatesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    ctrlTemplatesListe.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
    ctrlTemplatesListe.MultiSelect = false;
    ctrlTemplatesListe.ReadOnly = true;
    ctrlTemplatesListe.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
    ctrlTemplatesListe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    ctrlTemplatesListe.Columns["EditorUri"]!.Visible = false;
    ctrlTemplatesListe.Columns["PreviewUri"]!.Visible = false;
    ctrlTemplatesListe.Columns["VersionId"]!.Visible = false;
    ctrlTemplatesListe.Columns["LastUpdated"]!.DefaultCellStyle.Format = "dd.MM.yyyy hh:mm";
    ctrlTemplatesListe.Columns["LastUpdated"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Skala"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Klicks"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Versandt"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Klickquote"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Klickquote"]!.DefaultCellStyle.Format = "p";
  }

  private async Task<Result<(string HtmlContent, string PlainContent)>> LadeTemplateInhalt()
  {
    _cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));

    return await _sendGridService
      .GetTemplateContentsAsync(
        _selektiertesTemplate.SendGridTemplate.TemplateId,
        _selektiertesTemplate.SendGridTemplate.VersionId,
        _cts.Token
      )
      .TapError(FehlerAnzeigen);
  }

  private void ctrlTemplatesListe_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
  {
    if (ctrlTemplatesListe.Rows.Count == 0)
    {
      ctrlAktionen.Enabled = false;
      return;
    }

    if (ctrlTemplatesListe.SelectedRows.Count == 0)
    {
      ctrlAktionen.Enabled = false;

      return;
    }

    var selectedRow = ctrlTemplatesListe.SelectedRows[0];
    var template = (TabellenEintrag)selectedRow.DataBoundItem;

    if (selectedRow.Index > 0)
      _selektierteZeile = selectedRow.Index;
    _selektiertesTemplate = _alleTemplates.Single(t => t.SendGridTemplate.TemplateId == template.TemplateId);

    ctrlAktionen.Enabled = true;
    ctrlTags.Enabled = true;
    ctrlPakete.Enabled = true;
  }

  private void ctrlOeffneEditor_Click(object sender, EventArgs e)
  {
    Utils.OpenInBrowser(_selektiertesTemplate.SendGridTemplate.EditorUri.AbsoluteUri);
  }

  private void ctrlOeffnePreview_Click(object sender, EventArgs e)
  {
    Utils.OpenInBrowser(_selektiertesTemplate.SendGridTemplate.PreviewUri.AbsoluteUri);
  }

  private async void ctrlOeffneWebansicht_Click(object sender, EventArgs e)
  {
    var templateOderFehler = await LadeTemplateInhalt();

    if (templateOderFehler.IsFailure)
      return;

    var file = $"{Path.GetTempFileName()}.html";
    var uri = new Uri(file).AbsoluteUri;

    await File.WriteAllTextAsync(file, templateOderFehler.Value.HtmlContent, Encoding.UTF8);

    Utils.OpenInBrowser(uri);
  }

  private async void ctrlTemplatesListe_DoubleClick(object sender, EventArgs e)
  {
    using var einzelAnsicht = new SingleTemplateForm(_selektiertesTemplate, _sendGridService);
    einzelAnsicht.ZeigeTemplate();
    var speichern = einzelAnsicht.ShowDialog(this);

    if (speichern != DialogResult.Yes)
      return;

    await _managedTemplateRepository.Update(_selektiertesTemplate, CancellationToken.None);
    AktualisiereTabelle();
  }

  private async void ctrlArchivierteTemplate_Click(object sender, EventArgs e)
  {
    _selektiertesTemplate.Archiviert = true;
    _selektiertesTemplate.PlainContent = string.Empty;
    _selektiertesTemplate.Tags = [];
    _selektiertesTemplate.Pakete = [];
    _selektiertesTemplate.Absender = null;
    _selektiertesTemplate.Einstufung = null;

    await _managedTemplateRepository.Update(_selektiertesTemplate, CancellationToken.None);
    _selektierteZeile = 0;

    AktualisiereTabelle();
  }

  private void ctrlTemplatesFiltern_Click(object sender, EventArgs e)
  {
    AktualisiereTabelle();
  }

  private async void ctrlMarkiereAlsPhishingMail_Click(object sender, EventArgs e)
  {
    await Taggen(((Control)sender).Text);
  }

  private async void ctrlMarkiereAdministrativeMail_Click(object sender, EventArgs e)
  {
    await Taggen(((Control)sender).Text);
  }

  private async void ctrlMarkiereAlsNurCyberPortal_Click(object sender, EventArgs e)
  {
    await Taggen(((Control)sender).Text);
  }

  private async void ctrlMarkiereAlsNurCyberLounge_Click(object sender, EventArgs e)
  {
    await Taggen(((Control)sender).Text);
  }

  private async void ctrlMarkiereAlsNovaplast_Click(object sender, EventArgs e)
  {
    await Taggen(((Control)sender).Text);
  }

  private async Task Taggen(string tag)
  {
    var bereitsGetaggt = _selektiertesTemplate.Tags.Contains(tag);

    if (bereitsGetaggt)
      _selektiertesTemplate.Tags = _selektiertesTemplate.Tags.Where(p => p != tag).OrderBy(t => t).ToList();
    else
      _selektiertesTemplate.Tags = _selektiertesTemplate.Tags.Concat(new[] { tag }).Distinct().OrderBy(t => t).ToList();

    await _managedTemplateRepository.Update(_selektiertesTemplate, CancellationToken.None);
    AktualisiereTabelle();
  }

  private async void ctrlProfiPaketZuordnen_Click(object sender, EventArgs e)
  {
    await PaketZuordnen(Paket.Profi);
  }

  private async Task PaketZuordnen(Paket paket)
  {
    var bereitsZugeordnet = _selektiertesTemplate.Pakete.Contains(paket);

    if (bereitsZugeordnet)
      _selektiertesTemplate.Pakete = _selektiertesTemplate.Pakete.Where(p => p != paket).OrderBy(p => p).ToList();
    else
      _selektiertesTemplate.Pakete = _selektiertesTemplate
        .Pakete.Concat(new[] { paket })
        .Distinct()
        .OrderBy(p => p)
        .ToList();

    await _managedTemplateRepository.Update(_selektiertesTemplate, CancellationToken.None);
    AktualisiereTabelle();
  }

  private async void ctrlStarterPaketZuordnen_Click(object sender, EventArgs e)
  {
    await PaketZuordnen(Paket.Starter);
  }

  private async void ctrlBasisPaketZuordnen_Click(object sender, EventArgs e)
  {
    await PaketZuordnen(Paket.Basis);
  }

  private async void ctrlDemoPaketZuordnen_Click(object sender, EventArgs e)
  {
    await PaketZuordnen(Paket.Demo);
  }

  private async void ctrlExpertenPaketZuordnen_Click(object sender, EventArgs e)
  {
    await PaketZuordnen(Paket.Experte);
  }

  private void ctrlTemplatesListe_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;

    Clipboard.SetText(_selektiertesTemplate.SendGridTemplate.TemplateId);
    MessageBox.Show(
      "Template ID in Zwischenablage kopiert",
      "Information",
      MessageBoxButtons.OK,
      MessageBoxIcon.Information
    );
  }

  private async void ctrlCsvExportCyberLounge_Click(object sender, EventArgs e)
  {
    if (!_bestandsdatenImportiert || _zuordnungen.Count == 0)
    {
      MessageBox.Show("Zuerst die Bestandsdaten importieren!");
      return;
    }

    if (exportFolderDialog.ShowDialog() != DialogResult.OK)
      return;

    var exportDirectory = exportFolderDialog.SelectedPath;

    var angezeigteTemplates = FilterAnwenden();
    if (angezeigteTemplates.Any(t => t.Absender is null))
    {
      MessageBox.Show(
        "Es können nur Templates exportiert werden, bei denen der Absender gepflegt ist.",
        "Aktion nicht möglich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Asterisk
      );
      return;
    }

    if (angezeigteTemplates.Any(t => !t.Tags.Contains("Phishing-Mail")))
    {
      MessageBox.Show(
        "Es können nur Phishing-Templates exportiert werden. Fügen Sie ggf. den Phishing-Mail Tag hinzu.",
        "Aktion nicht möglich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Asterisk
      );
      return;
    }

    if (angezeigteTemplates.Any(t => t.Einstufung is null))
    {
      MessageBox.Show(
        "Es können nur Templates exportiert werden, die eingestuft sind.",
        "Aktion nicht möglich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Asterisk
      );
      return;
    }

    var header = "Id;TemplateId;Title;SenderEmail;SenderName;LevelOfDifficulty;BusinessSector;Department;Created;Link";
    var csvZeilen = angezeigteTemplates
      .Select(t =>
      {
        if (_zuordnungen.TryGetValue(t.SendGridTemplate.TemplateId, out var interneTemplateId))
          return t.ToCsvExport(interneTemplateId);

        return t.ToCsvExport(Guid.NewGuid().ToString());
      })
      .Where(zeile => !string.IsNullOrWhiteSpace(zeile))
      .Aggregate(header, (aktuell, neu) => $"{aktuell}\r\n{neu}");

    var csvDatei = Path.Combine(exportDirectory, "PhishingMailTemplates-Export.csv");
    await File.WriteAllTextAsync(csvDatei, csvZeilen, Encoding.UTF8, CancellationToken.None);
    MeldungAnzeigen("CSV exportiert: " + csvDatei);

    var sqlBuilder = new StringBuilder();
    sqlBuilder.AppendLine("BEGIN TRANSACTION;");

    var sqlZeilen = angezeigteTemplates
      .Select(t =>
      {
        if (_zuordnungen.TryGetValue(t.SendGridTemplate.TemplateId, out var interneTemplateId))
          return t.ToSqlExport(interneTemplateId);

        return t.ToSqlExport(Guid.NewGuid().ToString());
      })
      .Where(zeile => !string.IsNullOrWhiteSpace(zeile));

    sqlBuilder.AppendJoin(Environment.NewLine, sqlZeilen);
    sqlBuilder.AppendLine("COMMIT;");

    var sqlDatei = Path.Combine(exportDirectory, "PhishingMailTemplates-Export.sql");
    await File.WriteAllTextAsync(sqlDatei, sqlBuilder.ToString(), Encoding.UTF8, CancellationToken.None);
    MeldungAnzeigen("SQL exportiert: " + sqlDatei);
  }

  private async void ctrlImportiereBestandsdaten_Click(object sender, EventArgs e)
  {
    if (importFileDialog.ShowDialog() != DialogResult.OK)
      return;

    var datei = importFileDialog.FileName;

    if (!File.Exists(datei))
      MessageBox.Show(
        $"Datei nicht gefunden: {datei}",
        "Aktion nicht erfolgreich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );

    var zeilen = await File.ReadAllLinesAsync(datei, Encoding.UTF8, CancellationToken.None);

    foreach (var zeile in zeilen.Skip(1))
    {
      var werte = zeile.Split(";");
      var templateId = werte[1];

      var template = _alleTemplates.SingleOrDefault(t => t.SendGridTemplate.TemplateId == templateId);
      if (template == null)
        continue;

      var absenderAdresse = werte[3];
      var absenderName = werte[4];

      template.Absender = new Absender { Name = absenderName, Adresse = absenderAdresse };

      await _managedTemplateRepository.Update(template, CancellationToken.None);
      MeldungAnzeigen($"{template.Absender}in Template {template.SendGridTemplate.Name} aktualisiert.");
    }

    _zuordnungen = await ZuordnungenEinlesen(datei);

    ValidateImport();

    AktualisiereTabelle();

    _bestandsdatenImportiert = true;
  }

  private void ValidateImport()
  {
    var phishingTemplates = _alleTemplates.Where(x => x.Tags.Contains("Phishing-Mail")).ToList();

    var notInManager = _zuordnungen
      .Select(x => x.Key.ToLower())
      .Except(phishingTemplates.Select(x => x.SendGridTemplate.TemplateId.ToLower()))
      .ToList();

    var notInImport = phishingTemplates
      .Select(x => x.SendGridTemplate.TemplateId.ToLower())
      .Except(_zuordnungen.Select(x => x.Key.ToLower()))
      .ToList();

    var sb = new StringBuilder();
    sb.AppendLine(
      "Achtung: Die Anzahl der Phishing-Templates im Import und Manager hat eine größere Abweichung. Bitte die folgenden Daten prüfen:"
    );
    sb.AppendLine();
    sb.AppendLine($"Anzahl Phishing-Templates im Import aber nicht im Manager enthalten: {notInManager.Count}");
    notInManager.ForEach(id => sb.AppendLine(id));
    sb.AppendLine();
    sb.AppendLine($"Anzahl Phishing-Templates im Manager aber nicht im Import enthalten: {notInImport.Count}");
    notInImport.ForEach(id => sb.AppendLine(id));

    if (notInManager.Count > 5 || notInImport.Count > 5)
      MessageBox.Show(sb.ToString(), "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
  }

  private static async Task<Dictionary<string, string>> ZuordnungenEinlesen(string importPath)
  {
    if (!File.Exists(importPath))
      MessageBox.Show(
        "Datei nicht gefunden: " + importPath,
        "Aktion nicht erfolgreich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );

    var zeilen = await File.ReadAllLinesAsync(importPath, Encoding.UTF8, CancellationToken.None);

    var zuordnungen = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    foreach (var zeile in zeilen.Skip(1))
    {
      var werte = zeile.Split(";");
      var interneid = werte[0].Trim().ToUpper();
      var templateId = werte[1].Trim().ToUpper();
      zuordnungen.TryAdd(templateId, interneid);
    }

    return zuordnungen;
  }

  private async void ctrlKlicksEinlesen_Click(object sender, EventArgs e)
  {
    if (!_bestandsdatenImportiert || _zuordnungen.Count == 0)
    {
      MessageBox.Show("Zuerst die Bestandsdaten importieren!");
      return;
    }

    if (klicksFileDialog.ShowDialog() != DialogResult.OK)
      return;

    var datei = klicksFileDialog.FileName;

    if (!File.Exists(datei))
      MessageBox.Show(
        "Datei nicht gefunden: " + datei,
        "Aktion nicht erfolgreich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );

    var zeilen = await File.ReadAllLinesAsync(datei, Encoding.UTF8, CancellationToken.None);

    var klicks = new Dictionary<string, int>();
    var versandt = new Dictionary<string, int>();

    foreach (var zeile in zeilen.Skip(1))
    {
      var werte = zeile.Split(";");

      int.TryParse(werte[1].Trim(), out var klick);

      var interndeId = werte[4].Trim();

      klicks.TryAdd(interndeId, 0);
      klicks[interndeId] += klick;

      versandt.TryAdd(interndeId, 0);
      versandt[interndeId] += 1;
    }

    foreach (var template in _alleTemplates)
    {
      var templateId = template.SendGridTemplate.TemplateId.ToUpper();

      if (!_zuordnungen.ContainsKey(templateId))
        continue;

      var id = _zuordnungen[templateId];

      if (versandt.TryGetValue(id, out var value))
        template.Versandt = value;

      if (klicks.TryGetValue(id, out var klick))
        template.Klicks = klick;
    }

    AktualisiereTabelle();
  }

  private void ctrlTemplateFilterTags_KeyUp(object sender, KeyEventArgs e)
  {
    Filtern(e);
  }

  private void ctrlTemplateFilterName_KeyUp(object sender, KeyEventArgs e)
  {
    Filtern(e);
  }

  private void ctrlTemplateFilterSubject_KeyUp(object sender, KeyEventArgs e)
  {
    Filtern(e);
  }

  private void ctrlTemplateFilterInhalt_KeyUp(object sender, KeyEventArgs e)
  {
    Filtern(e);
  }

  private void Filtern(KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Enter)
      AktualisiereTabelle();
  }

  private void ctrlFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
  {
    AktualisiereTabelle();
  }

  private async void FormMain_Load(object sender, EventArgs e)
  {
    ctrlFilterPaket.DataSource = Enum.GetValues(typeof(Paket));
    ctrlFilterPaket.SelectedItem = null;

    ctrlTemplatesListe.InvokeIfRequired(() => ctrlTemplatesListe.DataSource = _tabellenListe);
    ctrlTemplatesListe.InvokeIfRequired(TabelleFormatieren);

    var databaseConfiguration = await _environmentManager.Get<DatabaseConfiguration>(ConnectionStringEnvVariableKey);

    if (databaseConfiguration.IsFailure)
    {
      MessageBox.Show(
        "Datenbank Konfiguration konnten nicht geladen werden. Bitte Ersteinrichtung vornehmen",
        "Hinweis",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
      );
      tbpManager.Enabled = false;
      tbcManager.SelectedTab = tbpEinstellungen;
      return;
    }

    var connectionString = databaseConfiguration.Value.ConnectionString;
    _managedTemplateRepository = new ManagedTemplateRepository(connectionString);
    tbxDatabaseConnectionString.Text = connectionString;

    _sendGridConfigurationRepository = new SendGridConfigurationDataTableRepository(
      connectionString,
      _cryptographyService
    );

    var sendGridConfiguration = await _sendGridConfigurationRepository.Get();

    if (sendGridConfiguration.IsFailure)
    {
      MessageBox.Show(
        "SendGrid Konfiguration konnten nicht geladen werden. Bitte Ersteinrichtung vornehmen",
        "Hinweis",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
      );
      tbpManager.Enabled = false;
      tbcManager.SelectedTab = tbpEinstellungen;
      return;
    }

    var sendGridApiKey = sendGridConfiguration.Value!.ApiKey;
    _sendGridService = new SendGridService(sendGridApiKey);
    tbxApiKey.Text = sendGridApiKey;
  }

  private void ctrl_PaketUebersicht_Click(object sender, EventArgs e)
  {
    using var paketUebersicht = new PaketUebersichtForm(_alleTemplates);
    paketUebersicht.ShowDialog(this);
  }

  private async void btnEinstellungenSpeichern_Click(object sender, EventArgs e)
  {
    btnEinstellungenSpeichern.Enabled = false;

    var databaseConfiguration = new DatabaseConfiguration(tbxDatabaseConnectionString.Text);

    var databaseConfigurationResult = await _environmentManager.Save(
      databaseConfiguration,
      ConnectionStringEnvVariableKey
    );

    if (databaseConfigurationResult.IsFailure)
    {
      MessageBox.Show(
        databaseConfigurationResult.Error,
        "Speichern fehlgeschlagen",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );
      return;
    }

    var sendGridConfiguration = new SendGridConfiguration(tbxApiKey.Text);

    var sendGridConfigurationResult = await _sendGridConfigurationRepository.Upsert(sendGridConfiguration);

    if (sendGridConfigurationResult.IsFailure)
    {
      MessageBox.Show(
        sendGridConfigurationResult.Error,
        "Speichern fehlgeschlagen",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );
      return;
    }

    MessageBox.Show("Konfiguration wurde erfolgreich gespeichert", "Gespeichert");
    _sendGridService = new SendGridService(sendGridConfiguration.ApiKey);
    _managedTemplateRepository = new ManagedTemplateRepository(databaseConfiguration.ConnectionString);
    tbpManager.Enabled = true;

    btnEinstellungenSpeichern.Enabled = true;
  }
}
