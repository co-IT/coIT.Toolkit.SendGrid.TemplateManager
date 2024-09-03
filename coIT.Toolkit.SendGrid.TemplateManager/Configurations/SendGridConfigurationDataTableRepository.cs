using Azure;
using Azure.Data.Tables;
using coIT.Libraries.ConfigurationManager.Cryptography;
using CSharpFunctionalExtensions;

namespace coIT.Toolkit.SendGrid.TemplateManager.Configurations;

public class SendGridConfigurationDataTableRepository
{
  private readonly SendGridConfigurationMapper _mapper;
  private readonly TableClient _tableClient;

  public SendGridConfigurationDataTableRepository(string connectionString, IDoCryptography cryptographyService)
  {
    _mapper = new SendGridConfigurationMapper(cryptographyService);
    _tableClient = new TableClient(connectionString, SendGridConfigurationEntity.TabellenName);
    _tableClient.CreateIfNotExists();
  }

  public async Task<Result<SendGridConfiguration>> Get(CancellationToken cancellationToken = default)
  {
    try
    {
      return await Result
        .Success()
        .Map(
          () =>
            _tableClient.GetEntityIfExistsAsync<SendGridConfigurationEntity>(
              SendGridConfigurationEntity.TabellenName,
              SendGridConfigurationEntity.GlobalIdentifier,
              cancellationToken: cancellationToken
            )
        )
        .Ensure(
          response => response.HasValue,
          $"Der Eintrag mit dem Namen '{SendGridConfigurationEntity.GlobalIdentifier}' konnte nicht gefunden werden."
        )
        .Map(response => response.Value)
        .Bind(_mapper.FromEntity!);
    }
    catch (Exception ex) when (ex is RequestFailedException or InvalidOperationException)
    {
      return Result.Failure<SendGridConfiguration>(ex.Message);
    }
  }

  public async Task<Result> Upsert(SendGridConfiguration configuration, CancellationToken cancellationToken = default)
  {
    try
    {
      return await _mapper
        .ToEntity(configuration)
        .Tap(_ => _tableClient.CreateIfNotExistsAsync(cancellationToken))
        .Tap(entity => _tableClient.UpsertEntityAsync(entity, cancellationToken: cancellationToken));
    }
    catch (RequestFailedException ex)
    {
      return Result.Failure(ex.Message);
    }
  }
}
