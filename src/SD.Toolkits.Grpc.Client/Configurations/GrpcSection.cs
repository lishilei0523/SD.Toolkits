using SD.Toolkits.Grpc.Client.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Grpc
{
    /// <summary>
    /// gRPC配置
    /// </summary>
    public class GrpcSection : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly GrpcSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static GrpcSection()
        {
            _Setting = (GrpcSection)ConfigurationManager.GetSection("sd.grpc");

            #region # 非空验证

            if (_Setting == null)
            {
                throw new ApplicationException("gRPC节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static GrpcSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static GrpcSection Setting
        {
            get { return _Setting; }
        }
        #endregion

        #region # 终节点列表 —— EndpointElementCollection EndpointElements
        /// <summary>
        /// 终节点列表
        /// </summary>
        [ConfigurationProperty("client")]
        [ConfigurationCollection(typeof(EndpointElementCollection), AddItemName = "endpoint")]
        public EndpointElementCollection EndpointElements
        {
            get
            {
                EndpointElementCollection collection = this["client"] as EndpointElementCollection;
                return collection ?? new EndpointElementCollection();
            }
            set
            {
                this["client"] = value;
            }
        }
        #endregion

        #region # 终节点配置列表 —— EndpointConfigurationElementCollection EndpointConfigurationElements
        /// <summary>
        /// 终节点列表
        /// </summary>
        [ConfigurationProperty("endpointConfigurations")]
        [ConfigurationCollection(typeof(EndpointConfigurationElementCollection), AddItemName = "endpointConfiguration")]
        public EndpointConfigurationElementCollection EndpointConfigurationElements
        {
            get
            {
                EndpointConfigurationElementCollection collection = this["endpointConfigurations"] as EndpointConfigurationElementCollection;
                return collection ?? new EndpointConfigurationElementCollection();
            }
            set
            {
                this["endpointConfigurations"] = value;
            }
        }
        #endregion

        #region # 授权拦截器列表 —— AuthInterceptorElementCollection AuthInterceptorElements
        /// <summary>
        /// 授权拦截器列表
        /// </summary>
        [ConfigurationProperty("authInterceptors")]
        [ConfigurationCollection(typeof(AuthInterceptorElementCollection), AddItemName = "authInterceptor")]
        public AuthInterceptorElementCollection AuthInterceptorElements
        {
            get
            {
                AuthInterceptorElementCollection collection = this["authInterceptors"] as AuthInterceptorElementCollection;
                return collection ?? new AuthInterceptorElementCollection();
            }
            set
            {
                this["authInterceptors"] = value;
            }
        }
        #endregion
    }
}
