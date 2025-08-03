namespace Vispect
{
    partial class SetupForm
    {
        private System.ComponentModel.IContainer components = null;

        internal System.Windows.Forms.TabControl tabSetupControl;
        internal System.Windows.Forms.TabPage tabCamera;
        internal System.Windows.Forms.TabPage tabPath;
        internal System.Windows.Forms.TabPage tabCommunicator;
        internal System.Windows.Forms.TabPage tabInspection;
        internal System.Windows.Forms.TabPage tabSignalDelay;

        internal System.Windows.Forms.Label lblCameraType;
        internal System.Windows.Forms.ComboBox comboCameraType;
        internal System.Windows.Forms.Button btnApplyCamera;

        /// <summary>
        /// 디자이너에서 사용하는 리소스 정리
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.Text = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(500, 340);
            this.Font = new System.Drawing.Font("굴림", 9F);

            // TabControl
            tabSetupControl = new System.Windows.Forms.TabControl
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            // Pages
            tabCamera = new System.Windows.Forms.TabPage("Camera") { Name = "Camera" };
            tabPath = new System.Windows.Forms.TabPage("Path") { Name = "Path" };
            tabCommunicator = new System.Windows.Forms.TabPage("Communicator") { Name = "Communicator" };
            tabInspection = new System.Windows.Forms.TabPage("Inspection") { Name = "Inspection" };
            tabSignalDelay = new System.Windows.Forms.TabPage("Signal Delay") { Name = "SignalDelay" };

            tabSetupControl.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                tabCamera, tabPath, tabCommunicator, tabInspection, tabSignalDelay
            });

            // --- Camera Tab controls ---
            lblCameraType = new System.Windows.Forms.Label
            {
                Text = "카메라 종류:",
                Left = 20,
                Top = 30,
                AutoSize = true
            };

            comboCameraType = new System.Windows.Forms.ComboBox
            {
                Left = 120,
                Top = 26,
                Width = 200,
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            };

            btnApplyCamera = new System.Windows.Forms.Button
            {
                Text = "적용",
                Left = 200,
                Top = 80,
                Width = 100,
                Height = 30
            };
            btnApplyCamera.FlatStyle = System.Windows.Forms.FlatStyle.System;

            tabCamera.Controls.Add(lblCameraType);
            tabCamera.Controls.Add(comboCameraType);
            tabCamera.Controls.Add(btnApplyCamera);

            // Add TabControl to form
            this.Controls.Add(tabSetupControl);
        }
    }
}