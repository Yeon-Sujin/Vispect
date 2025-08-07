using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vispect.Algorithm;
using Vispect.Grab;
using Vispect.Inspect;
using Vispect.Setting;
using Vispect.Teach;
using Vispect.Util;

namespace Vispect.Core
{
    public class InspStage : IDisposable
    {
        public static readonly int MAX_GRAB_BUF = 5;

        private ImageSpace _imageSpace = null;

        private GrabModel _grabManager = null;
        private CameraType _camType = CameraType.WebCam;

        SaigeAI _saigeAI;

        private PreviewImage _previewImage = null;

        private Model _model = null;

        private InspWindow _selectedInspWindow = null;

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

        public PreviewImage PreView
        {
            get => _previewImage;
        }

        public Model CurModel
        {
            get => _model;
        }

        public bool LiveMode { get; set; } = false;

        public int SelBufferIndex { get; set; } = 0;
        public eImageChannel SelImageChannel { get; set; } = eImageChannel.Gray;

        public bool Initialize()
        {
            SLogger.Write("InspStage 초기화!");
            _imageSpace = new ImageSpace();

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

        private void LoadSetting()
        {
            //카메라 설정 타입 얻기
            _camType = SettingXml.Inst.CamType;
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

            //_grabManager.SetExposureTime(25000);
        }

        public void SetImageBuffer(string filePath)
        {
            SLogger.Write($"Load Image : {filePath}");

            Mat matImage = Cv2.ImRead(filePath);

            int pixelBpp = 8;
            int imageWidth;
            int imageHeight;
            int imageStride;

            if (matImage.Type() == MatType.CV_8UC3)
                pixelBpp = 24;

            imageWidth = (matImage.Width + 3) / 4 * 4;
            imageHeight = matImage.Height;

            // 4바이트 정렬된 새로운 Mat 생성
            Mat alignedMat = new Mat();
            Cv2.CopyMakeBorder(matImage, alignedMat, 0, 0, 0, imageWidth - matImage.Width, BorderTypes.Constant, Scalar.Black);

            imageStride = imageWidth * matImage.ElemSize();

            if (_imageSpace != null)
            {
                _imageSpace.SetImageInfo(pixelBpp, imageWidth, imageHeight, imageStride);
            }

            SetBuffer(1);

            int bufferIndex = 0;

            // Mat의 데이터를 byte 배열로 복사
            int bufSize = (int)(alignedMat.Total() * alignedMat.ElemSize());
            Marshal.Copy(alignedMat.Data, ImageSpace.GetInspectionBuffer(bufferIndex), 0, bufSize);

            _imageSpace.Split(bufferIndex);

            DisplayGrabImage(bufferIndex);

            if (_previewImage != null)
            {
                Bitmap bitmap = ImageSpace.GetBitmap(0);
                _previewImage.SetImage(BitmapConverter.ToMat(bitmap));
            }
        }

        public void CheckImageBuffer()
        {
            if (_grabManager != null && SettingXml.Inst.CamType != CameraType.None)
            {
                int imageWidth;
                int imageHeight;
                int imageStride;
                _grabManager.GetResolution(out imageWidth, out imageHeight, out imageStride);

                if (_imageSpace.ImageSize.Width != imageWidth || _imageSpace.ImageSize.Height != imageHeight)
                {
                    int pixelBpp = 8;
                    _grabManager.GetPixelBpp(out pixelBpp);

                    _imageSpace.SetImageInfo(pixelBpp, imageWidth, imageHeight, imageStride);
                    SetBuffer(_imageSpace.BufferCount);
                }
            }
        }

        private void UpdateProperty(InspWindow inspWindow)
        {
            if (inspWindow is null)
                return;

            PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            if (propertiesForm is null)
                return;

            propertiesForm.UpdateProperty(inspWindow);
        }

        public void UpdateTeachingImage(int index)
        {
            if (_selectedInspWindow is null)
                return;

            SetTeachingImage(_selectedInspWindow, index);
        }

        public void DelTeachingImage(int index)
        {
            if (_selectedInspWindow is null)
                return;

            InspWindow inspWindow = _selectedInspWindow;

            inspWindow.DelWindowImage(index);

            MatchAlgorithm matchAlgo = (MatchAlgorithm)inspWindow.FindInspAlgorithm(InspectType.InspMatch);
            if (matchAlgo != null)
            {
                UpdateProperty(inspWindow);
            }
        }

        public void SetTeachingImage(InspWindow inspWindow, int index = -1)
        {
            if (inspWindow is null)
                return;

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm is null)
                return;

            Mat curImage = cameraForm.GetDisplayMat();
            if (curImage is null)
                return;

            if (inspWindow.WindowArea.Right >= curImage.Width ||
                inspWindow.WindowArea.Bottom >= curImage.Height)
            {
                SLogger.Write("ROI 영역이 잘못되었습니다!");
                return;
            }

            Mat windowImage = curImage[inspWindow.WindowArea];

            if (index < 0)
                inspWindow.AddWindowImage(windowImage);
            else
                inspWindow.SetWindowImage(windowImage, index);

            inspWindow.IsPatternLearn = false;

            MatchAlgorithm matchAlgo = (MatchAlgorithm)inspWindow.FindInspAlgorithm(InspectType.InspMatch);
            if (matchAlgo != null)
            {
                UpdateProperty(inspWindow);
            }
        }

