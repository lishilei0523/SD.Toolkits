using SD.Toolkits.SerialNumber.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SD.Toolkits.SerialNumber.Facade
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
            NumberGenerator._SyncLock = new object();
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
            lock (NumberGenerator._SyncLock)
            {
                Model.SerialNumber serialNumber = this._generatorDal.SingleOrDefault(prefix, formatDate, className, length);
                if (serialNumber == null)
                {
                    serialNumber = new Model.SerialNumber(prefix, formatDate, className, length, string.Format("创建{0}", description));
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

        #region # 批量生成编号方法 —— string[] GenerateNumbers(string prefix, string formatDate...
        /// <summary>
        /// 批量生成编号方法
        /// </summary>
        /// <param name="prefix">编号前缀</param>
        /// <param name="formatDate">格式化日期</param>
        /// <param name="className">类名</param>
        /// <param name="length">流水号长度</param>
        /// <param name="description">编号描述</param>
        /// <param name="count">生成数量</param>
        /// <returns>编号集</returns>
        public string[] GenerateNumbers(string prefix, string formatDate, string className, int length, string description, int count)
        {
            lock (_SyncLock)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    ICollection<string> numbers = new HashSet<string>();

                    for (int i = 0; i < count; i++)
                    {
                        Model.SerialNumber serialNumber = this._generatorDal.SingleOrDefault(prefix, formatDate, className, length);
                        if (serialNumber == null)
                        {
                            serialNumber = new Model.SerialNumber(prefix, formatDate, className, length, $"创建{description}");
                            this._generatorDal.Add(serialNumber);
                        }
                        else
                        {
                            serialNumber.UpdateInfo(serialNumber.TodayCount + 1, $"新增{description}");
                            this._generatorDal.Save(serialNumber);
                        }

                        StringBuilder numberBuilder = new StringBuilder();
                        numberBuilder.Append(serialNumber.Prefix);
                        numberBuilder.Append(serialNumber.FormatDate);
                        numberBuilder.Append(serialNumber.TodayCount.ToString($"D{length}"));

                        numbers.Add(numberBuilder.ToString());
                    }

                    scope.Complete();

                    return numbers.ToArray();
                }
            }
        }
        #endregion
    }
}
