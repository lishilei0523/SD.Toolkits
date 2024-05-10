using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using SD.Toolkits.OpenCV.SkiaSharp;
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
            using Mat matrix = Cv2.ImRead("Images/China.jpg");
            using SKBitmap bitmap = matrix.ToSKBitmap();

            using FileStream outputStream = File.OpenWrite("Images/China.SK.jpg");
            bitmap.Encode(SKEncodedImageFormat.Png, 80).SaveTo(outputStream);
        }
        #endregion

        #region # 测试映射OpenCV矩阵 —— void TestToMat()
        /// <summary>
        /// 测试映射OpenCV矩阵
        /// </summary>
        [TestMethod]
        public void TestToMat()
        {
            using SKFileStream inputStream = new SKFileStream("Images/China.jpg");
            using SKBitmap bitmap = SKBitmap.Decode(inputStream);

            using Mat matrix = bitmap.ToMat();
            matrix.SaveImage("Images/China.CV.png");
        }
        #endregion

        #region # 测试直方图图像 —— void TestHistogramImage()
        /// <summary>
        /// 测试直方图图像
        /// </summary>
        [TestMethod]
        public void TestHistogramImage()
        {
            using Mat matrix = Cv2.ImRead("Images/Cat.jpg", ImreadModes.Grayscale);
            using Mat histogramImage = matrix.GenerateHistogramImage();

            Cv2.ImShow("OpenCV直方图图像-原图", matrix);
            Cv2.ImShow("OpenCV直方图图像-直方图", histogramImage);
            Cv2.WaitKey();
        }
        #endregion
    }
}
