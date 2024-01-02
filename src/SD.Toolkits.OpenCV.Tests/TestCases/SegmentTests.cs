using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// 分割测试
    /// </summary>
    [TestClass]
    public class SegmentTests
    {
        #region # 测试颜色分割 —— void TestColorSegment()
        /// <summary>
        /// 测试颜色分割
        /// </summary>
        [TestMethod]
        public void TestColorSegment()
        {
            Scalar lowerYellow = new Scalar(20, 43, 46);
            Scalar upperYellow = new Scalar(34, 255, 255);

            using Mat matrix = Cv2.ImRead("Images/Flag.jpg");
            using Mat hsvMatrix = matrix.CvtColor(ColorConversionCodes.BGR2HSV);
            Mat result = hsvMatrix.ColorSegment(lowerYellow, upperYellow);

            Cv2.ImShow("OpenCV颜色分割-原图", matrix);
            Cv2.ImShow("OpenCV颜色分割-效果图", result);
            Cv2.WaitKey();
        }
        #endregion
    }
}
