using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Mathematics.Extensions;
using SD.Toolkits.Mathematics.Models;
using System.Diagnostics;

namespace SD.Toolkits.Mathematics.Tests.TestCases
{
    /// <summary>
    /// 空间扩展测试
    /// </summary>
    [TestClass]
    public class SpacialTests
    {
        #region # 测试旋转矩阵转欧拉角 —— void TestMatrixToEulerAngles()
        /// <summary>
        /// 测试旋转矩阵转欧拉角
        /// </summary>
        [TestMethod]
        public void TestMatrixToEulerAngles()
        {
            double[,] rArray3x3 =
            {
                {0.433013, -0.625, 0.649519},
                {0.75, 0.649519, 0.125},
                {-0.5, 0.433013, 0.75}
            };
            Matrix<double> rMatrix = DenseMatrix.OfArray(rArray3x3);
            EulerAngles eulerAngles = rMatrix.ToEulerAngles();

            Matrix<double> reMatrix = eulerAngles.ToRotationMatrix();
            Trace.WriteLine(reMatrix);

            //x: 30, y: 30, z: 60
            Trace.WriteLine(eulerAngles);
        }
        #endregion

        #region # 测试欧拉角转旋转矩阵 —— void TestEulerAnglesToMatrix()
        /// <summary>
        /// 测试欧拉角转旋转矩阵
        /// </summary>
        [TestMethod]
        public void TestEulerAnglesToMatrix()
        {
            Angle rx = Angle.FromDegrees(30);
            Angle ry = Angle.FromDegrees(30);
            Angle rz = Angle.FromDegrees(60);
            EulerAngles eulerAngles = new EulerAngles(rx, ry, rz);
            Matrix<double> rMatrix = eulerAngles.ToRotationMatrix();

            //{0.433013, -0.625, 0.649519},
            //{0.75, 0.649519, 0.125},
            //{-0.5, 0.433013, 0.75}
            Trace.WriteLine(rMatrix);
        }
        #endregion

        #region # 测试旋转矩阵转四元数 —— void TestMatrixToQuaternion()
        /// <summary>
        /// 测试旋转矩阵转四元数
        /// </summary>
        [TestMethod]
        public void TestMatrixToQuaternion()
        {
            double[,] rArray3x3 =
            {
                {0.433013, -0.625, 0.649519},
                {0.75, 0.649519, 0.125},
                {-0.5, 0.433013, 0.75}
            };
            Matrix<double> rMatrix = DenseMatrix.OfArray(rArray3x3);
            Quaternion quaternion = rMatrix.ToQuaternion();

            //0.841506387379205 + 0.0915068303461549i + 0.341506222490894j + 0.408493574000865k
            Trace.WriteLine(quaternion);
        }
        #endregion

        #region # 测试四元数转旋转矩阵 —— void TestMatrixToQuaternion()
        /// <summary>
        /// 测试四元数转旋转矩阵
        /// </summary>
        [TestMethod]
        public void TestQuaternionToMatrix()
        {
            double q0 = 0.841506387379205;
            double q1 = 0.0915068303461549;
            double q2 = 0.341506222490894;
            double q3 = 0.408493574000865;
            Quaternion quaternion = new Quaternion(q0, q1, q2, q3);
            Matrix<double> rMatrix = quaternion.ToRotationMatrix();

            //{0.433013, -0.625, 0.649519},
            //{0.75, 0.649519, 0.125},
            //{-0.5, 0.433013, 0.75}
            Trace.WriteLine(rMatrix);
        }
        #endregion

        #region # 测试四元数转欧拉角 —— void TestEulerAnglesToQuaternion()
        /// <summary>
        /// 测试四元数转欧拉角
        /// </summary>
        [TestMethod]
        public void TestQuaternionToEulerAngles()
        {
            double q0 = 0.841506387379205;
            double q1 = 0.0915068303461549;
            double q2 = 0.341506222490894;
            double q3 = 0.408493574000865;
            Quaternion quaternion = new Quaternion(q0, q1, q2, q3);
            EulerAngles eulerAngles = quaternion.ToEulerAngles();

            //x: 30, y: 30, z: 60
            Trace.WriteLine(eulerAngles);
        }
        #endregion

