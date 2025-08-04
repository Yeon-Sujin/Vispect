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
        private readonly System.Windows.Forms.Timer _liveTimer;

        public RunForm()
        {
            InitializeComponent();

            _liveTimer = new System.Windows.Forms.Timer();
            _liveTimer.Interval = 100;
            _liveTimer.Tick += LiveTimer_Tick;
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            if (!_isLive)
            {
                // 라이브 시작
                _isLive = true;
                btnLive.Text = "중지";
                _nextBufferIndex = 0;
                Debug.WriteLine("[RunForm] 라이브 시작");
                _liveTimer.Start();
            }
            else
            {
                // 라이브 중지
                _isLive = false;
                btnLive.Text = "동영상";
                Debug.WriteLine("[RunForm] 라이브 중지");
                _liveTimer.Stop();
            }
        }

        private void LiveTimer_Tick(object sender, EventArgs e)
        {
            if (!_isLive)
                return;

            TryGrab(_nextBufferIndex);
            _nextBufferIndex = (_nextBufferIndex + 1) % InspStage.MAX_GRAB_BUF;
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