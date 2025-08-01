using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vispect.Core;
using Vispect.ImagaeProcessing;
using WeifenLuo.WinFormsUI.Docking;

namespace Vispect
{
    public partial class CameraForm : DockContent
    {
        public Mat CurrentMat { get; private set; }

        public CameraForm()
        {
            InitializeComponent();
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
            Mat matToShow = null;
            Bitmap bitmapToShow = null;

            if (bitmap != null)
            {
                // 외부에서 주어진 비트맵이 있으면 그걸 기준으로
                bitmapToShow = bitmap;
                matToShow = BitmapConverter.ToMat(bitmap);
            }
            else
            {
                // 없으면 InspStage에서 현재 Mat을 가져와서
                Mat stageMat = Global.Inst.InspStage.GetMat();
                if (stageMat != null)
                {
                    matToShow = stageMat.Clone();
                    bitmapToShow = BitmapConverter.ToBitmap(matToShow);
                }
                else
                {
                    // fallback: InspStage에서 비트맵 직접 가져오기 (있다면)
                    Bitmap bmp = Global.Inst.InspStage.GetBitmap(0);
                    if (bmp != null)
                    {
                        bitmapToShow = bmp;
                        matToShow = BitmapConverter.ToMat(bmp);
                    }
                }
            }

            if (bitmapToShow == null || matToShow == null)
                return;

            // 1. 뷰어에 보여주고 CurrentMat 갱신
            imageViewer.LoadBitmap(bitmapToShow);
            CurrentMat = matToShow.Clone();

            // 2. Preview 갱신 (현재 보여주는 Mat 기준)
            Global.Inst.InspStage.PreView.SetImage(CurrentMat);
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
    }
}
