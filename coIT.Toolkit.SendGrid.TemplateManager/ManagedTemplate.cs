using System.Text;
using Azure;
using coIT.Libraries.Sendgrid.Contracts;
using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public record ManagedTemplate(SendGridTemplate SendGridTemplate)
{
  public SendGridTemplate SendGridTemplate { get; set; } = SendGridTemplate;
  public string PlainContent { get; set; } = string.Empty;

  public List<string> Tags { get; set; } = [];
  public List<Paket> Pakete { get; set; } = [];
  public bool Archiviert { get; set; }

  public PhishingMailEinstufung? Einstufung { get; set; }
  public Absender? Absender { get; set; }

  [JsonIgnore]
  public int Versandt { get; set; }

  [JsonIgnore]
  public int Klicks { get; set; }

  [JsonIgnore]
  public DateTimeOffset? Timestamp { get; set; }

  [JsonIgnore]
  public ETag ETag { get; set; }

  public TabellenEintrag ToTabellenEintrag()
  {
    return new TabellenEintrag
    {
      TemplateId = SendGridTemplate.TemplateId,
      VersionId = SendGridTemplate.VersionId,
      LastUpdated = SendGridTemplate.LastUpdated,
      Name = SendGridTemplate.Name,
      Subject = SendGridTemplate.Subject,
      EditorUri = SendGridTemplate.EditorUri,
      PreviewUri = SendGridTemplate.PreviewUri,
      Tags = Tags.OrderBy(t => t).Aggregate(string.Empty, (current, add) => $"{current} {add}").Trim(),
      Pakete = Pakete.OrderBy(p => p).Aggregate(string.Empty, (current, add) => $"{current} {add}").Trim(),
      AbsenderGepflegt = Absender != null,
      Eingestuft = Einstufung != null,
      Skala = Einstufung?.Bewertung ?? 0,
      Schwierigkeit = Einstufung is null ? string.Empty : Enum.GetName(Einstufung!.Klassifizierung)!,
      Klicks = Klicks,
      Versandt = Versandt,
    };
  }

  public string ToCsvExport(string interneTemplateId)
  {
    if (Absender is null)
      return string.Empty;

    if (Einstufung is null)
      return string.Empty;

    var zeile = new StringBuilder();
    var trenner = ";";
    zeile.Append(interneTemplateId + trenner);
    zeile.Append(SendGridTemplate.TemplateId + trenner);
    zeile.Append(SendGridTemplate.Name.Trim() + trenner);
    zeile.Append(Absender.Adresse.Trim() + trenner);
    zeile.Append(Absender.Name.Trim() + trenner);
    zeile.Append($"{(int)Einstufung.Klassifizierung}" + trenner);
    zeile.Append("1" + trenner);
    zeile.Append("1" + trenner);
    zeile.Append(SendGridTemplate.LastUpdated.ToString("O") + trenner);
    zeile.Append($"https://{Einstufung.TrackingLinkDomain.Trim()}/{Einstufung.TrackingLinkPfad.TrimStart(' ', '/')}");

    return zeile.ToString();
  }

  public string ToSqlExport(string interneTemplateId)
  {
    if (Absender is null)
      return string.Empty;

    if (Einstufung is null)
      return string.Empty;

    var sb = new StringBuilder();

    var templateSql = $"""
                       MERGE INTO [dbo].[PhishingMailTemplates] AS target
                       USING (VALUES
                           ('{interneTemplateId}', '{SendGridTemplate.TemplateId}', '{SendGridTemplate.Name.Trim()}', '{Absender.Adresse.Trim()}', '{Absender.Name.Trim()}', {(int)
                             Einstufung.Klassifizierung}, 1, 1, '{SendGridTemplate.LastUpdated:O}', 'https://{Einstufung.TrackingLinkDomain.Trim()}/{Einstufung.TrackingLinkPfad.TrimStart(
                             ' ',
                             '/'
                           )}')
                       ) AS source ([Id], [TemplateId], [Title], [SenderEmail], [SenderName], [LevelOfDifficulty], [BusinessSector], [Department], [Created], [Link])
                       ON target.[Id] = source.[Id]
                       WHEN MATCHED THEN
                           UPDATE SET
                               target.[TemplateId] = source.[TemplateId],
                               target.[Title] = source.[Title],
                               target.[SenderEmail] = source.[SenderEmail],
                               target.[SenderName] = source.[SenderName],
                               target.[LevelOfDifficulty] = source.[LevelOfDifficulty],
                               target.[BusinessSector] = source.[BusinessSector],
                               target.[Department] = source.[Department],
                               target.[Link] = source.[Link]
                       WHEN NOT MATCHED THEN
                           INSERT ([Id], [TemplateId], [Title], [SenderEmail], [SenderName], [LevelOfDifficulty], [BusinessSector], [Department], [Created], [Link])
                           VALUES (source.[Id], source.[TemplateId], source.[Title], source.[SenderEmail], source.[SenderName], source.[LevelOfDifficulty], source.[BusinessSector], source.[Department], source.[Created], source.[Link]);
                       """;

    sb.AppendLine(templateSql);

    var paketTemplateZuordnungen = Pakete.Select(paket => $"({(int)paket}, '{interneTemplateId}')");

    var paketSql =
      Pakete.Count != 0
        ? $"""
          MERGE INTO [dbo].[PackagePhishingMailTemplates] AS target
          USING (VALUES
              {string.Join(',', paketTemplateZuordnungen)}
          ) AS source ([PackageValue], [PhishingMailTemplateId])
          ON target.[PackageValue] = source.[PackageValue] AND target.[PhishingMailTemplateId] = source.[PhishingMailTemplateId]
          WHEN MATCHED AND target.[PackageValue] <> source.[PackageValue] THEN
              UPDATE SET
                  target.[PackageValue] = source.[PackageValue]
          WHEN NOT MATCHED BY TARGET THEN
              INSERT ([PackageValue], [PhishingMailTemplateId])
              VALUES (source.[PackageValue], source.[PhishingMailTemplateId])
          WHEN NOT MATCHED BY SOURCE AND target.[PhishingMailTemplateId] = '{interneTemplateId}' THEN
              DELETE;
          """
        : $"""
          DELETE FROM [dbo].[PackagePhishingMailTemplates]
                WHERE PhishingMailTemplateId = '{interneTemplateId}'
          GO
          """;

    sb.AppendLine(paketSql);

    return sb.ToString();
  }
}
