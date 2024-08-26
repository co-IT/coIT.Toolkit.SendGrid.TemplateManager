using System.ComponentModel;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public static class WinFormsControlExtensions
{
  public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
  {
    if (obj.InvokeRequired)
      obj.Invoke(action, null);
    else
      action();
  }
}
