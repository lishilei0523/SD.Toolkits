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
        private static GrpcSection _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static GrpcSection()
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

            _Setting = (GrpcSection)configuration.GetSection("sd.grpc");
        }
        #endregion

        #region # 访问器 —— static GrpcSection Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static GrpcSection Setting
        {
            get
            {
                if (_Setting == null)
                {
                    _Setting = (GrpcSection)ConfigurationManager.GetSection("sd.grpc");
                }
                if (_Setting == null)
                {
                    throw new ApplicationException("gRPC节点未配置，请检查程序！");
                }

                return _Setting;
            }
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
