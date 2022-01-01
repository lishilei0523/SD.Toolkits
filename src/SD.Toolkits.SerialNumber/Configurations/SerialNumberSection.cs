using SD.Toolkits.SerialNumber.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.SerialNumber
{
    /// <summary>
    /// SD.Toolkits.SerialNumber配置
    /// </summary>
    public class SerialNumberSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static SerialNumberSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static SerialNumberSection()
        {
            _Setting = null;
        }

        #endregion

        #region # 初始化 —— static void Initialize(Configuration configuration)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">配置</param>
        public static void Initialize(Configuration configuration)
        {
            #region # 验证

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "配置不可为空！");
            }

            #endregion

            _Setting = (SerialNumberSection)configuration.GetSection("sd.toolkits.serialNumber");
        }
        #endregion

        #region # 访问器 —— static SerialNumberSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static SerialNumberSection Setting
        {
            get
            {
                if (_Setting == null)
                {
                    _Setting = (SerialNumberSection)ConfigurationManager.GetSection("sd.toolkits.serialNumber");
                }
                if (_Setting == null)
                {
                    throw new ApplicationException("SD.Toolkits.SerialNumber节点未配置，请检查程序！");
                }

                return _Setting;
            }
        }
        #endregion

        #region # 序列种子提供者节点 —— SerialSeedProviderElement SerialSeedProvider
        /// <summary>
        /// 序列种子提供者节点
        /// </summary>
        [ConfigurationProperty("serialSeedProvider", IsRequired = true)]
        public SerialSeedProviderElement SerialSeedProvider
        {
            get { return (SerialSeedProviderElement)this["serialSeedProvider"]; }
            set { this["serialSeedProvider"] = value; }
        }
        #endregion

        #region # 连接字符串节点 —— ConnectionStringElement ConnectionString
        /// <summary>
        /// 连接字符串节点
        /// </summary>
        [ConfigurationProperty("connectionString", IsRequired = false)]
        public ConnectionStringElement ConnectionString
        {
            get { return (ConnectionStringElement)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }
        #endregion
    }
}
