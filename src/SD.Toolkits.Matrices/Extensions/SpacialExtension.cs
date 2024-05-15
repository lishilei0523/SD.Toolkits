using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using SD.Toolkits.Matrices.Models;
using System;

namespace SD.Toolkits.Matrices.Extensions
{
    /// <summary>
    /// 空间扩展
    /// </summary>
    public static class SpacialExtension
    {
        #region # 旋转矩阵转欧拉角 —— static EulerAngles ToEulerAngles(this Matrix<double> rMatrix)
        /// <summary>
        /// 旋转矩阵转欧拉角
        /// </summary>
        /// <param name="rMatrix">旋转矩阵3x3</param>
        /// <returns>欧拉角</returns>
        public static EulerAngles ToEulerAngles(this Matrix<double> rMatrix)
        {
            double r11 = rMatrix[0, 0];
            double r12 = rMatrix[0, 1];
            double r13 = rMatrix[0, 2];
            double r21 = rMatrix[1, 0];
            double r22 = rMatrix[1, 1];
            double r23 = rMatrix[1, 2];
            double r31 = rMatrix[2, 0];
            double r32 = rMatrix[2, 1];
            double r33 = rMatrix[2, 2];

            double rx = Math.Atan2(r32, r33);
            //double ry = Math.Atan2(-r31, Math.Sqrt(r32 * r32 + r33 * r33));
            double ry = Math.Atan2(-r31, Math.Sqrt(r11 * r11 + r21 * r21));
            double rz = Math.Atan2(r21, r11);
            EulerAngles eulerAngles = new EulerAngles(Angle.FromRadians(rx), Angle.FromRadians(ry), Angle.FromRadians(rz));

            return eulerAngles;
        }
        #endregion

        #region # 欧拉角转旋转矩阵 —— static Matrix<double> ToRotationMatrix(this EulerAngles eulerAngles)
        /// <summary>
        /// 欧拉角转旋转矩阵
        /// </summary>
        /// <param name="eulerAngles">欧拉角</param>
        /// <returns>旋转矩阵3x3</returns>
        public static Matrix<double> ToRotationMatrix(this EulerAngles eulerAngles)
        {
            double rxRadians = eulerAngles.Alpha.Radians;
            double ryRadians = eulerAngles.Beta.Radians;
            double rzRadians = eulerAngles.Gamma.Radians;
            double rxSin = Math.Sin(rxRadians);
            double rxCos = Math.Cos(rxRadians);
            double rySin = Math.Sin(ryRadians);
            double ryCos = Math.Cos(ryRadians);
            double rzSin = Math.Sin(rzRadians);
            double rzCos = Math.Cos(rzRadians);

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
            Matrix<double> rMatrix = rzMatrix * ryMatrix * rxMatrix;

            return rMatrix;
        }
        #endregion

        #region # 旋转矩阵转四元数 —— static Quaternion ToQuaternion(this Matrix<double> rMatrix)
        /// <summary>
        /// 旋转矩阵转四元数
        /// </summary>
        /// <param name="rMatrix">旋转矩阵3x3</param>
        /// <returns>四元数</returns>
        public static Quaternion ToQuaternion(this Matrix<double> rMatrix)
        {
            double r11 = rMatrix[0, 0];
            double r12 = rMatrix[0, 1];
            double r13 = rMatrix[0, 2];
            double r21 = rMatrix[1, 0];
            double r22 = rMatrix[1, 1];
            double r23 = rMatrix[1, 2];
            double r31 = rMatrix[2, 0];
            double r32 = rMatrix[2, 1];
            double r33 = rMatrix[2, 2];

            double q0 = Math.Sqrt(1 + r11 + r22 + r33) / 2;
            double q1 = Math.Sqrt(1 + r11 - r22 - r33) / 2;
            double q2 = Math.Sqrt(1 - r11 + r22 - r33) / 2;
            double q3 = Math.Sqrt(1 - r11 - r22 + r33) / 2;
            Quaternion quaternion = new Quaternion(q0, q1, q2, q3);

            return quaternion;
        }
        #endregion

