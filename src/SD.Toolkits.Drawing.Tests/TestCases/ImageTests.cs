﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiaSharp;
using System;
using System.IO;

namespace SD.Toolkits.Drawing.Tests.TestCases
{
    /// <summary>
    /// 图像测试
    /// </summary>
    [TestClass]
    public class ImageTests
    {
        #region # 测试调整质量 —— void TestTuneQuality()
        /// <summary>
        /// 测试调整质量
        /// </summary>
        [TestMethod]
        public void TestTuneQuality()
        {
            using SKFileStream inputStream = new SKFileStream("Images/Earth.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);
            using SKBitmap tunedBitmap = bitmap.TuneQuality(30);

            using FileStream outputStream = File.OpenWrite("Images/Earth.Tuned.jpg");
            tunedBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试剪裁图像 —— void TestClipBitmap()
        /// <summary>
        /// 测试剪裁图像
        /// </summary>
        [TestMethod]
        public void TestClipBitmap()
        {
            using SKFileStream inputStream = new SKFileStream("Images/Earth.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);
            using SKBitmap clippedBitmap = bitmap.ClipBitmap(SKRectI.Create(0, 540, 1920, 540));

            using FileStream outputStream = File.OpenWrite("Images/Earth.Clipping.jpg");
            clippedBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试剪裁图像 —— void TestClipBitmaps()
        /// <summary>
        /// 测试剪裁图像
        /// </summary>
        [TestMethod]
        public void TestClipBitmaps()
        {
            using SKFileStream inputStream = new SKFileStream("Images/Earth.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);

            SKRectI rectangle1 = SKRectI.Create(0, 0, 960, 540);
            SKRectI rectangle2 = SKRectI.Create(0, 540, 960, 540);
            SKRectI rectangle3 = SKRectI.Create(960, 0, 960, 540);
            SKRectI rectangle4 = SKRectI.Create(960, 540, 960, 540);
            SKBitmap[] clippedBitmaps = bitmap.ClipBitmaps(new[] { rectangle1, rectangle2, rectangle3, rectangle4 });
            foreach (SKBitmap clippedBitmap in clippedBitmaps)
            {
                int index = Array.IndexOf(clippedBitmaps, clippedBitmap);
                using FileStream outputStream = File.OpenWrite($"Images/Earth.Clipping{index}.jpg"); clippedBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
                clippedBitmap.Dispose();
            }
        }
        #endregion

        #region # 测试制作缩略图 —— void TestThumbnail()
        /// <summary>
        /// 测试制作缩略图
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

        #region # 测试制作文字水印 —— void TestTextWatermark()
        /// <summary>
        /// 测试制作文字水印
        /// </summary>
        [TestMethod]
        public void TestTextWatermark()
        {
            using SKFileStream inputStream = new SKFileStream("Images/World.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);
            using SKBitmap watermarkBitmap = bitmap.MakeTextWatermark("Hello World!", SKColors.Red.WithAlpha(128));

            using FileStream outputStream = File.OpenWrite("Images/World.Watermark.jpg");
            watermarkBitmap.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试制作图像水印 —— void TestImageWatermark()
        /// <summary>
        /// 测试制作图像水印
        /// </summary>
        [TestMethod]
        public void TestImageWatermark()
        {
            using SKFileStream watermarkStream = new SKFileStream("Images/Avatar.jpg");
            using SKFileStream bitmapStream = new SKFileStream("Images/China.jpg");
            using SKBitmap watermark = SKBitmap.Decode(watermarkStream);
            using SKBitmap bitmap = SKBitmap.Decode(bitmapStream);
            using SKBitmap watermarkBitmap = bitmap.MakeImageWatermark(watermark, 100, 100);

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