        #region # 测试欧拉角转四元数 —— void TestEulerAnglesToQuaternion()
        /// <summary>
        /// 测试欧拉角转四元数
        /// </summary>
        [TestMethod]
        public void TestEulerAnglesToQuaternion()
        {
            Angle rx = Angle.FromDegrees(30);
            Angle ry = Angle.FromDegrees(30);
            Angle rz = Angle.FromDegrees(60);
            EulerAngles eulerAngles = new EulerAngles(rx, ry, rz);
            Quaternion quaternion = eulerAngles.ToQuaternion();

            //0.841506387379205 + 0.0915068303461549i + 0.341506222490894j + 0.408493574000865k
            //0.84150635094611  + 0.0915063509461097i + 0.34150635094611j  + 0.40849364905389k}
            Trace.WriteLine(quaternion);
        }
        #endregion

        #region # 测试旋转平移矩阵转位姿 —— void TestMatrixToPose()
        /// <summary>
        /// 测试旋转平移矩阵转位姿
        /// </summary>
        [TestMethod]
        public void TestMatrixToPose()
        {
            double[,] rtArray4x4 =
            {
                {0.433013, -0.625, 0.649519, 1},
                {0.75, 0.649519, 0.125, -5},
                {-0.5, 0.433013, 0.75, 9},
                {0, 0, 0, 1}
            };
            Matrix<double> rtMatrix = DenseMatrix.OfArray(rtArray4x4);
            Pose pose = rtMatrix.ToPose();

            Trace.WriteLine(pose);
        }
        #endregion

        #region # 测试位姿转旋转平移矩阵 —— void TestPoseToMatrix()
        /// <summary>
        /// 测试位姿转旋转平移矩阵
        /// </summary>
        [TestMethod]
        public void TestPoseToMatrix()
        {
            Pose pose = new Pose(1, -5, 9, 30, 30, 60);
            Matrix<double> rtMatrix = pose.ToRotationTranslationMatrix();

            Trace.WriteLine(rtMatrix);
        }
        #endregion

        #region # 测试构造旋转平移矩阵 —— void TestBuildRotationTranslationMatrix()
        /// <summary>
        /// 测试构造旋转平移矩阵
        /// </summary>
        [TestMethod]
        public void TestBuildRotationTranslationMatrix()
        {
            double[,] rArray3x3 =
            {
                {0.433013, -0.625, 0.649519},
                {0.75, 0.649519, 0.125},
                {-0.5, 0.433013, 0.75}
            };
            Matrix<double> rMatrix = DenseMatrix.OfArray(rArray3x3);
            Vector3D tVector = new Vector3D(1, -5, 9);
            Matrix<double> rtMatrix = SpacialExtension.BuildRotationTranslationMatrix(rMatrix, tVector);

            Trace.WriteLine(rtMatrix);
        }
        #endregion

        #region # 测试分离旋转平移矩阵 —— void TestSplitRotationTranslationMatrix()
        /// <summary>
        /// 测试分离旋转平移矩阵
        /// </summary>
        [TestMethod]
        public void TestSplitRotationTranslationMatrix()
        {
            double[,] rtArray4x4 =
            {
                {0.433013, -0.625, 0.649519, 1},
                {0.75, 0.649519, 0.125, -5},
                {-0.5, 0.433013, 0.75, 9},
                {0, 0, 0, 1}
            };
            Matrix<double> rtMatrix = DenseMatrix.OfArray(rtArray4x4);

            rtMatrix.SplitRotationTranslationMatrix(out Matrix<double> rMatrix, out Vector3D tVector);
            Trace.WriteLine(rMatrix);
            Trace.WriteLine(tVector);
        }
        #endregion
    }
}