        #region # 四元数转旋转矩阵 —— static Matrix<double> ToRotationMatrix(this Quaternion quaternion)
        /// <summary>
        /// 四元数转旋转矩阵
        /// </summary>
        /// <param name="quaternion">四元数</param>
        /// <returns>旋转矩阵</returns>
        public static Matrix<double> ToRotationMatrix(this Quaternion quaternion)
        {
            double q0 = quaternion.Real;
            double q1 = quaternion.ImagX;
            double q2 = quaternion.ImagY;
            double q3 = quaternion.ImagZ;

            Matrix<double> rMatrix = DenseMatrix.Create(3, 3, 0);
            rMatrix[0, 0] = 1 - 2 * Math.Pow(q2, 2) - 2 * Math.Pow(q3, 2);
            rMatrix[1, 0] = 2 * q1 * q2 + 2 * q0 * q3;
            rMatrix[2, 0] = 2 * q1 * q3 - 2 * q0 * q2;

            rMatrix[0, 1] = 2 * q1 * q2 - 2 * q0 * q3;
            rMatrix[1, 1] = 1 - 2 * Math.Pow(q1, 2) - 2 * Math.Pow(q3, 2);
            rMatrix[2, 1] = 2 * q2 * q3 + 2 * q0 * q1;

            rMatrix[0, 2] = 2 * q1 * q3 + 2 * q0 * q2;
            rMatrix[1, 2] = 2 * q2 * q3 - 2 * q0 * q1;
            rMatrix[2, 2] = 1 - 2 * Math.Pow(q1, 2) - 2 * Math.Pow(q2, 2);

            return rMatrix;
        }
        #endregion

        #region # 欧拉角转四元数 —— static Quaternion ToQuaternion(this EulerAngles eulerAngles)
        /// <summary>
        /// 欧拉角转四元数
        /// </summary>
        /// <param name="eulerAngles">欧拉角</param>
        /// <returns>四元数</returns>
        public static Quaternion ToQuaternion(this EulerAngles eulerAngles)
        {
            double rxRadians = eulerAngles.Alpha.Radians;
            double ryRadians = eulerAngles.Beta.Radians;
            double rzRadians = eulerAngles.Gamma.Radians;
            double rxSin = Math.Sin(rxRadians * 0.5);
            double rxCos = Math.Cos(rxRadians * 0.5);
            double rySin = Math.Sin(ryRadians * 0.5);
            double ryCos = Math.Cos(ryRadians * 0.5);
            double rzSin = Math.Sin(rzRadians * 0.5);
            double rzCos = Math.Cos(rzRadians * 0.5);
            double q0 = rzCos * ryCos * rxCos + rzSin * rySin * rxSin;
            double q1 = rzCos * ryCos * rxSin - rzSin * rySin * rxCos;
            double q2 = rzSin * ryCos * rxSin + rzCos * rySin * rxCos;
            double q3 = rzSin * ryCos * rxCos - rzCos * rySin * rxSin;

            Quaternion quaternion = new Quaternion(q0, q1, q2, q3);

            return quaternion;
        }
        #endregion

        #region # 旋转平移矩阵转位姿 —— static Pose ToPose(this Matrix<double> rtMatrix)
        /// <summary>
        /// 旋转平移矩阵转位姿
        /// </summary>
        /// <param name="rtMatrix">旋转平移矩阵</param>
        /// <returns>位姿</returns>
        public static Pose ToPose(this Matrix<double> rtMatrix)
        {
            Matrix<double> rMatrix = rtMatrix.SubMatrix(0, 3, 0, 3);
            Matrix<double> tMatrix = rtMatrix.SubMatrix(0, 3, 3, 1);

            Vector3D tVector = Vector3D.OfVector(tMatrix.Column(0));
            EulerAngles eulerAngles = rMatrix.ToEulerAngles();

            Pose pose = new Pose(tVector.X, tVector.Y, tVector.Z, eulerAngles.Alpha.Degrees, eulerAngles.Beta.Degrees, eulerAngles.Gamma.Degrees);

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
            EulerAngles eulerAngles = pose.GetEulerAngles();
            Matrix<double> rMatrix = eulerAngles.ToRotationMatrix();
            Vector3D tVector = pose.GetTranslationVector();
            Matrix<double> rtMatrix = BuildRotationTranslationMatrix(rMatrix, tVector);

            return rtMatrix;
        }
        #endregion

        #region # 构造旋转平移矩阵 —— static Matrix<double> BuildRotationTranslationMatrix(Matrix<double> rMatrix...
        /// <summary>
        /// 构造旋转平移矩阵
        /// </summary>
        /// <param name="rMatrix">旋转矩阵3x3</param>
        /// <param name="tVector">平移向量3x1</param>
        /// <returns>旋转平移矩阵</returns>
        public static Matrix<double> BuildRotationTranslationMatrix(Matrix<double> rMatrix, Vector3D tVector)
        {
            Vector<double> homoVector = DenseVector.OfArray(new double[] { 0, 0, 0, 1 });
            Matrix<double> rtMatrix = rMatrix.InsertColumn(rMatrix.ColumnCount, tVector.ToVector());
            rtMatrix = rtMatrix.InsertRow(rMatrix.RowCount, homoVector);

            return rtMatrix;
        }
        #endregion
    }
}
