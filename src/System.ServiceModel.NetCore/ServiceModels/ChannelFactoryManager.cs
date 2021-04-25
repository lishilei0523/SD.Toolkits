using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.NetCore.Configurations;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.NetCore
{
    /// <summary>
    /// ChannelFactory管理者
    /// </summary>
    internal sealed class ChannelFactoryManager : IDisposable
    {
        #region # 字段及构造器

        /// <summary>
        /// 信道工厂幂等字典
        /// </summary>
        private static readonly IDictionary<Type, ChannelFactory> _Factories;

        /// <summary>
        /// 信道工厂管理者单例
        /// </summary>
        private static readonly ChannelFactoryManager _Current;

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ChannelFactoryManager()
        {
            _Factories = new Dictionary<Type, ChannelFactory>();
            _Current = new ChannelFactoryManager();
            _Sync = new object();
        }

        /// <summary>
        /// 私有化构造器
        /// </summary>
        private ChannelFactoryManager() { }

        #endregion


        //Public

        #region # 访问器 —— static ChannelFactoryManager Current
        /// <summary>
        /// 访问器
        /// </summary>
        public static ChannelFactoryManager Current
        {
            get { return _Current; }
        }
        #endregion

        #region # 获取给定服务契约类型的ChannelFactory实例 —— ChannelFactory<T> GetFactory<T>()
        /// <summary>
        /// 获取给定服务契约类型的ChannelFactory实例
        /// </summary>
        /// <typeparam name="T">服务契约类型</typeparam>
        /// <returns>给定服务契约类型的ChannelFactory实例</returns>
        public ChannelFactory<T> GetFactory<T>()
        {
            lock (_Sync)
            {
                ChannelFactory factory = null;
                try
                {
                    if (!_Factories.TryGetValue(typeof(T), out factory))
                    {
                        Binding binding = this.GetBinding<T>();
                        EndpointAddress address = this.GetEndpointAddress<T>();

                        factory = new ChannelFactory<T>(binding, address);
                        _Factories.Add(typeof(T), factory);
                    }

                    return factory as ChannelFactory<T>;
                }
                catch
                {
                    factory?.CloseChannel();
                    throw;
                }

            }
        }
        #endregion

        #region # 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            lock (_Sync)
            {
                foreach (Type type in _Factories.Keys)
                {
                    ChannelFactory factory = _Factories[type];
                    factory?.CloseChannel();
                }

                _Factories.Clear();
            }
        }
        #endregion


        //Private

        #region # 获取绑定 —— Binding GetBinding<T>()
        /// <summary>
        /// 获取绑定
        /// </summary>
        /// <returns>绑定实例</returns>
        private Binding GetBinding<T>()
        {
            string endpointName = typeof(T).FullName;

            #region # 验证

            if (!EndpointMediator.Endpoints.ContainsKey(endpointName))
            {
                throw new NullReferenceException($"名称为\"{endpointName}\"的终结点未配置！");
            }

            #endregion

            EndpointElement endpoint = EndpointMediator.Endpoints[endpointName];

            #region # 验证

            if (!Constants.AvailableBindings.ContainsKey(endpoint.Binding))
            {
                throw new InvalidOperationException($"目前不支持\"{endpoint.Binding}\"绑定！");
            }

            #endregion

            Binding binding = Constants.AvailableBindings[endpoint.Binding];

            return binding;
        }
        #endregion

        #region # 获取终结点地址 —— EndpointAddress GetEndpointAddress<T>()
        /// <summary>
        /// 获取终结点地址
        /// </summary>
        /// <returns>终结点地址</returns>
        private EndpointAddress GetEndpointAddress<T>()
        {
            string endpointName = typeof(T).FullName;

            #region # 验证

            if (!EndpointMediator.Endpoints.ContainsKey(endpointName))
            {
                throw new NullReferenceException($"名称为\"{endpointName}\"的终结点未配置！");
            }

            #endregion

            EndpointElement endpoint = EndpointMediator.Endpoints[endpointName];
            Uri uri = new Uri(endpoint.Address);
            AddressHeader[] addressHeaders = null;

            #region # 消息头处理
            //TODO 
            //if (!string.IsNullOrWhiteSpace(endpoint.AddressHeaderProvider?.Type) &&
            //    !string.IsNullOrWhiteSpace(endpoint.AddressHeaderProvider?.Assembly))
            //{
            //    Assembly assembly = Assembly.Load(endpoint.AddressHeaderProvider.Assembly);
            //    Type type = assembly.GetType(endpoint.AddressHeaderProvider.Type);

            //    #region # 验证

            //    if (!typeof(IAddressHeaderProvider).IsAssignableFrom(type))
            //    {
            //        throw new InvalidOperationException($"类型\"{type.FullName}\"未实现接口\"{nameof(IAddressHeaderProvider)}\"！");
            //    }

            //    #endregion

            //    IAddressHeaderProvider addressHeaderProvider = (IAddressHeaderProvider)Activator.CreateInstance(type);
            //    addressHeaders = addressHeaderProvider.GetAddressHeaders();
            //}

            #endregion

            EndpointAddress endpointAddress = new EndpointAddress(uri, addressHeaders ?? new AddressHeader[0]);

            return endpointAddress;
        }
        #endregion
    }
}
