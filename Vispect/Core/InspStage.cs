using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using Vispect.Algorithm;
using Vispect.Grab;
using Vispect.Inspect;

namespace Vispect.Core
{
    public class InspStage : IDisposable
    {
        public static readonly int MAX_GRAB_BUF = 5;

        private ImageSpace _imageSpace = null;

        private GrabModel _grabManager = null;
        private CameraType _camType = CameraType.WebCam;

        SaigeAI _saigeAI;

        BlobAlgorithm _blobAlgorithm = null;
        private PreviewImage _previewImage = null;

        private int _lastBinLower = 0;
        private int _lastBinUpper = 255;
        private bool _lastBinInvert = false;
        private ShowBinaryMode _lastShowMode = ShowBinaryMode.ShowBinaryNone;

        public void CacheBinarySettings(int lower, int upper, bool invert, ShowBinaryMode showMode)
        {
            _lastBinLower = lower;
            _lastBinUpper = upper;
            _lastBinInvert = invert;
            _lastShowMode = showMode;
        }

        public (int lower, int upper, bool invert, ShowBinaryMode showMode) GetCachedBinarySettings()
        {
            return (_lastBinLower, _lastBinUpper, _lastBinInvert, _lastShowMode);
        }

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

        public BlobAlgorithm BlobAlgorithm
        { 
            get => _blobAlgorithm;
        }

        public PreviewImage PreView
        {
            get => _previewImage;
        }

        public bool Initialize()
        { 
            _imageSpace = new ImageSpace();

            _blobAlgorithm = new BlobAlgorithm();
            _previewImage = new PreviewImage();

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

            UpdateProperty();
        }

        private void UpdateProperty()
        {
            if (BlobAlgorithm is null)
                return;

            PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            if (propertiesForm is null)
                return;

            propertiesForm.UpdateProperty(BlobAlgorithm);
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

        public void TryInspection()
        {
            if (_blobAlgorithm is null)
                return;

            Mat srcImage = Global.Inst.InspStage.GetMat();
            _blobAlgorithm.SetInspData(srcImage);

            _blobAlgorithm.InspRect = new Rect(0, 0, srcImage.Width, srcImage.Height);

            if (_blobAlgorithm.DoInspect())
            {
                DisplayResult();
            }
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

        private void _multiGrab_TransferCompleted(object sender, object e)
        {
            int bufferIndex = (int)e;
            Console.WriteLine($"_multiGrab_TransferCompleted {bufferIndex}");

            _imageSpace.Split(bufferIndex);

            DisplayGrabImage(bufferIndex);
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
