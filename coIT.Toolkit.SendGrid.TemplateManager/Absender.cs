namespace coIT.Toolkit.SendGrid.TemplateManager;

public record Absender
{
  public string Adresse { get; set; }
  public string Name { get; set; }
  public bool VonCyberVerantwortlichen { get; set; }
}
