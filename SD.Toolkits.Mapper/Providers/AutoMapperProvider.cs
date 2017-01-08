using System;
using System.Linq.Expressions;
using AutoMapper;
using SD.Toolkits.Mapper.Interfaces;

namespace SD.Toolkits.Mapper.Providers
{
    /// <summary>
    /// AutoMapper映射提供者
    /// </summary>
    public class AutoMapperProvider : IMapAdapter
    {
        /// <summary>
        /// 映射方法
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="sourceInstance">源类型实例</param>
        /// <param name="ignoreMembers">忽略映射成员集</param>
        /// <returns>目标类型实例</returns>
        public TTarget Map<TSource, TTarget>(TSource sourceInstance, params Expression<Func<TTarget, object>>[] ignoreMembers)
        {
            #region # 验证参数

            if (sourceInstance == null)
            {
                return default(TTarget);
            }

            #endregion

            if (AutoMapper.Mapper.FindTypeMapFor<TSource, TTarget>() == null)
            {
                //创建映射关系
                IMappingExpression<TSource, TTarget> mapConfig = AutoMapper.Mapper.CreateMap<TSource, TTarget>();

                #region # 忽略映射成员处理

                foreach (Expression<Func<TTarget, object>> ignoreMember in ignoreMembers)
                {
                    mapConfig.ForMember(ignoreMember, source => source.Ignore());
                }

                #endregion
            }

            return AutoMapper.Mapper.Map<TSource, TTarget>(sourceInstance);
        }
    }
}
