namespace coIT.Toolkit.SendGrid.TemplateManager;

public enum Paket
{
  Keins = 1,
  Demo = 2,
  Basis = 3,
  Starter = 4,
  Profi = 5,
  Experte = 6
}

public static class PaketKonfiguration
{
  public static readonly Dictionary<Paket, Dictionary<PhishingMailLevelOfDifficulty, byte>> Konfiguration = new()
  {
    {
      Paket.Keins, new Dictionary<PhishingMailLevelOfDifficulty, byte>
      {
        { PhishingMailLevelOfDifficulty.Verdächtig, 0 },
        { PhishingMailLevelOfDifficulty.Ungewiss, 0 },
        { PhishingMailLevelOfDifficulty.Authentisch, 0 },
        { PhishingMailLevelOfDifficulty.TäuschendEcht, 0 }
      }
    },
    {
      Paket.Demo, new Dictionary<PhishingMailLevelOfDifficulty, byte>
      {
        { PhishingMailLevelOfDifficulty.Verdächtig, 0 },
        { PhishingMailLevelOfDifficulty.Ungewiss, 0 },
        { PhishingMailLevelOfDifficulty.Authentisch, 0 },
        { PhishingMailLevelOfDifficulty.TäuschendEcht, 8 }
      }
    },
    {
      Paket.Basis, new Dictionary<PhishingMailLevelOfDifficulty, byte>
      {
        { PhishingMailLevelOfDifficulty.Verdächtig, 1 },
        { PhishingMailLevelOfDifficulty.Ungewiss, 1 },
        { PhishingMailLevelOfDifficulty.Authentisch, 2 },
        { PhishingMailLevelOfDifficulty.TäuschendEcht, 4 }
      }
    },
    {
      Paket.Starter, new Dictionary<PhishingMailLevelOfDifficulty, byte>
      {
        { PhishingMailLevelOfDifficulty.Verdächtig, 2 },
        { PhishingMailLevelOfDifficulty.Ungewiss, 2 },
        { PhishingMailLevelOfDifficulty.Authentisch, 4 },
        { PhishingMailLevelOfDifficulty.TäuschendEcht, 4 }
      }
    },
    {
      Paket.Profi, new Dictionary<PhishingMailLevelOfDifficulty, byte>
      {
        { PhishingMailLevelOfDifficulty.Verdächtig, 4 },
        { PhishingMailLevelOfDifficulty.Ungewiss, 4 },
        { PhishingMailLevelOfDifficulty.Authentisch, 8 },
        { PhishingMailLevelOfDifficulty.TäuschendEcht, 8 }
      }
    },
    {
      Paket.Experte, new Dictionary<PhishingMailLevelOfDifficulty, byte>
      {
        { PhishingMailLevelOfDifficulty.Verdächtig, 7 },
        { PhishingMailLevelOfDifficulty.Ungewiss, 7 },
        { PhishingMailLevelOfDifficulty.Authentisch, 14 },
        { PhishingMailLevelOfDifficulty.TäuschendEcht, 24 }
      }
    }
  };
}