using System.Diagnostics;

namespace coIT.Toolkit.SendGrid.TemplateManager;

internal class Utils
{
  public static void OpenInBrowser(string url)
  {
    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
  }
}
