using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// OpenCV测试
    /// </summary>
    [TestClass]
    public class OpenCVTests
    {
        #region # 测试直方图 —— void TestHistogram()
        /// <summary>
        /// 测试直方图
        /// </summary>
        [TestMethod]
        public void TestHistogram()
        {
            using Mat matrix = Cv2.ImRead("Images/Cat.jpg", ImreadModes.Grayscale);
            using Mat histogramImage = matrix.MakeHistogram();

            Cv2.ImShow("OpenCV计算直方图-原图", matrix);
            Cv2.ImShow("OpenCV计算直方图-直方图", histogramImage);
            Cv2.WaitKey();
        }
        #endregion
    }
}
