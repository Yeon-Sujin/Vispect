using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vispect.Core;
using Vispect.Inspect;

namespace Vispect.Property
{
    public partial class AIModuleProp : UserControl
    {
        SaigeAI _saigeAI;
        string _modelPath = string.Empty;

        //private enum EngineType { IAD, SEG, DET }
        //private EngineType _engineType = EngineType.IAD;

        public AIModuleProp()
        {
            InitializeComponent();
            cbEngineSelect.SelectedIndex = 0;
        }

        private void cbEngineSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbEngineSelect.SelectedItem.ToString())
            {
                case "IAD":
                    _engineType = EngineType.IAD;
                    break;
                case "SEG":
                    _engineType = EngineType.SEG;
                    break;
                case "DET":
                    _engineType = EngineType.DET;
                    break;
            }
        }

        private void btnSelAIModel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "AI 모델 파일 선택";
                openFileDialog.Filter = "AI Files|*.*;";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                { 
                    _modelPath = openFileDialog.FileName;
                    txtAIModelPath.Text = _modelPath;
                }
            }
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_modelPath))
            { 
                MessageBox.Show("모델 파일을 선택해주세요,", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_saigeAI == null)
            {
                _saigeAI = Global.Inst.InspStage.AIModule;
            }

            switch (_engineType)
            {
                case EngineType.IAD:
                    _saigeAI.LoadEngine(_modelPath);
                    MessageBox.Show("IAD 모델이 성공적으로 로드되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case EngineType.SEG:
                    MessageBox.Show("SEG 모델 경로가 설정되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case EngineType.DET:
                    MessageBox.Show("DET 모델 경로가 설정되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

        }

        private void btnInspAI_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = Global.Inst.InspStage.GetCurrentImage();

            switch (_engineType)
            {
                case EngineType.IAD:
                    if (_saigeAI == null)
                    {
                        MessageBox.Show("AI 모듈이 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _saigeAI.InspIAD(bitmap);
                    Bitmap iadResult = _saigeAI.GetResultImage();
                    Global.Inst.InspStage.UpdateDisplay(iadResult);
                    break;

                case EngineType.SEG:
                    RunSeg(bitmap, _modelPath);
                    break;

                case EngineType.DET:
                    RunDetection(bitmap, _modelPath);
                    break;
            }
        }
    }
}
