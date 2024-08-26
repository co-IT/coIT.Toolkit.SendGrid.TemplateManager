# coIT.Libraries.Template

## Anleitung

### Ersteinrichtung

1.
Öffne [https://github.com/co-IT/coIT.Libraries.Template](https://github.com/co-IT/coIT.Libraries.Template)
2. Gehe oben rechts auf `Template nutzen` und erstelle das neue Repository. Achte darauf, den
   Namespace `coIT.Libraries.` zu nutzen. Weitere Informationen zum Übernehmen eines Templates gibt
   es [hier](https://docs.github.com/en/repositories/creating-and-managing-repositories/creating-a-repository-from-a-template)
3. Clone das Repository
4. Benenne die `coIT.Libraries.Template.sln` in `coIT.Libraries.{Neue-Bibliothek}.sln` um
5. Falls bereits Code existert, kopiere diesen in das Verzeichnis
6. Öffne die Solution in Visual Studio
7. Füge den bestehenden Code als existierendes Projekt hinzu
8. Bennene das existierende Projekt in `coIT.Libraries.{Neue-Bibliothek}` um
9. Synchronisiere die Namespaces
10. Navigiere zu `Project > [Project Name] Properties > Package` und versehe die Library mindestens
    mit Name, Beschreibung, Author und Company
11. Führe den Befehl `dotnet csharpier .` im root-Verzeichnis des Repositories aus
12. Schreibe einen neuen commit und pushe das gesamte Repository
13. Wenn dieses Bibliothek auf andere in GitHub veröffentlichte Bibliotheken verweist folge der
    Erklärung unter `Besonderheiten`

### Besonderheiten

Wenn diese Bibliothek auf anderen Bibliotheken aus dem co-IT GitHub repository verweist, dann muss
in den Paketeinstellungen dieser anderen Bibliothek ein expliziter Verweis auf diese Bibliothek
gemacht werden um den Zugriff zu erlauben, solange diese Bibliothek nicht als intern sichtbar
markiert wurde.

[Hier](https://docs.github.com/en/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility#package-creation-visibility-for-organization-members)
ist ein Guide, wie man ein Packet intern sichtbar macht. Das ist das empfohlene vorgehen für alle
Packages.

Falls das für ein sensitives Packet aus irgendwelchen Gründen nicht möglich ist, folge diesem Guide:

> Go to your org's package settings. Should be https://github.com/orgs/<Organisation>
> /packages/nuget/<PackageId>/settings.
> Then under Manage Actions access, add the repository(ies) that need access to the package. Read
> access is fine.

[Quelle](https://github.com/orgs/community/discussions/75561#discussioncomment-9800644)

### Bibliothek veröffentlichen

1. Gehe zur GitHub Release Seite deiner Bibliothek
2. Erstelle ein neues Release
3. Versehe das release mit einem Tag, der dem Format `*.*.*` also z.B. `1.0.0` folgt
4. Gib dem Release eine Überschrift und Beschreibung
5. Nach dem Erstellen des Releases wird die Bibliothek automatisch gebaut und mit dem GitHub Package
   Manager veröffentlicht
6. Wenn
   erlaubt [gebe danach das Package intern in der Organisation frei](https://docs.github.com/en/packages/learn-github-packages/configuring-a-packages-access-control-and-visibility#package-creation-visibility-for-organization-members),
   dass in Zukunft alle repositories ohne explizite Erlaubniss das package herunterladen können

## Informationen

Das Repo enthält bereits:

- [CSharpier](https://csharpier.com/docs/About) und eine `.editorconfig`
- `.gitattributes` für konsistente Zeilenenden
- `.gitignore` für VisualStudio und Rider
- `.github/workflows/build_and_publish.yml` mit der das Projekt automatisch beim Erstellen von
  Releases veröffentlicht wird

## Einstellungen

> [!NOTE]
> Die .NET Version für den `Build and publish` workflow wird in der `global.json` festgelegt und
> muss in der Zukunft eventuell angepasst werden
