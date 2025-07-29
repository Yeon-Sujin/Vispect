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
using static Vispect.Inspect.SaigeAI;

namespace Vispect.Property
{
    public partial class AIModuleProp : UserControl
    {
        private SaigeAI _saigeAI;
        private string _modelPath = string.Empty;
        private EngineType _engineType = EngineType.IAD;

        private enum EngineType { IAD, SEG, DET }

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
                MessageBox.Show("모델 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_saigeAI == null)
            {
                _saigeAI = Global.Inst.InspStage.AIModule;
            }

            _saigeAI.SelectedEngineType = (SaigeAI.EngineType)_engineType;
            _saigeAI.LoadEngine(_modelPath);
            MessageBox.Show("모델이 성공적으로 로드되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnInspAI_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = Global.Inst.InspStage.GetCurrentImage();

            if (_saigeAI == null)
            {
                MessageBox.Show("AI 모듈이 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bitmap result = null;

            switch (_engineType)
            {
                case EngineType.IAD:
                    if (_saigeAI.InspIAD(bitmap))
                        result = _saigeAI.GetResultImage();
                    break;

                case EngineType.SEG:
                    if (_saigeAI.InspSEG(bitmap))
                        result = _saigeAI.GetResultImage();
                    break;

                case EngineType.DET:
                    if (_saigeAI.InspDET(bitmap))
                        result = _saigeAI.GetResultImage();
                    break;
            }

            if (result != null)
                Global.Inst.InspStage.UpdateDisplay(result);
            else
                MessageBox.Show("검사 결과가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
