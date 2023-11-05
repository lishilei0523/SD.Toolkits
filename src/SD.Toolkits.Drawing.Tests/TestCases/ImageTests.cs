using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiaSharp;
using System.IO;

namespace SD.Toolkits.Drawing.Tests.TestCases
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
            using SKBitmap thumbnail = bitmap.MakeThumbnail(640, 360);

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

        #region # 测试横向拼接图像 —— void TestMergeBitmapsHorizontally()
        /// <summary>
        /// 测试横向拼接图像
        /// </summary>
        [TestMethod]
        public void TestMergeBitmapsHorizontally()
        {
            using SKFileStream inputStream1 = new SKFileStream("Images/China.jpg");
            using SKFileStream inputStream2 = new SKFileStream("Images/Earth.jpg");
            using SKFileStream inputStream3 = new SKFileStream("Images/World.jpg");
            using SKBitmap bitmap1 = SKBitmap.Decode(inputStream1);
            using SKBitmap bitmap2 = SKBitmap.Decode(inputStream2);
            using SKBitmap bitmap3 = SKBitmap.Decode(inputStream3);

            SKBitmap[] bitmaps = { bitmap1, bitmap2, bitmap3 };
            using SKBitmap mergeBitmap = bitmaps.MergeBitmapsHorizontally();

            using FileStream outputStream = File.OpenWrite("Images/MergedHorizontally.jpg");
            mergeBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试纵向拼接图像 —— void TestMergeBitmapsVertically()
        /// <summary>
        /// 测试纵向拼接图像
        /// </summary>
        [TestMethod]
        public void TestMergeBitmapsVertically()
        {
            using SKFileStream inputStream1 = new SKFileStream("Images/China.jpg");
            using SKFileStream inputStream2 = new SKFileStream("Images/Earth.jpg");
            using SKFileStream inputStream3 = new SKFileStream("Images/World.jpg");
            using SKBitmap bitmap1 = SKBitmap.Decode(inputStream1);
            using SKBitmap bitmap2 = SKBitmap.Decode(inputStream2);
            using SKBitmap bitmap3 = SKBitmap.Decode(inputStream3);

            SKBitmap[] bitmaps = { bitmap1, bitmap2, bitmap3 };
            using SKBitmap mergeBitmap = bitmaps.MergeBitmapsVertically();

            using FileStream outputStream = File.OpenWrite("Images/MergedVertically.jpg");
            mergeBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion
    }
}
