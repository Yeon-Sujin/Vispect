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
using Vispect.UIControl;
using WeifenLuo.WinFormsUI.Docking;

namespace Vispect
{
    public partial class CameraForm : DockContent
    {
        public Mat CurrentMat { get; private set; }

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

        public void UpdateDisplay(Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                //#6_INSP_STAGE#3 업데이트시 bitmap이 없다면 InspSpace에서 가져온다
                bitmap = Global.Inst.InspStage.GetBitmap(0);
                if (bitmap == null)
                    return;
            }

            if (imageViewer != null)
                imageViewer.LoadBitmap(bitmap);

            //#7_BINARY_PREVIEW#10 현재 선택된 이미지로 Previwe이미지 갱신
            //이진화 프리뷰에서 각 채널별로 설정이 적용되도록, 현재 이미지를 프리뷰 클래스 설정            
            Mat curImage = Global.Inst.InspStage.GetMat();
            Global.Inst.InspStage.PreView.SetImage(curImage);
        }

        // 기존 GetDisplayImage()는 그대로 두고
        public Bitmap GetDisplayImage()
        {
            return imageViewer?.GetCurBitmap();
        }

        // Mat 전용 메서드 새로 추가
        public Mat GetDisplayMat()
        {
            // 필요하다면 .Clone() 해서 반환하세요
            return Global.Inst.InspStage.ImageSpace.GetMat();
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