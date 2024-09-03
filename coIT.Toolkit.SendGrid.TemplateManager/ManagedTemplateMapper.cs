using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

internal static class ManagedTemplateMapper
{
  public static ManagedTemplate FromEntity(ManagedTemplateEntity entity)
  {
    var template = JsonConvert.DeserializeObject<ManagedTemplate>(entity.Json);

    template.ETag = entity.ETag;
    template.Timestamp = entity.Timestamp;

    return template;
  }

  public static ManagedTemplateEntity ToEntity(ManagedTemplate template)
  {
    var json = JsonConvert.SerializeObject(template);

    var entity = new ManagedTemplateEntity
    {
      Json = json,
      SendGridTemplateId = template.SendGridTemplate.TemplateId,
      ETag = template.ETag,
      Timestamp = template.Timestamp,
    };

    return entity;
  }
}
