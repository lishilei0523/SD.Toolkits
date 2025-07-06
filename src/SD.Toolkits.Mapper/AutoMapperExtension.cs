using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SD.Toolkits.Mapper
{
    /// <summary>
    /// AutoMapper扩展
    /// </summary>
    public static class AutoMapperExtension
    {
        #region # 字段及构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        /// <summary>
        /// 映射配置字典
        /// </summary>

        private static readonly IDictionary<string, MapperConfiguration> _MapperConfigurations;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AutoMapperExtension()
        {
            _MapperConfigurations = new ConcurrentDictionary<string, MapperConfiguration>();
        }

        #endregion

        #region # 映射 —— static TTarget Map<TSource, TTarget>(this TSource...
        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="TSource">源实例类型</typeparam>
        /// <typeparam name="TTarget">目标实例类型</typeparam>
        /// <param name="sourceInstance">源实例</param>
        /// <param name="beforeMapEventHandler">映射前事件处理者</param>
        /// <param name="afterMapEventHandler">映射后事件处理者</param>
        /// <param name="ignoreMembers">忽略映射成员集</param>
        /// <returns>目标实例</returns>
        public static TTarget Map<TSource, TTarget>(this TSource sourceInstance, Action<TSource, TTarget> beforeMapEventHandler = null, Action<TSource, TTarget> afterMapEventHandler = null, params Expression<Func<TTarget, object>>[] ignoreMembers)
        {
            #region # 验证

            if (sourceInstance == null)
            {
                return default(TTarget);
            }

            #endregion

            lock (_Sync)
            {
                string key = $"{typeof(TSource).FullName},{typeof(TTarget).FullName}";
                if (!_MapperConfigurations.TryGetValue(key, out MapperConfiguration mapperConfiguration))
                {
                    MapperConfigurationExpression configExpression = new MapperConfigurationExpression();
                    IMappingExpression<TSource, TTarget> mappingExpression = configExpression.CreateMap<TSource, TTarget>();

                    #region # 忽略映射成员处理

                    foreach (Expression<Func<TTarget, object>> ignoreMember in ignoreMembers)
                    {
                        mappingExpression.ForMember(ignoreMember, source => source.Ignore());
                    }

                    #endregion

                    #region # 映射前后事件处理

                    if (beforeMapEventHandler != null)
                    {
                        mappingExpression.BeforeMap(beforeMapEventHandler);
                    }
                    if (afterMapEventHandler != null)
                    {
                        mappingExpression.AfterMap(afterMapEventHandler);
                    }

                    #endregion

                    mapperConfiguration = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
                    _MapperConfigurations.Add(key, mapperConfiguration);
                }

                IMapper mapper = new AutoMapper.Mapper(mapperConfiguration);
                TTarget targetInstance = mapper.Map<TTarget>(sourceInstance);

                return targetInstance;
            }
        }
        #endregion
    }
}
