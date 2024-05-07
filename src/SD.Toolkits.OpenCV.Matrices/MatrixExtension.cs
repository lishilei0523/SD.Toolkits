﻿using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Models;
using System;

namespace SD.Toolkits.OpenCV.Matrices
{
    /// <summary>
    /// 矩阵扩展
    /// </summary>
    public static class MatrixExtension
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
            Matrix<double> rtMatrix = MatrixExtension.BuildRotationTranslationMatrix(rArray3x3, tArray3x1);

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

            Matrix<double> rtMatrix = MatrixExtension.BuildRotationTranslationMatrix(rArray3x3, tArray3x1);

            return rtMatrix;
        }
        #endregion
    }
}
