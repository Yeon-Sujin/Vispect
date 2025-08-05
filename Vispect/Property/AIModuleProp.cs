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

            // ComboBox에 EngineType 문자열을 바인딩
            cbEngineSelect.DataSource =
                Enum.GetNames(typeof(EngineType))
                    .ToList();

            // 기본 선택값 지정(원한다면)
            cbEngineSelect.SelectedItem = EngineType.IAD.ToString();
        }

        private void cbEngineSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1) SelectedItem 이 null 이거나 문자열이 아니면 무시
            if (!(cbEngineSelect.SelectedItem is string selected)
                || string.IsNullOrWhiteSpace(selected))
            {
                return;
            }

            // 2) Enum.TryParse 로 안전하게 변환 시도
            if (Enum.TryParse<EngineType>(selected, ignoreCase: true, out var engine))
            {
                _engineType = engine;
            }
            else
            {
                // 3) 파싱 실패 시 안내
                MessageBox.Show(
                    $"알 수 없는 엔진 타입입니다: '{selected}'",
                    "경고",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
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

                default:
                    MessageBox.Show("지원되지 않는 AI 엔진 타입입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }

            if (result != null)
                Global.Inst.InspStage.UpdateDisplay(result);
            else
                MessageBox.Show("검사 결과가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
