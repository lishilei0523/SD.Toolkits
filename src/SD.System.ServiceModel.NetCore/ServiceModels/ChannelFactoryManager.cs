using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
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
                        ICollection<IEndpointBehavior> endpointBehaviors = this.GetEndpointBehaviors<T>();

                        factory = new ChannelFactory<T>(binding, address);

                        //添加终结点行为
                        foreach (IEndpointBehavior endpointBehavior in endpointBehaviors)
                        {
                            factory.Endpoint.EndpointBehaviors.Add(endpointBehavior);
                        }

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

            if (!ServiceModelExtension.AvailableBindings.ContainsKey(endpoint.Binding))
            {
                throw new InvalidOperationException($"目前不支持\"{endpoint.Binding}\"绑定！");
            }

            #endregion

            Binding binding = ServiceModelExtension.AvailableBindings[endpoint.Binding];

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
            EndpointAddress endpointAddress = new EndpointAddress(uri);

            return endpointAddress;
        }
        #endregion

        #region # 获取终结点行为列表 —— ICollection<IEndpointBehavior> GetEndpointBehaviors<T>()
        /// <summary>
        /// 获取终结点行为列表
        /// </summary>
        /// <returns>终结点行为列表</returns>
        private ICollection<IEndpointBehavior> GetEndpointBehaviors<T>()
        {
            string endpointName = typeof(T).FullName;

            #region # 验证

            if (!EndpointMediator.Endpoints.ContainsKey(endpointName))
            {
                throw new NullReferenceException($"名称为\"{endpointName}\"的终结点未配置！");
            }

            #endregion

            ICollection<IEndpointBehavior> endpointBehaviors = new HashSet<IEndpointBehavior>();
            EndpointElement endpoint = EndpointMediator.Endpoints[endpointName];
            if (!string.IsNullOrWhiteSpace(endpoint.BehaviorConfiguration))
            {
                BehaviorConfigurationElement behaviorConfiguration = EndpointMediator.BehaviorConfigurations[endpoint.BehaviorConfiguration];
                foreach (EndpointBehaviorElement endpointBehaviorElement in behaviorConfiguration.EndpointBehaviors)
                {
                    Assembly assembly = Assembly.Load(endpointBehaviorElement.Assembly);
                    Type type = assembly.GetType(endpointBehaviorElement.Type);

                    #region # 验证

                    if (!typeof(IEndpointBehavior).IsAssignableFrom(type))
                    {
                        throw new InvalidOperationException($"类型\"{type.FullName}\"未实现接口\"{nameof(IEndpointBehavior)}\"！");
                    }

                    #endregion

                    IEndpointBehavior endpointBehavior = (IEndpointBehavior)Activator.CreateInstance(type);
                    endpointBehaviors.Add(endpointBehavior);
                }
            }

            return endpointBehaviors;
        }
        #endregion
    }
}
