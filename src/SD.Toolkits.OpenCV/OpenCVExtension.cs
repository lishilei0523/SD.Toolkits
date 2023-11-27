using OpenCvSharp;
using System;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// OpenCV扩展
    /// </summary>
    public static class OpenCVExtension
    {
        #region # 制作直方图图像 —— static Mat MakeHistogram(this Mat matrix...
        /// <summary>
        /// 制作直方图图像
        /// </summary>
        /// <param name="matrix">图像</param>
        /// <param name="histogramWidth">直方图图像宽度</param>
        /// <param name="histogramHeigth">直方图图像高度</param>
        /// <returns>直方图图像</returns>
        public static Mat MakeHistogram(this Mat matrix, int histogramWidth = 1024, int histogramHeigth = 768)
        {
            Mat[] images = { matrix };
            Mat histogram = new Mat();
            int[] channels = { 0 };
            int[] histSize = { 256 };
            Rangef[] histRange = { new Rangef(0, 256) };
            Cv2.CalcHist(images, channels, null, histogram, 1, histSize, histRange);

            Mat histogramImage = new Mat(histogramHeigth, histogramWidth, MatType.CV_8UC1, Scalar.All(0));
            double binW = Math.Round((double)histogramImage.Width / histogram.Height);

            //归一化
            Cv2.Normalize(histogram, histogram, 0, histogramImage.Rows, NormTypes.MinMax);
            for (int index = 1; index < histogram.Height; index++)
            {
                Point point1 = new Point(binW * (index - 1), histogramImage.Height - Math.Round(histogram.At<float>(index - 1)));
                Point point2 = new Point(binW * (index), histogramImage.Height - Math.Round(histogram.At<float>(index)));
                Cv2.Line(histogramImage, point1, point2, Scalar.White, 1, LineTypes.AntiAlias);
            }

            return histogramImage;
        }
        #endregion
    }
}
