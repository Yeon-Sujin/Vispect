using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Vispect.Algorithm;
using Vispect.Grab;
using Vispect.Inspect;
using Vispect.Teach;

namespace Vispect.Core
{
    public class InspStage : IDisposable
    {
        public static readonly int MAX_GRAB_BUF = 5;

        private ImageSpace _imageSpace = null;

        private GrabModel _grabManager = null;
        private CameraType _camType = CameraType.WebCam;

        SaigeAI _saigeAI;

        BlobAlgorithm _blobAlgorithm = null; //확인필요...

        private PreviewImage _previewImage = null;

        private int _lastBinLower = 0; //~
        private int _lastBinUpper = 255;
        private bool _lastBinInvert = false;
        private ShowBinaryMode _lastShowMode = ShowBinaryMode.ShowBinaryNone; //~ 확인하기

        private Model _model = null;

        private InspWindow _selectedInspWindow = null;

        public void CacheBinarySettings(int lower, int upper, bool invert, ShowBinaryMode showMode)
        {
            _lastBinLower = lower;
            _lastBinUpper = upper;
            _lastBinInvert = invert;
            _lastShowMode = showMode;
        } //확인하기

        //public (int lower, int upper, bool invert, ShowBinaryMode showMode) GetCachedBinarySettings()
        //{
        //    return (_lastBinLower, _lastBinUpper, _lastBinInvert, _lastShowMode);
        //}

        public InspStage() { }

        public ImageSpace ImageSpace
        {
            get => _imageSpace;
        }

        public SaigeAI AIModule
        { 
            get { 
                if (_saigeAI is null)
                    _saigeAI = new SaigeAI();
                return _saigeAI;
            }
        }

        //public BlobAlgorithm BlobAlgorithm
        //{ 
        //    get => _blobAlgorithm;
        //}

        public PreviewImage PreView
        {
            get => _previewImage;
        }

        public Model CurModel
        {
            get => _model;
        }

        public bool LiveMode { get; set; } = false;

        public bool Initialize()
        { 
            _imageSpace = new ImageSpace();

            _blobAlgorithm = new BlobAlgorithm(); //확인필요
            _previewImage = new PreviewImage();

            _model = new Model();

            switch (_camType)
            {
                case CameraType.WebCam:
                    {
                        _grabManager = new WebCam();
                        break;
                    }
                case CameraType.HikRobotCam:
                    {
                        _grabManager = new HikRobotCam();
                        break;
                    }
            }

            if (_grabManager != null && _grabManager.InitGrab() == true)
            {
                _grabManager.TransferCompleted += _multiGrab_TransferCompleted;

                InitModelGrab(MAX_GRAB_BUF);
            }

            return true;
        }

        public void InitModelGrab(int bufferCount)
        {
            if (_grabManager == null)
                return;

            int pixelBpp = 8;
            _grabManager.GetPixelBpp(out pixelBpp);

            int inspectionWidth;
            int inspectionHeight;
            int inspectionStride;
            _grabManager.GetResolution(out  inspectionWidth, out inspectionHeight, out inspectionStride);

            if (_imageSpace != null)
            { 
                _imageSpace.SetImageInfo(pixelBpp, inspectionWidth, inspectionHeight, inspectionStride);
            }

            SetBuffer(bufferCount);

            //UpdateProperty();

        }

        private void UpdateProperty(InspWindow inspWindow)
        {
            //if (BlobAlgorithm is null)
            //    return;

            //PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            //if (propertiesForm is null)
            //    return;

            //propertiesForm.UpdateProperty(BlobAlgorithm);

            if (inspWindow is null)
                return;

            PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            if (propertiesForm is null)
                return;

            propertiesForm.UpdateProperty(inspWindow);
        }

        public void SetBuffer(int bufferCount)
        { 
            if (_grabManager == null)
                return;

            if (_imageSpace.BufferCount == bufferCount)
                return;

            _imageSpace.InitImageSpace(bufferCount);
            _grabManager.InitBuffer(bufferCount);

            for (int i = 0; i < bufferCount; i++)
            {
                _grabManager.SetBuffer(
                    _imageSpace.GetInspectionBuffer(i),
                    _imageSpace.GetInspectionBufferPtr(i),
                    _imageSpace.GetInspectionBufferHandle(i),
                    i);
            }
        }

