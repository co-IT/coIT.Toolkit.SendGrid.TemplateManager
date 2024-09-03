using System.Text;
using Azure.Data.Tables;
using coIT.Toolkit.SendGrid.TemplateManager;
using Newtonsoft.Json;

Console.WriteLine("ConnectionString eingeben:");
var connectionString = Console.ReadLine();

Console.WriteLine("Pfad zu JSON-Dateien eingeben:");
var path = Console.ReadLine();

if (!Directory.Exists(path))
{
  Console.WriteLine("Der Pfad existiert nicht.");
  return;
}

var tableClient = new TableClient(connectionString, ManagedTemplateEntity.TabellenName);

var files = Directory.GetFiles(path);

foreach (var file in files)
{
  Console.WriteLine($"Migriere {Path.GetFileName(file)}");

  var json = await File.ReadAllTextAsync(file, Encoding.UTF8);

  var managedTemplate = JsonConvert.DeserializeObject<ManagedTemplate>(json);

  var entity = new ManagedTemplateEntity
  {
    Json = JsonConvert.SerializeObject(managedTemplate),
    SendGridTemplateId = managedTemplate!.SendGridTemplate.TemplateId,
  };

  await tableClient.UpsertEntityAsync(entity);
}
