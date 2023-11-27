using OpenCvSharp;
using SkiaSharp;
using System.IO;

namespace SD.Toolkits.OpenCV
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
