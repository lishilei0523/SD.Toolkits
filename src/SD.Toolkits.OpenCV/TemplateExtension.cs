using OpenCvSharp;
using System;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// 模板扩展
    /// </summary>
    public static class TemplateExtension
    {
        #region # 滑动窗口 —— static void SlideWindow(this Mat matrix, int windowWidth, int windowHeight...
        /// <summary>
        /// 滑动窗口
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="windowWidth">窗口宽度</param>
        /// <param name="windowHeight">窗口高度</param>
        /// <param name="step">步长</param>
        /// <param name="action">操作</param>
        public static void SlideWindow(this Mat matrix, int windowWidth, int windowHeight, int step, Action<Rect, Mat> action)
        {
            for (int colIndex = 0; colIndex < matrix.Cols - step; colIndex += step)
            {
                for (int rowIndex = 0; rowIndex < matrix.Rows - step; rowIndex += step)
                {
                    int x = colIndex;
                    int y = rowIndex;
                    if (x + windowWidth > matrix.Width)
                    {
                        x -= x + windowWidth - matrix.Width;
                    }
                    if (y + windowHeight > matrix.Height)
                    {
                        y -= y + windowHeight - matrix.Height;
                    }

                    Rect window = new Rect(x, y, windowWidth, windowHeight);
                    Mat roi = matrix[window];
                    action.Invoke(window, roi);
                }
            }
        }
        #endregion
    }
}
