using SD.Toolkits.AspNet.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.AspNet
{
    /// <summary>
    /// ASP.NET服务器配置
    /// </summary>
    public class AspNetSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly AspNetSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AspNetSection()
        {
            _Setting = (AspNetSection)ConfigurationManager.GetSection("aspNetConfiguration");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("ASP.NET节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static AspNetSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static AspNetSection Setting
        {
            get { return _Setting; }
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

        #region # 节点地址列表 —— HostElementCollection HostElements
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
    }
}
