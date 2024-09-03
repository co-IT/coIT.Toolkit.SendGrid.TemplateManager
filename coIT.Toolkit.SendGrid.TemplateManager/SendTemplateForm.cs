using System.Net.Mail;
using CSharpFunctionalExtensions;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class SendTemplateForm : Form
{
  private readonly ManagedTemplate _template;

  public SendTemplateForm(ManagedTemplate template)
  {
    InitializeComponent();
    _template = template;
  }

  public SendOptions SendOptions { get; set; }

  private void btnSend_Click(object sender, EventArgs e)
  {
    var sendOptions = (SendOptions)propertyGrid.SelectedObject;

    var result = ValidateSendOptions(sendOptions);

    if (result.IsFailure)
    {
      MessageBox.Show(result.Error, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
      return;
    }

    SendOptions = sendOptions;
    DialogResult = DialogResult.OK;
    Close();
  }

  private Result ValidateSendOptions(SendOptions sendOptions)
  {
    return !MailAddress.TryCreate(sendOptions.RecipientEmail, out _)
      ? Result.Failure("Die E-Mail-Adresse ist ungültig.")
      : Result.Success();
  }

  private void SendTemplateForm_Load(object sender, EventArgs e)
  {
    var sendOptions = new SendOptions
    {
      SenderName = _template.Absender?.Name ?? string.Empty,
      SenderEmail = _template.Absender?.Adresse ?? string.Empty,
    };

    propertyGrid.SelectedObject = sendOptions;
  }
}
