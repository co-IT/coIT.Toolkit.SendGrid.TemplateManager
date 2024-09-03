using Azure.Data.Tables;
using coIT.Libraries.Sendgrid.Contracts;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public class ManagedTemplateRepository
{
  private readonly TableClient _tableClient;

  public ManagedTemplateRepository(string connectionString)
  {
    _tableClient = new TableClient(connectionString, ManagedTemplateEntity.TabellenName);
    _tableClient.CreateIfNotExists();
  }

  public async Task<List<ManagedTemplate>> GetAll(CancellationToken ct)
  {
    var templates = new List<ManagedTemplate>();

    var templateEntities = _tableClient.QueryAsync<ManagedTemplateEntity>(cancellationToken: ct);

    await foreach (var entity in templateEntities)
    {
      var template = ManagedTemplateMapper.FromEntity(entity);
      templates.Add(template);
    }

    return templates;
  }

  public async Task<ManagedTemplate?> Find(string sendGridTemplateId, CancellationToken ct)
  {
    var templateEntity = await _tableClient.GetEntityIfExistsAsync<ManagedTemplateEntity>(
      ManagedTemplateEntity.TabellenName,
      sendGridTemplateId,
      cancellationToken: ct
    );

    if (!templateEntity.HasValue)
      return null;

    return ManagedTemplateMapper.FromEntity(templateEntity.Value!);
  }

  public async Task Update(ManagedTemplate template, CancellationToken ct)
  {
    var entity = ManagedTemplateMapper.ToEntity(template);

    var response = await _tableClient.UpsertEntityAsync(entity, cancellationToken: ct);

    //TODO handle concurrency problems
  }

  public async Task Update(IEnumerable<SendGridTemplate> templates, CancellationToken ct)
  {
    foreach (var template in templates)
    {
      if (ct.IsCancellationRequested)
        return;

      var managedTemplate = new ManagedTemplate(template);

      var existing = await Find(managedTemplate.SendGridTemplate.TemplateId, ct);
      if (existing != null)
      {
        managedTemplate.PlainContent = string.Empty;
        managedTemplate.Tags = existing.Tags;
        managedTemplate.Pakete = existing.Pakete;
        managedTemplate.Einstufung = existing.Einstufung;
        managedTemplate.Absender = existing.Absender;
        managedTemplate.Archiviert = existing.Archiviert;
      }

      await Update(managedTemplate, ct);
    }
  }
}
