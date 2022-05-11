using SD.Toolkits.AspNet.Configurations;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.AspNet
{
    /// <summary>
    /// ASP.NET设置
    /// </summary>
    public static class AspNetSetting
    {
        #region # 字段及构造器

        /// <summary>
        /// Http端口列表
        /// </summary>
        private static readonly ICollection<int> _HttpPorts;

        /// <summary>
        /// Https端口列表
        /// </summary>
        private static readonly ICollection<int> _HttpsPorts;

        /// <summary>
        /// Net.TCP端口列表
        /// </summary>
        private static readonly ICollection<int> _NetTcpPorts;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AspNetSetting()
        {
            _HttpPorts = new HashSet<int>();
            _HttpsPorts = new HashSet<int>();
            _NetTcpPorts = new HashSet<int>();
            foreach (HostElement hostElement in AspNetSection.Setting.HostElements)
            {
                if (hostElement.Protocol == Constants.Http)
                {
                    _HttpPorts.Add(hostElement.Port);
                }
                if (hostElement.Protocol == Constants.Https)
                {
                    _HttpsPorts.Add(hostElement.Port);
                }
                if (hostElement.Protocol == Constants.NetTcp)
                {
                    _NetTcpPorts.Add(hostElement.Port);
                }
            }
        }

        #endregion

        #region # 是否需授权 —— static bool Authorized
        /// <summary>
        /// 是否需授权
        /// </summary>
        public static bool Authorized
        {
            get { return AspNetSection.Setting.Authorized; }
        }
        #endregion

        #region # Http端口列表 —— static ICollection<int> HttpPorts
        /// <summary>
        /// Http端口列表
        /// </summary>
        public static ICollection<int> HttpPorts
        {
            get { return _HttpPorts; }
        }
        #endregion

        #region # Https端口列表 —— static ICollection<int> HttpsPorts
        /// <summary>
        /// Https端口列表
        /// </summary>
        public static ICollection<int> HttpsPorts
        {
            get { return _HttpsPorts; }
        }
        #endregion

        #region # Net.TCP端口列表 —— static ICollection<int> NetTcpPorts
        /// <summary>
        /// Net.TCP端口列表
        /// </summary>
        public static ICollection<int> NetTcpPorts
        {
            get { return _NetTcpPorts; }
        }
        #endregion

        #region # 应用程序名称 —— static string ApplicationName
        /// <summary>
        /// 应用程序名称节点
        /// </summary>
        public static string ApplicationName
        {
            get { return AspNetSection.Setting.ApplicationName.Value; }
        }
        #endregion

        #region # 机器标识 —— static string MachineKey
        /// <summary>
        /// 机器标识
        /// </summary>
        public static string MachineKey
        {
            get { return AspNetSection.Setting.MachineKey.Value; }
        }
        #endregion

        #region # 登录页 —— static string LoginPage
        /// <summary>
        /// 登录页
        /// </summary>
        public static string LoginPage
        {
            get { return AspNetSection.Setting.LoginPage.Value; }
        }
        #endregion

        #region # 错误页 —— static string ErrorPage
        /// <summary>
        /// 错误页
        /// </summary>
        public static string ErrorPage
        {
            get { return AspNetSection.Setting.ErrorPage.Value; }
        }
        #endregion

        #region # 静态文件路径 —— static string StaticFilesPath
        /// <summary>
        /// 静态文件路径
        /// </summary>
        public static string StaticFilesPath
        {
            get { return AspNetSection.Setting.StaticFiles.Value; }
        }
        #endregion

        #region # 文件服务器路径 —— static string FileServerPath
        /// <summary>
        /// 文件服务器路径
        /// </summary>
        public static string FileServerPath
        {
            get { return AspNetSection.Setting.FileServer.Value; }
        }
        #endregion

        #region # OWIN服务器URL地址列表 —— static ICollection<string> OwinUrls
        /// <summary>
        /// OWIN服务器URL地址列表
        /// </summary>
        public static ICollection<string> OwinUrls
        {
            get
            {
                ICollection<string> owinUrls = new HashSet<string>();
                foreach (int httpPort in HttpPorts)
                {
                    owinUrls.Add($"http://*:{httpPort}");
                }
                foreach (int httpsPort in HttpsPorts)
                {
                    owinUrls.Add($"https://*:{httpsPort}");
                }

                return owinUrls;
            }
        }
        #endregion

        #region # X509证书 —— static X509Certificate2 X509
        /// <summary>
        /// X509证书
        /// </summary>
        public static X509Certificate2 X509
        {
            get
            {
                string path = AspNetSection.Setting.X509.Path;
                string password = AspNetSection.Setting.X509.Password;
                X509Certificate2 x509 = new X509Certificate2(path, password);

                return x509;
            }
        }
        #endregion
    }
}
