using OpenCvSharp;
using SD.Toolkits.OpenCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.OpenCV.Reconstructions
{
    /// <summary>
    /// Super特征工程
    /// </summary>
    public static class SuperEngineer
    {
        #region # 自适应缩放图像 —— static Mat ScaleImageAdaptively(Mat image, int scaledSize)
        /// <summary>
        /// 自适应缩放图像
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="scaledSize">缩放尺寸</param>
        /// <returns>缩放图像</returns>
        /// <remarks>缩放为正方形缩放，缩放尺寸为目标边长</remarks>
        public static Mat ScaleImageAdaptively(this Mat image, int scaledSize)
        {
            Mat scaledImage = new Mat();
            if (image.Width > image.Height)
            {
                int width = scaledSize;
                int height = (int)Math.Ceiling(scaledSize * 1.0 / image.Width * image.Height);
                Size size = new Size(width, height);
                Cv2.Resize(image, scaledImage, size, 0, 0, InterpolationFlags.Area);

                int surplusY = (width - height) / 2;
                Cv2.CopyMakeBorder(scaledImage, scaledImage, surplusY, surplusY, 0, 0, BorderTypes.Constant);
            }
            else
            {
                int height = scaledSize;
                int width = (int)Math.Ceiling(scaledSize * 1.0 / image.Height * image.Width);
                Size size = new Size(width, height);
                Cv2.Resize(image, scaledImage, size, 0, 0, InterpolationFlags.Area);

                int surplusX = (height - width) / 2;
                Cv2.CopyMakeBorder(scaledImage, scaledImage, 0, 0, surplusX, surplusX, BorderTypes.Constant);
            }

            return scaledImage;
        }
        #endregion

        #region # 缩放关键点列表 —— static IList<KeyPoint> ScaleKeyPoints(this IEnumerable<KeyPoint>...
        /// <summary>
        /// 缩放关键点列表
        /// </summary>
        /// <param name="keyPoints">关键点列表</param>
        /// <param name="imageWidth">图像宽度</param>
        /// <param name="imageHeight">图像高度</param>
        /// <param name="scaledSize">缩放尺寸</param>
        /// <returns>缩放关键点列表</returns>
        /// <remarks>缩放为正方形缩放，缩放尺寸为目标边长</remarks>
        public static IList<KeyPoint> ScaleKeyPoints(this IEnumerable<KeyPoint> keyPoints, int imageWidth,
            int imageHeight, int scaledSize)
        {
            #region # 验证

            keyPoints = keyPoints?.ToArray() ?? Array.Empty<KeyPoint>();
            if (!keyPoints.Any())
            {
                return Array.Empty<KeyPoint>();
            }

            #endregion

            int surplusX = 0;
            int surplusY = 0;
            if (imageWidth > imageHeight)
            {
                int width = scaledSize;
                int height = (int)Math.Ceiling(scaledSize * 1.0 / imageWidth * imageHeight);
                surplusY = (width - height) / 2;
            }
            else
            {
                int height = scaledSize;
                int width = (int)Math.Ceiling(scaledSize * 1.0 / imageHeight * imageWidth);
                surplusX = (height - width) / 2;
            }

            float scaleX = imageWidth * 1.0f / (scaledSize - surplusX * 2);
            float scaleY = imageHeight * 1.0f / (scaledSize - surplusY * 2);

            IList<KeyPoint> scaledKeyPoints = new List<KeyPoint>();
            foreach (KeyPoint keyPoint in keyPoints)
            {
                Point2f scaledPoint = new Point2f(keyPoint.Pt.X, keyPoint.Pt.Y);
                scaledPoint.X -= surplusX;
                scaledPoint.Y -= surplusY;
                scaledPoint.X *= scaleX;
                scaledPoint.Y *= scaleY;

                KeyPoint scaledKeyPoint = new KeyPoint(scaledPoint, 0);
                scaledKeyPoints.Add(scaledKeyPoint);
            }

            return scaledKeyPoints;
        }
        #endregion

        #region # 解析匹配结果 —— static MatchResult ResolveMatchResult(this IEnumerable<DMatch> matches...
        /// <summary>
        /// 解析匹配结果
        /// </summary>
        /// <param name="matches">OpenCV匹配结果集</param>
        /// <param name="sourceKeyPoints">源关键点集</param>
        /// <param name="targetKeyPoints">目标关键点集</param>
        /// <returns>匹配结果</returns>
        public static MatchResult ResolveMatchResult(this IEnumerable<DMatch> matches, IEnumerable<KeyPoint> sourceKeyPoints, IEnumerable<KeyPoint> targetKeyPoints)
        {
            #region # 验证

            matches = matches?.ToArray() ?? Array.Empty<DMatch>();
            sourceKeyPoints = sourceKeyPoints?.ToArray() ?? Array.Empty<KeyPoint>();
            targetKeyPoints = targetKeyPoints?.ToArray() ?? Array.Empty<KeyPoint>();
            if (!matches.Any() || !sourceKeyPoints.Any() || !targetKeyPoints.Any())
            {
                return new MatchResult(0, new Dictionary<int, KeyPoint>(), new Dictionary<int, KeyPoint>());
            }

            #endregion

            IDictionary<int, KeyPoint> matchedSourceKeyPoints = new Dictionary<int, KeyPoint>();
            IDictionary<int, KeyPoint> matchedTargetKeyPoints = new Dictionary<int, KeyPoint>();
            foreach (DMatch goodMatch in matches)
            {
                matchedSourceKeyPoints.Add(goodMatch.QueryIdx, sourceKeyPoints.ElementAt(goodMatch.QueryIdx));
                matchedTargetKeyPoints.Add(goodMatch.TrainIdx, targetKeyPoints.ElementAt(goodMatch.TrainIdx));
            }

            MatchResult matchResult = new MatchResult(matches.Count(), matchedSourceKeyPoints, matchedTargetKeyPoints);

            return matchResult;
        }
        #endregion

        #region # 匹配结果转DMatch列表 —— static ICollection<DMatch> ToDMatches(this MatchResult matchResult)
        /// <summary>
        /// 匹配结果转DMatch列表
        /// </summary>
        /// <param name="matchResult">匹配结果</param>
        /// <returns>DMatch列表</returns>
        public static ICollection<DMatch> ToDMatches(this MatchResult matchResult)
        {
            IList<DMatch> dMatches = new List<DMatch>();
            for (int index = 0; index < matchResult.MatchedCount; index++)
            {
                KeyValuePair<int, KeyPoint> srcKv = matchResult.MatchedSourceKeyPoints.ElementAt(index);
                KeyValuePair<int, KeyPoint> tgtKv = matchResult.MatchedTargetKeyPoints.ElementAt(index);
                DMatch dMatch = new DMatch(srcKv.Key, tgtKv.Key, 0);
                dMatches.Add(dMatch);
            }

            return dMatches;
        }
        #endregion

        #region # 获取图像特征 —— static float[] GetImageFeatures(this Mat image)
        /// <summary>
        /// 获取图像特征
        /// </summary>
        /// <param name="image">图像</param>
        /// <returns>图像特征数组</returns>
        public static float[] GetImageFeatures(this Mat image)
        {
            Mat image32FC1;
            if (image.Type() != MatType.CV_32FC1)
            {
                image32FC1 = new Mat();
                image.ConvertTo(image32FC1, MatType.CV_32FC1);
            }
            else
            {
                image32FC1 = image.Clone();
            }

            IList<float> imageFeatures = new List<float>();
            for (int rowIndex = 0; rowIndex < image.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < image.Cols; colIndex++)
                {
                    imageFeatures.Add(image32FC1.At<float>(rowIndex, colIndex) / 255.0f);
                }
            }

            //释放资源
            image32FC1.Dispose();

            return imageFeatures.ToArray();
        }
        #endregion

        #region # 获取关键点特征 —— static float[] GetKeyPointsFeatures(IEnumerable<long> keyPoints)
        /// <summary>
        /// 获取关键点特征
        /// </summary>
        /// <param name="keyPoints">关键点列表</param>
        /// <returns>关键点特征数组</returns>
        public static float[] GetKeyPointsFeatures(IEnumerable<long> keyPoints)
        {
            #region # 验证

            keyPoints = keyPoints?.ToArray() ?? Array.Empty<long>();
            if (!keyPoints.Any())
            {
                return Array.Empty<float>();
            }

            #endregion

            float[] keyPointsFeatures = new float[keyPoints.Count()];
            for (int index = 0; index < keyPoints.Count(); index++)
            {
                keyPointsFeatures[index] = (keyPoints.ElementAt(index) - 256) / 256.0f;
            }

            return keyPointsFeatures;
        }
        #endregion
    }
}
