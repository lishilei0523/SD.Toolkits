using SD.Toolkits.SerialNumber.Entities;

namespace SD.Toolkits.SerialNumber.Interfaces
{
    /// <summary>
    /// 序列种子提供者接口
    /// </summary>
    public interface ISerialSeedProvider
    {
        #region # 获取唯一序列种子 —— SerialSeed SingleOrDefault(string seedName...
        /// <summary>
        /// 获取唯一序列种子，
        /// </summary>
        /// <param name="seedName">种子名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="serialLength">流水长度</param>
        /// <returns>序列种子</returns>
        /// <remarks>如没有，则返回null</remarks>
        SerialSeed SingleOrDefault(string seedName, string prefix, string timestamp, int serialLength);
        #endregion

        #region # 创建序列种子 —— void Create(SerialSeed serialSeed)
        /// <summary>
        /// 创建序列种子
        /// </summary>
        /// <param name="serialSeed">序列种子</param>
        void Create(SerialSeed serialSeed);
        #endregion

        #region # 保存序列种子 —— void Save(SerialSeed serialSeed)
        /// <summary>
        /// 保存序列种子
        /// </summary>
        /// <param name="serialSeed">序列种子</param>
        /// <returns>受影响的行数</returns>
        void Save(SerialSeed serialSeed);
        #endregion
    }
}
