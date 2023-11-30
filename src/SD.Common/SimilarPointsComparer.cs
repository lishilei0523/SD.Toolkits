using System;
using System.Collections.Generic;
using System.Drawing;

namespace SD.Common
{
    /// <summary>
    /// 相似点比较器
    /// </summary>
    public class SimilarPointsComparer : IEqualityComparer<Point>
    {
        #region # 构造器

        #region 01.创建相似点比较器构造器
        /// <summary>
        /// 创建相似点比较器构造器
        /// </summary>
        /// <param name="thresholdX">X阈值</param>
        /// <param name="thresholdY">Y阈值</param>
        public SimilarPointsComparer(int thresholdX, int thresholdY)
        {
            this.ThresholdX = thresholdX;
            this.ThresholdY = thresholdY;
        }
        #endregion

        #endregion

        #region # 属性

        #region X阈值 —— int ThresholdX
        /// <summary>
        /// X阈值
        /// </summary>
        public int ThresholdX { get; private set; }
        #endregion

        #region Y阈值 —— int ThresholdY
        /// <summary>
        /// Y阈值
        /// </summary>
        public int ThresholdY { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 是否相似 —— bool Equals(Point source, Point target)
        /// <summary>
        /// 是否相似
        /// </summary>
        /// <param name="source">源点</param>
        /// <param name="target">目标点</param>
        /// <returns>是否相似</returns>
        public bool Equals(Point source, Point target)
        {
            int offsetX = Math.Abs(target.X - source.X);
            int offsetY = Math.Abs(target.Y - source.Y);
            if (offsetX > this.ThresholdX || offsetY > this.ThresholdY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 获取哈希码 —— int GetHashCode(Point point)
        /// <summary>
        /// 获取哈希码
        /// </summary>
        public int GetHashCode(Point point)
        {
            return 0;
        }
        #endregion 

        #endregion
    }
}
