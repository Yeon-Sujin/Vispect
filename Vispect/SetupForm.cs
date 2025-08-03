using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            btnApplyCamera.Click += BtnApplyCamera_Click;
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

        private void BtnApplyCamera_Click(object sender, EventArgs e)
        {
            string selected = comboCameraType.SelectedItem?.ToString() ?? "None";
            MessageBox.Show($"[Setup] Camera 적용: {selected}");

            if (selected != "None")
            {
                // 예: 카메라 타입 변수 설정 (CameraType enum이 정의되어 있다고 가정)
                // Global.Inst.InspStage.SetCameraType(...) 같은 메서드가 있으면 호출
            }
        }
    }
}
