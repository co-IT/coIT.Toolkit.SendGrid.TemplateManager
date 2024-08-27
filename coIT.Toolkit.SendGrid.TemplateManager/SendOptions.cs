using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public record SendOptions
{
  [Category("Phishing Spezifisch"), Description("Empfänger-Email"), Required]
  public string RecipientEmail { get; set; } = "";

  [Category("Phishing Spezifisch"), Description("Vorname des Empfängers")]
  [JsonProperty("firstName")]
  public string FirstName { get; set; } = "Max";

  [Category("Phishing Spezifisch"), Description("Nachname des Empfängers")]
  [JsonProperty("lastName")]
  public string LastName { get; set; } = "Mustermann";

  [Category("Phishing Spezifisch"), Description("Name des Unternehmens")]
  [JsonProperty("company")]
  public string Company { get; set; } = "Test GmbH";

  [Category("Phishing Spezifisch"), Description("Branche des Unternehmens")]
  [JsonProperty("businessSector")]
  public string BusinessSector { get; set; } = "Dienstleistungen";

  [Category("Phishing Spezifisch"), Description("Name des Cyber-Verantwortlichen")]
  [JsonProperty("accountantName")]
  public string AccountantName { get; set; } = "Johann Meier";

  [Category("Phishing Spezifisch"), Description("Abteilung des Empfängers")]
  [JsonProperty("department")]
  public string Department { get; set; } = "Einkauf";

  [Category("Phishing Spezifisch"), Description("Anrede des Empfängers")]
  public string Salutation => $"Sehr geehrter Herr {LastName}";

  [Category("Phishing Spezifisch"), Description("Eindeutiger Tracking-Link")]
  public string Link { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für die Admin-Konfiguration")]
  public string UrlAdminConfiguration { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für den Leistungsumfang")]
  public string UrlScopeOfServices { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für die Allgemeinen Geschäftsbedingungen")]
  public string UrlGeneralTermsAndConditions { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für die Nutzungsbedingungen")]
  public string UrlTermsOfUse { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für Hilfe zu Buchhaltungsfunktionen")]
  public string UrlHelpAccountantFunctions { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für Hilfe zur Buchhalterregistrierung")]
  public string UrlHelpAccountantRegistration { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für Hilfe zu Agenturen")]
  public string UrlHelpAgencies { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für Hilfe zu Benutzerfunktionen")]
  public string UrlHelpUserFunctions { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für Hilfe zur Benutzerregistrierung")]
  public string UrlHelpUserRegistration { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für Hilfe zur Phishing-Aktivierung")]
  public string UrlHelpPhishingActivation { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für die Online-Banking-Checkliste")]
  public string UrlOnlineBankingChecklist { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL für die Datenschutzerklärung")]
  public string UrlPrivacy { get; set; } = "https://co-IT.eu";

  // Time
  [Category("Zeit"), Description("Aktuelles Datum im Format dd.MM.yyyy")]
  [JsonProperty("today")]
  public string Today { get; set; } = DateTime.Now.ToString("dd.MM.yyyy");

  [Category("Zeit"), Description("Aktueller Wochentag")]
  [JsonProperty("todayWeekDay")]
  public string TodayWeekDay { get; set; } =
    CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);

  [Category("Zeit"), Description("Datum des Tages in einer Woche im Format dd.MM.yyyy")]
  [JsonProperty("nextWeek")]
  public string NextWeek { get; set; } = DateTime.Now.AddDays(7).ToString("dd.MM.yyyy");

  [Category("Zeit"), Description("Datum des Tages vor einer Woche im Format dd.MM.yyyy")]
  [JsonProperty("lastWeek")]
  public string LastWeek { get; set; } = DateTime.Now.AddDays(-7).ToString("dd.MM.yyyy");

  [Category("Zeit"), Description("Aktuelles Jahr")]
  [JsonProperty("thisYear")]
  public string ThisYear { get; set; } = DateTime.Now.Year.ToString();

  [Category("Zeit"), Description("Nächstes Jahr")]
  [JsonProperty("nextYear")]
  public string NextYear { get; set; } = DateTime.Now.AddYears(1).Year.ToString();

  [Category("Zeit"), Description("Letztes Jahr")]
  [JsonProperty("lastYear")]
  public string LastYear { get; set; } = DateTime.Now.AddYears(-1).Year.ToString();

  // Common
  [Category("Allgemein"), Description("Name der Anwendung")]
  public string AppName { get; set; } = "Cyber Lounge";

  [Category("Allgemein"), Description("Basis-URL der Anwendung")]
  public string AppBasePath { get; set; } = "https://cyber-lounge.co-IT.eu";

  [Category("Allgemein"), Description("Umgebungsname")]
  public string Environment { get; set; } = "Staging";

  [Category("Allgemein"), Description("Bezeichnung für die Umgebung")]
  public string EnvironmentLabel { get; set; } = "Beta";

  [Category("Allgemein"), Description("Name des Absenders")]
  public string SenderName { get; set; } = "Cyber Lounge";

  [Category("Allgemein"), Description("E-Mail des Absenders")]
  public string SenderEmail { get; set; } = "cyber-lounge@co-IT.eu";

  [Category("Allgemein"), Description("Standard-E-Mail-Adresse")]
  public string DefaultEmail { get; set; } = "cyber-lounge@co-IT.eu";

  [Category("Allgemein"), Description("No-Reply-E-Mail-Adresse")]
  public string NoReplyEmail { get; set; } = "noreply.cyber-lounge@co-IT.eu";

  [Category("Allgemein"), Description("Support-E-Mail-Adresse")]
  public string SupportEmail { get; set; } = "cl-support@co-IT.eu";

  [Category("Allgemein"), Description("E-Mail-Disclaimer")]
  [JsonProperty("disclaimer")]
  public string Disclaimer { get; set; } = "Viel Spaß mit der Cyber Lounge!";
}
