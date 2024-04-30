using OpenCvSharp;
using ScottPlot;
using SD.Toolkits.OpenCV.Extensions;
using SkiaSharp;
using System.IO;
using System.Linq;

namespace SD.Toolkits.OpenCV.SkiaSharp
{
    /// <summary>
    /// SkiaSharp扩展
    /// </summary>
    public static class SkiaSharpExtension
    {
        #region # OpenCV矩阵映射SKBitmap —— static SKBitmap ToSKBitmap(this Mat matrix...
        /// <summary>
        /// OpenCV矩阵映射SKBitmap
        /// </summary>
        public static SKBitmap ToSKBitmap(this Mat matrix, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Jpeg)
        {
            string imageExtension = imageFormat.GetImageExtension();
            byte[] imageBytes = matrix.ToBytes(imageExtension);
            SKBitmap bitmap = SKBitmap.Decode(imageBytes);

            return bitmap;
        }
        #endregion

        #region # SKBitmap映射OpenCV矩阵 —— static Mat ToCVMatrix(this SKBitmap bitmap...
        /// <summary>
        /// SKBitmap映射OpenCV矩阵
        /// </summary>
        public static Mat ToCVMatrix(this SKBitmap bitmap, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Jpeg, int quantity = 100)
        {
            using MemoryStream stream = new MemoryStream();
            bitmap.Encode(stream, imageFormat, quantity);
            byte[] imageBytes = stream.ToArray();
            Mat matrix = Mat.ImDecode(imageBytes);

            return matrix;
        }
        #endregion

        #region # 生成直方图图像 —— static Mat GenerateHistogramImage(this Mat matrix...
        /// <summary>
        /// 生成直方图图像
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="width">直方图图像宽度</param>
        /// <param name="height">直方图图像高度</param>
        /// <returns>直方图图像矩阵</returns>
        public static Mat GenerateHistogramImage(this Mat matrix, int width = 1024, int height = 768)
        {
            //生成直方图矩阵
            using Mat histogram = matrix.GenerateHistogram();
            histogram.GetArray(out float[] histVector);

            //ScottPlot绘图
            double[] values = histVector.Select(x => (double)x).ToArray();
            double[] positions = Enumerable.Range(1, histVector.Length).Select(x => (double)x).ToArray();
            Plot plot = new Plot();
            plot.Add.Bars(positions, values);
            byte[] imageBytes = plot.GetImageBytes(width, height, ImageFormat.Jpeg);

            //转换OpenCV图像矩阵
            Mat histImage = Cv2.ImDecode(imageBytes, ImreadModes.Color);

            return histImage;
        }
        #endregion

        #region # 获取图像扩展名 —— static string GetImageExtension(this SKEncodedImageFormat...
        /// <summary>
        /// 获取图像扩展名
        /// </summary>
        /// <param name="imageFormat">图像格式</param>
        /// <returns>图像扩展名</returns>
        public static string GetImageExtension(this SKEncodedImageFormat imageFormat)
        {
            string imageExtension = imageFormat switch
            {
                SKEncodedImageFormat.Bmp => ".bmp",
                SKEncodedImageFormat.Gif => ".gif",
                SKEncodedImageFormat.Ico => ".ico",
                SKEncodedImageFormat.Jpeg => ".jpg",
                SKEncodedImageFormat.Png => ".png",
                SKEncodedImageFormat.Wbmp => ".wbmp",
                SKEncodedImageFormat.Webp => ".webp",
                SKEncodedImageFormat.Pkm => ".pkm",
                SKEncodedImageFormat.Ktx => ".ktx",
                SKEncodedImageFormat.Astc => ".astc",
                SKEncodedImageFormat.Dng => ".dng",
                SKEncodedImageFormat.Heif => ".heif",
                SKEncodedImageFormat.Avif => ".avif",
                _ => ".jpg"
            };

            return imageExtension;
        }
        #endregion
    }
}
