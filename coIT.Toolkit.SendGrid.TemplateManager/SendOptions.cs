using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public record SendOptions
{
  [Category("Phishing Spezifisch"), Description("Empfänger-Email"), Required]
  public string RecipientEmail { get; set; } = "";

  [Category("Phishing Spezifisch"), Description("First name of the user")]
  [JsonProperty("firstName")]
  public string FirstName { get; init; } = "Max";

  [Category("Phishing Spezifisch"), Description("Last name of the user")]
  [JsonProperty("lastName")]
  public string LastName { get; init; } = "Mustermann";

  [Category("Phishing Spezifisch"), Description("Name of the company")]
  [JsonProperty("company")]
  public string Company { get; init; } = "Test GmbH";

  [Category("Phishing Spezifisch"), Description("Business sector of the company")]
  [JsonProperty("businessSector")]
  public string BusinessSector { get; init; } = "Dienstleistungen";

  [Category("Phishing Spezifisch"), Description("Full name of the accountant")]
  [JsonProperty("accountantName")]
  public string AccountantName { get; init; } = "Johann Meier";

  [Category("Phishing Spezifisch"), Description("Department of the user")]
  [JsonProperty("department")]
  public string Department { get; init; } = "Einkauf";

  [Category("Phishing Spezifisch"), Description("Salutation of the user")]
  public string Salutation => $"Sehr geehrter Herr {LastName}";

  [Category("Phishing Spezifisch"), Description("Unique tracking link")]
  public string Link { get; init; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Admin Configuration")]
  public string UrlAdminConfiguration { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Scope of Services")]
  public string UrlScopeOfServices { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for General Terms and Conditions")]
  public string UrlGeneralTermsAndConditions { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Terms of Use")]
  public string UrlTermsOfUse { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Help on Accountant Functions")]
  public string UrlHelpAccountantFunctions { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Help on Accountant Registration")]
  public string UrlHelpAccountantRegistration { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Help on Agencies")]
  public string UrlHelpAgencies { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Help on User Functions")]
  public string UrlHelpUserFunctions { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Help on User Registration")]
  public string UrlHelpUserRegistration { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Help on Phishing Activation")]
  public string UrlHelpPhishingActivation { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Online Banking Checklist")]
  public string UrlOnlineBankingChecklist { get; set; } = "https://co-IT.eu";

  [Category("URLs"), Description("URL for Privacy Policy")]
  public string UrlPrivacy { get; set; } = "https://co-IT.eu";

  // Time
  [Category("Time"), Description("Current date in dd.MM.yyyy format")]
  [JsonProperty("today")]
  public string Today { get; set; } = DateTime.Now.ToString("dd.MM.yyyy");

  [Category("Time"), Description("Current day of the week")]
  [JsonProperty("todayWeekDay")]
  public string TodayWeekDay { get; set; } =
    CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);

  [Category("Time"), Description("Date for the same day next week in dd.MM.yyyy format")]
  [JsonProperty("nextWeek")]
  public string NextWeek { get; set; } = DateTime.Now.AddDays(7).ToString("dd.MM.yyyy");

  [Category("Time"), Description("Date for the same day last week in dd.MM.yyyy format")]
  [JsonProperty("lastWeek")]
  public string LastWeek { get; set; } = DateTime.Now.AddDays(-7).ToString("dd.MM.yyyy");

  [Category("Time"), Description("Current year")]
  [JsonProperty("thisYear")]
  public string ThisYear { get; set; } = DateTime.Now.Year.ToString();

  [Category("Time"), Description("Next year")]
  [JsonProperty("nextYear")]
  public string NextYear { get; set; } = DateTime.Now.AddYears(1).Year.ToString();

  [Category("Time"), Description("Last year")]
  [JsonProperty("lastYear")]
  public string LastYear { get; set; } = DateTime.Now.AddYears(-1).Year.ToString();

  // Common
  [Category("Common"), Description("Name of the application")]
  public string AppName { get; set; } = "Cyber Lounge";

  [Category("Common"), Description("Base path of the application")]
  public string AppBasePath { get; set; } = "https://cyber-lounge.co-IT.eu";

  [Category("Common"), Description("Current environment name")]
  public string Environment { get; set; } = "Staging";

  [Category("Common"), Description("Label for the current environment")]
  public string EnvironmentLabel { get; set; } = "Beta";

  [Category("Common"), Description("Name of the sender")]
  public string SenderName { get; set; } = "Cyber Lounge";

  [Category("Common"), Description("Email of the sender")]
  public string SenderEmail { get; set; } = "cyber-lounge@co-IT.eu";

  [Category("Common"), Description("Default email address")]
  public string DefaultEmail { get; set; } = "cyber-lounge@co-IT.eu";

  [Category("Common"), Description("No-reply email address")]
  public string NoReplyEmail { get; set; } = "noreply.cyber-lounge@co-IT.eu";

  [Category("Common"), Description("Support email address")]
  public string SupportEmail { get; set; } = "cl-support@co-IT.eu";

  [Category("Common"), Description("Email disclaimer")]
  [JsonProperty("disclaimer")]
  public string Disclaimer { get; set; } = "Viel Spaß mit der Cyber Lounge!";
}
