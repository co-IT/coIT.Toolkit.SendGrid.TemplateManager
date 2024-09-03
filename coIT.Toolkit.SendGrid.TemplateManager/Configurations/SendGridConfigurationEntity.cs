using Azure;
using Azure.Data.Tables;

namespace coIT.Toolkit.SendGrid.TemplateManager.Configurations;

public record SendGridConfigurationEntity : ITableEntity
{
  internal static readonly string TabellenName = "SendGridKonfiguration";

  // Globale Konfiguration für alle Nutzer
  internal static readonly string GlobalIdentifier = "global";

  public required string ApiKey { get; init; }

  public string PartitionKey
  {
    get => TabellenName;
    set { }
  }
  public string RowKey
  {
    get => GlobalIdentifier;
    set { }
  }
  public DateTimeOffset? Timestamp { get; set; }
  public ETag ETag { get; set; }
}
