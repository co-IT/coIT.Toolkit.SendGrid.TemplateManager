using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public record ManagedTemplateEntity : ITableEntity
{
  public static readonly string TabellenName = "ManagedTemplates";

  [IgnoreDataMember]
  public required string SendGridTemplateId { get; init; }
  public required string Json { get; init; }

  public string PartitionKey
  {
    get => TabellenName;
    set { }
  }

  public string RowKey
  {
    get => SendGridTemplateId;
    set { }
  }

  public DateTimeOffset? Timestamp { get; set; }
  public ETag ETag { get; set; }
}
