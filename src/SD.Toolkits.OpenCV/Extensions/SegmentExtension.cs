using OpenCvSharp;

namespace SD.Toolkits.OpenCV.Extensions
{
    /// <summary>
    /// 分割扩展
    /// </summary>
    public static class SegmentExtension
    {
        #region # 生成掩膜 —— static Mat GenerateMask(this Mat matrix, Rect rectangle)
        /// <summary>
        /// 生成掩膜
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="rectangle">矩形</param>
        /// <returns>掩膜矩阵</returns>
        public static Mat GenerateMask(this Mat matrix, Rect rectangle)
        {
            Mat mask = Mat.Zeros(matrix.Size(), MatType.CV_8UC1);
            mask[rectangle].SetTo(255);

            return mask;
        }
        #endregion

        #region # 适用掩膜 —— static Mat ApplyMask(this Mat matrix, Rect rectangle)
        /// <summary>
        /// 适用掩膜
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="rectangle">矩形</param>
        /// <returns>结果图像矩阵</returns>
        public static Mat ApplyMask(this Mat matrix, Rect rectangle)
        {
            using Mat mask = matrix.GenerateMask(rectangle);
            Mat result = new Mat();
            matrix.CopyTo(result, mask);

            return result;
        }
        #endregion

        #region # 颜色分割 —— static Mat ColorSegment(this Mat hsvMatrix, Scalar lowerScalar...
        /// <summary>
        /// 颜色分割
        /// </summary>
        /// <param name="hsvMatrix">HSV图像矩阵</param>
        /// <param name="lowerScalar">颜色下限</param>
        /// <param name="upperScalar">颜色上限</param>
        /// <returns>分割结果图像矩阵</returns>
        public static Mat ColorSegment(this Mat hsvMatrix, Scalar lowerScalar, Scalar upperScalar)
        {
            using Mat mask = new Mat();
            Cv2.InRange(hsvMatrix, lowerScalar, upperScalar, mask);

            Mat result = new Mat();
            Cv2.BitwiseAnd(hsvMatrix, hsvMatrix, result, mask);
            result = result.CvtColor(ColorConversionCodes.HSV2BGR);

            return result;
        }
        #endregion
    }
}
