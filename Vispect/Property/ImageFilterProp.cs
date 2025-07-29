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
        private ImageProcessor processor = new ImageProcessor();
        private Mat originalImage;
        private Mat processedImage;

        public ImageFilterProp()
        {
            InitializeComponent();

            cbImageFilter.Items.AddRange(new string[]
            {
                "Grayscale",
                "HSV",
                "Flip Horizontal",
                "Flip Vertical",
                "Binary",
                "Blur",
                "Rotate 90",
                "Rotate 180",
                "Rotate 270",
                "Perspective",
                "Affine",
                "Morphology",
                "Canny"
            });

            cbImageFilter.SelectionChangeCommitted += cbImageFilter_SelectionChangeCommitted;
        }

        public void SetOriginalImage(Mat image)
        {
            originalImage = image;
            processedImage = null;
        }


        private void cbImageFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (originalImage == null)
                return;

            try
            {
                string selectedFilter = cbImageFilter.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedFilter))
                    return;

                switch (selectedFilter)
                {
                    case "Grayscale":
                        processedImage = processor.ApplyGrayscale(originalImage);
                        break;

                    case "HSV":
                        processedImage = processor.ApplyHSV(originalImage);
                        break;

                    case "Flip Horizontal":
                        processedImage = processor.ApplyFlip(originalImage, FlipMode.X);
                        break;

                    case "Flip Vertical":
                        processedImage = processor.ApplyFlip(originalImage, FlipMode.Y);
                        break;

                    case "Binary":
                        processedImage = processor.ApplyBinary(originalImage, 128); // threshold 예시값
                        break;

                    case "Blur":
                        processedImage = processor.ApplyBlur(originalImage);
                        break;

                    case "Rotate 90":
                        processedImage = processor.ApplyRotate(originalImage, 90);
                        break;

                    case "Rotate 180":
                        processedImage = processor.ApplyRotate(originalImage, 180);
                        break;

                    case "Rotate 270":
                        processedImage = processor.ApplyRotate(originalImage, 270);
                        break;

                    case "Perspective":
                        processedImage = processor.ApplyPerspective(originalImage);
                        break;

                    case "Affine":
                        processedImage = processor.ApplyAffine(originalImage);
                        break;

                    case "Morphology":
                        processedImage = processor.ApplyMorphology(originalImage);
                        break;

                    case "Canny":
                        processedImage = processor.ApplyCanny(originalImage, 100, 200);
                        break;

                    default:
                        processedImage = originalImage.Clone();
                        break;
                }

                if (this.Parent is Form parentForm)
                {
                    var pictureBox = parentForm.Controls["pictureBox1"] as PictureBox;
                    if (pictureBox != null)
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(processedImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"필터 적용 중 오류가 발생했습니다: {ex.Message}");
            }
        }
    }
}