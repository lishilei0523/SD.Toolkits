using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Extensions;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// Retinex算法测试
    /// </summary>
    [TestClass]
    public class RetinexTests
    {
        #region # 测试单尺度Retinex增强 —— void TestSingleScaleRetinex()
        /// <summary>
        /// 测试单尺度Retinex增强
        /// </summary>
        [TestMethod]
        public void TestSingleScaleRetinex()
        {
            using Mat matrix = Cv2.ImRead("Images/Cloud.jpg");
            using Mat result = matrix.SingleScaleRetinex(80);

            Cv2.ImShow("OpenCV SSR-原图", matrix);
            Cv2.ImShow("OpenCV SSR-效果图", result);
            Cv2.WaitKey();
        }
        #endregion
    }
}
