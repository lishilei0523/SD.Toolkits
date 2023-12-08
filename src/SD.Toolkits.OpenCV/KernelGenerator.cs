using OpenCvSharp;
using System;
using System.Threading.Tasks;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// 卷积核生成器
    /// </summary>
    public static class KernelGenerator
    {
        #region # 生成理想低通滤波核 —— static Mat GenerateIdealLPKernel(Size kernelSize, float sigma)
        /// <summary>
        /// 生成理想低通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>理想低通滤波核矩阵</returns>
        public static Mat GenerateIdealLPKernel(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想低通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance < sigma)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成理想低通滤波核 —— static Mat GenerateIdealLPKernelP(Size kernelSize, float sigma)
        /// <summary>
        /// 生成理想低通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>理想低通滤波核矩阵</returns>
        public static Mat GenerateIdealLPKernelP(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想低通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance < sigma)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成理想高通滤波核 —— static Mat GenerateIdealHPKernel(Size kernelSize, float sigma)
        /// <summary>
        /// 生成理想高通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>理想高通滤波核矩阵</returns>
        public static Mat GenerateIdealHPKernel(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想高通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance < sigma)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成理想高通滤波核 —— static Mat GenerateIdealHPKernelP(Size kernelSize, float sigma)
        /// <summary>
        /// 生成理想高通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>理想高通滤波核矩阵</returns>
        public static Mat GenerateIdealHPKernelP(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想高通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance < sigma)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成理想带通滤波核 —— static Mat GenerateIdealBPKernel(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成理想带通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>理想带通滤波核矩阵</returns>
        public static Mat GenerateIdealBPKernel(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想带通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance >= sigma - bandWidth / 2 && distance <= sigma + bandWidth)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成理想带通滤波核 —— static Mat GenerateIdealBPKernelP(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成理想带通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>理想带通滤波核矩阵</returns>
        public static Mat GenerateIdealBPKernelP(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想带通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance >= sigma - bandWidth / 2 && distance <= sigma + bandWidth)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成理想带阻滤波核 —— static Mat GenerateIdealBRKernel(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成理想带阻滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>理想带阻滤波核矩阵</returns>
        public static Mat GenerateIdealBRKernel(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想带阻滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance >= sigma - bandWidth / 2 && distance <= sigma + bandWidth)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成理想带阻滤波核 —— static Mat GenerateIdealBRKernelP(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成理想带阻滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>理想带阻滤波核矩阵</returns>
        public static Mat GenerateIdealBRKernelP(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建理想带阻滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    if (distance >= sigma - bandWidth / 2 && distance <= sigma + bandWidth)
                    {
                        kernel.At<float>(rowIndex, colIndex) = 0;
                    }
                    else
                    {
                        kernel.At<float>(rowIndex, colIndex) = 1;
                    }
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成高斯低通滤波核 —— static Mat GenerateGaussianLPKernel(Size kernelSize, float sigma)
        /// <summary>
        /// 生成高斯低通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>高斯低通滤波核矩阵</returns>
        public static Mat GenerateGaussianLPKernel(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯低通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    kernel.At<float>(rowIndex, colIndex) = (float)Math.Exp(-norm2 / (2.0f * Math.Pow(sigma, 2)));
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成高斯低通滤波核 —— static Mat GenerateGaussianLPKernelP(Size kernelSize, float sigma)
        /// <summary>
        /// 生成高斯低通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>高斯低通滤波核矩阵</returns>
        public static Mat GenerateGaussianLPKernelP(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯低通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    kernel.At<float>(rowIndex, colIndex) = (float)Math.Exp(-norm2 / (2.0f * Math.Pow(sigma, 2)));
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成高斯高通滤波核 —— static Mat GenerateGaussianHPKernel(Size kernelSize, float sigma)
        /// <summary>
        /// 生成高斯高通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>高斯高通滤波核矩阵</returns>
        public static Mat GenerateGaussianHPKernel(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯高通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    kernel.At<float>(rowIndex, colIndex) = 1.0f - (float)Math.Exp(-norm2 / (2.0f * Math.Pow(sigma, 2.0)));
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成高斯高通滤波核 —— static Mat GenerateGaussianHPKernelP(Size kernelSize, float sigma)
        /// <summary>
        /// 生成高斯高通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <returns>高斯高通滤波核矩阵</returns>
        public static Mat GenerateGaussianHPKernelP(Size kernelSize, float sigma)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯高通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    kernel.At<float>(rowIndex, colIndex) = 1.0f - (float)Math.Exp(-norm2 / (2.0f * Math.Pow(sigma, 2.0)));
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成高斯带通滤波核 —— static Mat GenerateGaussianBPKernel(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成高斯带通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>高斯带通滤波核矩阵</returns>
        public static Mat GenerateGaussianBPKernel(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯带通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double numerator = norm2 - Math.Pow(sigma, 2);
                    double denominator = distance * bandWidth;
                    double element = -Math.Pow(numerator / denominator, 2);
                    kernel.At<float>(rowIndex, colIndex) = (float)Math.Exp(element);
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成高斯带通滤波核 —— static Mat GenerateGaussianBPKernelP(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成高斯带通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>高斯带通滤波核矩阵</returns>
        public static Mat GenerateGaussianBPKernelP(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯带通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double numerator = norm2 - Math.Pow(sigma, 2);
                    double denominator = distance * bandWidth;
                    double element = -Math.Pow(numerator / denominator, 2);
                    kernel.At<float>(rowIndex, colIndex) = (float)Math.Exp(element);
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成高斯带阻滤波核 —— static Mat GenerateGaussianBRKernel(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成高斯带阻滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>高斯带阻滤波核矩阵</returns>
        public static Mat GenerateGaussianBRKernel(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯带阻滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double numerator = norm2 - Math.Pow(sigma, 2);
                    double denominator = distance * bandWidth;
                    double element = -Math.Pow(numerator / denominator, 2);
                    kernel.At<float>(rowIndex, colIndex) = 1 - (float)Math.Exp(element);
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成高斯带阻滤波核 —— static Mat GenerateGaussianBRKernelP(Size kernelSize, float sigma, float bandWidth)
        /// <summary>
        /// 生成高斯带阻滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <returns>高斯带阻滤波核矩阵</returns>
        public static Mat GenerateGaussianBRKernelP(Size kernelSize, float sigma, float bandWidth)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建高斯带阻滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double numerator = norm2 - Math.Pow(sigma, 2);
                    double denominator = distance * bandWidth;
                    double element = -Math.Pow(numerator / denominator, 2);
                    kernel.At<float>(rowIndex, colIndex) = 1 - (float)Math.Exp(element);
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成高斯同态滤波核 —— static Mat GenerateGaussianHomoKernel(Size kernelSize, float gammaH, float gammaL...
        /// <summary>
        /// 生成高斯同态滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="gammaH">高频增益</param>
        /// <param name="gammaL">低频增益</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="slope">滤波斜率</param>
        /// <returns>高斯同态滤波核矩阵</returns>
        public static Mat GenerateGaussianHomoKernel(Size kernelSize, float gammaH, float gammaL, float sigma, float slope)
        {
            #region # 验证

            if (gammaH < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "高频增益必须大于等于1！");
            }
            if (gammaL >= 1 || gammaL <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "低频增益必须小于1大于0！");
            }
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (slope <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slope), "滤波斜率不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);
            double gammaDiff = gammaH - gammaL;

            //构建高斯同态滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double element = gammaDiff * (1 - Math.Exp(-slope * norm2 / (2.0 * Math.Pow(sigma, 2)))) + gammaL;
                    kernel.At<float>(rowIndex, colIndex) = (float)element;
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成高斯同态滤波核 —— static Mat GenerateGaussianHomoKernelP(Size kernelSize, float gammaH, float gammaL...
        /// <summary>
        /// 生成高斯同态滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="gammaH">高频增益</param>
        /// <param name="gammaL">低频增益</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="slope">滤波斜率</param>
        /// <returns>高斯同态滤波核矩阵</returns>
        public static Mat GenerateGaussianHomoKernelP(Size kernelSize, float gammaH, float gammaL, float sigma, float slope)
        {
            #region # 验证

            if (gammaH < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "高频增益必须大于等于1！");
            }
            if (gammaL >= 1 || gammaL <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "低频增益必须小于1大于0！");
            }
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (slope <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slope), "滤波斜率不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);
            double gammaDiff = gammaH - gammaL;

            //构建高斯同态滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double element = gammaDiff * (1 - Math.Exp(-slope * norm2 / (2.0 * Math.Pow(sigma, 2)))) + gammaL;
                    kernel.At<float>(rowIndex, colIndex) = (float)element;
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯低通滤波核 —— static Mat GenerateButterworthLPKernel(Size kernelSize, float sigma, int n)
        /// <summary>
        /// 生成巴特沃斯低通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯低通滤波核矩阵</returns>
        public static Mat GenerateButterworthLPKernel(Size kernelSize, float sigma, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯低通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    kernel.At<float>(rowIndex, colIndex) = 1.0f / (float)(1.0f + Math.Pow(distance / sigma, 2.0f * n));
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯低通滤波核 —— static Mat GenerateButterworthLPKernelP(Size kernelSize, float sigma, int n)
        /// <summary>
        /// 生成巴特沃斯低通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯低通滤波核矩阵</returns>
        public static Mat GenerateButterworthLPKernelP(Size kernelSize, float sigma, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯低通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    kernel.At<float>(rowIndex, colIndex) = 1.0f / (float)(1.0f + Math.Pow(distance / sigma, 2.0f * n));
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯高通滤波核 —— static Mat GenerateButterworthHPKernel(Size kernelSize, float sigma, int n)
        /// <summary>
        /// 生成巴特沃斯高通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯高通滤波核矩阵</returns>
        public static Mat GenerateButterworthHPKernel(Size kernelSize, float sigma, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯高通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = Math.Abs((colIndex - center.X));
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    kernel.At<float>(rowIndex, colIndex) = 1.0f - 1.0f / (float)(1.0f + Math.Pow(distance / sigma, 2.0f * n));
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯高通滤波核 —— static Mat GenerateButterworthHPKernelP(Size kernelSize, float sigma, int n)
        /// <summary>
        /// 生成巴特沃斯高通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯高通滤波核矩阵</returns>
        public static Mat GenerateButterworthHPKernelP(Size kernelSize, float sigma, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯高通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = Math.Abs((colIndex - center.X));
                    double distance = Math.Sqrt(Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2));
                    kernel.At<float>(rowIndex, colIndex) = 1.0f - 1.0f / (float)(1.0f + Math.Pow(distance / sigma, 2.0f * n));
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯带通滤波核 —— static Mat GenerateButterworthBPKernel(Size kernelSize, float sigma, float bandWidth...
        /// <summary>
        /// 生成巴特沃斯带通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯带通滤波核矩阵</returns>
        public static Mat GenerateButterworthBPKernel(Size kernelSize, float sigma, float bandWidth, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯带通滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double element = (distance * bandWidth) / (norm2 - Math.Pow(sigma, 2));
                    double elementPow = Math.Pow(element, 2.0f * n);
                    kernel.At<float>(rowIndex, colIndex) = 1.0f - 1.0f / (float)(1.0f + elementPow);
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯带通滤波核 —— static Mat GenerateButterworthBPKernelP(Size kernelSize, float sigma, float bandWidth...
        /// <summary>
        /// 生成巴特沃斯带通滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯带通滤波核矩阵</returns>
        public static Mat GenerateButterworthBPKernelP(Size kernelSize, float sigma, float bandWidth, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯带通滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double element = (distance * bandWidth) / (norm2 - Math.Pow(sigma, 2));
                    double elementPow = Math.Pow(element, 2.0f * n);
                    kernel.At<float>(rowIndex, colIndex) = 1.0f - 1.0f / (float)(1.0f + elementPow);
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯带阻滤波核 —— static Mat GenerateButterworthBRKernel(Size kernelSize, float sigma, float bandWidth...
        /// <summary>
        /// 生成巴特沃斯带阻滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯带阻滤波核矩阵</returns>
        public static Mat GenerateButterworthBRKernel(Size kernelSize, float sigma, float bandWidth, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯带阻滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double element = (distance * bandWidth) / (norm2 - Math.Pow(sigma, 2));
                    double elementPow = Math.Pow(element, 2.0f * n);
                    kernel.At<float>(rowIndex, colIndex) = 1.0f / (float)(1.0f + elementPow);
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯带阻滤波核 —— static Mat GenerateButterworthBRKernelP(Size kernelSize, float sigma, float bandWidth...
        /// <summary>
        /// 生成巴特沃斯带阻滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="bandWidth">滤波带宽</param>
        /// <param name="n">阶数</param>
        /// <returns>巴特沃斯带阻滤波核矩阵</returns>
        public static Mat GenerateButterworthBRKernelP(Size kernelSize, float sigma, float bandWidth, int n = 2)
        {
            #region # 验证

            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (bandWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bandWidth), "滤波带宽不可小于等于0！");
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "阶数不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);

            //构建巴特沃斯带阻滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double element = (distance * bandWidth) / (norm2 - Math.Pow(sigma, 2));
                    double elementPow = Math.Pow(element, 2.0f * n);
                    kernel.At<float>(rowIndex, colIndex) = 1.0f / (float)(1.0f + elementPow);
                });
            });

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯同态滤波核 —— static Mat GenerateButterworthHomoKernel(Size kernelSize, float gammaH, float gammaL...
        /// <summary>
        /// 生成巴特沃斯同态滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="gammaH">高频增益</param>
        /// <param name="gammaL">低频增益</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="slope">斜率</param>
        /// <returns>巴特沃斯同态滤波核矩阵</returns>
        public static Mat GenerateButterworthHomoKernel(Size kernelSize, float gammaH, float gammaL, float sigma, float slope)
        {
            #region # 验证

            if (gammaH < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "高频增益必须大于等于1！");
            }
            if (gammaL >= 1 || gammaL <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "低频增益必须小于1大于0！");
            }
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (slope <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slope), "滤波斜率不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);
            double gammaDiff = gammaH - gammaL;

            //构建巴特沃斯同态滤波核
            for (int rowIndex = 0; rowIndex < kernel.Rows; rowIndex++)
            {
                int rowDiff = rowIndex - center.Y;
                for (int colIndex = 0; colIndex < kernel.Cols; colIndex++)
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double denominator = 1 + Math.Pow(sigma / slope * distance, 2);
                    double element = gammaDiff * (1 / denominator) + gammaL;
                    kernel.At<float>(rowIndex, colIndex) = (float)element;
                }
            }

            return kernel;
        }
        #endregion

        #region # 生成巴特沃斯同态滤波核 —— static Mat GenerateButterworthHomoKernelP(Size kernelSize, float gammaH, float gammaL...
        /// <summary>
        /// 生成巴特沃斯同态滤波核
        /// </summary>
        /// <param name="kernelSize">核矩阵尺寸</param>
        /// <param name="gammaH">高频增益</param>
        /// <param name="gammaL">低频增益</param>
        /// <param name="sigma">滤波半径</param>
        /// <param name="slope">斜率</param>
        /// <returns>巴特沃斯同态滤波核矩阵</returns>
        public static Mat GenerateButterworthHomoKernelP(Size kernelSize, float gammaH, float gammaL, float sigma, float slope)
        {
            #region # 验证

            if (gammaH < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "高频增益必须大于等于1！");
            }
            if (gammaL >= 1 || gammaL <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(gammaH), "低频增益必须小于1大于0！");
            }
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), "滤波半径不可小于等于0！");
            }
            if (slope <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slope), "滤波斜率不可小于等于0！");
            }

            #endregion

            Mat kernel = Mat.Zeros(kernelSize, MatType.CV_32FC1);
            Point center = new Point(kernel.Width / 2, kernel.Height / 2);
            double gammaDiff = gammaH - gammaL;

            //构建巴特沃斯同态滤波核
            Parallel.For(0, kernel.Rows, rowIndex =>
            {
                int rowDiff = rowIndex - center.Y;
                Parallel.For(0, kernel.Cols, colIndex =>
                {
                    int colDiff = colIndex - center.X;
                    double norm2 = Math.Pow(rowDiff, 2) + Math.Pow(colDiff, 2);
                    double distance = Math.Sqrt(norm2);
                    double denominator = 1 + Math.Pow(sigma / slope * distance, 2);
                    double element = gammaDiff * (1 / denominator) + gammaL;
                    kernel.At<float>(rowIndex, colIndex) = (float)element;
                });
            });

            return kernel;
        }
        #endregion
    }
}
