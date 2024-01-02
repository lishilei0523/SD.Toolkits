using OpenCvSharp;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// 分割扩展
    /// </summary>
    public static class SegmentExtension
    {
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