        public void SetBuffer(int bufferCount)
        {
            _imageSpace.InitImageSpace(bufferCount);

            if (_grabManager != null)
            {
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

            SLogger.Write("버퍼 초기화 성공!");
        }

        public void TryInspection(InspWindow inspWindow = null)
        {
            if (inspWindow is null)
            {
                if (_selectedInspWindow is null)
                    return;

                inspWindow = _selectedInspWindow;
            }

            UpdateDiagramEntity();

            inspWindow.ResetInspResult();

            List<DrawInspectInfo> totalArea = new List<DrawInspectInfo>();

            Rect windowArea = inspWindow.WindowArea;

            foreach (var inspAlgo in inspWindow.AlgorithmList)
            {
                if (!inspAlgo.IsUse)
                    continue;

                //검사 영역 초기화
                inspAlgo.TeachRect = windowArea;
                inspAlgo.InspRect = windowArea;

                Mat srcImage = Global.Inst.InspStage.GetMat();
                inspAlgo.SetInspData(srcImage);

                if (!inspAlgo.DoInspect())
                    continue;

                List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                int resultCnt = inspAlgo.GetResultRect(out resultArea);
                if (resultCnt > 0)
                {
                    totalArea.AddRange(resultArea);
                }

                InspectType inspType = inspAlgo.InspectType;

                string resultInfo = string.Join("\r\n", inspAlgo.ResultString);

                InspResult inspResult = new InspResult
                {
                    ObjectID = inspWindow.UID,
                    InspType = inspAlgo.InspectType,
                    IsDefect = inspAlgo.IsDefect,
                    ResultInfos = resultInfo
                };

                switch (inspType)
                {
                    case InspectType.InspMatch:
                        {
                            MatchAlgorithm matchAlgo = inspAlgo as MatchAlgorithm;
                            inspResult.ResultValue = $"{matchAlgo.OutScore}";
                            break;
                        }
                    case InspectType.InspBinary:
                        {
                            BlobAlgorithm blobAlgo = (BlobAlgorithm)inspAlgo;
                            int min = blobAlgo.BlobFilters[blobAlgo.FILTER_COUNT].min;
                            int max = blobAlgo.BlobFilters[blobAlgo.FILTER_COUNT].max;
                            inspResult.ResultValue = $"{blobAlgo.OutBlobCount}/{min}~{max}";
                            break;
                        }
                }

                inspWindow.AddInspResult(inspResult);
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

            ResultForm resultForm = MainForm.GetDockForm<ResultForm>();
            if (resultForm != null)
            {
                resultForm.AddWindowResult(inspWindow);
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

            SetTeachingImage(inspWindow);
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

        public void Grab(int bufferIndex)
        {
            if (_grabManager == null)
                return;

            _grabManager.Grab(bufferIndex, true);
        }

        private async void _multiGrab_TransferCompleted(object sender, object e)
        {
            int bufferIndex = (int)e;
            SLogger.Write($"TransferCompleted {bufferIndex}");

            _imageSpace.Split(bufferIndex);

            DisplayGrabImage(bufferIndex);

            if (_previewImage != null)
            {
                Bitmap bitmap = ImageSpace.GetBitmap(0);
                _previewImage.SetImage(BitmapConverter.ToMat(bitmap));
            }

            if (LiveMode)
            {
                SLogger.Write("Grab");
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

        public Mat GetMat(int bufferIndex = -1, eImageChannel imageChannel = eImageChannel.None)
        {
            if (bufferIndex >= 0)
                SelBufferIndex = bufferIndex;

            //#BINARY FILTER#14 채널 정보가 유지되도록, eImageChannel.None 타입을 추가
            if (imageChannel != eImageChannel.None)
                SelImageChannel = imageChannel;

            return Global.Inst.InspStage.ImageSpace.GetMat(SelBufferIndex, SelImageChannel);
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

        //#12_MODEL SAVE#4 Mainform에서 호출되는 모델 열기와 저장 함수        
        public bool LoadModel(string filePath)
        {
            SLogger.Write($"모델 로딩:{filePath}");

            _model = _model.Load(filePath);

            if (_model is null)
            {
                SLogger.Write($"모델 로딩 실패:{filePath}");
                return false;
            }

            string inspImagePath = _model.InspectImagePath;
            if (File.Exists(inspImagePath))
            {
                Global.Inst.InspStage.SetImageBuffer(inspImagePath);
            }

            UpdateDiagramEntity();

            return true;
        }
        // 수진언니 내가 사랑해 걀걀걀 -??? // 주연아 나도♡
        public void SaveModel(string filePath)
        {
            SLogger.Write($"모델 저장:{filePath}");

            //입력 경로가 없으면 현재 모델 저장
            if (string.IsNullOrEmpty(filePath))
                Global.Inst.InspStage.CurModel.Save();
            else
                Global.Inst.InspStage.CurModel.SaveAs(filePath);
        }

        #region Disposable

        private bool disposed = false; // to detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    if (_saigeAI != null)
                    {
                        _saigeAI.Dispose();
                        _saigeAI = null;
                    }
                    if (_grabManager != null)
                    {
                        _grabManager.Dispose();
                        _grabManager = null;
                    }
                }

                // Dispose unmanaged managed resources.

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion //Disposable
    }
}