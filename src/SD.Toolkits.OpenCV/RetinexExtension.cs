using OpenCvSharp;
using System;

namespace SD.Toolkits.OpenCV
{
    /// <summary>
    /// Retinex算法扩展
    /// </summary>
    public static class RetinexExtension
    {
        #region # 单尺度Retinex增强 —— static Mat SingleScaleRetinex(this Mat matrix, float sigma)
        /// <summary>
        /// 单尺度Retinex增强
        /// </summary>
        /// <param name="matrix">图像矩阵</param>
        /// <param name="sigma">标准偏差值</param>
        /// <returns>增强图像矩阵</returns>
        public static unsafe Mat SingleScaleRetinex(this Mat matrix, float sigma)
        {
            Size matrixSize = matrix.Size();
            using Mat logMatrix = new Mat(matrixSize, MatType.CV_32FC3);
            using Mat gaussMatrix = new Mat(matrixSize, MatType.CV_32FC3);
            using Mat gaussLogMatrix = new Mat(matrixSize, MatType.CV_32FC3);
            using Mat dstLogMatrix = new Mat(matrixSize, MatType.CV_32FC3);
            Mat result = new Mat(matrixSize, MatType.CV_32FC3);

            float minValue = 0.01f;
            int kernelSize = (int)(sigma * 3 / 2) * 2 + 1;

            //求Log(S(x,y))
            matrix.ForEachAsVec3b((valuePtr, positionPtr) =>
            {
                int rowIndex = positionPtr[0];
                int colIndex = positionPtr[1];
                Vec3b vector = *valuePtr;
                logMatrix.At<Vec3f>(rowIndex, colIndex)[0] = vector[0] <= minValue ? minValue : (float)Math.Log10(vector[0]);
                logMatrix.At<Vec3f>(rowIndex, colIndex)[1] = vector[1] <= minValue ? minValue : (float)Math.Log10(vector[1]);
                logMatrix.At<Vec3f>(rowIndex, colIndex)[2] = vector[2] <= minValue ? minValue : (float)Math.Log10(vector[2]);
            });

            //求Log(L(x,y))
            Cv2.GaussianBlur(matrix, gaussMatrix, new Size(kernelSize, kernelSize), sigma);
            gaussMatrix.ForEachAsVec3b((valuePtr, positionPtr) =>
            {
                int rowIndex = positionPtr[0];
                int colIndex = positionPtr[1];
                Vec3b vector = *valuePtr;
                gaussLogMatrix.At<Vec3f>(rowIndex, colIndex)[0] = vector[0] <= minValue ? minValue : (float)Math.Log10(vector[0]);
                gaussLogMatrix.At<Vec3f>(rowIndex, colIndex)[1] = vector[1] <= minValue ? minValue : (float)Math.Log10(vector[1]);
                gaussLogMatrix.At<Vec3f>(rowIndex, colIndex)[2] = vector[2] <= minValue ? minValue : (float)Math.Log10(vector[2]);
            });

            //求Log(S(x,y)) - Log(L(x,y))
            gaussLogMatrix.ForEachAsVec3f((valuePtr, positionPtr) =>
            {
                int rowIndex = positionPtr[0];
                int colIndex = positionPtr[1];
                Vec3f vector1 = logMatrix.At<Vec3f>(rowIndex, colIndex);
                Vec3f vector2 = gaussLogMatrix.At<Vec3f>(rowIndex, colIndex);
                dstLogMatrix.At<Vec3f>(rowIndex, colIndex)[0] = vector1[0] - vector2[0];
                dstLogMatrix.At<Vec3f>(rowIndex, colIndex)[1] = vector1[1] - vector2[1];
                dstLogMatrix.At<Vec3f>(rowIndex, colIndex)[2] = vector1[2] - vector2[2];
            });

            //求B/G/R三通道的min，max
            Vec3f bgrMin = new Vec3f();
            Vec3f bgrMax = new Vec3f();
            dstLogMatrix.ForEachAsVec3f((valuePtr, positionPtr) =>
            {
                Vec3f vector = *valuePtr;
                if (vector[0] > bgrMax[0])
                {
                    bgrMax[0] = vector[0];
                }
                if (vector[1] > bgrMax[1])
                {
                    bgrMax[1] = vector[1];
                }
                if (vector[2] > bgrMax[2])
                {
                    bgrMax[2] = vector[2];
                }
                if (vector[0] < bgrMin[0])
                {
                    bgrMin[0] = vector[0];
                }
                if (vector[1] < bgrMin[1])
                {
                    bgrMin[1] = vector[1];
                }
                if (vector[2] < bgrMin[2])
                {
                    bgrMin[2] = vector[2];
                }
            });

            //量化处理
            dstLogMatrix.ForEachAsVec3f((valuePtr, positionPtr) =>
            {
                int rowIndex = positionPtr[0];
                int colIndex = positionPtr[1];
                Vec3f vector = *valuePtr;
                result.At<Vec3f>(rowIndex, colIndex)[0] = 255 * (vector[0] - bgrMin[0]) / (bgrMax[0] - bgrMin[0]);
                result.At<Vec3f>(rowIndex, colIndex)[1] = 255 * (vector[1] - bgrMin[1]) / (bgrMax[1] - bgrMin[1]);
                result.At<Vec3f>(rowIndex, colIndex)[2] = 255 * (vector[2] - bgrMin[2]) / (bgrMax[2] - bgrMin[2]);
            });

            //转回8UC3
            result.ConvertTo(result, MatType.CV_8UC3);

            return result;
        }
        #endregion
    }
}
