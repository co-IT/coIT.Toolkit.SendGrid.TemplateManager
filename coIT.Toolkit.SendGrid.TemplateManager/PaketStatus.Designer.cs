namespace coIT.Toolkit.SendGrid.TemplateManager
{
    partial class PaketStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ctrl_PaketName = new Label();
            ctrl_Table = new TableLayoutPanel();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // ctrl_PaketName
            // 
            ctrl_PaketName.AutoSize = true;
            ctrl_PaketName.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctrl_PaketName.Location = new Point(61, 10);
            ctrl_PaketName.Margin = new Padding(2, 0, 2, 0);
            ctrl_PaketName.Name = "ctrl_PaketName";
            ctrl_PaketName.Size = new Size(90, 20);
            ctrl_PaketName.TabIndex = 10;
            ctrl_PaketName.Text = "PaketName";
            ctrl_PaketName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ctrl_Table
            // 
            ctrl_Table.AutoSize = true;
            ctrl_Table.BackColor = SystemColors.ControlLight;
            ctrl_Table.ColumnCount = 3;
            ctrl_Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ctrl_Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            ctrl_Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            ctrl_Table.Dock = DockStyle.Fill;
            ctrl_Table.Location = new Point(0, 0);
            ctrl_Table.Name = "ctrl_Table";
            ctrl_Table.RowCount = 1;
            ctrl_Table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            ctrl_Table.Size = new Size(206, 121);
            ctrl_Table.TabIndex = 12;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ctrl_PaketName);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ctrl_Table);
            splitContainer1.Size = new Size(206, 165);
            splitContainer1.SplitterDistance = 40;
            splitContainer1.TabIndex = 13;
            // 
            // PaketStatus
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(splitContainer1);
            Name = "PaketStatus";
            Size = new Size(206, 165);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label ctrl_PaketName;
        private TableLayoutPanel ctrl_Table;
        private SplitContainer splitContainer1;
    }
}
