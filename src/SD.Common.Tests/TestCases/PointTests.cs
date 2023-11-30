using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 坐标点测试
    /// </summary>
    [TestClass]
    public class PointTests
    {
        #region # 测试去重相似点 —— void TestDistinctSimilarPoints()
        /// <summary>
        /// 测试去重相似点
        /// </summary>
        [TestMethod]
        public void TestDistinctSimilarPoints()
        {
            const int threshold = 5;
            Point a = new Point(10, 20);
            Point b = new Point(12, 22);
            Point c = new Point(52, 99);
            Point d = new Point(300, 500);
            Point e = new Point(11, 210);
            Point f = new Point(48, 101);
            Point g = new Point(50, 100);
            Point[] points = { a, b, c, d, e, f, g };

            IEqualityComparer<Point> comparer = new SimilarPointsComparer(threshold, threshold);
            ICollection<Point> distinctedPoints = new HashSet<Point>(points, comparer);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(distinctedPoints.Count, 4);
        }
        #endregion
    }
}
