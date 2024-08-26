using Newtonsoft.Json;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public record PhishingMailEinstufung
{
  public bool CVNameVerwendet { get; set; }
  public bool AktuellesThema { get; set; }
  public bool AuthErsterLink { get; set; }
  public bool Bilder { get; set; }
  public bool VariablesDatum { get; set; }
  public bool HatLogo { get; set; }
  public bool AuthTrackingLink { get; set; }
  public bool PersonalisierteAnrede { get; set; }
  public bool BeruflicheRelevanz { get; set; }

  public Bewertungsskala Unternehmensinfo { get; set; }
  public Bewertungsskala Sprachniveau { get; set; }
  public Bewertungsskala DruckUndAuswirkung { get; set; }
  public Bewertungsskala Formatierung { get; set; }
  public Bewertungsskala PersoenlicheRelevanz { get; set; }

  public bool AuthAnzeigename { get; set; }
  public bool AuthAbsenderadresse { get; set; }
  public string TrackingLinkDomain { get; set; }
  public string TrackingLinkPfad { get; set; }

  [JsonIgnore]
  public int Bewertung
  {
    get
    {
      var punkte = 0;

      punkte += HatLogo ? 1 : 0;
      punkte += VariablesDatum ? 2 : 0;
      punkte += Bilder ? 3 : 0;
      punkte += AuthErsterLink ? 4 : 0;
      punkte += AktuellesThema ? 5 : 0;
      punkte += CVNameVerwendet ? 6 : 0;
      punkte += Umrechnen(Unternehmensinfo, 7);
      punkte += AuthTrackingLink ? 8 : 0;
      punkte += PersonalisierteAnrede ? 9 : 0;
      punkte += Umrechnen(Sprachniveau, 10);
      punkte += Umrechnen(DruckUndAuswirkung, 11);
      punkte += Umrechnen(Formatierung, 12);
      punkte += BeruflicheRelevanz ? 13 : 0;
      punkte += Umrechnen(PersoenlicheRelevanz, 14);
      punkte += AuthAbsenderadresse ? 15 : 0;
      punkte += AuthAnzeigename ? 16 : 0;

      return punkte;
    }
  }

  [JsonIgnore]
  public PhishingMailLevelOfDifficulty Klassifizierung
  {
    get
    {
      var gesamt = Enumerable.Range(0, 16).Sum();

      var verdaechtig = gesamt / 4 * 1;
      var ungewiss = gesamt / 4 * 2;
      var authentisch = gesamt / 4 * 3;

      if (Bewertung <= verdaechtig)
        return PhishingMailLevelOfDifficulty.Verdächtig;
      if (Bewertung <= ungewiss)
        return PhishingMailLevelOfDifficulty.Ungewiss;
      if (Bewertung <= authentisch)
        return PhishingMailLevelOfDifficulty.Authentisch;

      return PhishingMailLevelOfDifficulty.TäuschendEcht;
    }
  }

  private static int Umrechnen(Bewertungsskala bewertungsskala, int multiplikator)
  {
    var punkte = Enumerable.Range(0, multiplikator);

    if (bewertungsskala == Bewertungsskala.Keine)
      return punkte.Min();

    if (bewertungsskala == Bewertungsskala.Niedrig)
      return punkte.Skip(2).Min();

    if (bewertungsskala == Bewertungsskala.Mittel)
      return multiplikator / 2;

    if (bewertungsskala == Bewertungsskala.Hoch)
      return punkte.OrderByDescending(v => v).Skip(2).Max();

    if (bewertungsskala == Bewertungsskala.Extrem)
      return punkte.Max();

    throw new NotImplementedException();
  }
}