        public void TryInspection(InspWindow inspWindow = null)
        {
            //if (_blobAlgorithm is null)
            //    return;

            //Mat srcImage = Global.Inst.InspStage.GetMat();
            //_blobAlgorithm.SetInspData(srcImage);

            //_blobAlgorithm.InspRect = new Rect(0, 0, srcImage.Width, srcImage.Height);

            //if (_blobAlgorithm.DoInspect())
            //{
            //    DisplayResult();
            //}

            if (inspWindow is null)
            {
                if (_selectedInspWindow is null)
                    return;

                inspWindow = _selectedInspWindow;
            }

            UpdateDiagramEntity();

            List<DrawInspectInfo> totalArea = new List<DrawInspectInfo>();

            Rect windowArea = inspWindow.WindowArea;

            foreach (var inspAlgo in inspWindow.AlgorithmList)
            {
                //검사 영역 초기화
                inspAlgo.TeachRect = windowArea;
                inspAlgo.InspRect = windowArea;

                InspectType inspType = inspAlgo.InspectType;

                switch (inspType)
                {
                    case InspectType.InspBinary:
                        {
                            BlobAlgorithm blobAlgo = (BlobAlgorithm)inspAlgo;

                            Mat srcImage = Global.Inst.InspStage.GetMat();
                            blobAlgo.SetInspData(srcImage);

                            if (blobAlgo.DoInspect())
                            {
                                List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                                int resultCnt = blobAlgo.GetResultRect(out resultArea);
                                if (resultCnt > 0)
                                {
                                    totalArea.AddRange(resultArea);
                                }
                            }

                            break;
                        }
                }

                if (inspAlgo.DoInspect())
                {
                    List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                    int resultCnt = inspAlgo.GetResultRect(out resultArea);
                    if (resultCnt > 0)
                    {
                        totalArea.AddRange(resultArea);
                    }
                }
            }

            if (totalArea.Count > 0)
            {
                //찾은 위치를 이미지상에서 표시
                var cameraForm = MainForm.GetDockForm<CameraForm>();
                if (cameraForm != null)
                {
                    cameraForm.AddRect(totalArea);
                }
            }
        }

        public void SelectInspWindow(InspWindow inspWindow)
        {
            _selectedInspWindow = inspWindow;

            var propForm = MainForm.GetDockForm<PropertiesForm>();
            if (propForm != null)
            {
                if (inspWindow is null)
                {
                    propForm.ResetProperty();
                    return;
                }

                //속성창을 현재 선택된 ROI에 대한 것으로 변경
                propForm.ShowProperty(inspWindow);
            }

            UpdateProperty(inspWindow);

            Global.Inst.InspStage.PreView.SetInspWindow(inspWindow);
        }

        //ImageViwer에서 ROI를 추가하여, InspWindow생성하는 함수
        public void AddInspWindow(InspWindowType windowType, Rect rect)
        {
            InspWindow inspWindow = _model.AddInspWindow(windowType);
            if (inspWindow is null)
                return;

            inspWindow.WindowArea = rect;
            inspWindow.IsTeach = false;
            UpdateProperty(inspWindow);
            UpdateDiagramEntity();

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.SelectDiagramEntity(inspWindow);
                SelectInspWindow(inspWindow);
            }
        }

        public bool AddInspWindow(InspWindow sourceWindow, OpenCvSharp.Point offset)
        {
            InspWindow cloneWindow = sourceWindow.Clone(offset);
            if (cloneWindow is null)
                return false;

            if (!_model.AddInspWindow(cloneWindow))
                return false;

            UpdateProperty(cloneWindow);
            UpdateDiagramEntity();

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.SelectDiagramEntity(cloneWindow);
                SelectInspWindow(cloneWindow);
            }

