using OpenCvSharp;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// 傅里叶扩展
    /// </summary>
    public static class FourierExtension
    {
        #region # 频率域滤波 —— Mat FrequencyBlur(Mat borderedMatrix, Mat kernelMatrix)
        /// <summary>
        /// 频率域滤波
        /// </summary>
        /// <param name="borderedMatrix">傅里叶边框矩阵</param>
        /// <param name="kernelMatrix">滤波核矩阵</param>
        /// <returns>滤波效果矩阵</returns>
        public static Mat FrequencyBlur(this Mat borderedMatrix, Mat kernelMatrix)
        {
            using Mat zeroMatrix = Mat.Zeros(borderedMatrix.Size(), MatType.CV_32FC1);
            using Mat complexMatrix = new Mat();
            Mat[] planes = { borderedMatrix, zeroMatrix };
            Cv2.Merge(planes, complexMatrix);

            //离散傅里叶变换
            Cv2.Dft(complexMatrix, complexMatrix, DftFlags.ComplexOutput);

            //分割实部与虚部，planes[0]为实部，planes[1]为虚部
            Cv2.Split(complexMatrix, out planes);

            //迁移频域
            ShiftDFT(planes[0]);

            //滤波器函数与DFT结果的乘积
            using Mat blurReal = new Mat();
            using Mat blurImag = new Mat();
            Mat blur = new Mat();
            Cv2.Multiply(planes[0], kernelMatrix, blurReal);  // 滤波（实部与滤波器模板对应元素相乘）
            Cv2.Multiply(planes[1], kernelMatrix, blurImag);  // 滤波（虚部与滤波器模板对应元素相乘）
            Mat[] blurs = { blurReal, blurImag };

            //再次迁移进行逆变换
            ShiftDFT(blurReal);

            //实部与虚部合并
            Cv2.Merge(blurs, blur);

            //离散傅里叶逆变换
            Cv2.Idft(blur, blur, DftFlags.ComplexOutput);
            blur = blur / blur.Rows / blur.Cols;

            //分离通道，主要获取通道
            Cv2.Split(blur, out Mat[] blurPlanes);

            blur.Dispose();
            blurPlanes[1].Dispose();

            return blurPlanes[0];
        }
        #endregion

        #region # 生成傅里叶变换边框矩阵 —— static Mat GenerateDFTBorderedMatrix(this Mat matrix)
        /// <summary>
        /// 生成傅里叶变换边框矩阵
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <returns>边框图像矩阵</returns>
        public static Mat GenerateDFTBorderedMatrix(this Mat matrix)
        {
            Size matrixSize = matrix.Size();
            int originalWidth = matrixSize.Width;
            int originalHeight = matrixSize.Height;
            int optimalWidth = Cv2.GetOptimalDFTSize(matrixSize.Width);    //获取DFT变换的最佳宽度
            int optimalHeight = Cv2.GetOptimalDFTSize(matrixSize.Height);  //获取DFT变换的最佳高度

            Mat borderedMatrix = new Mat();
            Cv2.CopyMakeBorder(matrix, borderedMatrix, 0, optimalHeight - originalHeight, 0, optimalWidth - originalWidth, BorderTypes.Constant, Scalar.All(0));
            borderedMatrix.ConvertTo(borderedMatrix, MatType.CV_32FC1);

            return borderedMatrix;
        }
        #endregion

        #region # 迁移频域 —— static void ShiftDFT(this Mat magnitudeMatrix)
        /// <summary>
        /// 迁移频域
        /// </summary>
        /// <param name="magnitudeMatrix">频谱矩阵</param>
        public static void ShiftDFT(this Mat magnitudeMatrix)
        {
            //一分为四，左上与右下交换，右上与左下交换
            //重新排列傅里叶图像中的象限，使原点位于图像中心
            int magnitudeCx = magnitudeMatrix.Cols / 2;
            int magnitudeCy = magnitudeMatrix.Rows / 2;
            using Mat magnitudeQ0 = new Mat(magnitudeMatrix, new Rect(0, 0, magnitudeCx, magnitudeCy));                         //左上
            using Mat magnitudeQ1 = new Mat(magnitudeMatrix, new Rect(magnitudeCx, 0, magnitudeCx, magnitudeCy));               //右上
            using Mat magnitudeQ2 = new Mat(magnitudeMatrix, new Rect(0, magnitudeCy, magnitudeCx, magnitudeCy));               //左下
            using Mat magnitudeQ3 = new Mat(magnitudeMatrix, new Rect(magnitudeCx, magnitudeCy, magnitudeCx, magnitudeCy));     //右下

            //交换象限
            using Mat exchangedMatrix = new Mat();

            //左上角与右下角对换
            magnitudeQ0.CopyTo(exchangedMatrix);
            magnitudeQ3.CopyTo(magnitudeQ0);
            exchangedMatrix.CopyTo(magnitudeQ3);

            //右上角与左下角对换
            magnitudeQ1.CopyTo(exchangedMatrix);
            magnitudeQ2.CopyTo(magnitudeQ1);
            exchangedMatrix.CopyTo(magnitudeQ2);
        }
        #endregion
    }
}
