using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using SkiaSharp;
using System.IO;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// SkiaSharp测试
    /// </summary>
    [TestClass]
    public class SkiaSharpTests
    {
        #region # 测试映射SKBitmap —— void TestToSKBitmap()
        /// <summary>
        /// 测试直方图
        /// </summary>
        [TestMethod]
        public void TestToSKBitmap()
        {
            using Mat matrix = Cv2.ImRead("Images/Earth.jpg");
            using SKBitmap bitmap = matrix.ToSKBitmap(SKEncodedImageFormat.Png);

            using FileStream outputStream = File.OpenWrite("Images/Earth.SK.png");
            bitmap.Encode(SKEncodedImageFormat.Png, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试映射OpenCV矩阵 —— void TestToCVMatrix()
        /// <summary>
        /// 测试映射OpenCV矩阵
        /// </summary>
        [TestMethod]
        public void TestToCVMatrix()
        {
            using SKFileStream inputStream = new SKFileStream("Images/Earth.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);
            using Mat matrix = bitmap.ToCVMatrix(SKEncodedImageFormat.Png);

            matrix.SaveImage("Images/Earth.CV.png");
        }
        #endregion
    }
}
