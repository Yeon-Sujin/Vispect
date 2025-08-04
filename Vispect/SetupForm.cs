using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vispect.Core;
using Vispect.Grab;

namespace Vispect
{
    public enum SetupType
    {
        Camera,
        Path,
        Communicator,
        Inspection,
        SignalDelay
    }

    public partial class SetupForm : Form
    {
        Dictionary<string, TabPage> _allTabs = new Dictionary<string, TabPage>();

        public ComboBox CameraTypeCombo => comboCameraType;
        public Button ApplyCameraButton => btnApplyCamera;

        public SetupForm()
        {
            InitializeComponent();

            LoadOptionControl(SetupType.Camera);
            LoadOptionControl(SetupType.Path);
            LoadOptionControl(SetupType.Communicator);
            LoadOptionControl(SetupType.Inspection);
            LoadOptionControl(SetupType.SignalDelay);

            comboCameraType.Items.Clear();
            comboCameraType.Items.AddRange(new string[] { "None", "WebCam", "HikRobotCam" });
            comboCameraType.SelectedIndex = 0;

            btnApplyCamera.Click += btnApplyCamera_Click;
        }

        private void LoadOptionControl(SetupType setupType)
        {
            string tabName = setupType.ToString();

            foreach (TabPage tabPage in tabSetupControl.TabPages)
            {
                if (tabPage.Text == tabName)
                    return;
            }

            if (_allTabs.TryGetValue(tabName, out TabPage existing))
            {
                tabSetupControl.TabPages.Add(existing);
                return;
            }

            if (tabSetupControl.TabPages.ContainsKey(tabName))
            {
                var page = tabSetupControl.TabPages[tabName];
                _allTabs[tabName] = page;
                return;
            }

            var newTab = new TabPage(tabName) { Name = tabName, Text = tabName, Dock = DockStyle.Fill };
            tabSetupControl.TabPages.Add(newTab);
            _allTabs[tabName] = newTab;
        }

        private void btnApplyCamera_Click(object sender, EventArgs e)
        {
            string selected = comboCameraType.SelectedItem?.ToString() ?? "None";
            MessageBox.Show($"[Setup] Camera 적용: {selected}");

            if (selected != "None")
            {
                CameraType type;
                if (selected.Equals("WebCam", StringComparison.OrdinalIgnoreCase))
                    type = CameraType.WebCam;
                else if (selected.Equals("HikRobotCam", StringComparison.OrdinalIgnoreCase))
                    type = CameraType.HikRobotCam;
                else
                    return;

                Global.Inst.InspStage.SetCameraType(type);
            }
        }
    }
}
