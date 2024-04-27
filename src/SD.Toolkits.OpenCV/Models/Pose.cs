namespace SD.Toolkits.OpenCV.Models
{
    /// <summary>
    /// 位姿
    /// </summary>
    public class Pose
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        private Pose() { }
        #endregion

        #region 01.创建位姿构造器
        /// <summary>
        /// 创建位姿构造器
        /// </summary>
        /// <param name="id">标识Id</param>
        /// <param name="x">X轴位置</param>
        /// <param name="y">Y轴位置</param>
        /// <param name="z">Z轴位置</param>
        /// <param name="rx">X轴旋转角度</param>
        /// <param name="ry">Y轴旋转角度</param>
        /// <param name="rz">Z轴旋转角度</param>
        public Pose(string id, double x, double y, double z, double rx, double ry, double rz)
            : this()
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.RX = rx;
            this.RY = ry;
            this.RZ = rz;
        }
        #endregion

        #endregion

        #region # 属性

        #region 标识Id —— string Id
        /// <summary>
        /// 标识Id
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region X轴位置 —— double X
        /// <summary>
        /// X轴位置
        /// </summary>
        public double X { get; private set; }
        #endregion

        #region Y轴位置 —— double Y
        /// <summary>
        /// Y轴位置
        /// </summary>
        public double Y { get; private set; }
        #endregion

        #region Z轴位置 —— double Z
        /// <summary>
        /// Z轴位置
        /// </summary>
        public double Z { get; private set; }
        #endregion

        #region X轴旋转角度 —— double RX
        /// <summary>
        /// X轴旋转角度
        /// </summary>
        public double RX { get; private set; }
        #endregion

        #region Y轴旋转角度 —— double RY
        /// <summary>
        /// Y轴旋转角度
        /// </summary>
        public double RY { get; private set; }
        #endregion

        #region Z轴旋转角度 —— double RZ
        /// <summary>
        /// Z轴旋转角度
        /// </summary>
        public double RZ { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 获取平移向量 —— double[] GetTranslationArray3x1()
        /// <summary>
        /// 获取平移向量
        /// </summary>
        /// <returns>平移向量: 1维数组3x1</returns>
        public double[] GetTranslationArray3x1()
        {
            return new[] { this.X, this.Y, this.Z };
        }
        #endregion

        #region 获取欧拉角向量 —— double[] GetEulerAngles()
        /// <summary>
        /// 获取欧拉角向量
        /// </summary>
        /// <returns>欧拉角向量: 1维数组3x1</returns>
        public double[] GetEulerAngles()
        {
            double[] eulerAngles = new double[] { this.RX, this.RY, this.RZ };

            return eulerAngles;
        }
        #endregion

        #endregion
    }
}
