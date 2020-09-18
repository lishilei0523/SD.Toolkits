using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Linq.Expressions;

namespace SD.Toolkits.Mapper
{
    /// <summary>
    /// AutoMapper扩展
    /// </summary>
    public static class AutoMapperExtension
    {
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
            #region # 验证参数

            if (sourceInstance == null)
            {
                return default(TTarget);
            }

            #endregion

            MapperConfigurationExpression config = new MapperConfigurationExpression();
            IMappingExpression<TSource, TTarget> mapConfig = config.CreateMap<TSource, TTarget>();

            #region # 忽略映射成员处理

            foreach (Expression<Func<TTarget, object>> ignoreMember in ignoreMembers)
            {
                mapConfig.ForMember(ignoreMember, source => source.Ignore());
            }

            #endregion

            #region # 映射前后事件处理

            if (beforeMapEventHandler != null)
            {
                mapConfig.BeforeMap(beforeMapEventHandler);
            }
            if (afterMapEventHandler != null)
            {
                mapConfig.AfterMap(afterMapEventHandler);
            }

            #endregion

            IMapper mapper = new AutoMapper.Mapper(new MapperConfiguration(config));
            return mapper.Map<TTarget>(sourceInstance);
        }
        #endregion
    }
}
