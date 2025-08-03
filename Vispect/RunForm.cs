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
using WeifenLuo.WinFormsUI.Docking;

namespace Vispect
{
    public partial class RunForm : DockContent
    {
        private bool _isInspecting = false;
        private System.Windows.Forms.Timer _inspectTimer;
        private int _nextBufferIndex = 0;

        public RunForm()
        {
            InitializeComponent();

            _inspectTimer = new System.Windows.Forms.Timer
            {
                Interval = 100
            };
            _inspectTimer.Tick += InspectTimer_Tick;

            btnInsp.Click += btnInsp_Click;
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.Grab(0);
        }

        private void btnInsp_Click(object sender, EventArgs e)
        {
            if (!_isInspecting)
            {
                _isInspecting = true;
                btnInsp.Text = "중지";
                _nextBufferIndex = 0;
                _inspectTimer.Start();
            }
            else
            {
                _isInspecting = false;
                btnInsp.Text = "검사";
                _inspectTimer.Stop();
            }
        }
        private void InspectTimer_Tick(object sender, EventArgs e)
        {
            Global.Inst.InspStage.Grab(_nextBufferIndex);

            _nextBufferIndex = (_nextBufferIndex + 1) % 5;
        }
    }
}
