using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vispect.Algorithm;
using Vispect.Core;
using Vispect.ImagaeProcessing;
using Vispect.Teach;
using WeifenLuo.WinFormsUI.Docking;

namespace Vispect
{
    public partial class CameraForm : DockContent
    {
        public Mat CurrentMat { get; private set; }

        private bool _suppressPreviewUpdate = false;

        public CameraForm()
        {
            InitializeComponent();

            imageViewer.DiagramEntityEvent += ImageViewer_DiagramEntityEvent;
        }

        private void ImageViewer_DiagramEntityEvent(object sender, DiagramEntityEventArgs e)
        {
            switch (e.ActionType)
            {
                case EntityActionType.Select:
                    Global.Inst.InspStage.SelectInspWindow(e.InspWindow);
                    imageViewer.Focus();
                    break;
                case EntityActionType.Inspect:
                    UpdateDiagramEntity();
                    Global.Inst.InspStage.TryInspection(e.InspWindow);
                    break;
                case EntityActionType.Add:
                    Global.Inst.InspStage.AddInspWindow(e.WindowType, e.Rect);
                    break;
                case EntityActionType.Copy:
                    Global.Inst.InspStage.AddInspWindow(e.InspWindow, e.OffsetMove);
                    break;
                case EntityActionType.Move:
                    Global.Inst.InspStage.MoveInspWindow(e.InspWindow, e.OffsetMove);
                    break;
                case EntityActionType.Resize:
                    Global.Inst.InspStage.ModifyInspWindow(e.InspWindow, e.Rect);
                    break;
                case EntityActionType.Delete:
                    Global.Inst.InspStage.DelInspWindow(e.InspWindow);
                    break;
                case EntityActionType.DeleteList:
                    Global.Inst.InspStage.DelInspWindow(e.InspWindowList);
                    break;
            }
        }

        private Bitmap FixOrientation(Image img)
        {
            const int orientationId = 0x0112;
            try
            {
                if (Array.IndexOf(img.PropertyIdList, orientationId) > -1)
                {
                    var prop = img.GetPropertyItem(orientationId);
                    int val = prop.Value[0];

                    switch (val)
                    {
                        case 2: img.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                        case 3: img.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                        case 4: img.RotateFlip(RotateFlipType.Rotate180FlipX); break;
                        case 5: img.RotateFlip(RotateFlipType.Rotate90FlipX); break;
                        case 6: img.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                        case 7: img.RotateFlip(RotateFlipType.Rotate270FlipX); break;
                        case 8: img.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                        default: break;
                    }
                    try { img.RemovePropertyItem(orientationId); } catch { }
                }
            }
            catch
            {
            }

            return new Bitmap(img);
        }
        
        public void LoadImage(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            try
            {
                using (Image img = Image.FromFile(filePath))
                {
                    Bitmap fixedBmp = FixOrientation(img);
                    CurrentMat = BitmapConverter.ToMat(fixedBmp);

                    imageViewer.LoadBitmap(fixedBmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 실패: {ex.Message}");
            }
        }

        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageViewer.Width = this.Width - margin * 2;
            imageViewer.Height = this.Height - margin * 2;

            imageViewer.Location = new System.Drawing.Point(margin, margin);
        }

        public void UpdateDisplay(Bitmap bitmap = null, bool updatePreview = true)
        {
            if (bitmap != null)
            {
                imageViewer.LoadBitmap(bitmap);
                CurrentMat = BitmapConverter.ToMat(bitmap);
            }
            else
            {
                Bitmap bmp = Global.Inst.InspStage.GetBitmap(0);
                if (bmp != null)
                {
                    imageViewer.LoadBitmap(bmp);
                    CurrentMat = BitmapConverter.ToMat(bmp);
                }
            }

            if (updatePreview == false)
                return;

            if (!_suppressPreviewUpdate)
            {
                Mat curImage = Global.Inst.InspStage.GetMat();
                Global.Inst.InspStage.PreView.SetImage(curImage);
            }
        }

        public Bitmap GetDisplayImage()
        {
            return imageViewer != null ? imageViewer.GetCurBitmap() : null;
        }

        public void UpdateDisplayFromMat(Mat mat)
        {
            if (mat == null)
                return;

            // 화면용 비트맵 만들고 띄우기
            Bitmap bmp = BitmapConverter.ToBitmap(mat);
            imageViewer.LoadBitmap(bmp);

            // CurrentMat 갱신 (복제)
            CurrentMat = mat.Clone();

            // Preview도 업데이트
            Global.Inst.InspStage.PreView.SetImage(CurrentMat);
        }

        public void UpdateImageViewer()
        { 
            imageViewer.Invalidate();
        }

        public void UpdateDiagramEntity()
        {
            imageViewer.ResetEntity();

            Model model = Global.Inst.InspStage.CurModel;
            List<DiagramEntity> diagramEntityList = new List<DiagramEntity>();

            foreach (InspWindow window in model.InspWindowList)
            {
                if (window is null)
                    continue;

                DiagramEntity entity = new DiagramEntity()
                {
                    LinkedWindow = window,
                    EntityROI = new Rectangle(
                        window.WindowArea.X, window.WindowArea.Y,
                            window.WindowArea.Width, window.WindowArea.Height),
                    EntityColor = imageViewer.GetWindowColor(window.InspWindowType),
                    IsHold = window.IsTeach
                };
                diagramEntityList.Add(entity);
            }

            imageViewer.SetDiagramEntityList(diagramEntityList);
        }

        public void SelectDiagramEntity(InspWindow window)
        {
            imageViewer.SelectDiagramEntity(window);
        }

        public void ResetDisplay()
        {
            imageViewer.ResetEntity();
        }

        public void AddRect(List<DrawInspectInfo> rectInfos)
        {
            imageViewer.AddRect(rectInfos);
        }

        public void AddRoi(InspWindowType inspWindowType)
        {
            imageViewer.NewRoi(inspWindowType);
        }
    }
}