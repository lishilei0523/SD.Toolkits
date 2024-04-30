using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Extensions;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// 直方图测试
    /// </summary>
    [TestClass]
    public class HistogramTests
    {
        #region # 测试映射直方图 —— void TestMapHistogram()
        /// <summary>
        /// 测试映射直方图
        /// </summary>
        [TestMethod]
        public void TestMapHistogram()
        {
            using Mat sourceMatrix = Cv2.ImRead("Images/Horse.jpg");
            using Mat referenceMatrix = Cv2.ImRead("Images/Cat.jpg");

            Cv2.Split(sourceMatrix, out Mat[] sourceChannels);
            Cv2.Split(referenceMatrix, out Mat[] referenceChannels);

            using Mat bChannel = HistogramExtension.MapHistogram(sourceChannels[0], referenceChannels[0]);
            using Mat gChannel = HistogramExtension.MapHistogram(sourceChannels[1], referenceChannels[1]);
            using Mat rChannel = HistogramExtension.MapHistogram(sourceChannels[2], referenceChannels[2]);

            using Mat result = new Mat();
            Mat[] channels = { bChannel, gChannel, rChannel };
            Cv2.Merge(channels, result);

            Cv2.ImShow("OpenCV映射直方图-原图1", sourceMatrix);
            Cv2.ImShow("OpenCV映射直方图-原图2", referenceMatrix);
            Cv2.ImShow("OpenCV映射直方图-效果图", result);
            Cv2.WaitKey();
        }
        #endregion

        #region # 测试自适应直方图均衡化 —— void TestAdaptiveEqualizeHist()
        /// <summary>
        /// 测试自适应直方图均衡化
        /// </summary>
        [TestMethod]
        public void TestAdaptiveEqualizeHist()
        {
            using Mat matrix = Cv2.ImRead("Images/Lena.jpg", ImreadModes.Grayscale);
            using Mat result = matrix.AdaptiveEqualizeHist();

            Cv2.ImShow("OpenCV自适应直方图均衡化-原图", matrix);
            Cv2.ImShow("OpenCV自适应直方图均衡化-效果图", result);
            Cv2.WaitKey();
        }
        #endregion
    }
}
