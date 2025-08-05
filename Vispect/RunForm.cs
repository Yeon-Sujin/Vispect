using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Vispect.Core;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading.Tasks;

namespace Vispect
{
    public partial class RunForm : DockContent
    {
        private bool _isLive = false;
        private int _nextBufferIndex = 0;
        private readonly object _grabLock = new object();

        public RunForm()
        {
            InitializeComponent();

        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.LiveMode = !Global.Inst.InspStage.LiveMode;

            if (Global.Inst.InspStage.LiveMode)
            {
                Global.Inst.InspStage.Grab(0);
            }
        }

        private void TryGrab(int bufferIndex)
        {
            if(Global.Inst == null || Global.Inst.InspStage == null)
                return;

            lock (_grabLock)
            {
                try
                {
                    Debug.WriteLine($"[RunForm] Grab 호출, 버퍼: {bufferIndex} at {DateTime.Now:HH:mm:ss.fff}");
                    Global.Inst.InspStage.Grab(bufferIndex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[RunForm] Grab 예외: {ex.Message}");
                }
            }
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[RunForm] 수동 Grab 0");
            TryGrab(0);
        }

        private void btnInsp_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("[RunForm] 검사 호출");
            Global.Inst.InspStage.TryInspection();
        }
    }
}