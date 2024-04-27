using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// 几何变换测试
    /// </summary>
    [TestClass]
    public class GeometryTests
    {
        #region # 测试提取轮廓内图像 —— void TestExtractMatrixInContour()
        /// <summary>
        /// 测试提取轮廓内图像
        /// </summary>
        [TestMethod]
        public void TestExtractMatrixInContour()
        {
            using Mat matrix = Cv2.ImRead("Images/China.jpg", ImreadModes.Grayscale);

            Point[] contourPoints =
            {
                new Point(100, 100),
                new Point(1000, 150),
                new Point(1200, 600),
                new Point(400, 700)
            };
            using Mat contourMatrix = matrix.ExtractMatrixInContour(contourPoints);

            Cv2.ImShow("OpenCV提取轮廓内图像-原图", matrix);
            Cv2.ImShow("OpenCV提取轮廓内图像-效果图", contourMatrix);
            Cv2.WaitKey();
        }
        #endregion

        #region # 测试提取轮廓内像素 —— void TestExtractMatrixInContour()
        /// <summary>
        /// 测试提取轮廓内像素
        /// </summary>
        [TestMethod]
        public void TestExtractPixelsInContour()
        {
            using Mat matrix = Cv2.ImRead("Images/China.jpg", ImreadModes.Grayscale);

            Point[] contourPoints =
            {
                new Point(100, 100),
                new Point(1000, 150),
                new Point(1200, 600),
                new Point(400, 700)
            };
            IEnumerable<byte> pixels = matrix.ExtractPixelsInContour(contourPoints);
            Assert.IsTrue(pixels.Any());
        }
        #endregion

        #region # 测试仿射变换 —— void TestAffineTrans()
        /// <summary>
        /// 测试仿射变换
        /// </summary>
        [TestMethod]
        public void TestAffineTrans()
        {
            using Mat matrix = Cv2.ImRead("Images/Chessboard.jpg");

            Point2f[] sourcePoints =
            {
                new Point2f(50, 50),
                new Point2f(200, 50),
                new Point2f(50, 200)
            };
            Point2f[] targetPoints =
            {
                new Point2f(100, 100),
                new Point2f(200, 50),
                new Point2f(100, 250)
            };
            using Mat result = matrix.AffineTrans(sourcePoints, targetPoints);

            Cv2.ImShow("OpenCV仿射变换-原图", matrix);
            Cv2.ImShow("OpenCV仿射变换-效果图", result);
            Cv2.WaitKey();
        }
        #endregion

        #region # 测试透射变换 —— void TestPerspectiveTrans()
        /// <summary>
        /// 测试透射变换
        /// </summary>
        [TestMethod]
        public void TestPerspectiveTrans()
        {
            using Mat matrix = Cv2.ImRead("Images/Chessboard.jpg");

            Point2f[] sourcePoints =
            {
                new Point2f(56, 65),
                new Point2f(368, 52),
                new Point2f(28, 387),
                new Point2f(389, 390)
            };
            Point2f[] targetPoints =
            {
                new Point2f(100, 145),
                new Point2f(300, 100),
                new Point2f(80, 290),
                new Point2f(310, 300)
            };
            using Mat result = matrix.PerspectiveTrans(sourcePoints, targetPoints);

            Cv2.ImShow("OpenCV透射变换-原图", matrix);
            Cv2.ImShow("OpenCV透射变换-效果图", result);
            Cv2.WaitKey();
        }
        #endregion
    }
}
