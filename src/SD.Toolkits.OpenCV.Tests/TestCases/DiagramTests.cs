using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// 图表测试
    /// </summary>
    [TestClass]
    public class DiagramTests
    {
        #region # 测试灰度直方图 —— void TestGrayHistogram()
        /// <summary>
        /// 测试灰度直方图
        /// </summary>
        [TestMethod]
        public void TestGrayHistogram()
        {
            using Mat matrix = Cv2.ImRead("Images/Cat.jpg", ImreadModes.Grayscale);
            using Mat histogramImage = matrix.GenerateGrayHistogram();

            Cv2.ImShow("OpenCV计算直方图-原图", matrix);
            Cv2.ImShow("OpenCV计算直方图-直方图", histogramImage);
            Cv2.WaitKey();
        }
        #endregion

        #region # 测试频率谱图 —— void TestFrequencySpectrum()
        /// <summary>
        /// 测试频率谱图
        /// </summary>
        [TestMethod]
        public void TestFrequencySpectrum()
        {
            using Mat matrix = Cv2.ImRead("Images/Deer.jpg", ImreadModes.Grayscale);
            using Mat magnitudeMatrix = matrix.GenerateFrequencySpectrum();

            Cv2.ImShow("OpenCV傅里叶变换-原图", matrix);
            Cv2.ImShow("OpenCV傅里叶变换-频率谱图", magnitudeMatrix);
            Cv2.WaitKey();
        }
        #endregion

        #region # 测试相位谱图 —— void TestPhaseSpectrum()
        /// <summary>
        /// 测试相位谱图
        /// </summary>
        [TestMethod]
        public void TestPhaseSpectrum()
        {
            using Mat matrix = Cv2.ImRead("Images/Deer.jpg", ImreadModes.Grayscale);
            using Mat phaseMatrix = matrix.GeneratePhaseSpectrum();

            Cv2.ImShow("OpenCV傅里叶变换-原图", matrix);
            Cv2.ImShow("OpenCV傅里叶变换-相位谱图", phaseMatrix);
            Cv2.WaitKey();
        }
        #endregion
    }
}
