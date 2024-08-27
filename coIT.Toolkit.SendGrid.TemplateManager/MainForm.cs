using System.Diagnostics;
using System.Text;
using coIT.Libraries.ConfigurationManager;
using coIT.Libraries.ConfigurationManager.Cryptography;
using coIT.Libraries.ConfigurationManager.Serialization;
using coIT.Libraries.Sendgrid;
using CSharpFunctionalExtensions;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class MainForm : Form
{
  private List<ManagedTemplate> _alleTemplates = Enumerable.Empty<ManagedTemplate>().ToList();
  private CancellationTokenSource _cts;

  private ManagedTemplate _selektiertesTemplate;
  private int _selektierteZeile;
  private Func<ManagedTemplate, object> _sortierung = t => t.SendGridTemplate.Name;

  private readonly EnvironmentManager _environmentManager;
  private ManagedTemplateRepository _managedTemplateRepository;
  private SendGridService _sendGridService;

  public MainForm()
  {
    InitializeComponent();

    var schlüssel =
      "eyJJdGVtMSI6Ijk2QUttM1JmZEs3OG55NGgxTzUrb1NlVm5PQm1GNnhiTHR3TzRmY3BPckE9IiwiSXRlbTIiOiJZdVo1eC9JdXRWQzg2RXJFRTQzWHF3PT0ifQ==";
    var cryptographyService = AesCryptographyService.FromKey(schlüssel);
    var serializer = new NewtonsoftJsonSerializer();
    _environmentManager = new EnvironmentManager(cryptographyService.Value, serializer);
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
      .Tap(templates => _managedTemplateRepository.AktualisiereLokaleKopie(templates, ct))
      .Map(_ => _managedTemplateRepository.LadeAusLokalenKopien(ct))
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
          template.HtmlContent = inhalt.HtmlContent;
          template.PlainContent = inhalt.PlainContent;
        })
        .Tap(_ => _managedTemplateRepository.AktualisiereLokaleKopie(template, ct))
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
    _cts = new CancellationTokenSource();

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
    _alleTemplates = await _managedTemplateRepository.LadeAusLokalenKopien(_cts.Token);

    AktualisiereTabelle();

    MeldungAnzeigen("Templates geladen");

    ctrl_PaketUebersicht.Enabled = true;
  }

  private void AktualisiereTabelle()
  {
    var sortiertUndGefiltert = FilterAnwenden()
      //.OrderBy(_sortierung)
      .Select(t => t.ToTabellenEintrag())
      .ToList();

    ctrlTemplatesListe.InvokeIfRequired(
      () => ctrlTemplatesListe.DataSource = sortiertUndGefiltert.AsSortableBindingList()
    );
    ctrlTemplatesListe.InvokeIfRequired(TabelleFormatieren);
    ctrlTemplatesListe.InvokeIfRequired(() =>
    {
      if (
        ctrlTemplatesListe.Rows.Count > 0
        && ctrlTemplatesListe.Rows.Count >= _selektierteZeile
        && _selektierteZeile > 1
      )
        ctrlTemplatesListe.Rows[_selektierteZeile].Selected = true;
    });
  }

  private List<ManagedTemplate> FilterAnwenden()
  {
    IEnumerable<ManagedTemplate> templates = _alleTemplates;
    MeldungAnzeigen($"Templates insgesamt: {templates.Count()}");

    templates = templates.Where(t => !t.Archiviert);
    MeldungAnzeigen($"ohne archivierte Templates: {templates.Count()}");

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterName.Text.Trim()))
    {
      templates = templates.Where(t =>
        (t.SendGridTemplate?.Name ?? string.Empty).Contains(
          ctrlTemplateFilterName.Text.Trim(),
          StringComparison.InvariantCultureIgnoreCase
        )
      );

      MeldungAnzeigen($"nach Namen-Filterung: {templates.Count()}");
    }

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterSubject.Text.Trim()))
    {
      templates = templates.Where(t =>
        (t.SendGridTemplate?.Subject ?? string.Empty).Contains(
          ctrlTemplateFilterSubject.Text.Trim(),
          StringComparison.InvariantCultureIgnoreCase
        )
      );
      MeldungAnzeigen($"nach Subject-Filterung: {templates.Count()}");
    }

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterInhalt.Text.Trim()))
    {
      templates = templates.Where(t =>
        (t.PlainContent ?? string.Empty).Contains(
          ctrlTemplateFilterInhalt.Text.Trim(),
          StringComparison.InvariantCultureIgnoreCase
        )
      );
      MeldungAnzeigen($"nach Inhalt-Filterung: {templates.Count()}");
    }

    if (!string.IsNullOrWhiteSpace(ctrlTemplateFilterTags.Text.Trim()))
    {
      templates = templates.Where(t =>
        t.Tags.Any(tag => tag.Contains(ctrlTemplateFilterTags.Text.Trim(), StringComparison.InvariantCultureIgnoreCase))
      );
      MeldungAnzeigen($"nach Tag-Filterung: {templates.Count()}");
    }

    if (ctrlFilterPaket.SelectedItem != null)
    {
      templates = templates.Where(t => t.Pakete.Any(paket => paket == (Paket)ctrlFilterPaket.SelectedItem));
      MeldungAnzeigen($"nach Paket-Filterung: {templates.Count()}");
    }

    templates = ctrlFilterStatus.Text switch
    {
      "eingestuft" => templates.Where(t => t.Einstufung is not null),
      "einzustufen" => templates.Where(t => t.Einstufung is null),
      _ => templates,
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
    ctrlTemplatesListe.Columns["EditorUri"].Visible = false;
    ctrlTemplatesListe.Columns["PreviewUri"].Visible = false;
    ctrlTemplatesListe.Columns["VersionId"].Visible = false;
    ctrlTemplatesListe.Columns["LastUpdated"].DefaultCellStyle.Format = "dd.MM.yyyy hh:mm";
    ctrlTemplatesListe.Columns["LastUpdated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Skala"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Klicks"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Versandt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Klickquote"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    ctrlTemplatesListe.Columns["Klickquote"].DefaultCellStyle.Format = "p";
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
    StarteBrowserMit(_selektiertesTemplate.SendGridTemplate.EditorUri.AbsoluteUri);
  }

  private void ctrlOeffnePreview_Click(object sender, EventArgs e)
  {
    StarteBrowserMit(_selektiertesTemplate.SendGridTemplate.PreviewUri.AbsoluteUri);
  }

  private static void StarteBrowserMit(string url)
  {
    var args = $"/c start msedge {url}";

    var psi = new ProcessStartInfo("cmd.exe")
    {
      Arguments = args,
      UseShellExecute = false,
      WindowStyle = ProcessWindowStyle.Normal,
      CreateNoWindow = false,
    };

    Process.Start(psi);
  }

  private async void ctrlOeffneWebansicht_Click(object sender, EventArgs e)
  {
    var templateOderFehler = await LadeTemplateInhalt();

    if (templateOderFehler.IsFailure)
      return;

    var file = $"{Path.GetTempFileName()}.html";
    var uri = new Uri(file).AbsoluteUri;

    await File.WriteAllTextAsync(file, templateOderFehler.Value.HtmlContent, Encoding.UTF8);
    var args = $"/c start msedge {uri}";

    var psi = new ProcessStartInfo("cmd.exe") { Arguments = args, UseShellExecute = true };

    Process.Start(psi);
  }

  private async void ctrlTemplatesListe_DoubleClick(object sender, EventArgs e)
  {
    using var einzelAnsicht = new SingleTemplateForm(_selektiertesTemplate, _sendGridService);
    einzelAnsicht.ZeigeTemplate();
    var speichern = einzelAnsicht.ShowDialog(this);

    if (speichern == DialogResult.Yes)
    {
      await _managedTemplateRepository.AktualisiereLokaleKopie(_selektiertesTemplate, CancellationToken.None);
      AktualisiereTabelle();
    }
  }

  private async void ctrlArchivierteTemplate_Click(object sender, EventArgs e)
  {
    _selektiertesTemplate.Archiviert = true;
    _selektiertesTemplate.HtmlContent = string.Empty;
    _selektiertesTemplate.PlainContent = string.Empty;
    _selektiertesTemplate.Tags = [];
    _selektiertesTemplate.Pakete = [];
    _selektiertesTemplate.Absender = null;
    _selektiertesTemplate.Einstufung = null;

    await _managedTemplateRepository.AktualisiereLokaleKopie(_selektiertesTemplate, CancellationToken.None);
    _selektierteZeile = 0;

    AktualisiereTabelle();
  }

  private void ctrlTemplatesFiltern_Click(object sender, EventArgs e)
  {
    AktualisiereTabelle();
  }

  private void ctrlSortiereNachName_Click(object sender, EventArgs e)
  {
    _sortierung = template => template.SendGridTemplate.Name;
    AktualisiereTabelle();
  }

  private void ctrlSortiereNachSubject_Click(object sender, EventArgs e)
  {
    _sortierung = template => template.SendGridTemplate.Subject;
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

    await _managedTemplateRepository.AktualisiereLokaleKopie(_selektiertesTemplate, CancellationToken.None);
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

    await _managedTemplateRepository.AktualisiereLokaleKopie(_selektiertesTemplate, CancellationToken.None);
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

  private void ctrlJsonBearbeiten_Click(object sender, EventArgs e)
  {
    _managedTemplateRepository.EditiereLokaleKopie(_selektiertesTemplate);
    MessageBox.Show(
      "Nach dem Editieren müssen die lokalen Daten manuell neugeladen werden.",
      "Hinweis",
      MessageBoxButtons.OK
    );
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

    var zuordnungen = await ZuordnungenEinlesen();

    var header = "Id;TemplateId;Title;SenderEmail;SenderName;LevelOfDifficulty;BusinessSector;Department;Created;Link";
    var csvZeilen = angezeigteTemplates
      .Select(t =>
      {
        if (zuordnungen.TryGetValue(t.SendGridTemplate.TemplateId, out var interneTemplateId))
          return t.ToCsvExport(interneTemplateId);

        return t.ToCsvExport(Guid.NewGuid().ToString());
      })
      .Where(zeile => !string.IsNullOrWhiteSpace(zeile))
      .Aggregate(header, (aktuell, neu) => $"{aktuell}\r\n{neu}");

    var csvDatei = "../phishing-mails-export.csv";
    await File.WriteAllTextAsync(csvDatei, csvZeilen, Encoding.UTF8, CancellationToken.None);
    MeldungAnzeigen("CSV exportiert: " + csvDatei);

    var sqlBuilder = new StringBuilder();
    sqlBuilder.AppendLine("BEGIN TRANSACTION;");

    var sqlZeilen = angezeigteTemplates
      .Select(t =>
      {
        if (zuordnungen.TryGetValue(t.SendGridTemplate.TemplateId, out var interneTemplateId))
          return t.ToSqlExport(interneTemplateId);

        return t.ToSqlExport(Guid.NewGuid().ToString());
      })
      .Where(zeile => !string.IsNullOrWhiteSpace(zeile));

    sqlBuilder.AppendJoin(Environment.NewLine, sqlZeilen);
    sqlBuilder.AppendLine("COMMIT;");

    var sqlDatei = "../phishing-mails-export.sql";
    await File.WriteAllTextAsync(sqlDatei, sqlBuilder.ToString(), Encoding.UTF8, CancellationToken.None);
    MeldungAnzeigen("SQL exportiert: " + sqlDatei);
  }

  private async void ctrlImportiereBestandsdaten_Click(object sender, EventArgs e)
  {
    var datei = "../phishing-mails-import.csv";

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

      await _managedTemplateRepository.AktualisiereLokaleKopie(template, CancellationToken.None);
      MeldungAnzeigen($"{template.Absender}in Template {template.SendGridTemplate.Name} aktualisiert.");
    }

    AktualisiereTabelle();
  }

  private static async Task<Dictionary<string, string>> ZuordnungenEinlesen()
  {
    var datei = "../phishing-mails-import.csv";

    if (!File.Exists(datei))
      MessageBox.Show(
        "Datei nicht gefunden: " + datei,
        "Aktion nicht erfolgreich",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );

    var zeilen = await File.ReadAllLinesAsync(datei, Encoding.UTF8, CancellationToken.None);

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
    var datei = "../klicks.csv";

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

      var klick = 0;
      int.TryParse(werte[1].Trim(), out klick);

      var interndeId = werte[4].Trim();

      klicks.TryAdd(interndeId, 0);
      klicks[interndeId] += klick;

      versandt.TryAdd(interndeId, 0);
      versandt[interndeId] += 1;
    }

    var zuordnungen = await ZuordnungenEinlesen();
    foreach (var template in _alleTemplates)
    {
      var templateId = template.SendGridTemplate.TemplateId.ToUpper();

      if (!zuordnungen.ContainsKey(templateId))
        continue;

      var id = zuordnungen[templateId];

      if (versandt.ContainsKey(id))
        template.Versandt = versandt[id];

      if (klicks.ContainsKey(id))
        template.Klicks = klicks[id];
    }

    AktualisiereTabelle();
  }

  private void ctrlTemplateFilterPaket_KeyUp(object sender, KeyEventArgs e)
  {
    Filtern(e);
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

    var einstellungenGeladen = await _environmentManager
      .Get<Einstellungen>()
      .Tap(einstellungen => _sendGridService = new SendGridService(einstellungen.ApiKey))
      .Tap(einstellungen =>
        _managedTemplateRepository = ManagedTemplateRepository.Erstellen(einstellungen.DatenbankPfad)
      );

    if (einstellungenGeladen.IsFailure)
    {
      MessageBox.Show(
        "Einstellungen konnten nicht geladen werden. Bitte Ersteinrichtung vornehmen",
        "Fehler",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
      );
      tbpManager.Enabled = false;
      tbcManager.SelectedTab = tbpEinstellungen;
    }
  }

  private void ctrl_PaketUebersicht_Click(object sender, EventArgs e)
  {
    using var paketUebersicht = new PaketUebersichtForm(_alleTemplates);
    paketUebersicht.ShowDialog(this);
  }

  private void btnDatenbankAuswählen_Click(object sender, EventArgs e)
  {
    DialogResult result = dlgDatenbankOrdner.ShowDialog();

    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dlgDatenbankOrdner.SelectedPath))
    {
      tbxDatenbankPfad.Text = dlgDatenbankOrdner.SelectedPath;
    }
  }

  private async void btnEinstellungenSpeichern_Click(object sender, EventArgs e)
  {
    btnEinstellungenSpeichern.Enabled = false;

    var einstellungen = new Einstellungen { ApiKey = tbxApiKey.Text, DatenbankPfad = tbxDatenbankPfad.Text };

    var ergebnis = await _environmentManager.Save(einstellungen);

    if (ergebnis.IsFailure)
    {
      MessageBox.Show(ergebnis.Error, "Speichern fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    else
    {
      MessageBox.Show("Konfiguration wurde erfolgreich gespeichert", "Gespeichert");
      _sendGridService = new SendGridService(einstellungen.ApiKey);
      _managedTemplateRepository = ManagedTemplateRepository.Erstellen(einstellungen.DatenbankPfad);
      tbpManager.Enabled = true;
    }

    btnEinstellungenSpeichern.Enabled = true;
  }
}
