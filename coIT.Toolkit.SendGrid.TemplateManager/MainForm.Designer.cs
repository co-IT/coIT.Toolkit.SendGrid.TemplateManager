namespace coIT.Toolkit.SendGrid.TemplateManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      groupBox3 = new GroupBox();
      ctrlKlicksEinlesen = new Button();
      ctrlImportiereBestandsdaten = new Button();
      ctrlCsvExportCyberLounge = new Button();
      ctrlPakete = new GroupBox();
      ctrl_PaketUebersicht = new Button();
      ctrlBasisPaketZuordnen = new Button();
      ctrlDemoPaketZuordnen = new Button();
      ctrlExpertenPaketZuordnen = new Button();
      ctrlStarterPaketZuordnen = new Button();
      ctrlProfiPaketZuordnen = new Button();
      ctrlTags = new GroupBox();
      ctrlMarkiereAlsNovaplast = new Button();
      ctrlMarkiereAlsNurCyberLounge = new Button();
      ctrlMarkiereAlsPhishingMail = new Button();
      ctrlMarkiereAlsNurCyberPortal = new Button();
      ctrlMarkiereAdministrativeMail = new Button();
      groupBox1 = new GroupBox();
      ctrlFilterPaket = new ComboBox();
      ctrlFilterStatus = new ComboBox();
      label6 = new Label();
      label4 = new Label();
      label5 = new Label();
      ctrlTemplateFilterTags = new TextBox();
      label3 = new Label();
      ctrlTemplateFilterInhalt = new TextBox();
      label2 = new Label();
      label1 = new Label();
      ctrlTemplatesFiltern = new Button();
      ctrlTemplateFilterSubject = new TextBox();
      ctrlTemplateFilterName = new TextBox();
      ctrlDatenLadenGruppe = new GroupBox();
      ctrlTemplatesLadenAusDatei = new Button();
      ctrlTemplatesLadenVonSendGrid = new Button();
      ctrlAktionen = new GroupBox();
      ctrlJsonBearbeiten = new Button();
      ctrlTemplateArchivieren = new Button();
      ctrlOeffneEditor = new Button();
      ctrlOeffneWebansicht = new Button();
      ctrlOeffnePreview = new Button();
      tbcManager = new TabControl();
      tbpManager = new TabPage();
      ctrlTemplatesListe = new DataGridView();
      panel1 = new Panel();
      ctrlMeldungen = new ListBox();
      tbpEinstellungen = new TabPage();
      groupBox2 = new GroupBox();
      btnEinstellungenSpeichern = new Button();
      btnDatenbankAuswählen = new Button();
      tbxDatenbankPfad = new TextBox();
      label8 = new Label();
      tbxApiKey = new TextBox();
      label7 = new Label();
      dlgDatenbankOrdner = new FolderBrowserDialog();
      importFileDialog = new OpenFileDialog();
      klicksFileDialog = new OpenFileDialog();
      exportFolderDialog = new FolderBrowserDialog();
      groupBox3.SuspendLayout();
      ctrlPakete.SuspendLayout();
      ctrlTags.SuspendLayout();
      groupBox1.SuspendLayout();
      ctrlDatenLadenGruppe.SuspendLayout();
      ctrlAktionen.SuspendLayout();
      tbcManager.SuspendLayout();
      tbpManager.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ctrlTemplatesListe).BeginInit();
      panel1.SuspendLayout();
      tbpEinstellungen.SuspendLayout();
      groupBox2.SuspendLayout();
      SuspendLayout();
      // 
      // groupBox3
      // 
      groupBox3.Controls.Add(ctrlKlicksEinlesen);
      groupBox3.Controls.Add(ctrlImportiereBestandsdaten);
      groupBox3.Controls.Add(ctrlCsvExportCyberLounge);
      groupBox3.Location = new Point(177, 18);
      groupBox3.Margin = new Padding(2);
      groupBox3.Name = "groupBox3";
      groupBox3.Padding = new Padding(2);
      groupBox3.Size = new Size(156, 124);
      groupBox3.TabIndex = 10;
      groupBox3.TabStop = false;
      groupBox3.Text = "Drittsystem";
      // 
      // ctrlKlicksEinlesen
      // 
      ctrlKlicksEinlesen.Location = new Point(14, 86);
      ctrlKlicksEinlesen.Margin = new Padding(2);
      ctrlKlicksEinlesen.Name = "ctrlKlicksEinlesen";
      ctrlKlicksEinlesen.Size = new Size(125, 25);
      ctrlKlicksEinlesen.TabIndex = 5;
      ctrlKlicksEinlesen.Text = "Klicks einlesen";
      ctrlKlicksEinlesen.UseVisualStyleBackColor = true;
      ctrlKlicksEinlesen.Click += ctrlKlicksEinlesen_Click;
      // 
      // ctrlImportiereBestandsdaten
      // 
      ctrlImportiereBestandsdaten.Location = new Point(14, 21);
      ctrlImportiereBestandsdaten.Margin = new Padding(2);
      ctrlImportiereBestandsdaten.Name = "ctrlImportiereBestandsdaten";
      ctrlImportiereBestandsdaten.Size = new Size(125, 25);
      ctrlImportiereBestandsdaten.TabIndex = 4;
      ctrlImportiereBestandsdaten.Text = "Import von CL";
      ctrlImportiereBestandsdaten.UseVisualStyleBackColor = true;
      ctrlImportiereBestandsdaten.Click += ctrlImportiereBestandsdaten_Click;
      // 
      // ctrlCsvExportCyberLounge
      // 
      ctrlCsvExportCyberLounge.Location = new Point(14, 52);
      ctrlCsvExportCyberLounge.Margin = new Padding(2);
      ctrlCsvExportCyberLounge.Name = "ctrlCsvExportCyberLounge";
      ctrlCsvExportCyberLounge.Size = new Size(125, 26);
      ctrlCsvExportCyberLounge.TabIndex = 0;
      ctrlCsvExportCyberLounge.Tag = "";
      ctrlCsvExportCyberLounge.Text = "Export zu CL";
      ctrlCsvExportCyberLounge.UseVisualStyleBackColor = true;
      ctrlCsvExportCyberLounge.Click += ctrlCsvExportCyberLounge_Click;
      // 
      // ctrlPakete
      // 
      ctrlPakete.Controls.Add(ctrl_PaketUebersicht);
      ctrlPakete.Controls.Add(ctrlBasisPaketZuordnen);
      ctrlPakete.Controls.Add(ctrlDemoPaketZuordnen);
      ctrlPakete.Controls.Add(ctrlExpertenPaketZuordnen);
      ctrlPakete.Controls.Add(ctrlStarterPaketZuordnen);
      ctrlPakete.Controls.Add(ctrlProfiPaketZuordnen);
      ctrlPakete.Enabled = false;
      ctrlPakete.Location = new Point(676, 18);
      ctrlPakete.Margin = new Padding(2);
      ctrlPakete.Name = "ctrlPakete";
      ctrlPakete.Padding = new Padding(2);
      ctrlPakete.Size = new Size(145, 133);
      ctrlPakete.TabIndex = 10;
      ctrlPakete.TabStop = false;
      ctrlPakete.Text = "Phishing";
      // 
      // ctrl_PaketUebersicht
      // 
      ctrl_PaketUebersicht.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ctrl_PaketUebersicht.BackColor = SystemColors.ActiveCaption;
      ctrl_PaketUebersicht.Enabled = false;
      ctrl_PaketUebersicht.Location = new Point(4, 103);
      ctrl_PaketUebersicht.Margin = new Padding(2);
      ctrl_PaketUebersicht.Name = "ctrl_PaketUebersicht";
      ctrl_PaketUebersicht.Size = new Size(137, 25);
      ctrl_PaketUebersicht.TabIndex = 9;
      ctrl_PaketUebersicht.Text = "Paket Übersicht";
      ctrl_PaketUebersicht.UseVisualStyleBackColor = false;
      ctrl_PaketUebersicht.Click += ctrl_PaketUebersicht_Click;
      // 
      // ctrlBasisPaketZuordnen
      // 
      ctrlBasisPaketZuordnen.Location = new Point(77, 21);
      ctrlBasisPaketZuordnen.Margin = new Padding(2);
      ctrlBasisPaketZuordnen.Name = "ctrlBasisPaketZuordnen";
      ctrlBasisPaketZuordnen.Size = new Size(60, 23);
      ctrlBasisPaketZuordnen.TabIndex = 8;
      ctrlBasisPaketZuordnen.Text = "Basis";
      ctrlBasisPaketZuordnen.UseVisualStyleBackColor = true;
      ctrlBasisPaketZuordnen.Click += ctrlBasisPaketZuordnen_Click;
      // 
      // ctrlDemoPaketZuordnen
      // 
      ctrlDemoPaketZuordnen.Location = new Point(4, 21);
      ctrlDemoPaketZuordnen.Margin = new Padding(2);
      ctrlDemoPaketZuordnen.Name = "ctrlDemoPaketZuordnen";
      ctrlDemoPaketZuordnen.Size = new Size(60, 23);
      ctrlDemoPaketZuordnen.TabIndex = 7;
      ctrlDemoPaketZuordnen.Text = "Demo";
      ctrlDemoPaketZuordnen.UseVisualStyleBackColor = true;
      ctrlDemoPaketZuordnen.Click += ctrlDemoPaketZuordnen_Click;
      // 
      // ctrlExpertenPaketZuordnen
      // 
      ctrlExpertenPaketZuordnen.Location = new Point(4, 75);
      ctrlExpertenPaketZuordnen.Margin = new Padding(2);
      ctrlExpertenPaketZuordnen.Name = "ctrlExpertenPaketZuordnen";
      ctrlExpertenPaketZuordnen.Size = new Size(60, 23);
      ctrlExpertenPaketZuordnen.TabIndex = 4;
      ctrlExpertenPaketZuordnen.Text = "Experte";
      ctrlExpertenPaketZuordnen.UseVisualStyleBackColor = true;
      ctrlExpertenPaketZuordnen.Click += ctrlExpertenPaketZuordnen_Click;
      // 
      // ctrlStarterPaketZuordnen
      // 
      ctrlStarterPaketZuordnen.Location = new Point(4, 48);
      ctrlStarterPaketZuordnen.Margin = new Padding(2);
      ctrlStarterPaketZuordnen.Name = "ctrlStarterPaketZuordnen";
      ctrlStarterPaketZuordnen.Size = new Size(60, 23);
      ctrlStarterPaketZuordnen.TabIndex = 6;
      ctrlStarterPaketZuordnen.Text = "Starter";
      ctrlStarterPaketZuordnen.UseVisualStyleBackColor = true;
      ctrlStarterPaketZuordnen.Click += ctrlStarterPaketZuordnen_Click;
      // 
      // ctrlProfiPaketZuordnen
      // 
      ctrlProfiPaketZuordnen.Location = new Point(77, 48);
      ctrlProfiPaketZuordnen.Margin = new Padding(2);
      ctrlProfiPaketZuordnen.Name = "ctrlProfiPaketZuordnen";
      ctrlProfiPaketZuordnen.Size = new Size(60, 23);
      ctrlProfiPaketZuordnen.TabIndex = 5;
      ctrlProfiPaketZuordnen.Text = "Profi";
      ctrlProfiPaketZuordnen.UseVisualStyleBackColor = true;
      ctrlProfiPaketZuordnen.Click += ctrlProfiPaketZuordnen_Click;
      // 
      // ctrlTags
      // 
      ctrlTags.Controls.Add(ctrlMarkiereAlsNovaplast);
      ctrlTags.Controls.Add(ctrlMarkiereAlsNurCyberLounge);
      ctrlTags.Controls.Add(ctrlMarkiereAlsPhishingMail);
      ctrlTags.Controls.Add(ctrlMarkiereAlsNurCyberPortal);
      ctrlTags.Controls.Add(ctrlMarkiereAdministrativeMail);
      ctrlTags.Enabled = false;
      ctrlTags.Location = new Point(513, 18);
      ctrlTags.Margin = new Padding(2);
      ctrlTags.Name = "ctrlTags";
      ctrlTags.Padding = new Padding(2);
      ctrlTags.Size = new Size(145, 158);
      ctrlTags.TabIndex = 9;
      ctrlTags.TabStop = false;
      ctrlTags.Text = "Tags";
      // 
      // ctrlMarkiereAlsNovaplast
      // 
      ctrlMarkiereAlsNovaplast.Location = new Point(8, 130);
      ctrlMarkiereAlsNovaplast.Margin = new Padding(2);
      ctrlMarkiereAlsNovaplast.Name = "ctrlMarkiereAlsNovaplast";
      ctrlMarkiereAlsNovaplast.Size = new Size(125, 23);
      ctrlMarkiereAlsNovaplast.TabIndex = 8;
      ctrlMarkiereAlsNovaplast.Text = "Novaplast";
      ctrlMarkiereAlsNovaplast.UseVisualStyleBackColor = true;
      ctrlMarkiereAlsNovaplast.Click += ctrlMarkiereAlsNovaplast_Click;
      // 
      // ctrlMarkiereAlsNurCyberLounge
      // 
      ctrlMarkiereAlsNurCyberLounge.Location = new Point(8, 103);
      ctrlMarkiereAlsNurCyberLounge.Margin = new Padding(2);
      ctrlMarkiereAlsNurCyberLounge.Name = "ctrlMarkiereAlsNurCyberLounge";
      ctrlMarkiereAlsNurCyberLounge.Size = new Size(125, 23);
      ctrlMarkiereAlsNurCyberLounge.TabIndex = 7;
      ctrlMarkiereAlsNurCyberLounge.Text = "Nur Cyber Lounge";
      ctrlMarkiereAlsNurCyberLounge.UseVisualStyleBackColor = true;
      ctrlMarkiereAlsNurCyberLounge.Click += ctrlMarkiereAlsNurCyberLounge_Click;
      // 
      // ctrlMarkiereAlsPhishingMail
      // 
      ctrlMarkiereAlsPhishingMail.Location = new Point(8, 20);
      ctrlMarkiereAlsPhishingMail.Margin = new Padding(2);
      ctrlMarkiereAlsPhishingMail.Name = "ctrlMarkiereAlsPhishingMail";
      ctrlMarkiereAlsPhishingMail.Size = new Size(125, 23);
      ctrlMarkiereAlsPhishingMail.TabIndex = 4;
      ctrlMarkiereAlsPhishingMail.Text = "Phishing-Mail";
      ctrlMarkiereAlsPhishingMail.UseVisualStyleBackColor = true;
      ctrlMarkiereAlsPhishingMail.Click += ctrlMarkiereAlsPhishingMail_Click;
      // 
      // ctrlMarkiereAlsNurCyberPortal
      // 
      ctrlMarkiereAlsNurCyberPortal.Location = new Point(8, 75);
      ctrlMarkiereAlsNurCyberPortal.Margin = new Padding(2);
      ctrlMarkiereAlsNurCyberPortal.Name = "ctrlMarkiereAlsNurCyberPortal";
      ctrlMarkiereAlsNurCyberPortal.Size = new Size(125, 23);
      ctrlMarkiereAlsNurCyberPortal.TabIndex = 6;
      ctrlMarkiereAlsNurCyberPortal.Text = "Nur Cyber Portal";
      ctrlMarkiereAlsNurCyberPortal.UseVisualStyleBackColor = true;
      ctrlMarkiereAlsNurCyberPortal.Click += ctrlMarkiereAlsNurCyberPortal_Click;
      // 
      // ctrlMarkiereAdministrativeMail
      // 
      ctrlMarkiereAdministrativeMail.Location = new Point(8, 47);
      ctrlMarkiereAdministrativeMail.Margin = new Padding(2);
      ctrlMarkiereAdministrativeMail.Name = "ctrlMarkiereAdministrativeMail";
      ctrlMarkiereAdministrativeMail.Size = new Size(125, 23);
      ctrlMarkiereAdministrativeMail.TabIndex = 5;
      ctrlMarkiereAdministrativeMail.Text = "Administrative Mail";
      ctrlMarkiereAdministrativeMail.UseVisualStyleBackColor = true;
      ctrlMarkiereAdministrativeMail.Click += ctrlMarkiereAdministrativeMail_Click;
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(ctrlFilterPaket);
      groupBox1.Controls.Add(ctrlFilterStatus);
      groupBox1.Controls.Add(label6);
      groupBox1.Controls.Add(label4);
      groupBox1.Controls.Add(label5);
      groupBox1.Controls.Add(ctrlTemplateFilterTags);
      groupBox1.Controls.Add(label3);
      groupBox1.Controls.Add(ctrlTemplateFilterInhalt);
      groupBox1.Controls.Add(label2);
      groupBox1.Controls.Add(label1);
      groupBox1.Controls.Add(ctrlTemplatesFiltern);
      groupBox1.Controls.Add(ctrlTemplateFilterSubject);
      groupBox1.Controls.Add(ctrlTemplateFilterName);
      groupBox1.Location = new Point(839, 18);
      groupBox1.Margin = new Padding(2);
      groupBox1.Name = "groupBox1";
      groupBox1.Padding = new Padding(2);
      groupBox1.Size = new Size(474, 131);
      groupBox1.TabIndex = 10;
      groupBox1.TabStop = false;
      groupBox1.Text = "Filter";
      // 
      // ctrlFilterPaket
      // 
      ctrlFilterPaket.FormattingEnabled = true;
      ctrlFilterPaket.Location = new Point(58, 20);
      ctrlFilterPaket.Margin = new Padding(2);
      ctrlFilterPaket.Name = "ctrlFilterPaket";
      ctrlFilterPaket.Size = new Size(175, 23);
      ctrlFilterPaket.TabIndex = 13;
      // 
      // ctrlFilterStatus
      // 
      ctrlFilterStatus.FormattingEnabled = true;
      ctrlFilterStatus.Items.AddRange(new object[] { "alle", "einzustufen", "eingestuft" });
      ctrlFilterStatus.Location = new Point(58, 73);
      ctrlFilterStatus.Margin = new Padding(2);
      ctrlFilterStatus.Name = "ctrlFilterStatus";
      ctrlFilterStatus.Size = new Size(175, 23);
      ctrlFilterStatus.TabIndex = 12;
      ctrlFilterStatus.Text = "alle";
      ctrlFilterStatus.SelectedIndexChanged += ctrlFilterStatus_SelectedIndexChanged;
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Location = new Point(10, 75);
      label6.Margin = new Padding(2, 0, 2, 0);
      label6.Name = "label6";
      label6.Size = new Size(39, 15);
      label6.TabIndex = 11;
      label6.Text = "Status";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new Point(9, 50);
      label4.Margin = new Padding(2, 0, 2, 0);
      label4.Name = "label4";
      label4.Size = new Size(30, 15);
      label4.TabIndex = 10;
      label4.Text = "Tags";
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Location = new Point(10, 24);
      label5.Margin = new Padding(2, 0, 2, 0);
      label5.Name = "label5";
      label5.Size = new Size(36, 15);
      label5.TabIndex = 9;
      label5.Text = "Paket";
      // 
      // ctrlTemplateFilterTags
      // 
      ctrlTemplateFilterTags.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      ctrlTemplateFilterTags.Location = new Point(58, 48);
      ctrlTemplateFilterTags.Margin = new Padding(2);
      ctrlTemplateFilterTags.Name = "ctrlTemplateFilterTags";
      ctrlTemplateFilterTags.Size = new Size(175, 23);
      ctrlTemplateFilterTags.TabIndex = 8;
      ctrlTemplateFilterTags.KeyUp += ctrlTemplateFilterTags_KeyUp;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(264, 75);
      label3.Margin = new Padding(2, 0, 2, 0);
      label3.Name = "label3";
      label3.Size = new Size(37, 15);
      label3.TabIndex = 6;
      label3.Text = "Inhalt";
      // 
      // ctrlTemplateFilterInhalt
      // 
      ctrlTemplateFilterInhalt.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      ctrlTemplateFilterInhalt.Location = new Point(316, 73);
      ctrlTemplateFilterInhalt.Margin = new Padding(2);
      ctrlTemplateFilterInhalt.Name = "ctrlTemplateFilterInhalt";
      ctrlTemplateFilterInhalt.Size = new Size(147, 23);
      ctrlTemplateFilterInhalt.TabIndex = 5;
      ctrlTemplateFilterInhalt.KeyUp += ctrlTemplateFilterInhalt_KeyUp;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(264, 52);
      label2.Margin = new Padding(2, 0, 2, 0);
      label2.Name = "label2";
      label2.Size = new Size(46, 15);
      label2.TabIndex = 4;
      label2.Text = "Subject";
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(264, 24);
      label1.Margin = new Padding(2, 0, 2, 0);
      label1.Name = "label1";
      label1.Size = new Size(39, 15);
      label1.TabIndex = 3;
      label1.Text = "Name";
      // 
      // ctrlTemplatesFiltern
      // 
      ctrlTemplatesFiltern.Anchor = AnchorStyles.Left;
      ctrlTemplatesFiltern.Location = new Point(316, 102);
      ctrlTemplatesFiltern.Margin = new Padding(2);
      ctrlTemplatesFiltern.Name = "ctrlTemplatesFiltern";
      ctrlTemplatesFiltern.Size = new Size(146, 23);
      ctrlTemplatesFiltern.TabIndex = 2;
      ctrlTemplatesFiltern.Text = "Filtern";
      ctrlTemplatesFiltern.UseVisualStyleBackColor = true;
      ctrlTemplatesFiltern.Click += ctrlTemplatesFiltern_Click;
      // 
      // ctrlTemplateFilterSubject
      // 
      ctrlTemplateFilterSubject.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      ctrlTemplateFilterSubject.Location = new Point(316, 47);
      ctrlTemplateFilterSubject.Margin = new Padding(2);
      ctrlTemplateFilterSubject.Name = "ctrlTemplateFilterSubject";
      ctrlTemplateFilterSubject.Size = new Size(147, 23);
      ctrlTemplateFilterSubject.TabIndex = 1;
      ctrlTemplateFilterSubject.KeyUp += ctrlTemplateFilterSubject_KeyUp;
      // 
      // ctrlTemplateFilterName
      // 
      ctrlTemplateFilterName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      ctrlTemplateFilterName.Location = new Point(316, 22);
      ctrlTemplateFilterName.Margin = new Padding(2);
      ctrlTemplateFilterName.Name = "ctrlTemplateFilterName";
      ctrlTemplateFilterName.Size = new Size(147, 23);
      ctrlTemplateFilterName.TabIndex = 0;
      ctrlTemplateFilterName.KeyUp += ctrlTemplateFilterName_KeyUp;
      // 
      // ctrlDatenLadenGruppe
      // 
      ctrlDatenLadenGruppe.Controls.Add(ctrlTemplatesLadenAusDatei);
      ctrlDatenLadenGruppe.Controls.Add(ctrlTemplatesLadenVonSendGrid);
      ctrlDatenLadenGruppe.Location = new Point(4, 18);
      ctrlDatenLadenGruppe.Margin = new Padding(2);
      ctrlDatenLadenGruppe.Name = "ctrlDatenLadenGruppe";
      ctrlDatenLadenGruppe.Padding = new Padding(2);
      ctrlDatenLadenGruppe.Size = new Size(156, 124);
      ctrlDatenLadenGruppe.TabIndex = 9;
      ctrlDatenLadenGruppe.TabStop = false;
      ctrlDatenLadenGruppe.Text = "Templates laden";
      // 
      // ctrlTemplatesLadenAusDatei
      // 
      ctrlTemplatesLadenAusDatei.Location = new Point(13, 47);
      ctrlTemplatesLadenAusDatei.Margin = new Padding(2);
      ctrlTemplatesLadenAusDatei.Name = "ctrlTemplatesLadenAusDatei";
      ctrlTemplatesLadenAusDatei.Size = new Size(125, 23);
      ctrlTemplatesLadenAusDatei.TabIndex = 4;
      ctrlTemplatesLadenAusDatei.Text = "aus Datenbank";
      ctrlTemplatesLadenAusDatei.UseVisualStyleBackColor = true;
      ctrlTemplatesLadenAusDatei.Click += LadeTemplatesAusLokalemSpeicher;
      // 
      // ctrlTemplatesLadenVonSendGrid
      // 
      ctrlTemplatesLadenVonSendGrid.Location = new Point(14, 19);
      ctrlTemplatesLadenVonSendGrid.Margin = new Padding(2);
      ctrlTemplatesLadenVonSendGrid.Name = "ctrlTemplatesLadenVonSendGrid";
      ctrlTemplatesLadenVonSendGrid.Size = new Size(125, 23);
      ctrlTemplatesLadenVonSendGrid.TabIndex = 0;
      ctrlTemplatesLadenVonSendGrid.Tag = "";
      ctrlTemplatesLadenVonSendGrid.Text = "von SendGrid";
      ctrlTemplatesLadenVonSendGrid.UseVisualStyleBackColor = true;
      ctrlTemplatesLadenVonSendGrid.Click += LadeTemplatesVonSendGrid;
      // 
      // ctrlAktionen
      // 
      ctrlAktionen.Controls.Add(ctrlJsonBearbeiten);
      ctrlAktionen.Controls.Add(ctrlTemplateArchivieren);
      ctrlAktionen.Controls.Add(ctrlOeffneEditor);
      ctrlAktionen.Controls.Add(ctrlOeffneWebansicht);
      ctrlAktionen.Controls.Add(ctrlOeffnePreview);
      ctrlAktionen.Enabled = false;
      ctrlAktionen.Location = new Point(351, 18);
      ctrlAktionen.Margin = new Padding(2);
      ctrlAktionen.Name = "ctrlAktionen";
      ctrlAktionen.Padding = new Padding(2);
      ctrlAktionen.Size = new Size(145, 133);
      ctrlAktionen.TabIndex = 8;
      ctrlAktionen.TabStop = false;
      ctrlAktionen.Text = "Aktionen";
      // 
      // ctrlJsonBearbeiten
      // 
      ctrlJsonBearbeiten.Location = new Point(8, 102);
      ctrlJsonBearbeiten.Margin = new Padding(2);
      ctrlJsonBearbeiten.Name = "ctrlJsonBearbeiten";
      ctrlJsonBearbeiten.Size = new Size(38, 23);
      ctrlJsonBearbeiten.TabIndex = 8;
      ctrlJsonBearbeiten.Text = "🖊";
      ctrlJsonBearbeiten.UseVisualStyleBackColor = true;
      ctrlJsonBearbeiten.Click += ctrlJsonBearbeiten_Click;
      // 
      // ctrlTemplateArchivieren
      // 
      ctrlTemplateArchivieren.Location = new Point(50, 103);
      ctrlTemplateArchivieren.Margin = new Padding(2);
      ctrlTemplateArchivieren.Name = "ctrlTemplateArchivieren";
      ctrlTemplateArchivieren.Size = new Size(83, 23);
      ctrlTemplateArchivieren.TabIndex = 7;
      ctrlTemplateArchivieren.Text = "Archivieren";
      ctrlTemplateArchivieren.UseVisualStyleBackColor = true;
      ctrlTemplateArchivieren.Click += ctrlArchivierteTemplate_Click;
      // 
      // ctrlOeffneEditor
      // 
      ctrlOeffneEditor.Location = new Point(8, 20);
      ctrlOeffneEditor.Margin = new Padding(2);
      ctrlOeffneEditor.Name = "ctrlOeffneEditor";
      ctrlOeffneEditor.Size = new Size(125, 23);
      ctrlOeffneEditor.TabIndex = 4;
      ctrlOeffneEditor.Text = "Öffne in Editor";
      ctrlOeffneEditor.UseVisualStyleBackColor = true;
      ctrlOeffneEditor.Click += ctrlOeffneEditor_Click;
      // 
      // ctrlOeffneWebansicht
      // 
      ctrlOeffneWebansicht.Location = new Point(8, 75);
      ctrlOeffneWebansicht.Margin = new Padding(2);
      ctrlOeffneWebansicht.Name = "ctrlOeffneWebansicht";
      ctrlOeffneWebansicht.Size = new Size(125, 23);
      ctrlOeffneWebansicht.TabIndex = 6;
      ctrlOeffneWebansicht.Text = "Öffne Webansicht";
      ctrlOeffneWebansicht.UseVisualStyleBackColor = true;
      ctrlOeffneWebansicht.Click += ctrlOeffneWebansicht_Click;
      // 
      // ctrlOeffnePreview
      // 
      ctrlOeffnePreview.Location = new Point(8, 47);
      ctrlOeffnePreview.Margin = new Padding(2);
      ctrlOeffnePreview.Name = "ctrlOeffnePreview";
      ctrlOeffnePreview.Size = new Size(125, 23);
      ctrlOeffnePreview.TabIndex = 5;
      ctrlOeffnePreview.Text = "Öffne Preview";
      ctrlOeffnePreview.UseVisualStyleBackColor = true;
      ctrlOeffnePreview.Click += ctrlOeffnePreview_Click;
      // 
      // tbcManager
      // 
      tbcManager.Controls.Add(tbpManager);
      tbcManager.Controls.Add(tbpEinstellungen);
      tbcManager.Dock = DockStyle.Fill;
      tbcManager.Location = new Point(0, 0);
      tbcManager.Name = "tbcManager";
      tbcManager.SelectedIndex = 0;
      tbcManager.Size = new Size(1454, 804);
      tbcManager.TabIndex = 1;
      // 
      // tbpManager
      // 
      tbpManager.Controls.Add(ctrlTemplatesListe);
      tbpManager.Controls.Add(panel1);
      tbpManager.Controls.Add(ctrlMeldungen);
      tbpManager.Location = new Point(4, 24);
      tbpManager.Name = "tbpManager";
      tbpManager.Padding = new Padding(3);
      tbpManager.Size = new Size(1446, 776);
      tbpManager.TabIndex = 0;
      tbpManager.Text = "Manager";
      tbpManager.UseVisualStyleBackColor = true;
      // 
      // ctrlTemplatesListe
      // 
      ctrlTemplatesListe.AllowUserToOrderColumns = true;
      ctrlTemplatesListe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      ctrlTemplatesListe.Dock = DockStyle.Fill;
      ctrlTemplatesListe.Location = new Point(3, 184);
      ctrlTemplatesListe.Margin = new Padding(2);
      ctrlTemplatesListe.Name = "ctrlTemplatesListe";
      ctrlTemplatesListe.RowHeadersWidth = 62;
      ctrlTemplatesListe.Size = new Size(1440, 510);
      ctrlTemplatesListe.TabIndex = 0;
      ctrlTemplatesListe.CellMouseUp += ctrlTemplatesListe_CellMouseUp;
      ctrlTemplatesListe.RowStateChanged += ctrlTemplatesListe_RowStateChanged;
      ctrlTemplatesListe.DoubleClick += ctrlTemplatesListe_DoubleClick;
      // 
      // panel1
      // 
      panel1.Controls.Add(ctrlDatenLadenGruppe);
      panel1.Controls.Add(ctrlAktionen);
      panel1.Controls.Add(ctrlTags);
      panel1.Controls.Add(groupBox1);
      panel1.Controls.Add(groupBox3);
      panel1.Controls.Add(ctrlPakete);
      panel1.Dock = DockStyle.Top;
      panel1.Location = new Point(3, 3);
      panel1.Name = "panel1";
      panel1.Size = new Size(1440, 181);
      panel1.TabIndex = 12;
      // 
      // ctrlMeldungen
      // 
      ctrlMeldungen.Dock = DockStyle.Bottom;
      ctrlMeldungen.FormattingEnabled = true;
      ctrlMeldungen.ItemHeight = 15;
      ctrlMeldungen.Location = new Point(3, 694);
      ctrlMeldungen.Margin = new Padding(2);
      ctrlMeldungen.Name = "ctrlMeldungen";
      ctrlMeldungen.ScrollAlwaysVisible = true;
      ctrlMeldungen.Size = new Size(1440, 79);
      ctrlMeldungen.TabIndex = 11;
      // 
      // tbpEinstellungen
      // 
      tbpEinstellungen.Controls.Add(groupBox2);
      tbpEinstellungen.Location = new Point(4, 24);
      tbpEinstellungen.Name = "tbpEinstellungen";
      tbpEinstellungen.Padding = new Padding(3);
      tbpEinstellungen.Size = new Size(1446, 776);
      tbpEinstellungen.TabIndex = 1;
      tbpEinstellungen.Text = "Einstellungen";
      tbpEinstellungen.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      groupBox2.Controls.Add(btnEinstellungenSpeichern);
      groupBox2.Controls.Add(btnDatenbankAuswählen);
      groupBox2.Controls.Add(tbxDatenbankPfad);
      groupBox2.Controls.Add(label8);
      groupBox2.Controls.Add(tbxApiKey);
      groupBox2.Controls.Add(label7);
      groupBox2.Location = new Point(8, 29);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(491, 180);
      groupBox2.TabIndex = 0;
      groupBox2.TabStop = false;
      groupBox2.Text = "SendGrid";
      // 
      // btnEinstellungenSpeichern
      // 
      btnEinstellungenSpeichern.Location = new Point(287, 130);
      btnEinstellungenSpeichern.Name = "btnEinstellungenSpeichern";
      btnEinstellungenSpeichern.Size = new Size(147, 23);
      btnEinstellungenSpeichern.TabIndex = 5;
      btnEinstellungenSpeichern.Text = "Einstellungen speichern";
      btnEinstellungenSpeichern.UseVisualStyleBackColor = true;
      btnEinstellungenSpeichern.Click += btnEinstellungenSpeichern_Click;
      // 
      // btnDatenbankAuswählen
      // 
      btnDatenbankAuswählen.Location = new Point(359, 75);
      btnDatenbankAuswählen.Name = "btnDatenbankAuswählen";
      btnDatenbankAuswählen.Size = new Size(75, 23);
      btnDatenbankAuswählen.TabIndex = 4;
      btnDatenbankAuswählen.Text = "auswählen";
      btnDatenbankAuswählen.UseVisualStyleBackColor = true;
      btnDatenbankAuswählen.Click += btnDatenbankAuswählen_Click;
      // 
      // tbxDatenbankPfad
      // 
      tbxDatenbankPfad.Enabled = false;
      tbxDatenbankPfad.Location = new Point(84, 75);
      tbxDatenbankPfad.Name = "tbxDatenbankPfad";
      tbxDatenbankPfad.Size = new Size(269, 23);
      tbxDatenbankPfad.TabIndex = 3;
      // 
      // label8
      // 
      label8.AutoSize = true;
      label8.Location = new Point(16, 78);
      label8.Name = "label8";
      label8.Size = new Size(67, 15);
      label8.TabIndex = 2;
      label8.Text = "Datenbank:";
      // 
      // tbxApiKey
      // 
      tbxApiKey.Location = new Point(84, 38);
      tbxApiKey.Name = "tbxApiKey";
      tbxApiKey.Size = new Size(350, 23);
      tbxApiKey.TabIndex = 1;
      // 
      // label7
      // 
      label7.AutoSize = true;
      label7.Location = new Point(16, 41);
      label7.Name = "label7";
      label7.Size = new Size(50, 15);
      label7.TabIndex = 0;
      label7.Text = "Api Key:";
      // 
      // importFileDialog
      // 
      importFileDialog.FileName = "import";
      importFileDialog.Filter = "csv files (*.csv)|*.csv";
      // 
      // klicksFileDialog
      // 
      klicksFileDialog.FileName = "klicks";
      klicksFileDialog.Filter = "csv files (*.csv)|*.csv";
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1454, 804);
      Controls.Add(tbcManager);
      Margin = new Padding(2);
      Name = "MainForm";
      Text = "SendGrid Template Manager";
      Load += FormMain_Load;
      groupBox3.ResumeLayout(false);
      ctrlPakete.ResumeLayout(false);
      ctrlTags.ResumeLayout(false);
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ctrlDatenLadenGruppe.ResumeLayout(false);
      ctrlAktionen.ResumeLayout(false);
      tbcManager.ResumeLayout(false);
      tbpManager.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)ctrlTemplatesListe).EndInit();
      panel1.ResumeLayout(false);
      tbpEinstellungen.ResumeLayout(false);
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      ResumeLayout(false);
    }

    #endregion
    private Button ctrlTemplatesLadenVonSendGrid;
        private GroupBox ctrlAktionen;
        private Button ctrlTemplateArchivieren;
        private Button ctrlOeffneEditor;
        private Button ctrlOeffneWebansicht;
        private Button ctrlOeffnePreview;
        private GroupBox ctrlDatenLadenGruppe;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Button ctrlTemplatesFiltern;
        private TextBox ctrlTemplateFilterSubject;
        private TextBox ctrlTemplateFilterName;
        private Label label3;
        private TextBox ctrlTemplateFilterInhalt;
        private GroupBox ctrlTags;
        private Button ctrlMarkiereAlsNurCyberLounge;
        private Button ctrlMarkiereAlsPhishingMail;
        private Button ctrlMarkiereAlsNurCyberPortal;
        private Button ctrlMarkiereAdministrativeMail;
        private Button ctrlTemplatesLadenAusDatei;
        private GroupBox ctrlPakete;
        private Button ctrlBasisPaketZuordnen;
        private Button ctrlDemoPaketZuordnen;
        private Button ctrlExpertenPaketZuordnen;
        private Button ctrlStarterPaketZuordnen;
        private Button ctrlProfiPaketZuordnen;
        private Label label4;
        private Label label5;
        private TextBox ctrlTemplateFilterTags;
        private Button ctrlJsonBearbeiten;
        private GroupBox groupBox3;
        private Button ctrlImportiereBestandsdaten;
        private Button ctrlCsvExportCyberLounge;
        private Label label6;
        private ComboBox ctrlFilterStatus;
        private Button ctrlKlicksEinlesen;
        private ComboBox ctrlFilterPaket;
        private Button ctrl_PaketUebersicht;
        private Button ctrlMarkiereAlsNovaplast;
    private TabControl tbcManager;
    private TabPage tbpManager;
    private TabPage tbpEinstellungen;
    private GroupBox groupBox2;
    private Label label8;
    private TextBox tbxApiKey;
    private Label label7;
    private TextBox tbxDatenbankPfad;
    private Button btnDatenbankAuswählen;
    private Button btnEinstellungenSpeichern;
    private FolderBrowserDialog dlgDatenbankOrdner;
    private DataGridView ctrlTemplatesListe;
    private ListBox ctrlMeldungen;
    private Panel panel1;
    private OpenFileDialog importFileDialog;
    private OpenFileDialog klicksFileDialog;
    private FolderBrowserDialog exportFolderDialog;
  }
}
