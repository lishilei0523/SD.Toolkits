using SD.Toolkits.MVC.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.MVC
{
    /// <summary>
    /// MVC服务器配置
    /// </summary>
    public class MvcSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly MvcSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static MvcSection()
        {
            _Setting = (MvcSection)ConfigurationManager.GetSection("mvcSection");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("MVC节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static MvcSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static MvcSection Setting
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

        #region # 登录页节点 —— PageElement LoginPage
        /// <summary>
        /// 登录页节点
        /// </summary>
        [ConfigurationProperty("loginPage", IsRequired = false)]
        public PageElement LoginPage
        {
            get { return (PageElement)this["loginPage"]; }
            set { this["loginPage"] = value; }
        }
        #endregion

        #region # 错误页节点 —— PageElement ErrorPage
        /// <summary>
        /// 错误页节点
        /// </summary>
        [ConfigurationProperty("errorPage", IsRequired = false)]
        public PageElement ErrorPage
        {
            get { return (PageElement)this["errorPage"]; }
            set { this["errorPage"] = value; }
        }
        #endregion
    }
}
