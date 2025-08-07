namespace Vispect
{
    partial class RunForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunForm));
            this.btnGrab = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnInsp = new System.Windows.Forms.Button();
            this.runImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // btnGrab
            // 
            this.btnGrab.ImageIndex = 0;
            this.btnGrab.ImageList = this.runImageList;
            this.btnGrab.Location = new System.Drawing.Point(13, 26);
            this.btnGrab.Margin = new System.Windows.Forms.Padding(4);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(150, 100);
            this.btnGrab.TabIndex = 1;
            this.btnGrab.UseVisualStyleBackColor = true;
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // btnLive
            // 
            this.btnLive.ImageIndex = 1;
            this.btnLive.ImageList = this.runImageList;
            this.btnLive.Location = new System.Drawing.Point(189, 26);
            this.btnLive.Margin = new System.Windows.Forms.Padding(4);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(150, 100);
            this.btnLive.TabIndex = 2;
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnInsp
            // 
            this.btnInsp.ImageIndex = 2;
            this.btnInsp.ImageList = this.runImageList;
            this.btnInsp.Location = new System.Drawing.Point(367, 26);
            this.btnInsp.Margin = new System.Windows.Forms.Padding(4);
            this.btnInsp.Name = "btnInsp";
            this.btnInsp.Size = new System.Drawing.Size(150, 100);
            this.btnInsp.TabIndex = 3;
            this.btnInsp.UseVisualStyleBackColor = true;
            this.btnInsp.Click += new System.EventHandler(this.btnInsp_Click);
            // 
            // runImageList
            // 
            this.runImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("runImageList.ImageStream")));
            this.runImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.runImageList.Images.SetKeyName(0, "스크린샷 2025-08-07 134930.png");
            this.runImageList.Images.SetKeyName(1, "스크린샷 2025-08-07 135012.png");
            this.runImageList.Images.SetKeyName(2, "스크린샷 2025-08-07 135118.png");
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 156);
            this.Controls.Add(this.btnInsp);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnGrab);
            this.Name = "RunForm";
            this.Text = "RunForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGrab;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnInsp;
        private System.Windows.Forms.ImageList runImageList;
    }
}