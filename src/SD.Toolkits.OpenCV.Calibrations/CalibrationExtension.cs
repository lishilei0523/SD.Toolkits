using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.OpenCV.Calibrations
{
    /// <summary>
    /// 标定扩展
    /// </summary>
    public static class CalibrationExtension
    {
        #region # double三维向量转数组 —— static double[] ToArray(this Vec3d vector)
        /// <summary>
        /// double三维向量转数组
        /// </summary>
        /// <param name="vector">双精度浮点型三维向量</param>
        /// <returns>3x1数组</returns>
        public static double[] ToArray(this Vec3d vector)
        {
            return new[] { vector.Item0, vector.Item1, vector.Item2 };
        }
        #endregion

        #region # Matrix转Mat —— static Mat ToMat(this Matrix<double> matrix)
        /// <summary>
        /// Matrix转Mat
        /// </summary>
        public static Mat ToMat(this Matrix<double> matrix)
        {
            Mat mat = Mat.FromArray(matrix.ToArray());

            return mat;
        }
        #endregion

        #region # Mat转Matrix —— static Matrix ToMatrix(this Mat mat)
        /// <summary>
        /// Mat转Matrix
        /// </summary>
        public static Matrix ToMatrix(this Mat mat)
        {
            double[,] array = new double[mat.Rows, mat.Cols];
            for (int rowIndex = 0; rowIndex < mat.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < mat.Cols; colIndex++)
                {
                    array[rowIndex, colIndex] = mat.At<double>(rowIndex, colIndex);
                }
            }

            Matrix matrix = DenseMatrix.OfArray(array);

            return matrix;
        }
        #endregion

        #region # 旋转向量转旋转矩阵 —— static double[,] RotationVectorToRotationMatrix(this Vec3d rotationVector)
        /// <summary>
        /// 旋转向量转旋转矩阵
        /// </summary>
        /// <param name="rotationVector">旋转向量</param>
        /// <returns>3x3旋转矩阵: 2维数组</returns>
        public static double[,] RotationVectorToRotationMatrix(this Vec3d rotationVector)
        {
            double[] rArray3x1 = rotationVector.ToArray();
            double[,] rArray3x3 = rArray3x1.RotationVectorToRotationMatrix();

            return rArray3x3;
        }
        #endregion

        #region # 旋转向量转旋转矩阵 —— static double[,] RotationVectorToRotationMatrix(this double[] rArray3x1)
        /// <summary>
        /// 旋转向量转旋转矩阵
        /// </summary>
        /// <param name="rArray3x1">旋转向量: 1维数组3x1</param>
        /// <returns>3x3旋转矩阵: 2维数组</returns>
        public static double[,] RotationVectorToRotationMatrix(this double[] rArray3x1)
        {
            Cv2.Rodrigues(rArray3x1, out double[,] rArray3x3, out _);

            return rArray3x3;
        }
        #endregion

        #region # 旋转矩阵转欧拉角 —— static double[] ToEulerAngles(this Matrix<double> rotationMatrix)
        /// <summary>
        /// 旋转矩阵转欧拉角
        /// </summary>
        /// <param name="rotationMatrix">旋转矩阵3x3</param>
        /// <returns>3x1欧拉角向量: 一维数组</returns>
        public static double[] ToEulerAngles(this Matrix<double> rotationMatrix)
        {
            double m11 = rotationMatrix[0, 0];
            double m12 = rotationMatrix[0, 1];
            double m13 = rotationMatrix[0, 2];
            double m21 = rotationMatrix[1, 0];
            double m22 = rotationMatrix[1, 1];
            double m23 = rotationMatrix[1, 2];
            double m31 = rotationMatrix[2, 0];
            double m32 = rotationMatrix[2, 1];
            double m33 = rotationMatrix[2, 2];

            double rx = Math.Atan2(m32, m33);
            double ry = Math.Atan2(-m31, Math.Sqrt(m32 * m32 + m33 * m33));
            double rz = Math.Atan2(m21, m11);

            //弧度转角度
            rx *= 180.0 / Math.PI;
            ry *= 180.0 / Math.PI;
            rz *= 180.0 / Math.PI;

            double[] eulerArray3x1 = { rx, ry, rz };

            return eulerArray3x1;
        }
        #endregion

        #region # 欧拉角转旋转矩阵 —— static Matrix<double> ToRotationMatrix(this double[] eulerArray3x1)
        /// <summary>
        /// 欧拉角转旋转矩阵
        /// </summary>
        /// <param name="eulerArray3x1">3x1欧拉角向量: 一维数组</param>
        /// <returns>旋转矩阵3x3</returns>
        public static Matrix<double> ToRotationMatrix(this double[] eulerArray3x1)
        {
            double rxRadian = eulerArray3x1[0] / 180 * Math.PI;
            double ryRadian = eulerArray3x1[1] / 180 * Math.PI;
            double rzRadian = eulerArray3x1[2] / 180 * Math.PI;

            double rxSin = Math.Sin(rxRadian);
            double rxCos = Math.Cos(rxRadian);
            double rySin = Math.Sin(ryRadian);
            double ryCos = Math.Cos(ryRadian);
            double rzSin = Math.Sin(rzRadian);
            double rzCos = Math.Cos(rzRadian);

            double[,] rxArray3x3 =
            {
                {1, 0, 0},
                {0, rxCos, -rxSin},
                {0, rxSin, rxCos}
            };
            double[,] ryArray3x3 =
            {
                {ryCos, 0, rySin},
                {0, 1, 0},
                {-rySin, 0, ryCos}
            };
            double[,] rzArray3x3 =
            {
                {rzCos, -rzSin, 0},
                {rzSin, rzCos, 0},
                {0, 0, 1}
            };

            Matrix<double> rxMatrix = DenseMatrix.OfArray(rxArray3x3);
            Matrix<double> ryMatrix = DenseMatrix.OfArray(ryArray3x3);
            Matrix<double> rzMatrix = DenseMatrix.OfArray(rzArray3x3);
            Matrix<double> rotationMatrix = rzMatrix * ryMatrix * rxMatrix;

            return rotationMatrix;
        }
        #endregion

        #region # 构造旋转平移矩阵 —— static Matrix<double> BuildRotationTranslationMatrix(double[,] rArray3x3...
        /// <summary>
        /// 构造旋转平移矩阵
        /// </summary>
        /// <param name="rArray3x3">3x3旋转矩阵: 2维数组</param>
        /// <param name="tArray3x1">3x1平移向量: 1维数组</param>
        /// <returns>旋转平移矩阵</returns>
        public static Matrix<double> BuildRotationTranslationMatrix(double[,] rArray3x3, double[] tArray3x1)
        {
            Matrix<double> rMatrix = DenseMatrix.OfArray(rArray3x3);
            Vector<double> tVector = DenseVector.OfArray(tArray3x1);
            Vector homoVector = DenseVector.OfArray(new double[] { 0, 0, 0, 1 });
            Matrix<double> rtMatrix = rMatrix.InsertColumn(rMatrix.ColumnCount, tVector).InsertRow(rMatrix.RowCount, homoVector);

            return rtMatrix;
        }
        #endregion

        #region # 构造旋转平移矩阵 —— static Matrix<double> BuildRotationTranslationMatrix(Mat rMat, Mat tMat)
        /// <summary>
        /// 构造旋转平移矩阵
        /// </summary>
        /// <param name="rMat">3x3旋转矩阵</param>
        /// <param name="tMat">3x1平移向量</param>
        /// <returns>旋转平移矩阵</returns>
        public static Matrix<double> BuildRotationTranslationMatrix(Mat rMat, Mat tMat)
        {
            double[,] rArray3x3 = new double[3, 3];
            double[] tArray3x1;
            for (int rowIndex = 0; rowIndex < rMat.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < rMat.Cols; colIndex++)
                {
                    rArray3x3[rowIndex, colIndex] = rMat.At<double>(rowIndex, colIndex);
                }
            }
            tMat.GetArray(out tArray3x1);

            Matrix<double> rtMatrix = BuildRotationTranslationMatrix(rArray3x3, tArray3x1);

            return rtMatrix;
        }
        #endregion

        #region # 旋转平移矩阵转位姿 —— static Pose ToPose(this Matrix<double> rotationTranslationMatrix)
        /// <summary>
        /// 旋转平移矩阵转位姿
        /// </summary>
        /// <param name="rotationTranslationMatrix">旋转平移矩阵</param>
        /// <returns>位姿</returns>
        public static Pose ToPose(this Matrix<double> rotationTranslationMatrix)
        {
            Matrix<double> rMatrix = rotationTranslationMatrix.SubMatrix(0, 3, 0, 3);
            Matrix<double> tMatrix = rotationTranslationMatrix.SubMatrix(0, 3, 3, 1);

            double[] eulerArray3x1 = rMatrix.ToEulerAngles();
            Vector<double> transVector = tMatrix.Column(0);

            Pose pose = new Pose(Guid.NewGuid().ToString(), transVector[0], transVector[1], transVector[2], eulerArray3x1[0], eulerArray3x1[1], eulerArray3x1[2]);

            return pose;
        }
        #endregion

        #region # 位姿转旋转平移矩阵 —— static Matrix<double> ToRotationTranslationMatrix(this Pose pose)
        /// <summary>
        /// 位姿转旋转平移矩阵
        /// </summary>
        /// <param name="pose">位姿</param>
        /// <returns>旋转平移矩阵</returns>
        public static Matrix<double> ToRotationTranslationMatrix(this Pose pose)
        {
            double[,] rArray3x3 = pose.GetRotationArray3x3();
            double[] tArray3x1 = pose.GetTranslationArray3x1();
            Matrix<double> rtMatrix = BuildRotationTranslationMatrix(rArray3x3, tArray3x1);

            return rtMatrix;
        }
        #endregion

        #region # 位姿获取旋转矩阵 —— static double[,] GetRotationArray3x3(this Pose pose)
        /// <summary>
        /// 位姿获取旋转矩阵
        /// </summary>
        /// <param name="pose">位姿</param>
        /// <returns>3x3旋转矩阵: 2维数组</returns>
        public static double[,] GetRotationArray3x3(this Pose pose)
        {
            double[] eulerAngles = pose.GetEulerAngles();
            Matrix<double> rotationMatrix = eulerAngles.ToRotationMatrix();

            return rotationMatrix.ToArray();
        }
        #endregion

        #region # 生成标定板世界坐标点列表 —— static ICollection<Point3f> GeneratePatternPoints(float patternSideSize...
        /// <summary>
        /// 生成标定板世界坐标点列表
        /// </summary>
        /// <param name="patternSideSize">标定板方格边长</param>
        /// <param name="patternSize">标定板尺寸</param>
        /// <returns>标定板世界坐标点列表</returns>
        public static ICollection<Point3f> GeneratePatternPoints(float patternSideSize, Size patternSize)
        {
            IList<Point3f> patternPoints = new List<Point3f>();
            for (int i = 0; i < patternSize.Height; i++)
            {
                for (int j = 0; j < patternSize.Width; j++)
                {
                    Point3f point = new Point3f(j * patternSideSize, i * patternSideSize, 0);
                    patternPoints.Add(point);
                }
            }

            return patternPoints;
        }
        #endregion

        #region # 获取优化的棋盘格角点列表 —— static bool GetOptimizedChessboardCorners(this Mat image, Size patternSize...
        /// <summary>
        /// 获取优化的棋盘格角点列表
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="patternSize">标定板尺寸</param>
        /// <param name="cornerPoints">角点列表</param>
        /// <returns>是否成功</returns>
        public static bool GetOptimizedChessboardCorners(this Mat image, Size patternSize, out ICollection<Point2f> cornerPoints)
        {
            bool success = Cv2.FindChessboardCorners(image, patternSize, out Point2f[] keyPoints, ChessboardFlags.AdaptiveThresh | ChessboardFlags.FastCheck | ChessboardFlags.NormalizeImage);
            if (success)
            {
                //优化角点
                TermCriteria criteria = new TermCriteria(CriteriaTypes.MaxIter | CriteriaTypes.Count, 50, 0.001);
                cornerPoints = Cv2.CornerSubPix(image, keyPoints, new Size(11, 11), new Size(-1, -1), criteria);
            }
            else
            {
                cornerPoints = new List<Point2f>();
            }

            return success;
        }
        #endregion

        #region # 获取优化的圆形格角点列表 —— static bool GetOptimizedCirclesGridCorners(this Mat image, Size patternSize...
        /// <summary>
        /// 获取优化的圆形格角点列表
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="patternSize">标定板尺寸</param>
        /// <param name="cornerPoints">角点列表</param>
        /// <returns>是否成功</returns>
        public static bool GetOptimizedCirclesGridCorners(this Mat image, Size patternSize, out ICollection<Point2f> cornerPoints)
        {
            bool success = Cv2.FindCirclesGrid(image, patternSize, out Point2f[] keyPoints);
            if (success)
            {
                //优化角点
                TermCriteria criteria = new TermCriteria(CriteriaTypes.Eps | CriteriaTypes.MaxIter, 30, 0.001);
                cornerPoints = Cv2.CornerSubPix(image, keyPoints, new Size(11, 11), new Size(-1, -1), criteria);
            }
            else
            {
                cornerPoints = new List<Point2f>();
            }

            return success;
        }
        #endregion

        #region # 验证对极约束 —— static double[] CheckEpipolarConstraints(Mat cameraMat...
        /// <summary>
        /// 验证对极约束
        /// </summary>
        /// <param name="cameraMat">相机内参矩阵</param>
        /// <param name="sourcePoints">源关键点集</param>
        /// <param name="targetPoints">目标关键点集</param>
        /// <param name="rMat">旋转矩阵</param>
        /// <param name="tMat">平移矩阵</param>
        /// <returns>对极约束列表</returns>
        public static double[] CheckEpipolarConstraints(Mat cameraMat, IList<Point2f> sourcePoints, IList<Point2f> targetPoints, Mat rMat, Mat tMat)
        {
            IList<double> epipolarConstraints = new List<double>();
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                double[] y1Array = { sourcePoints[i].X, sourcePoints[i].Y, 1 };
                double[] y2Array = { targetPoints[i].X, targetPoints[i].Y, 1 };
                Mat y1 = Mat.FromArray(y1Array);
                Mat y2 = Mat.FromArray(y2Array);

                double[,] txArray =
                {
                    {0, -tMat.At<double>(2, 0), tMat.At<double>(1, 0)},
                    {tMat.At<double>(2, 0), 0, -tMat.At<double>(0, 0)},
                    {-tMat.At<double>(1, 0), tMat.At<double>(0, 0), 0}
                };
                Mat txMat = Mat.FromArray(txArray);

                Mat d = y2.T() * cameraMat.Inv().T() * txMat * rMat * cameraMat.Inv() * y1;
                epipolarConstraints.Add(d.At<double>(0, 0));
            }

            return epipolarConstraints.ToArray();
        }
        #endregion

        #region # 验证对极约束 —— static double[] CheckEpipolarConstraints(Matrix<double> cameraMatrix...
        /// <summary>
        /// 验证对极约束
        /// </summary>
        /// <param name="cameraMatrix">相机内参矩阵</param>
        /// <param name="sourcePoints">源关键点集</param>
        /// <param name="targetPoints">目标关键点集</param>
        /// <param name="rMatrix">旋转矩阵</param>
        /// <param name="tMatrix">平移矩阵</param>
        /// <returns>对极约束列表</returns>
        public static double[] CheckEpipolarConstraints(Matrix<double> cameraMatrix, IList<Point2f> sourcePoints, IList<Point2f> targetPoints, Matrix<double> rMatrix, Matrix<double> tMatrix)
        {
            IList<double> epipolarConstraints = new List<double>();
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                double[] y1Array = { sourcePoints[i].X, sourcePoints[i].Y, 1 };
                double[] y2Array = { targetPoints[i].X, targetPoints[i].Y, 1 };
                Vector<double> y1 = DenseVector.OfArray(y1Array);
                Vector<double> y2 = DenseVector.OfArray(y2Array);

                double[,] txArray =
                {
                    { 0, -tMatrix[2, 0], tMatrix[1, 0] },
                    { tMatrix[2, 0], 0, -tMatrix[0, 0] },
                    { -tMatrix[1, 0], tMatrix[0, 0], 0 }
                };
                Matrix<double> txMatrix = DenseMatrix.OfArray(txArray);

                Vector<double> d = y2.ToRowMatrix() * cameraMatrix.Inverse().Transpose() * txMatrix * rMatrix * cameraMatrix.Inverse() * y1;
                epipolarConstraints.Add(d.Single());
            }

            return epipolarConstraints.ToArray();
        }
        #endregion

        #region # 矫正畸变 —— static Mat RectifyDistortions(this Mat image, CameraIntrinsics cameraIntrinsics)
        /// <summary>
        /// 矫正畸变
        /// </summary>
        /// <param name="image">图像矩阵</param>
        /// <param name="cameraIntrinsics">相机内参</param>
        /// <returns>矫正图像矩阵</returns>
        public static Mat RectifyDistortions(this Mat image, CameraIntrinsics cameraIntrinsics)
        {
            #region # 验证

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "图像不可为空！");
            }
            if (cameraIntrinsics == null)
            {
                throw new ArgumentNullException(nameof(cameraIntrinsics), "相机内参不可为空！");
            }

            #endregion

            Mat result = new Mat();
            Cv2.Undistort(image, result, InputArray.Create(cameraIntrinsics.IntrinsicMatrix), InputArray.Create(cameraIntrinsics.DistortionVector));

            return result;
        }
        #endregion

        #region # 矫正畸变 —— static IDictionary<string, Mat> RectifyDistortions(this IDictionary<string, Mat> images...
        /// <summary>
        /// 矫正畸变
        /// </summary>
        /// <param name="images">图像矩阵字典</param>
        /// <param name="cameraIntrinsics">相机内参</param>
        /// <returns>矫正图像矩阵字典</returns>
        public static IDictionary<string, Mat> RectifyDistortions(this IDictionary<string, Mat> images, CameraIntrinsics cameraIntrinsics)
        {
            #region # 验证

            images ??= new Dictionary<string, Mat>();
            if (!images.Any())
            {
                return new Dictionary<string, Mat>();
            }
            if (images.Values.Select(x => x.Width).Distinct().Count() != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(images), "图像宽度不一致");
            }
            if (images.Values.Select(x => x.Height).Distinct().Count() != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(images), "图像高度不一致");
            }
            if (cameraIntrinsics == null)
            {
                throw new ArgumentNullException(nameof(cameraIntrinsics), "相机内参不可为空！");
            }

            #endregion

            using Mat map1 = new Mat();
            using Mat map2 = new Mat();
            using Mat r = new Mat();
            Size imageSize = images.Values.First().Size();
            Cv2.InitUndistortRectifyMap(InputArray.Create(cameraIntrinsics.IntrinsicMatrix), InputArray.Create(cameraIntrinsics.DistortionVector), r, InputArray.Create(cameraIntrinsics.IntrinsicMatrix), imageSize, MatType.CV_16SC2, map1, map2);

            IDictionary<string, Mat> results = new Dictionary<string, Mat>();
            foreach (KeyValuePair<string, Mat> kv in images)
            {
                Mat result = new Mat();
                Cv2.Remap(kv.Value, result, map1, map2);
                results.Add(kv.Key, result);
            }

            return results;
        }
        #endregion
    }
}
