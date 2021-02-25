using SD.Toolkits.SerialNumber.Entities;
using SD.Toolkits.SerialNumber.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.Toolkits.SerialNumber.Mediators
{
    /// <summary>
    /// 序列号生成器
    /// </summary>
    public sealed class NumberGenerator
    {
        #region # 字段及构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _SyncLock = new object();

        /// <summary>
        /// 序列种子仓储实现
        /// </summary>
        private readonly SerialSeedRepository _serialSeedRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        public NumberGenerator()
        {
            try
            {
                this._serialSeedRepository = new SerialSeedRepository();
            }
            catch (TypeLoadException typeLoadException)
            {
                if (typeLoadException.InnerException != null)
                {
                    throw typeLoadException.InnerException;
                }
                throw;
            }
        }

        #endregion

        #region # 生成序列号 —— string Generate(string seedName, string prefix...
        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="seedName">种子名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="timeFormat">时间格式</param>
        /// <param name="serialLength">流水长度</param>
        /// <param name="description">描述</param>
        /// <returns>序列号</returns>
        public string Generate(string seedName, string prefix, string timeFormat, int serialLength, string description)
        {
            lock (_SyncLock)
            {
                #region # 验证参数

                seedName = string.IsNullOrWhiteSpace(seedName) ? string.Empty : seedName;
                prefix = string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix;
                timeFormat = string.IsNullOrWhiteSpace(timeFormat) ? string.Empty : timeFormat;
                serialLength = serialLength < 1 ? 1 : serialLength;

                #endregion

                string timestamp = string.IsNullOrWhiteSpace(timeFormat) ? string.Empty : DateTime.Now.ToString(timeFormat);
                SerialSeed serialSeed = this._serialSeedRepository.SingleOrDefault(seedName, prefix, timestamp, serialLength);

                if (serialSeed == null)
                {
                    serialSeed = new SerialSeed(seedName, prefix, timestamp, serialLength, description);
                    this._serialSeedRepository.Create(serialSeed);
                }
                else
                {
                    serialSeed.UpdateInfo(serialSeed.TodayCount + 1);
                    this._serialSeedRepository.Save(serialSeed);
                }

                int serial = serialSeed.TodayCount;
                string serialText = serial.ToString($"D{serialLength}");

                StringBuilder keyBuilder = new StringBuilder();
                keyBuilder.Append(serialSeed.Prefix);
                keyBuilder.Append(serialSeed.Timestamp);
                keyBuilder.Append(serialText);

                return keyBuilder.ToString();
            }
        }
        #endregion

        #region # 批量生成序列号 —— string[] GenerateRange(string seedName, string prefix...
        /// <summary>
        /// 批量生成序列号
        /// </summary>
        /// <param name="seedName">种子名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="timeFormat">时间格式</param>
        /// <param name="serialLength">流水长度</param>
        /// <param name="description">描述</param>
        /// <param name="count">数量</param>
        /// <returns>序列号集</returns>
        public string[] GenerateRange(string seedName, string prefix, string timeFormat, int serialLength, string description, int count)
        {
            lock (_SyncLock)
            {
                #region # 验证参数

                seedName = string.IsNullOrWhiteSpace(seedName) ? string.Empty : seedName;
                prefix = string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix;
                timeFormat = string.IsNullOrWhiteSpace(timeFormat) ? string.Empty : timeFormat;
                serialLength = serialLength < 1 ? 1 : serialLength;
                if (count < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(count), "数量不可小于1！");
                }

                #endregion

                string timestamp = string.IsNullOrWhiteSpace(timeFormat) ? string.Empty : DateTime.Now.ToString(timeFormat);
                SerialSeed serialSeed = this._serialSeedRepository.SingleOrDefault(seedName, prefix, timestamp, serialLength);
                int initialIndex = serialSeed?.TodayCount ?? 0;

                if (serialSeed == null)
                {
                    serialSeed = new SerialSeed(seedName, prefix, timestamp, serialLength, description);
                    serialSeed.UpdateInfo(count);

                    this._serialSeedRepository.Create(serialSeed);
                }
                else
                {
                    serialSeed.UpdateInfo(serialSeed.TodayCount + count);
                    this._serialSeedRepository.Save(serialSeed);
                }

                ICollection<string> keys = new HashSet<string>();
                for (int index = 1; index <= count; index++)
                {
                    int serial = initialIndex + index;
                    string serialText = serial.ToString($"D{serialLength}");

                    StringBuilder keyBuilder = new StringBuilder();
                    keyBuilder.Append(serialSeed.Prefix);
                    keyBuilder.Append(serialSeed.Timestamp);
                    keyBuilder.Append(serialText);

                    keys.Add(keyBuilder.ToString());
                }

                return keys.ToArray();
            }
        }
        #endregion
    }
}
