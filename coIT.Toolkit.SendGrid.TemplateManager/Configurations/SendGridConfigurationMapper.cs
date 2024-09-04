using coIT.Libraries.ConfigurationManager.Cryptography;
using CSharpFunctionalExtensions;

namespace coIT.Toolkit.SendGrid.TemplateManager.Configurations;

internal class SendGridConfigurationMapper
{
  private readonly IDoCryptography _cryptographyService;

  public SendGridConfigurationMapper(IDoCryptography cryptographyService)
  {
    _cryptographyService = cryptographyService;
  }

  public Result<SendGridConfiguration> FromEntity(SendGridConfigurationEntity entity)
  {
    return _cryptographyService.Decrypt(entity.ApiKey).Map(apiKey => new SendGridConfiguration(apiKey));
  }

  public Result<SendGridConfigurationEntity> ToEntity(SendGridConfiguration configuration)
  {
    return _cryptographyService
      .Encrypt(configuration.ApiKey)
      .Map(apiKey => new SendGridConfigurationEntity { ApiKey = apiKey });
  }
}
