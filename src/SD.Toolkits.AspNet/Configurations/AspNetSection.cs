using SD.Toolkits.AspNet.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.AspNet
{
    /// <summary>
    /// SD.Toolkits.AspNet配置
    /// </summary>
    public class AspNetSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static AspNetSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AspNetSection()
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

            _Setting = (AspNetSection)configuration.GetSection("sd.toolkits.aspNet");
        }
        #endregion

        #region # 访问器 —— static AspNetSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static AspNetSection Setting
        {
            get
            {
                if (_Setting == null)
                {
                    _Setting = (AspNetSection)ConfigurationManager.GetSection("sd.toolkits.aspNet");
                }
                if (_Setting == null)
                {
                    throw new ApplicationException("SD.Toolkits.AspNet节点未配置，请检查程序！");
                }

                return _Setting;
            }
        }
        #endregion

        #region # 是否需授权 —— bool Authorized
        /// <summary>
        /// 是否需授权
        /// </summary>
        [ConfigurationProperty("authorized", IsRequired = true, IsKey = true)]
        public bool Authorized
        {
            get { return (bool)this["authorized"]; }
            set { this["authorized"] = value; }
        }
        #endregion

        #region # 节点地址列表节点 —— HostElementCollection HostElements
        /// <summary>
        /// 节点地址列表
        /// </summary>
        [ConfigurationProperty("hosts")]
        [ConfigurationCollection(typeof(HostElementCollection), AddItemName = "host")]
        public HostElementCollection HostElements
        {
            get
            {
                HostElementCollection collection = this["hosts"] as HostElementCollection;
                return collection ?? new HostElementCollection();
            }
            set { this["hosts"] = value; }
        }
        #endregion

        #region # 应用程序名称节点 —— TextElement ApplicationName
        /// <summary>
        /// 应用程序名称节点
        /// </summary>
        [ConfigurationProperty("applicationName", IsRequired = false)]
        public TextElement ApplicationName
        {
            get { return (TextElement)this["applicationName"]; }
            set { this["applicationName"] = value; }
        }
        #endregion

        #region # 机器标识节点 —— TextElement MachineKey
        /// <summary>
        /// 机器标识节点
        /// </summary>
        [ConfigurationProperty("machineKey", IsRequired = false)]
        public TextElement MachineKey
        {
            get { return (TextElement)this["machineKey"]; }
            set { this["machineKey"] = value; }
        }
        #endregion

        #region # 登录页节点 —— TextElement LoginPage
        /// <summary>
        /// 登录页节点
        /// </summary>
        [ConfigurationProperty("loginPage", IsRequired = false)]
        public TextElement LoginPage
        {
            get { return (TextElement)this["loginPage"]; }
            set { this["loginPage"] = value; }
        }
        #endregion

        #region # 错误页节点 —— TextElement ErrorPage
        /// <summary>
        /// 错误页节点
        /// </summary>
        [ConfigurationProperty("errorPage", IsRequired = false)]
        public TextElement ErrorPage
        {
            get { return (TextElement)this["errorPage"]; }
            set { this["errorPage"] = value; }
        }
        #endregion

        #region # 静态文件节点 —— TextElement StaticFiles
        /// <summary>
        /// 静态文件节点
        /// </summary>
        [ConfigurationProperty("staticFiles", IsRequired = false)]
        public TextElement StaticFiles
        {
            get { return (TextElement)this["staticFiles"]; }
            set { this["staticFiles"] = value; }
        }
        #endregion

        #region # 文件服务器节点 —— TextElement FileServer
        /// <summary>
        /// 文件服务器节点
        /// </summary>
        [ConfigurationProperty("fileServer", IsRequired = false)]
        public TextElement FileServer
        {
            get { return (TextElement)this["fileServer"]; }
            set { this["fileServer"] = value; }
        }
        #endregion

        #region # X509证书节点 —— X509Element X509
        /// <summary>
        /// 文件服务器节点
        /// </summary>
        [ConfigurationProperty("x509", IsRequired = false)]
        public X509Element X509
        {
            get { return (X509Element)this["x509"]; }
            set { this["x509"] = value; }
        }
        #endregion
    }
}
