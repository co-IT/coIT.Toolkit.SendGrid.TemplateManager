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
      btnSend = new Button();
      propertyGrid = new PropertyGrid();
      SuspendLayout();
      // 
      // btnSend
      // 
      btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      btnSend.Location = new Point(12, 777);
      btnSend.Name = "btnSend";
      btnSend.Size = new Size(408, 23);
      btnSend.TabIndex = 2;
      btnSend.Text = "Feuer frei!";
      btnSend.UseVisualStyleBackColor = true;
      btnSend.Click += btnSend_Click;
      // 
      // propertyGrid
      // 
      propertyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      propertyGrid.Location = new Point(12, 12);
      propertyGrid.Name = "propertyGrid";
      propertyGrid.Size = new Size(1097, 758);
      propertyGrid.TabIndex = 3;
      // 
      // SendTemplateForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1121, 811);
      Controls.Add(propertyGrid);
      Controls.Add(btnSend);
      Name = "SendTemplateForm";
      Text = "SendTemplateForm";
      Load += SendTemplateForm_Load;
      ResumeLayout(false);
    }

    #endregion
    private Button btnSend;
    private PropertyGrid propertyGrid;
  }
}