            return true;
        }


        //입력된 윈도우 이동
        public void MoveInspWindow(InspWindow inspWindow, OpenCvSharp.Point offset)
        {
            if (inspWindow == null)
                return;

            inspWindow.OffsetMove(offset);
            UpdateProperty(inspWindow);
        }

        //#MODEL#10 기존 ROI 수정되었을때, 그 정보를 InspWindow에 반영
        public void ModifyInspWindow(InspWindow inspWindow, Rect rect)
        {
            if (inspWindow == null)
                return;

            inspWindow.WindowArea = rect;
            inspWindow.IsTeach = false;

            UpdateProperty(inspWindow);
        }

        //#MODEL#11 InspWindow 삭제하기
        public void DelInspWindow(InspWindow inspWindow)
        {
            _model.DelInspWindow(inspWindow);
            UpdateDiagramEntity();
        }


        public void DelInspWindow(List<InspWindow> inspWindowList)
        {
            _model.DelInspWindowList(inspWindowList);
            UpdateDiagramEntity();
        }

        private bool DisplayResult()
        {
            if (_blobAlgorithm is null)
                return false;

            List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
            int resultCnt = _blobAlgorithm.GetResultRect(out resultArea);
            if (resultCnt > 0)
            {
                //찾은 위치를 이미지상에서 표시
                var cameraForm = MainForm.GetDockForm<CameraForm>();
                if (cameraForm != null)
                {
                    cameraForm.ResetDisplay();
                    cameraForm.AddRect(resultArea);
                }
            }

            return true;
        }

        public void Grab(int bufferIndex)
        {
            if (_grabManager == null)
                return;

            _grabManager.Grab(bufferIndex, true);
        }

        private async void _multiGrab_TransferCompleted(object sender, object e)
        {
            int bufferIndex = (int)e;
            Console.WriteLine($"_multiGrab_TransferCompleted {bufferIndex}");

            _imageSpace.Split(bufferIndex);

            DisplayGrabImage(bufferIndex);

            if (_previewImage != null)
            {
                Bitmap bitmap = ImageSpace.GetBitmap(0);
                _previewImage.SetImage(BitmapConverter.ToMat(bitmap));
            }

            if (LiveMode)
            {
                await Task.Delay(100);  // 비동기 대기
                _grabManager.Grab(bufferIndex, true);  // 다음 촬영 시작
            }
        }

        private void DisplayGrabImage(int bufferIndex)
        { 
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDisplay();
            }
        }

        public void UpdateDisplay(Bitmap bitmap)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            { 
                cameraForm.UpdateDisplay(bitmap);
            }
        }

        public Bitmap GetCurrentImage()
        { 
            Bitmap bitmap = null;
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                bitmap = cameraForm.GetDisplayImage();
            }

            return bitmap;
        }

        public Bitmap GetBitmap(int bufferIndex = -1)
        { 
            if (Global.Inst.InspStage.ImageSpace is null)
                return null;

            return Global.Inst.InspStage.ImageSpace.GetBitmap();
        }

        public Mat GetMat()
        { 
            return Global.Inst.InspStage.ImageSpace.GetMat();
        }

        //변경된 모델 정보 갱신하여, ImageViewer와 모델트리에 반영
        public void UpdateDiagramEntity()
        {
            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDiagramEntity();
            }

            ModelTreeForm modelTreeForm = MainForm.GetDockForm<ModelTreeForm>();
            if (modelTreeForm != null)
            {
                modelTreeForm.UpdateDiagramEntity();
            }
        }

        public void RedrawMainView()
        {
            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateImageViewer();
            }
        }

        public void SetCameraType(CameraType newType)
        {
            if (_camType == newType)
                return;

            if (_grabManager != null)
            {
                try
                {
                    _grabManager.TransferCompleted -= _multiGrab_TransferCompleted;
                }
                catch { }
                _grabManager = null;
            }

            _camType = newType;

            switch (_camType)
            {
                case CameraType.WebCam:
                    _grabManager = new WebCam();
                    break;
                case CameraType.HikRobotCam:
                    _grabManager = new HikRobotCam();
                    break;
                default:
                    _grabManager = new WebCam();
                    break;
            }

            bool initOk = false;
            if (_grabManager != null)
            {
                initOk = _grabManager.InitGrab();
                if (!initOk)
                {
                    MessageBox.Show($"카메라 초기화 실패: {_camType}. 기본 WebCam으로 복구합니다.");
                    _camType = CameraType.WebCam;
                    _grabManager = new WebCam();
                    initOk = _grabManager.InitGrab();
                }

                if (initOk)
                {
                    _grabManager.TransferCompleted += _multiGrab_TransferCompleted;
                    InitModelGrab(MAX_GRAB_BUF);
                }
                else
                {
                    _grabManager = null;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        { 
            if (!disposed)
            {
                if (disposing)
                { 
                    
                }

                disposed = true;
            }
        }

        public void Dispose()
        { 
            Dispose(true);
        }
    }
}
