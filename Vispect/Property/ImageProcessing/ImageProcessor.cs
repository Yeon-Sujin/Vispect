using OpenCvSharp;
using System;

namespace Vispect.ImagaeProcessing
{
    public class ImageProcessor
    {
        private Mat ExecuteSafely(Func<Mat> func, string errorMessage) 
            // 모든 처리 함수에 공통적으로 예외 처리 적용
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(errorMessage, ex);
            }
        }

        public Mat ApplyGrayscale(Mat image) =>
            ExecuteSafely(() =>
                image.Channels() > 1 ? image.CvtColor(ColorConversionCodes.BGR2GRAY) : image.Clone(),
                "Grayscale 처리 중 오류가 발생했습니다.");

        public Mat ApplyHSV(Mat image) =>
            ExecuteSafely(() =>
            {
                if (image.Channels() <= 1)
                    throw new InvalidOperationException("HSV 변환은 컬러 이미지에서만 가능합니다.");

                return image.CvtColor(ColorConversionCodes.BGR2HSV);
            },
            "HSV 처리 중 오류가 발생했습니다.");

        public Mat ApplyFlip(Mat image, FlipMode mode) =>
            ExecuteSafely(() =>
            {
                if (!Enum.IsDefined(typeof(FlipMode), mode))
                    throw new ArgumentException("유효하지 않은 FlipMode 입니다.");

                Mat result = new Mat();
                Cv2.Flip(image, result, mode);
                return result;
            }, "Flip 처리 중 오류가 발생했습니다.");

        public Mat ApplyPyramidDown(Mat image) =>
            ExecuteSafely(() =>
            {
                Mat result = new Mat();
                Cv2.PyrDown(image, result);
                return result;
            }, "PyramidDown 처리 중 오류가 발생했습니다.");

        public Mat ApplyResize(Mat image, int width, int height) =>
            ExecuteSafely(() =>
            {
                if (width <= 0 || height <= 0)
                    throw new ArgumentException("가로 및 세로 크기는 0보다 커야 합니다.");

                Mat result = new Mat();
                Cv2.Resize(image, result, new OpenCvSharp.Size(width, height));
                return result;
            }, "Resize 처리 중 오류가 발생했습니다.");

        public Mat ApplyBinary(Mat image, int threshold) =>
            ExecuteSafely(() =>
            {
                if (threshold < 0 || threshold > 255)
                    throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold 값은 0~255 사이여야 합니다.");

                Mat gray = image.Channels() > 1 ? image.CvtColor(ColorConversionCodes.BGR2GRAY) : image.Clone();
                Mat result = new Mat();
                Cv2.Threshold(gray, result, threshold, 255, ThresholdTypes.Binary);
                return result;
            }, "Binary 처리 중 오류가 발생했습니다.");

        public Mat ApplyBlur(Mat image) =>
            ExecuteSafely(() =>
            {
                Mat result = new Mat();
                Cv2.BilateralFilter(image, result, d: 9, sigmaColor: 75, sigmaSpace: 75);
                return result;
            }, "Blur 처리 중 오류가 발생했습니다.");

        public Mat ApplyRotate(Mat image, int angle) =>
            ExecuteSafely(() =>
            {
                Mat result = new Mat();
                switch (angle)
                {
                    case 90:
                        Cv2.Transpose(image, result);
                        Cv2.Flip(result, result, FlipMode.Y);
                        break;
                    case 180:
                        Cv2.Flip(image, result, FlipMode.XY);
                        break;
                    case 270:
                        Cv2.Transpose(image, result);
                        Cv2.Flip(result, result, FlipMode.X);
                        break;
                    default:
                        throw new ArgumentException("지원하지 않는 회전 각도입니다. 90, 180, 270만 허용됩니다.");
                }
                return result;
            }, "Rotate 처리 중 오류가 발생했습니다.");

        public Mat ApplyPerspective(Mat image) =>
            ExecuteSafely(() =>
            {
                Point2f[] src = { new Point2f(0, 0), new Point2f(image.Cols, 0), new Point2f(image.Cols, image.Rows), new Point2f(0, image.Rows) };
                Point2f[] dst = { new Point2f(50, 50), new Point2f(image.Cols - 50, 20), new Point2f(image.Cols - 20, image.Rows - 50), new Point2f(30, image.Rows - 20) };
                Mat matrix = Cv2.GetPerspectiveTransform(src, dst);
                Mat result = new Mat();
                Cv2.WarpPerspective(image, result, matrix, image.Size());
                return result;
            }, "Perspective 처리 중 오류가 발생했습니다.");

        public Mat ApplyAffine(Mat image) =>
            ExecuteSafely(() =>
            {
                Point2f[] src = { new Point2f(0, 0), new Point2f(image.Cols - 1, 0), new Point2f(0, image.Rows - 1) };
                Point2f[] dst = { new Point2f(0, 0), new Point2f(image.Cols * 0.8f, image.Rows * 0.2f), new Point2f(image.Cols * 0.2f, image.Rows * 0.8f) };
                Mat matrix = Cv2.GetAffineTransform(src, dst);
                Mat result = new Mat();
                Cv2.WarpAffine(image, result, matrix, image.Size());
                return result;
            }, "Affine 처리 중 오류가 발생했습니다.");

        public Mat ApplyMorphology(Mat image) =>
            ExecuteSafely(() =>
            {
                Mat gray = image.Channels() > 1 ? image.CvtColor(ColorConversionCodes.BGR2GRAY) : image.Clone();
                Mat result = new Mat();
                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(5, 5));
                Cv2.Erode(gray, result, kernel);
                return result;
            }, "Morphology 처리 중 오류가 발생했습니다.");

        public Mat ApplyCanny(Mat image, int threshold1, int threshold2) =>
            ExecuteSafely(() =>
            {
                if (threshold1 < 0 || threshold2 < 0 || threshold1 > 255 || threshold2 > 255)
                    throw new ArgumentOutOfRangeException("Threshold 값은 0~255 사이여야 합니다.");

                Mat gray = image.Channels() > 1 ? image.CvtColor(ColorConversionCodes.BGR2GRAY) : image.Clone();
                Mat result = new Mat();
                Cv2.Canny(gray, result, threshold1, threshold2);
                return result;
            }, "Canny 처리 중 오류가 발생했습니다.");
    }
}