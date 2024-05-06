using MathNet.Numerics.LinearAlgebra;
using OpenCvSharp;
using SD.Toolkits.OpenCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.OpenCV.Calibrations
{
    /// <summary>
    /// 相机标定者
    /// </summary>
    public static class Calibrator
    {
        #region # 单目标定 —— static CameraIntrinsics MonoCalibrate(string cameraId, float patternSideSize...
        /// <summary>
        /// 单目标定
        /// </summary>
        /// <param name="cameraId">相机Id</param>
        /// <param name="patternSideSize">标定板方格边长</param>
        /// <param name="patternSize">标定板尺寸</param>
        /// <param name="patternType">标定板类型</param>
        /// <param name="imageSize">图像尺寸</param>
        /// <param name="images">图像字典</param> 
        /// <param name="extrinsicMatrices">外参矩阵字典</param>
        /// <param name="failedImageKeys">失败图像键列表</param>
        /// <returns>相机内参</returns>
        public static CameraIntrinsics MonoCalibrate(string cameraId, float patternSideSize, Size patternSize, PatternType patternType, Size imageSize, IDictionary<string, Mat> images, out IDictionary<string, Matrix<double>> extrinsicMatrices, out ICollection<string> failedImageKeys)
        {
            #region # 验证

            images ??= new Dictionary<string, Mat>();
            if (!images.Any())
            {
                throw new ArgumentNullException(nameof(images), "图片不可为空！");
            }
            if (images.Count < 6)
            {
                throw new ArgumentNullException(nameof(images), "图片不可小于6张！");
            }

            #endregion

            IDictionary<string, ICollection<Point3f>> patternPointsGroup = new Dictionary<string, ICollection<Point3f>>();
            IDictionary<string, ICollection<Point2f>> cornerPointsGroup = new Dictionary<string, ICollection<Point2f>>();
            extrinsicMatrices = new Dictionary<string, Matrix<double>>();
            failedImageKeys = new HashSet<string>();

            //世界坐标系点列表
            ICollection<Point3f> patternPoints = CalibrationExtension.GeneratePatternPoints(patternSideSize, patternSize);

            //获取图像角点
            foreach (KeyValuePair<string, Mat> kv in images)
            {
                bool success;
                ICollection<Point2f> cornerPoints;
                if (patternType == PatternType.Chessboard)
                {
                    success = kv.Value.GetOptimizedChessboardCorners(patternSize, out cornerPoints);
                }
                else if (patternType == PatternType.CirclesGrid)
                {
                    success = kv.Value.GetOptimizedCirclesGridCorners(patternSize, out cornerPoints);
                }
                else
                {
                    throw new NotSupportedException("不支持的标定板格式！");
                }

                if (success)
                {
                    patternPointsGroup.Add(kv.Key, patternPoints);
                    cornerPointsGroup.Add(kv.Key, cornerPoints);
                }
                else
                {
                    failedImageKeys.Add(kv.Key);
                }
            }

            //标定相机
            double[,] cameraArray3x3 = new double[3, 3];//内参二维数组3x3
            double[] distortionArray5x1 = new double[5];//畸变一位数组5x1
            double calibratedReprojectionError = Cv2.CalibrateCamera(patternPointsGroup.Values, cornerPointsGroup.Values, imageSize, cameraArray3x3, distortionArray5x1, out Vec3d[] rVecs, out Vec3d[] tVecs);

            //重投影误差
            double reprojectionError = 0;
            for (int i = 0; i < patternPointsGroup.Count; i++)
            {
                KeyValuePair<string, ICollection<Point3f>> patternPointsKV = patternPointsGroup.ElementAt(i);
                KeyValuePair<string, ICollection<Point2f>> cornerPointsKV = cornerPointsGroup.ElementAt(i);
                double[] rArray3x1 = rVecs[i].ToArray();
                double[] tArray3x1 = tVecs[i].ToArray();

                //计算重投影误差
                Cv2.ProjectPoints(patternPointsKV.Value, rArray3x1, tArray3x1, cameraArray3x3, distortionArray5x1, out Point2f[] imagePoints, out double[,] jacobian);
                using Mat cornerPointsMat = Mat.FromArray(cornerPointsKV.Value);
                using Mat imagePointsMat = Mat.FromArray(imagePoints);
                double unitError = Cv2.Norm(cornerPointsMat, imagePointsMat) / imagePoints.Length;
                reprojectionError += unitError;

                //外参矩阵字典
                double[,] rArray3x3 = rArray3x1.RotationVectorToRotationMatrix();
                Matrix<double> rtMatrix = CalibrationExtension.BuildRotationTranslationMatrix(rArray3x3, tArray3x1);
                extrinsicMatrices.Add(patternPointsKV.Key, rtMatrix);
            }

            reprojectionError /= patternPointsGroup.Count;
            CameraIntrinsics cameraIntrinsics = new CameraIntrinsics(cameraId, calibratedReprojectionError, reprojectionError, distortionArray5x1, cameraArray3x3);

            return cameraIntrinsics;
        }
        #endregion

        #region # 标定眼在手上 —— static Matrix<double> CalibrateEyeInHand(HandEyeCalibrationMethod calibrationMode...
        /// <summary>
        /// 标定眼在手上
        /// </summary>
        /// <param name="calibrationMode">标定模式</param>
        /// <param name="robotPoses">机器人位姿字典</param>
        /// <param name="extrinsicMatrices">外参矩阵字典</param>
        /// <returns>相机到抓手旋转平移矩阵</returns>
        public static Matrix<double> CalibrateEyeInHand(HandEyeCalibrationMethod calibrationMode, IDictionary<string, Pose> robotPoses, IDictionary<string, Matrix<double>> extrinsicMatrices)
        {
            #region # 验证

            robotPoses ??= new Dictionary<string, Pose>();
            extrinsicMatrices ??= new Dictionary<string, Matrix<double>>();
            if (!robotPoses.Any())
            {
                throw new ArgumentNullException(nameof(robotPoses), "机器人位姿字典不可为空！");
            }
            if (!extrinsicMatrices.Any())
            {
                throw new ArgumentNullException(nameof(extrinsicMatrices), "外参矩阵字典不可为空！");
            }
            if (robotPoses.Count != extrinsicMatrices.Count)
            {
                throw new InvalidOperationException("机器人位姿字典与外参矩阵字典长度不一致！");
            }
            if (robotPoses.Keys.Except(extrinsicMatrices.Keys).Any() || extrinsicMatrices.Keys.Except(robotPoses.Keys).Any())
            {
                throw new InvalidOperationException("机器人位姿字典与外参矩阵字典键不一致！");
            }

            #endregion

            List<Mat> rEndToBaseMats = new List<Mat>();         //抓手到基底旋转矩阵列表
            List<Mat> tEndToBaseMats = new List<Mat>();         //抓手到基底平移向量列表
            List<Mat> rPatternToCameraMats = new List<Mat>();   //标定板到相机旋转矩阵列表
            List<Mat> tPatternToCameraMats = new List<Mat>();   //标定板到相机平移向量列表
            foreach (KeyValuePair<string, Pose> kv in robotPoses)
            {
                Pose pose = kv.Value;
                Matrix<double> extrinsicMatrix = extrinsicMatrices[kv.Key];

                //抓手到基底旋转矩阵
                double[,] robotRotationArray3x3 = pose.GetRotationArray3x3();
                Mat rEndToBaseMat = Mat.FromArray(robotRotationArray3x3);

                //抓手到基底平移向量
                double[] robotTranslationArray3x1 = pose.GetTranslationArray3x1();
                Mat tEndToBaseMat = Mat.FromArray(robotTranslationArray3x1);

                //标定板到相机旋转矩阵
                Matrix<double> rExtrinsicMatrix = extrinsicMatrix.SubMatrix(0, 3, 0, 3);
                Mat rPatternToCameraMat = rExtrinsicMatrix.ToMat();

                //标定板到相机平移向量
                Matrix<double> tExtrinsicMatrix = extrinsicMatrix.SubMatrix(0, 3, 3, 1);
                Mat tPatternToCameraMat = tExtrinsicMatrix.ToMat();

                rEndToBaseMats.Add(rEndToBaseMat);
                tEndToBaseMats.Add(tEndToBaseMat);
                rPatternToCameraMats.Add(rPatternToCameraMat);
                tPatternToCameraMats.Add(tPatternToCameraMat);
            }

            //手眼标定
            using Mat rCameraToEndMat = new Mat();
            using Mat tCameraToEndMat = new Mat();
            Cv2.CalibrateHandEye(rEndToBaseMats, tEndToBaseMats, rPatternToCameraMats, tPatternToCameraMats, rCameraToEndMat, tCameraToEndMat, calibrationMode);

            //释放资源
            rEndToBaseMats.ForEach(mat => mat.Dispose());
            tEndToBaseMats.ForEach(mat => mat.Dispose());
            rPatternToCameraMats.ForEach(mat => mat.Dispose());
            tPatternToCameraMats.ForEach(mat => mat.Dispose());

            //相机到抓手旋转平移矩阵
            Matrix<double> rtCameraToEndMatrix = CalibrationExtension.BuildRotationTranslationMatrix(rCameraToEndMat, tCameraToEndMat);

            return rtCameraToEndMatrix;
        }
        #endregion

        #region # 标定眼在手外 —— static Matrix<double> CalibrateEyeToHand(HandEyeCalibrationMethod calibrationMode...
        /// <summary>
        /// 标定眼在手外
        /// </summary>
        /// <param name="calibrationMode">标定模式</param>
        /// <param name="robotPoses">机器人位姿字典</param>
        /// <param name="extrinsicMatrices">外参矩阵字典</param>
        /// <returns>相机到基底旋转平移矩阵</returns>
        public static Matrix<double> CalibrateEyeToHand(HandEyeCalibrationMethod calibrationMode, IDictionary<string, Pose> robotPoses, IDictionary<string, Matrix<double>> extrinsicMatrices)
        {
            #region # 验证

            robotPoses ??= new Dictionary<string, Pose>();
            extrinsicMatrices ??= new Dictionary<string, Matrix<double>>();
            if (!robotPoses.Any())
            {
                throw new ArgumentNullException(nameof(robotPoses), "机器人位姿字典不可为空！");
            }
            if (!extrinsicMatrices.Any())
            {
                throw new ArgumentNullException(nameof(extrinsicMatrices), "外参矩阵字典不可为空！");
            }
            if (robotPoses.Count != extrinsicMatrices.Count)
            {
                throw new InvalidOperationException("机器人位姿字典与外参矩阵字典长度不一致！");
            }
            if (robotPoses.Keys.Except(extrinsicMatrices.Keys).Any() || extrinsicMatrices.Keys.Except(robotPoses.Keys).Any())
            {
                throw new InvalidOperationException("机器人位姿字典与外参矩阵字典键不一致！");
            }

            #endregion

            List<Mat> rBaseToEndMats = new List<Mat>();        //基底到抓手旋转矩阵列表
            List<Mat> tBaseToEndMats = new List<Mat>();        //基底到抓手平移向量列表
            List<Mat> rPatternToCameraMats = new List<Mat>();  //标定板到相机旋转矩阵列表
            List<Mat> tPatternToCameraMats = new List<Mat>();  //标定板到相机平移向量列表
            foreach (KeyValuePair<string, Pose> kv in robotPoses)
            {
                Pose pose = kv.Value;
                Matrix<double> extrinsicMatrix = extrinsicMatrices[kv.Key];

                //抓手到基底旋转平移矩阵
                Matrix<double> rtBaseToEndMatrix = pose.ToRotationTranslationMatrix();

                //基底到抓手旋转平移矩阵
                Matrix<double> rtEndToBaseMatrix = rtBaseToEndMatrix.Inverse();

                //基底到抓手旋转矩阵
                Matrix<double> rEndToBaseMatrix = rtEndToBaseMatrix.SubMatrix(0, 3, 0, 3);
                Mat rEndToBaseMat = rEndToBaseMatrix.ToMat();

                //基底到抓手平移向量
                Matrix<double> tEndToBaseMatrix = rtEndToBaseMatrix.SubMatrix(0, 3, 3, 1);
                Mat tEndToBaseMat = tEndToBaseMatrix.ToMat();

                //标定板到相机旋转矩阵
                Matrix<double> rExtrinsicMatrix = extrinsicMatrix.SubMatrix(0, 3, 0, 3);
                Mat rPatternToCameraMat = rExtrinsicMatrix.ToMat();

                //标定板到相机平移向量
                Matrix<double> tExtrinsicMatrix = extrinsicMatrix.SubMatrix(0, 3, 3, 1);
                Mat tPatternToCameraMat = tExtrinsicMatrix.ToMat();

                rBaseToEndMats.Add(rEndToBaseMat);
                tBaseToEndMats.Add(tEndToBaseMat);
                rPatternToCameraMats.Add(rPatternToCameraMat);
                tPatternToCameraMats.Add(tPatternToCameraMat);
            }

            //手眼标定
            using Mat rCameraToBaseMat = new Mat();
            using Mat tCameraToBaseMat = new Mat();
            Cv2.CalibrateHandEye(rBaseToEndMats, tBaseToEndMats, rPatternToCameraMats, tPatternToCameraMats, rCameraToBaseMat, tCameraToBaseMat, calibrationMode);

            //释放资源
            rBaseToEndMats.ForEach(mat => mat.Dispose());
            tBaseToEndMats.ForEach(mat => mat.Dispose());
            rPatternToCameraMats.ForEach(mat => mat.Dispose());
            tPatternToCameraMats.ForEach(mat => mat.Dispose());

            //相机到基底旋转平移矩阵
            Matrix<double> rtCameraToBaseMatrix = CalibrationExtension.BuildRotationTranslationMatrix(rCameraToBaseMat, tCameraToBaseMat);

            return rtCameraToBaseMatrix;
        }
        #endregion

        #region # 解析外参矩阵 —— static Matrix<double> SolveExtrinsicMatrix(int patternSideSize, Size patternSize...
        /// <summary>
        /// 解析外参矩阵
        /// </summary>
        /// <param name="patternSideSize">标定板方格边长</param>
        /// <param name="patternSize">标定板尺寸</param>
        /// <param name="patternType">标定板类型</param>
        /// <param name="image">图像</param>
        /// <param name="cameraIntrinsics">相机内参</param>
        /// <returns>外参矩阵</returns>
        public static Matrix<double> SolveExtrinsicMatrix(int patternSideSize, Size patternSize, PatternType patternType, Mat image, CameraIntrinsics cameraIntrinsics)
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

            bool success;
            ICollection<Point2f> cornerPoints;
            if (patternType == PatternType.Chessboard)
            {
                success = image.GetOptimizedChessboardCorners(patternSize, out cornerPoints);
            }
            else if (patternType == PatternType.CirclesGrid)
            {
                success = image.GetOptimizedCirclesGridCorners(patternSize, out cornerPoints);
            }
            else
            {
                throw new NotSupportedException("不支持的标定板格式！");
            }

            if (success)
            {
                ICollection<Point3f> patternPoints = CalibrationExtension.GeneratePatternPoints(patternSideSize, patternSize);
                Matrix<double> extrinsicMatrix = SolveExtrinsicMatrix(patternPoints, cornerPoints, cameraIntrinsics);

                return extrinsicMatrix;
            }

            throw new ArithmeticException("未获取足够角点！");
        }
        #endregion

        #region # 解析外参矩阵 —— static Matrix<double> SolveExtrinsicMatrix(IEnumerable<Point3f> patternPoints...
        /// <summary>
        /// 解析外参矩阵
        /// </summary>
        /// <param name="patternPoints">世界坐标系点列表</param>
        /// <param name="cornerPoints">图像角点列表</param>
        /// <param name="cameraIntrinsics">相机内参</param>
        /// <returns>外参矩阵</returns>
        public static Matrix<double> SolveExtrinsicMatrix(IEnumerable<Point3f> patternPoints, IEnumerable<Point2f> cornerPoints, CameraIntrinsics cameraIntrinsics)
        {
            #region # 验证

            patternPoints = patternPoints?.ToArray() ?? Array.Empty<Point3f>();
            cornerPoints = cornerPoints?.ToArray() ?? Array.Empty<Point2f>();
            if (!patternPoints.Any())
            {
                throw new ArgumentNullException(nameof(patternPoints), "世界坐标系点列表不可为空！");
            }
            if (!cornerPoints.Any())
            {
                throw new ArgumentNullException(nameof(cornerPoints), "图像角点列表不可为空！");
            }
            if (patternPoints.Count() != cornerPoints.Count())
            {
                throw new InvalidOperationException("世界坐标系点与图像角点长度不一致！");
            }

            #endregion

            double[] rArray3x1 = { };
            double[] tArray3x1 = { };
            Cv2.SolvePnP(patternPoints, cornerPoints, cameraIntrinsics.IntrinsicMatrix, cameraIntrinsics.DistortionVector.ToArray(), ref rArray3x1, ref tArray3x1);

            //旋转向量转旋转矩阵
            double[,] rArray3x3 = rArray3x1.RotationVectorToRotationMatrix();

            //构造RT矩阵
            Matrix<double> rtPatternToCameraMatrix = CalibrationExtension.BuildRotationTranslationMatrix(rArray3x3, tArray3x1);

            return rtPatternToCameraMatrix;
        }
        #endregion
    }
}
