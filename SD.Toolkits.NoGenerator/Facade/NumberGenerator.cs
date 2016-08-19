using System;
using System.Text;
using SD.Toolkits.NoGenerator.DAL;
using SD.Toolkits.NoGenerator.Model;

namespace SD.Toolkits.NoGenerator.Facade
{
    /// <summary>
    /// 编号生成器API
    /// </summary>
    public sealed class NumberGenerator
    {
        #region # 字段及构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _SyncLock;

        /// <summary>
        /// 编号生成器数据访问层对象
        /// </summary>
        private readonly GeneratorDal _generatorDal;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static NumberGenerator()
        {
            _SyncLock = new object();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public NumberGenerator()
        {
            try
            {
                this._generatorDal = new GeneratorDal();
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

        #region # 生成编号方法 —— string GenerateNumber(string prefix, string formatDate...
        /// <summary>
        /// 生成编号方法
        /// </summary>
        /// <param name="prefix">编号前缀</param>
        /// <param name="formatDate">格式化日期</param>
        /// <param name="className">类名</param>
        /// <param name="length">流水号长度</param>
        /// <param name="description">编号描述</param>
        /// <returns>编号</returns>
        public string GenerateNumber(string prefix, string formatDate, string className, int length, string description)
        {
            lock (_SyncLock)
            {
                SerialNumber serialNumber = this._generatorDal.SingleOrDefault(prefix, formatDate, className, length);
                if (serialNumber == null)
                {
                    serialNumber = new SerialNumber(prefix, formatDate, className, length, string.Format("创建{0}", description));
                    this._generatorDal.Add(serialNumber);
                }
                else
                {
                    serialNumber.UpdateInfo(serialNumber.TodayCount + 1, string.Format("新增{0}", description));
                    this._generatorDal.Save(serialNumber);
                }

                StringBuilder numberBuilder = new StringBuilder();
                numberBuilder.Append(serialNumber.Prefix);
                numberBuilder.Append(serialNumber.FormatDate);
                numberBuilder.Append(serialNumber.TodayCount.ToString(string.Format("D{0}", length)));

                return numberBuilder.ToString();
            }
        }
        #endregion
    }
}
