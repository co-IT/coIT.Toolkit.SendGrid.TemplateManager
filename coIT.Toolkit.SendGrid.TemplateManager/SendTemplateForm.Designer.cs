namespace coIT.Toolkit.SendGrid.TemplateManager
{
  partial class SendTemplateForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      label1 = new Label();
      tbxEmail = new TextBox();
      btnSend = new Button();
      SuspendLayout();
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(10, 15);
      label1.Name = "label1";
      label1.Size = new Size(44, 15);
      label1.TabIndex = 0;
      label1.Text = "E-Mail:";
      // 
      // tbxEmail
      // 
      tbxEmail.Location = new Point(57, 12);
      tbxEmail.Name = "tbxEmail";
      tbxEmail.Size = new Size(330, 23);
      tbxEmail.TabIndex = 1;
      // 
      // btnSend
      // 
      btnSend.Location = new Point(12, 51);
      btnSend.Name = "btnSend";
      btnSend.Size = new Size(375, 23);
      btnSend.TabIndex = 2;
      btnSend.Text = "Feuer frei!";
      btnSend.UseVisualStyleBackColor = true;
      btnSend.Click += btnSend_Click;
      // 
      // SendTemplateForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(404, 83);
      Controls.Add(btnSend);
      Controls.Add(tbxEmail);
      Controls.Add(label1);
      Name = "SendTemplateForm";
      Text = "SendTemplateForm";
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox tbxEmail;
    private Button btnSend;
  }
}