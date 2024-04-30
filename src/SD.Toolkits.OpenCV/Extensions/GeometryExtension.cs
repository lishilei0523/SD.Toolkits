using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.OpenCV.Extensions
{
    /// <summary>
    /// 几何变换扩展
    /// </summary>
    public static class GeometryExtension
    {
        #region # 仿射变换 —— static Mat AffineTrans(this Mat matrix, IEnumerable<Point2f> sourcePoints...
        /// <summary>
        /// 仿射变换
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sourcePoints">源定位点集</param>
        /// <param name="targetPoints">目标定位点集</param>
        /// <returns>变换后图像矩阵</returns>
        /// <remarks>源/目标定位点必须大于等于3个，且数量必须相等</remarks>
        public static Mat AffineTrans(this Mat matrix, IEnumerable<Point2f> sourcePoints, IEnumerable<Point2f> targetPoints)
        {
            #region # 验证

            sourcePoints = sourcePoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            targetPoints = targetPoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            if (sourcePoints.Count() < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(sourcePoints), "源定位点集数量不可小于3！");
            }
            if (targetPoints.Count() < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(targetPoints), "目标定位点集数量不可小于3！");
            }
            if (sourcePoints.Count() != targetPoints.Count())
            {
                throw new ArgumentOutOfRangeException("源定位点集数量与目标定位点集数量必须相等！", (Exception)null);
            }

            #endregion

            using Mat affineMatrix = Cv2.GetAffineTransform(sourcePoints, targetPoints);
            Mat result = new Mat();
            Cv2.WarpAffine(matrix, result, affineMatrix, matrix.Size());

            return result;
        }
        #endregion

        #region # 透射变换 —— static Mat PerspectiveTrans(this Mat matrix, IEnumerable<Point2f> sourcePoints...
        /// <summary>
        /// 透射变换
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sourcePoints">源定位点集</param>
        /// <param name="targetPoints">目标定位点集</param>
        /// <returns>变换后图像矩阵</returns>
        /// <remarks>源/目标定位点必须大于等于4个，且数量必须相等</remarks>
        public static Mat PerspectiveTrans(this Mat matrix, IEnumerable<Point2f> sourcePoints, IEnumerable<Point2f> targetPoints)
        {
            #region # 验证

            sourcePoints = sourcePoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            targetPoints = targetPoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            if (sourcePoints.Count() < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(sourcePoints), "源定位点集数量不可小于4！");
            }
            if (targetPoints.Count() < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(targetPoints), "目标定位点集数量不可小于4！");
            }
            if (sourcePoints.Count() != targetPoints.Count())
            {
                throw new ArgumentOutOfRangeException("源定位点集数量与目标定位点集数量必须相等！", (Exception)null);
            }

            #endregion

            using Mat perspectiveMatrix = Cv2.GetPerspectiveTransform(sourcePoints, targetPoints);
            Mat result = new Mat();
            Cv2.WarpPerspective(matrix, result, perspectiveMatrix, matrix.Size());

            return result;
        }
        #endregion

        #region # 透射变换 —— static Point2f[] PerspectiveTrans(this IEnumerable<Point2f> contourPoints...
        /// <summary>
        /// 透射变换
        /// </summary>
        /// <param name="contourPoints">轮廓坐标点集</param>
        /// <param name="sourcePoints">源定位点集</param>
        /// <param name="targetPoints">目标定位点集</param>
        /// <returns>变换后轮廓坐标点集</returns>
        /// <remarks>源/目标定位点必须大于等于4个，且数量必须相等</remarks>
        public static Point2f[] PerspectiveTrans(this IEnumerable<Point2f> contourPoints, IEnumerable<Point2f> sourcePoints, IEnumerable<Point2f> targetPoints)
        {
            #region # 验证

            sourcePoints = sourcePoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            targetPoints = targetPoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            if (sourcePoints.Count() < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(sourcePoints), "源定位点集数量不可小于4！");
            }
            if (targetPoints.Count() < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(targetPoints), "目标定位点集数量不可小于4！");
            }
            if (sourcePoints.Count() != targetPoints.Count())
            {
                throw new ArgumentOutOfRangeException("源定位点集数量与目标定位点集数量必须相等！", (Exception)null);
            }

            #endregion

            using Mat perspectiveMatrix = Cv2.GetPerspectiveTransform(sourcePoints, targetPoints);
            Point2f[] targetCoutour = Cv2.PerspectiveTransform(contourPoints, perspectiveMatrix);

            return targetCoutour;
        }
        #endregion

        #region # 透射变换 —— static Point2d[] PerspectiveTrans(this IEnumerable<Point2d> contourPoints...
        /// <summary>
        /// 透射变换
        /// </summary>
        /// <param name="contourPoints">轮廓坐标点集</param>
        /// <param name="sourcePoints">源定位点集</param>
        /// <param name="targetPoints">目标定位点集</param>
        /// <returns>变换后轮廓坐标点集</returns>
        /// <remarks>源/目标定位点必须大于等于4个，且数量必须相等</remarks>
        public static Point2d[] PerspectiveTrans(this IEnumerable<Point2d> contourPoints, IEnumerable<Point2f> sourcePoints, IEnumerable<Point2f> targetPoints)
        {
            #region # 验证

            sourcePoints = sourcePoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            targetPoints = targetPoints?.Distinct().ToArray() ?? Array.Empty<Point2f>();
            if (sourcePoints.Count() < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(sourcePoints), "源定位点集数量不可小于4！");
            }
            if (targetPoints.Count() < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(targetPoints), "目标定位点集数量不可小于4！");
            }
            if (sourcePoints.Count() != targetPoints.Count())
            {
                throw new ArgumentOutOfRangeException("源定位点集数量与目标定位点集数量必须相等！", (Exception)null);
            }

            #endregion

            using Mat perspectiveMatrix = Cv2.GetPerspectiveTransform(sourcePoints, targetPoints);
            Point2d[] targetCoutour = Cv2.PerspectiveTransform(contourPoints, perspectiveMatrix);

            return targetCoutour;
        }
        #endregion

        #region # 距离变换 —— static Mat DistanceTrans(this Mat matrix)
        /// <summary>
        /// 距离变换
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <returns>距离变换图像矩阵</returns>
        public static Mat DistanceTrans(this Mat matrix)
        {
            //阈值分割
            using Mat binaryMatrix = new Mat();
            Cv2.Threshold(matrix, binaryMatrix, 0, 255, ThresholdTypes.BinaryInv | ThresholdTypes.Otsu);

            //距离变换
            Mat distanceMatrix = new Mat();
            Cv2.DistanceTransform(binaryMatrix, distanceMatrix, DistanceTypes.L2, DistanceTransformMasks.Mask3);

            //归一化
            Cv2.Normalize(distanceMatrix, distanceMatrix, 0, 1, NormTypes.MinMax);

            return distanceMatrix;
        }
        #endregion
    }
}
