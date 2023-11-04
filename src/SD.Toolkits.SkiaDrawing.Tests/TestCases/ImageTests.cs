using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.SkiaDrawing.Enums;
using SkiaSharp;
using System.IO;

namespace SD.Toolkits.SkiaDrawing.Tests.TestCases
{
    /// <summary>
    /// 图像测试
    /// </summary>
    [TestClass]
    public class ImageTests
    {
        #region # 测试缩略图 —— void TestThumbnail()
        /// <summary>
        /// 测试缩略图
        /// </summary>
        [TestMethod]
        public void TestThumbnail()
        {
            using SKFileStream inputStream = new SKFileStream("Images/Earth.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);
            using SKBitmap thumbnail = bitmap.MakeThumbnail(640, 360, ThumbnailMode.WidthAndHeight);

            using FileStream outputStream = File.OpenWrite("Images/Earth.Thumbnail.jpg");
            thumbnail.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试文字水印 —— void TestTextWatermark()
        /// <summary>
        /// 测试文字水印
        /// </summary>
        [TestMethod]
        public void TestTextWatermark()
        {
            using SKFileStream inputStream = new SKFileStream("Images/China.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);
            using SKBitmap watermarkBitmap = bitmap.MakeTextWatermark("Hello World!", SKColors.Red);

            using FileStream outputStream = File.OpenWrite("Images/China.Watermark.jpg");
            watermarkBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion
    }
}
