using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using System;

namespace SD.Toolkits.OpenCV.Tests.TestCases
{
    /// <summary>
    /// 模板测试
    /// </summary>
    [TestClass]
    public class TemplateTests
    {
        #region # 测试滑动窗口 —— void TestSlideWindow()
        /// <summary>
        /// 测试线性变换
        /// </summary>
        [TestMethod]
        public void TestSlideWindow()
        {
            Action<Rect, Mat> action = (rect, roi) =>
            {
                roi.SaveImage($"Images/{rect.X}-{rect.Y}.jpg");
            };

            using Mat matrix = Cv2.ImRead("Images/Lena.jpg");
            for (int i = 0; i < 10; i++)
            {
                matrix.SlideWindow(200, 200, 50, action);
            }

            Cv2.WaitKey();
        }
        #endregion
    }
}
