using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// WCF节点组
    /// </summary>
    public sealed class ServiceModelSectionGroup : ConfigurationSectionGroup
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static ServiceModelSectionGroup _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ServiceModelSectionGroup()
        {
            _Setting = null;
        }

        #endregion

        #region # 初始化 —— static void Initialize(Configuration configuration)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">配置</param>
        public static void Initialize(Configuration.Configuration configuration)
        {
            #region # 验证

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "配置不可为空！");
            }

            #endregion

            _Setting = (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel.client") ??
                       (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel");
        }
        #endregion

        #region # 访问器 —— static ServiceModelSectionGroup Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static ServiceModelSectionGroup Setting
        {
            get
            {
                if (_Setting == null)
                {
                    Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _Setting = (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel.client") ??
                               (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel");
                }
                if (_Setting == null)
                {
                    throw new ApplicationException("WCF节点组未配置，请检查程序！");
                }

                return _Setting;
            }
        }
        #endregion

        #region # 客户端节点 —— ClientsSection Clients
        /// <summary>
        /// 客户端节点
        /// </summary>
        public ClientsSection Clients
        {
            get => (ClientsSection)this.Sections[ConfigurationStrings.ClientSectionName];
        }
        #endregion

        #region # 绑定节点 —— BindingsSection Bindings
        /// <summary>
        /// 绑定节点
        /// </summary>
        public BindingsSection Bindings
        {
            get => (BindingsSection)this.Sections[ConfigurationStrings.BindingsSectionGroupName];
        }
        #endregion

        #region # 行为节点 —— BehaviorsSection Behaviors
        /// <summary>
        /// 行为节点
        /// </summary>
        public BehaviorsSection Behaviors
        {
            get => (BehaviorsSection)this.Sections[ConfigurationStrings.EndpointBehaviors];
        }
        #endregion
    }
}
