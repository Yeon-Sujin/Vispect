namespace Vispect
{
    partial class MainForm
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImageOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImageSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetupopen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInspect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuSetup,
            this.mnuInspect});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.mnuMain.Size = new System.Drawing.Size(1119, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImageOpen,
            this.mnuImageSave});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 22);
            this.mnuFile.Text = "File";
            // 
            // mnuImageOpen
            // 
            this.mnuImageOpen.Name = "mnuImageOpen";
            this.mnuImageOpen.Size = new System.Drawing.Size(180, 22);
            this.mnuImageOpen.Text = "Image Open";
            this.mnuImageOpen.Click += new System.EventHandler(this.mnuImageOpen_Click);
            // 
            // mnuImageSave
            // 
            this.mnuImageSave.Name = "mnuImageSave";
            this.mnuImageSave.Size = new System.Drawing.Size(180, 22);
            this.mnuImageSave.Text = "Image Save";
            // 
            // mnuSetup
            // 
            this.mnuSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetupopen});
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(50, 22);
            this.mnuSetup.Text = "Setup";
            // 
            // mnuSetupopen
            // 
            this.mnuSetupopen.Name = "mnuSetupopen";
            this.mnuSetupopen.Size = new System.Drawing.Size(180, 22);
            this.mnuSetupopen.Text = "Setup";
            this.mnuSetupopen.Click += new System.EventHandler(this.mnuSetupopen_Click);
            // 
            // mnuInspect
            // 
            this.mnuInspect.Name = "mnuInspect";
            this.mnuInspect.Size = new System.Drawing.Size(57, 22);
            this.mnuInspect.Text = "Inspect";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 570);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuImageOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuImageSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuSetupopen;
        private System.Windows.Forms.ToolStripMenuItem mnuInspect;
    }
}