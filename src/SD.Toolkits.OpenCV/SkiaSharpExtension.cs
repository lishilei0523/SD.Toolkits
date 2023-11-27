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
        #region # OpenCV图像映射SKBitmap —— static SKBitmap ToSKBitmap(this Mat matrix)
        /// <summary>
        /// OpenCV图像映射SKBitmap
        /// </summary>
        public static SKBitmap ToSKBitmap(this Mat matrix)
        {
            byte[] imageBytes = matrix.ToBytes();
            SKBitmap bitmap = SKBitmap.Decode(imageBytes);

            return bitmap;
        }
        #endregion

        #region # SKBitmap映射OpenCV图像 —— static Mat ToMatrix(this SKBitmap bitmap...
        /// <summary>
        /// SKBitmap映射OpenCV图像
        /// </summary>
        public static Mat ToMatrix(this SKBitmap bitmap, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Jpeg, int quantity = 100)
        {
            using MemoryStream stream = new MemoryStream();
            bitmap.Encode(stream, imageFormat, quantity);
            byte[] imageBytes = stream.ToArray();
            Mat matrix = Mat.ImDecode(imageBytes);

            return matrix;
        }
        #endregion
    }
}
