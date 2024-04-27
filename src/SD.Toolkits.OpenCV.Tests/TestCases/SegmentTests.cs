using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Extensions;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// 分割测试
    /// </summary>
    [TestClass]
    public class SegmentTests
    {
        #region # 测试生成掩膜 —— void TestGenerateMask()
        /// <summary>
        /// 测试生成掩膜
        /// </summary>
        [TestMethod]
        public void TestGenerateMask()
        {
            using Mat matrix = Cv2.ImRead("Images/Ballon.jpg");

            Rect rectangle = new Rect(140, 45, 330, 335);
            using Mat mask = matrix.GenerateMask(rectangle);

            Cv2.ImShow("OpenCV生成掩膜-原图", matrix);
            Cv2.ImShow("OpenCV生成掩膜-掩膜", mask);
            Cv2.WaitKey();
        }
        #endregion

        #region # 测试适用掩膜 —— void TestApplyMask()
        /// <summary>
        /// 测试适用掩膜
        /// </summary>
        [TestMethod]
        public void TestApplyMask()
        {
            using Mat matrix = Cv2.ImRead("Images/Ballon.jpg");

            Rect rectangle = new Rect(140, 45, 330, 335);
            using Mat result = matrix.ApplyMask(rectangle);

            Cv2.ImShow("OpenCV适用分割-原图", matrix);
            Cv2.ImShow("OpenCV适用掩膜-效果图", result);
            Cv2.WaitKey();
        }
        #endregion

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
