# coIT.Toolkit.SendGrid.TemplateManager

Anwendung zur Verwaltung von SendGrid Templates für Cyber Produkte

## Anleitung

### Ersteinrichtung

`.msi`-Datei von [GitHub Releases](https://github.com/co-IT/coIT.Toolkit.SendGrid.TemplateManager/releases) herunterladen und installieren

Wichtig: Es muss der verschlüsselte Datenbank ConnectionString als Umgebungsvariable `COIT_TOOLKIT_DATABASE_CONNECTIONSTRING` zur Verfügung stehen.

### Updates

Beim Programmstart wird in den GitHub Releases automatisch nach Updates gesucht und diese installiert.

## Development

Das Repo enthält bereits:

- [CSharpier](https://csharpier.com/docs/About) und eine `.editorconfig`
- `.gitattributes` für konsistente Zeilenenden
- `.gitignore` für VisualStudio und Rider
- `.github/workflows/build_and_publish.yml` mit der das Projekt automatisch beim Erstellen von
  Releases veröffentlicht wird

### Deployment

> [!NOTE]
> Die .NET Version für den `Build and publish` workflow wird in der `global.json` festgelegt und
> muss in der Zukunft eventuell angepasst werden
