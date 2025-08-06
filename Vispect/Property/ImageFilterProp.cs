using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vispect.ImagaeProcessing;

namespace Vispect
{
    public partial class ImageFilterProp : UserControl
    {
        private readonly ImageProcessor processor = new ImageProcessor();
        private Mat originalImage;
        private Mat processedImage;
        private string selectedFilter;
        private readonly Stack<Mat> undoStack = new Stack<Mat>();

        public ImageFilterProp()
        {
            InitializeComponent();

            cbImageFilter.Items.AddRange(new string[]
            {
                "Grayscale",
                "HSV",
                "Flip",
                "PyramidDown",
                "Resize",
                "Binary",
                "Blur",
                "Rotate",
                "Perspective",
                "Affine",
                "Morphology",
                "Canny"
            });

            cbImageFilter.SelectionChangeCommitted += cbImageFilter_SelectionChangeCommitted;

            HideAllOptionPanels();
        }

        public void SetOriginalImage(Mat image)
        {
            originalImage = image;
            processedImage = null;
            undoStack.Clear();
        }

        private void cbImageFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedFilter = cbImageFilter.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedFilter) || originalImage == null)
                return;

            HideAllOptionPanels();

            switch (selectedFilter)
            {
                case "Flip":
                    panelFlipOptions.Visible = true;
                    break;
                case "Resize":
                    panelResizeOptions.Visible = true;
                    break;
                case "Binary":
                    panelBinaryOptions.Visible = true;
                    break;
                case "Canny":
                    panelCannyOptions.Visible = true;
                    break;
                case "Rotate":
                    panelRotateOptions.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void HideAllOptionPanels()
        {
            panelResizeOptions.Visible = false;
            panelBinaryOptions.Visible = false;
            panelFlipOptions.Visible = false;
            panelRotateOptions.Visible = false;
            panelCannyOptions.Visible = false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (originalImage == null || string.IsNullOrEmpty(selectedFilter))
                return;

            Mat before = processedImage != null ? processedImage.Clone() : originalImage.Clone();
            undoStack.Push(before);

            ApplySelectedFilter();
        }

        private void btnOriginal_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;
            processedImage = originalImage.Clone();
            UpdateCameraForm(processedImage);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                processedImage = undoStack.Pop();
                UpdateCameraForm(processedImage);
            }
        }

        private void ApplySelectedFilter()
        {
            if (originalImage == null) return;

            try
            {
                Mat sourceMat = processedImage != null ? processedImage : originalImage;

                switch (selectedFilter)
                {
                    case "Grayscale":
                        processedImage = processor.ApplyGrayscale(sourceMat);
                        break;
                    case "HSV":
                        processedImage = processor.ApplyHSV(sourceMat);
                        break;
                    case "Flip":
                        {
                            FlipMode mode;
                            if (radioBoth.Checked)
                                mode = FlipMode.XY;
                            else if (radioVertical.Checked)
                                mode = FlipMode.Y;
                            else
                                mode = FlipMode.X;
                            processedImage = processor.ApplyFlip(sourceMat, mode);
                        }
                        break;
                    case "PyramidDown":
                        Console.WriteLine($"[PyramidDown] before: {sourceMat.Size()}");
                        processedImage = processor.ApplyPyramidDown(sourceMat);
                        Console.WriteLine($"[PyramidDown] after: {processedImage.Size()}");
                        break;
                    case "Resize":
                        {
                            int width = (int)numWidth.Value;
                            int height = (int)numHeight.Value;
                            processedImage = processor.ApplyResize(sourceMat, width, height);
                        }
                        break;
                    case "Binary":
                        {
                            int threshold = trackBarThreshold.Value;
                            processedImage = processor.ApplyBinary(sourceMat, threshold);
                        }
                        break;
                    case "Canny":
                        {
                            int low = trackBarCannyLower.Value;
                            int high = trackBarCannyUpper.Value;
                            processedImage = processor.ApplyCanny(sourceMat, low, high);
                        }
                        break;
                    case "Rotate":
                        {
                            int angle = trackBarAngle.Value;
                            if (angle % 90 != 0)
                            {
                                var center = new OpenCvSharp.Point2f(sourceMat.Cols / 2f, sourceMat.Rows / 2f);
                                Mat rotMat = Cv2.GetRotationMatrix2D(center, angle, 1.0);
                                Mat dst = new Mat();
                                Cv2.WarpAffine(sourceMat, dst, rotMat, sourceMat.Size());
                                processedImage = dst;
                            }
                            else
                            {
                                processedImage = processor.ApplyRotate(sourceMat, angle);
                            }
                        }
                        break;
                    case "Blur":
                        processedImage = processor.ApplyBlur(sourceMat);
                        break;
                    case "Perspective":
                        processedImage = processor.ApplyPerspective(sourceMat);
                        break;
                    case "Affine":
                        processedImage = processor.ApplyAffine(sourceMat);
                        break;
                    case "Morphology":
                        processedImage = processor.ApplyMorphology(sourceMat);
                        break;
                    default:
                        processedImage = sourceMat.Clone();
                        break;
                }

                if (processedImage != null)
                {
                    var cameraForm = MainForm.GetDockForm<CameraForm>();
                    if (cameraForm != null)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"필터 적용 중 오류: {ex.Message}");
            }
        }

        private void UpdateCameraForm(Mat result)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                var bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
                cameraForm.UpdateDisplay(bmp);
            }
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            if (lblThresholdValue != null)
                lblThresholdValue.Text = trackBarThreshold.Value.ToString();
        }

        private void trackBarAngle_Scroll(object sender, EventArgs e)
        {
            if (lblAngleValue != null)
                lblAngleValue.Text = $"{trackBarAngle.Value}°";
        }

        private void trackBarCannyLower_Scroll(object sender, EventArgs e)
        {
            if (lblCannyLowerValue != null)
                lblCannyLowerValue.Text = trackBarCannyLower.Value.ToString();
        }

        private void trackBarCannyUpper_Scroll(object sender, EventArgs e)
        {
            if (lblCannyUpperValue != null)
                lblCannyUpperValue.Text = trackBarCannyUpper.Value.ToString();
        }
    }
}