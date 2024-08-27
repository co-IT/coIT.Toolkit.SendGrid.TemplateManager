using System.Net.Mail;

namespace coIT.Toolkit.SendGrid.TemplateManager;

public partial class SendTemplateForm : Form
{
  public SendTemplateForm()
  {
    InitializeComponent();
  }

  public MailAddress Email { get; set; }

  private void btnSend_Click(object sender, EventArgs e)
  {
    if (MailAddress.TryCreate(tbxEmail.Text, out var email))
    {
      Email = email;
      DialogResult = DialogResult.OK;
      Close();
    }
    else
    {
      MessageBox.Show("E-Mail-Adresse ungültig.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
}
