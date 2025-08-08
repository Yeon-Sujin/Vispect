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
using Vispect.Setting;

namespace Vispect
{
    public partial class RunForm : DockContent
    {
        public RunForm()
        {
            InitializeComponent();
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.LiveMode = !Global.Inst.InspStage.LiveMode;

            //#17_WORKING_STATE#6 LIVE 상태 화면 표시
            if (Global.Inst.InspStage.LiveMode)
            {
                Global.Inst.InspStage.SetWorkingState(WorkingState.LIVE);

                //#13_SET_IMAGE_BUFFER#4 그랩시 이미지 버퍼를 먼저 설정하도록 변경
                Global.Inst.InspStage.CheckImageBuffer();
                Global.Inst.InspStage.Grab(0);
            }
            else
            {
                Global.Inst.InspStage.SetWorkingState(WorkingState.NONE);
            }
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.CheckImageBuffer();
            Global.Inst.InspStage.Grab(0);
        }

        private void btnInsp_Click(object sender, EventArgs e)
        {
            string serialID = $"{DateTime.Now:MM-dd HH:mm:ss}";
            Global.Inst.InspStage.InspectReady("LOT_NUMBER", serialID);

            if (SettingXml.Inst.CamType == Grab.CameraType.None)
            {
                bool cycleMode = SettingXml.Inst.CycleMode;
                Global.Inst.InspStage.CycleInspect(cycleMode);
            }
            else
            {
                Global.Inst.InspStage.StartAutoRun();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.StopCycle();
        }
    }
}