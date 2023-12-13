using OpenCvSharp;
using System;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// 滤波扩展
    /// </summary>
    public static class BlurExtension
    {
        #region # 伽马校正 —— static Mat GammaCorrect(this Mat matrix, float gamma)
        /// <summary>
        /// 伽马校正
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="gamma">伽马值</param>
        /// <returns>校正图像矩阵</returns>
        public static unsafe Mat GammaCorrect(this Mat matrix, float gamma)
        {
            Mat result = matrix.Clone();

            byte[] bins = new byte[256];
            for (int index = 0; index < bins.Length; index++)
            {
                bins[index] = (byte)Math.Ceiling(Math.Pow(index / 255.0f, gamma) * 255.0f);
            }

            int channelCount = result.Channels();
            if (channelCount == 1)
            {
                result.ForEachAsByte((valuePrt, positionPtr) =>
                {
                    int rowIndex = positionPtr[0];
                    int colIndex = positionPtr[1];
                    result.At<byte>(rowIndex, colIndex) = bins[*valuePrt];
                });
            }
            if (channelCount == 3)
            {
                result.ForEachAsVec3b((valuePrt, positionPtr) =>
                {
                    int rowIndex = positionPtr[0];
                    int colIndex = positionPtr[1];
                    result.At<Vec3b>(rowIndex, colIndex)[0] = bins[(*valuePrt)[0]];
                    result.At<Vec3b>(rowIndex, colIndex)[1] = bins[(*valuePrt)[1]];
                    result.At<Vec3b>(rowIndex, colIndex)[2] = bins[(*valuePrt)[2]];
                });
            }

            return result;
        }
        #endregion

        #region # 理想低通滤波 —— static Mat IdealLPBlur(this Mat matrix, float sigma)
        /// <summary>
        /// 理想低通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat IdealLPBlur(this Mat matrix, float sigma)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateIdealLPKernel(borderedMatrix.Size(), sigma);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 理想高通滤波 —— static Mat IdealHPBlur(this Mat matrix, float sigma)
        /// <summary>
        /// 理想高通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat IdealHPBlur(this Mat matrix, float sigma)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateIdealHPKernel(borderedMatrix.Size(), sigma);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 理想带通滤波 —— static Mat IdealBPBlur(this Mat matrix, float sigma, float bandWidth)
        /// <summary>
        /// 理想带通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat IdealBPBlur(this Mat matrix, float sigma, float bandWidth)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateIdealBPKernel(borderedMatrix.Size(), sigma, bandWidth);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 理想带阻滤波 —— static Mat IdealBRBlur(this Mat matrix, float sigma, float bandWidth)
        /// <summary>
        /// 理想带阻滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat IdealBRBlur(this Mat matrix, float sigma, float bandWidth)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateIdealBRKernel(borderedMatrix.Size(), sigma, bandWidth);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 高斯低通滤波 —— static Mat GaussianLPBlur(this Mat matrix, float sigma)
        /// <summary>
        /// 高斯低通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat GaussianLPBlur(this Mat matrix, float sigma)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateGaussianLPKernel(borderedMatrix.Size(), sigma);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 高斯高通滤波 —— static Mat GaussianHPBlur(this Mat matrix, float sigma)
        /// <summary>
        /// 高斯高通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat GaussianHPBlur(this Mat matrix, float sigma)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateGaussianHPKernel(borderedMatrix.Size(), sigma);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 高斯带通滤波 —— static Mat GaussianBPBlur(this Mat matrix, float sigma, float bandWidth)
        /// <summary>
        /// 高斯带通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat GaussianBPBlur(this Mat matrix, float sigma, float bandWidth)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateGaussianBPKernel(borderedMatrix.Size(), sigma, bandWidth);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 高斯带阻滤波 —— static Mat GaussianBRBlur(this Mat matrix, float sigma, float bandWidth)
        /// <summary>
        /// 高斯带阻滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat GaussianBRBlur(this Mat matrix, float sigma, float bandWidth)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateGaussianBRKernel(borderedMatrix.Size(), sigma, bandWidth);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 高斯同态滤波 —— static Mat GaussianHomoBlur(this Mat matrix, float gammaH, float gammaL...
        /// <summary>
        /// 高斯同态滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="gammaH">高频增益</param>
        /// <param name="gammaL">低频增益</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="slope">滤波斜率</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat GaussianHomoBlur(this Mat matrix, float gammaH, float gammaL, float sigma, float slope)
        {
            Mat homoMatrix = matrix.Clone();
            homoMatrix.ConvertTo(homoMatrix, MatType.CV_32FC1);

            //对数化
            homoMatrix += Scalar.All(1);
            Cv2.Log(homoMatrix, homoMatrix);

            //频域滤波
            using Mat borderedMatrix = homoMatrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateGaussianHomoKernel(borderedMatrix.Size(), gammaH, gammaL, sigma, slope);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);

            //指数化
            Cv2.Exp(result, result);
            result -= Scalar.All(1);

            //转换颜色通道
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            //释放资源
            homoMatrix.Dispose();

            return result;
        }
        #endregion

        #region # 巴特沃斯低通滤波 —— static Mat ButterworthLPBlur(this Mat matrix, float sigma, int n)
        /// <summary>
        /// 巴特沃斯低通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="n">阶数</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat ButterworthLPBlur(this Mat matrix, float sigma, int n = 2)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateButterworthLPKernel(borderedMatrix.Size(), sigma, n);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 巴特沃斯高通滤波 —— static Mat ButterworthHPBlur(this Mat matrix, float sigma, int n)
        /// <summary>
        /// 巴特沃斯高通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="n">阶数</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat ButterworthHPBlur(this Mat matrix, float sigma, int n = 2)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateButterworthHPKernel(borderedMatrix.Size(), sigma, n);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 巴特沃斯带通滤波 —— static Mat ButterworthBPBlur(this Mat matrix, float sigma, float bandWidth, int n)
        /// <summary>
        /// 巴特沃斯带通滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <param name="n">阶数</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat ButterworthBPBlur(this Mat matrix, float sigma, float bandWidth, int n = 2)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateButterworthBPKernel(borderedMatrix.Size(), sigma, bandWidth, n);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 巴特沃斯带阻滤波 —— static Mat ButterworthBRBlur(this Mat matrix, float sigma, float bandWidth, int n)
        /// <summary>
        /// 巴特沃斯带阻滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <param name="n">阶数</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat ButterworthBRBlur(this Mat matrix, float sigma, float bandWidth, int n = 2)
        {
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateButterworthBRKernel(borderedMatrix.Size(), sigma, bandWidth, n);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            return result;
        }
        #endregion

        #region # 巴特沃斯同态滤波 —— static Mat ButterworthHomoBlur(this Mat matrix, float gammaH, float gammaL...
        /// <summary>
        /// 巴特沃斯同态滤波
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="gammaH">高频增益</param>
        /// <param name="gammaL">低频增益</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="slope">滤波斜率</param>
        /// <returns>滤波图像矩阵</returns>
        public static Mat ButterworthHomoBlur(this Mat matrix, float gammaH, float gammaL, float sigma, float slope)
        {
            Mat homoMatrix = matrix.Clone();
            homoMatrix.ConvertTo(homoMatrix, MatType.CV_32FC1);

            //对数化
            homoMatrix += Scalar.All(1);
            Cv2.Log(homoMatrix, homoMatrix);

            //频域滤波
            using Mat borderedMatrix = homoMatrix.GenerateDFTBorderedMatrix();
            using Mat kernelMatrix = KernelGenerator.GenerateButterworthHomoKernel(borderedMatrix.Size(), gammaH, gammaL, sigma, slope);
            Mat result = borderedMatrix.FrequencyBlur(kernelMatrix);

            //指数化
            Cv2.Exp(result, result);
            result -= Scalar.All(1);

            //转换颜色通道
            result = result[matrix.BoundingRect()];
            result.ConvertTo(result, MatType.CV_8UC1);

            //释放资源
            homoMatrix.Dispose();

            return result;
        }
        #endregion
    }
}
