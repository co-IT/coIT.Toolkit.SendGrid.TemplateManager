namespace coIT.Toolkit.SendGrid.TemplateManager
{
    partial class PaketUebersichtForm
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
            ctrl_PaketeLayout = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // ctrl_PaketeLayout
            // 
            ctrl_PaketeLayout.AutoScroll = true;
            ctrl_PaketeLayout.Dock = DockStyle.Fill;
            ctrl_PaketeLayout.Location = new Point(0, 0);
            ctrl_PaketeLayout.Name = "ctrl_PaketeLayout";
            ctrl_PaketeLayout.Size = new Size(800, 450);
            ctrl_PaketeLayout.TabIndex = 0;
            // 
            // PaketUebersichtForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ctrl_PaketeLayout);
            Name = "PaketUebersichtForm";
            Text = "PaketUebersicht";
            Load += PaketUebersicht_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel ctrl_PaketeLayout;
    }
}