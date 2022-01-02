using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel
{
    /// <summary>
    /// WCF�ڵ���
    /// </summary>
    public sealed class ServiceModelSectionGroup : ConfigurationSectionGroup
    {
        #region # �ֶμ�������

        /// <summary>
        /// ����
        /// </summary>
        private static ServiceModelSectionGroup _Setting;

        /// <summary>
        /// ��̬������
        /// </summary>
        static ServiceModelSectionGroup()
        {
            _Setting = null;
        }

        #endregion

        #region # ��ʼ�� ���� static void Initialize(Configuration configuration)
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="configuration">����</param>
        public static void Initialize(Configuration.Configuration configuration)
        {
            #region # ��֤

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "���ò���Ϊ�գ�");
            }

            #endregion

            _Setting = (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel.client") ??
                       (ServiceModelSectionGroup)configuration.GetSectionGroup("system.serviceModel");
        }
        #endregion

        #region # ������ ���� static ServiceModelSectionGroup Setting
        /// <summary>
        /// ������
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
                    throw new ApplicationException("WCF�ڵ���δ���ã��������");
                }

                return _Setting;
            }
        }
        #endregion

        #region # �ͻ��˽ڵ� ���� ClientsSection Clients
        /// <summary>
        /// �ͻ��˽ڵ�
        /// </summary>
        public ClientsSection Clients
        {
            get => (ClientsSection)this.Sections[ConfigurationStrings.ClientSectionName];
        }
        #endregion

        #region # �󶨽ڵ� ���� BindingsSection Bindings
        /// <summary>
        /// �󶨽ڵ�
        /// </summary>
        public BindingsSection Bindings
        {
            get => (BindingsSection)this.Sections[ConfigurationStrings.BindingsSectionGroupName];
        }
        #endregion

        #region # ��Ϊ�ڵ� ���� BehaviorsSection Behaviors
        /// <summary>
        /// ��Ϊ�ڵ�
        /// </summary>
        public BehaviorsSection Behaviors
        {
            get => (BehaviorsSection)this.Sections[ConfigurationStrings.EndpointBehaviors];
        }
        #endregion
    }
}
