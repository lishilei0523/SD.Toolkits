using OpenCvSharp;

namespace SD.Toolkits.OpenCV.Extensions
{
    /// <summary>
    /// 图表扩展
    /// </summary>
    public static class DiagramExtension
    {
        #region # 生成频率谱图 —— static Mat GenerateFrequencySpectrum(this Mat matrix)
        /// <summary>
        /// 生成频率谱图
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <returns>频率谱图矩阵</returns>
        public static Mat GenerateFrequencySpectrum(this Mat matrix)
        {
            //为傅立叶变换的结果分配存储空间
            //将planes数组组合成一个多通道的数组，两个通道搭配，分别保存实部和虚部
            //傅里叶变换的结果是复数，这就是说对于每个图像原像素值，会有两个图像值
            //此外，频域值范围远远超过图象值范围，因此至少将频域储存在float中
            //所以我们将输入图像转换成浮点型，并且多加一个额外通道来存储复数部分
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat zeroMatrix = Mat.Zeros(borderedMatrix.Size(), MatType.CV_32FC1);
            using Mat complexMatrix = new Mat();
            Mat[] planes = { borderedMatrix, zeroMatrix };
            Cv2.Merge(planes, complexMatrix);

            //离散傅里叶变换
            Cv2.Dft(complexMatrix, complexMatrix, DftFlags.ComplexOutput);

            //分割实部与虚部，planes[0]为实部，planes[1]为虚部
            Cv2.Split(complexMatrix, out planes);

            //将复数转为幅值，保存在planes[0]
            Cv2.Magnitude(planes[0], planes[1], planes[0]);
            Mat magnitudeMatrix = planes[0];

            //幅度太大，适用自然对数尺度替换线性尺度
            magnitudeMatrix += Scalar.All(1);
            Cv2.Log(magnitudeMatrix + 1, magnitudeMatrix);

            //行、列都为偶数，如果有奇数行或奇数列，进行频谱裁剪
            magnitudeMatrix = magnitudeMatrix[new Rect(0, 0, magnitudeMatrix.Cols & -2, magnitudeMatrix.Rows & -2)];

            //迁移频域
            magnitudeMatrix.ShiftDFT();

            //原幅度值仍更不便于显示，归一化后方便显示
            Cv2.Normalize(magnitudeMatrix, magnitudeMatrix, 0, 1, NormTypes.MinMax);

            return magnitudeMatrix;
        }
        #endregion

        #region # 生成相位谱图 —— static Mat GeneratePhaseSpectrum(this Mat matrix)
        /// <summary>
        /// 生成相位谱图
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <returns>相位谱图矩阵</returns>
        public static Mat GeneratePhaseSpectrum(this Mat matrix)
        {
            //为傅立叶变换的结果分配存储空间
            //将planes数组组合成一个多通道的数组，两个通道搭配，分别保存实部和虚部
            //傅里叶变换的结果是复数，这就是说对于每个图像原像素值，会有两个图像值
            //此外，频域值范围远远超过图象值范围，因此至少将频域储存在float中
            //所以我们将输入图像转换成浮点型，并且多加一个额外通道来存储复数部分
            using Mat borderedMatrix = matrix.GenerateDFTBorderedMatrix();
            using Mat zeroMatrix = Mat.Zeros(borderedMatrix.Size(), MatType.CV_32FC1);
            using Mat complexMatrix = new Mat();
            Mat[] planes = { borderedMatrix, zeroMatrix };
            Cv2.Merge(planes, complexMatrix);

            //离散傅里叶变换
            Cv2.Dft(complexMatrix, complexMatrix, DftFlags.ComplexOutput);

            //分割实部与虚部，planes[0]为实部，planes[1]为虚部
            Cv2.Split(complexMatrix, out planes);

            //将复数转为相位值，保存在planes[0]
            Cv2.Phase(planes[0], planes[1], planes[0]);
            Mat phaseMatrix = planes[0];

            //适用自然对数尺度替换线性尺度
            phaseMatrix += Scalar.All(1);
            Cv2.Log(phaseMatrix + 1, phaseMatrix);

            //行、列都为偶数，如果有奇数行或奇数列，进行频谱裁剪
            phaseMatrix = phaseMatrix[new Rect(0, 0, phaseMatrix.Cols & -2, phaseMatrix.Rows & -2)];

            //迁移频域
            phaseMatrix.ShiftDFT();

            //归一化后方便显示
            Cv2.Normalize(phaseMatrix, phaseMatrix, 0, 1, NormTypes.MinMax);

            return phaseMatrix;
        }
        #endregion
    }
}
