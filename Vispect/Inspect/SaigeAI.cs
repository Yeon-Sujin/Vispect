using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaigeVision.Net.V2;
using SaigeVision.Net.V2.Detection;
using SaigeVision.Net.V2.IAD;
using SaigeVision.Net.V2.Segmentation;

namespace Vispect.Inspect
{
    public class SaigeAI : IDisposable
    {
        public enum EngineType { IAD, SEG, DET }
        private EngineType _engineType = EngineType.IAD;

        public EngineType SelectedEngineType
        {
            get { return _engineType; }
            set { _engineType = value; }
        }

        private IADEngine _iadEngine;
        private SegmentationEngine _segEngine;
        private DetectionEngine _detEngine;

        private IADResult _iadResult;
        private SegmentationResult _segResult;
        private DetectionResult _detResult;

        private Bitmap _inspImage;

        public SaigeAI()
        { 

        }

        public void LoadEngine(string modelPath)
        {
            if (_engineType == EngineType.IAD)
            {
                if (_iadEngine != null)
                    _iadEngine.Dispose();

                _iadEngine = new IADEngine(modelPath, 0);
                IADOption option = _iadEngine.GetInferenceOption();
                option.CalcScoremap = false;
                option.CalcHeatmap = false;
                option.CalcMask = false;
                option.CalcObject = true;
                option.CalcObjectAreaAndApplyThreshold = true;
                option.CalcObjectScoreAndApplyThreshold = true;
                option.CalcTime = true;
                _iadEngine.SetInferenceOption(option);
            }
            else if (_engineType == EngineType.SEG)
            {
                if (_segEngine != null)
                    _segEngine.Dispose();

                _segEngine = new SegmentationEngine(modelPath, 0);
            }
            else if (_engineType == EngineType.DET)
            {
                if (_detEngine != null)
                    _detEngine.Dispose();

                _detEngine = new DetectionEngine(modelPath, 0);
            }
        }

        public bool InspIAD(Bitmap bmpImage)
        {
            if (_iadEngine == null)
            { 
                MessageBox.Show("엔진이 초기화되지 않았습니다. LoadEngine 메서드를 호출하여 엔진을 초기화하세요.");
                return false;
            }

            _inspImage = bmpImage;
            SrImage srImage = new SrImage(bmpImage);
            Stopwatch sw = Stopwatch.StartNew();
            _iadResult = _iadEngine.Inspection(srImage);
            sw.Stop();
            return true;
        }

        public bool InspSEG(Bitmap bmpImage)
        {
            if (_segEngine == null)
            {
                MessageBox.Show("엔진이 초기화되지 않았습니다. LoadEngine 메서드를 호출하여 엔진을 초기화하세요.");
                return false;
            }

            _inspImage = bmpImage;
            SrImage srImage = new SrImage(bmpImage);
            Stopwatch sw = Stopwatch.StartNew();
            _segResult = _segEngine.Inspection(srImage);
            sw.Stop();
            return true;
        }

        public bool InspDET(Bitmap bmpImage)
        {
            if (_detEngine == null)
            {
                MessageBox.Show("엔진이 초기화되지 않았습니다. LoadEngine 메서드를 호출하여 엔진을 초기화하세요.");
                return false;
            }

            _inspImage = bmpImage;
            SrImage srImage = new SrImage(bmpImage);
            Stopwatch sw= Stopwatch.StartNew();
            _detResult = _detEngine.Inspection(srImage);
            sw.Stop();
            return true;
        }

        public Bitmap GetResultImage()
        {
            if (_inspImage == null)
                return null;

            Bitmap resultImage = _inspImage.Clone(
                new Rectangle(0, 0, _inspImage.Width, _inspImage.Height),
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            if (_engineType == EngineType.IAD && _iadResult != null)
                DrawIADResult(_iadResult, resultImage);
            else if (_engineType == EngineType.SEG && _segResult != null)
                DrawSegResult(_segResult, resultImage);
            else if (_engineType == EngineType.DET && _detResult != null)
                DrawDetectionResult(_detResult, resultImage);

            return resultImage;
        }

        private void DrawIADResult(IADResult result, Bitmap bmp)
        { 
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

            foreach (var prediction in result.SegmentedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));

                using (GraphicsPath gp = new GraphicsPath())
                {
                    if (prediction.Contour.Value.Count < 3) continue;
                    gp.AddPolygon(prediction.Contour.Value.ToArray());
                    foreach (var innerValue in prediction.Contour.InnerValue)
                    { 
                        gp.AddPolygon(innerValue.ToArray());
                    }
                    g.FillPath(brush, gp);
                }
                step += 50;
            }
        }

        private void DrawSegResult(SegmentationResult result, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

            foreach (var prediction in result.SegmentedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));
                using (GraphicsPath gp = new GraphicsPath())
                {
                    if (prediction.Contour.Value.Count < 4) continue;
                    gp.AddPolygon(prediction.Contour.Value.ToArray());
                    foreach (var innerValue in prediction.Contour.InnerValue)
                    {
                        gp.AddPolygon(innerValue.ToArray());
                    }
                    g.FillPath(brush, gp);
                }
                step += 50;
            }
        }


        private void DrawDetectionResult(DetectionResult result, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

            foreach (var prediction in result.DetectedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));
                using (GraphicsPath gp = new GraphicsPath())
                {
                    float x = (float)prediction.BoundingBox.X;
                    float y = (float)prediction.BoundingBox.Y;
                    float width = (float)prediction.BoundingBox.Width;
                    float height = (float)prediction.BoundingBox.Height;
                    gp.AddRectangle(new RectangleF(x, y, width, height));
                    g.DrawPath(new Pen(brush, 10), gp);
                }
                step += 50;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                { 
                    if(_iadEngine != null)
                        _iadEngine.Dispose();
                    if(_segEngine != null)
                        _segEngine.Dispose();
                    if(_detEngine != null)
                        _detEngine.Dispose();
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