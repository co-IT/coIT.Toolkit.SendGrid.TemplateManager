namespace coIT.Toolkit.SendGrid.TemplateManager;

public record TabellenEintrag
{
  public string TemplateId { get; set; }
  public string VersionId { get; set; }
  public DateTimeOffset LastUpdated { get; set; }

  public string Name { get; set; }
  public string Subject { get; set; }

  public string Tags { get; set; }
  public string Pakete { get; set; }

  public bool AbsenderGepflegt { get; set; }
  public bool Eingestuft { get; set; }
  public int Skala { get; set; }
  public string Schwierigkeit { get; set; }

  public decimal Klickquote => Versandt == 0 ? 0 : Klicks / (decimal)Versandt; //Versandt <= 0 ? "-" : $"{(Klicks / (decimal)Versandt):P}";
  public int Versandt { get; set; }
  public int Klicks { get; set; }

  public Uri EditorUri { get; set; }
  public Uri PreviewUri { get; set; }
}
