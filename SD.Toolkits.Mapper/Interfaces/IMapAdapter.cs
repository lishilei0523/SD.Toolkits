using System;
using System.Linq.Expressions;

namespace SD.Toolkits.Mapper.Interfaces
{
    /// <summary>
    /// 映射适配器接口
    /// </summary>
    public interface IMapAdapter
    {
        /// <summary>
        /// 映射方法
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="instance">源类型实例</param>
        /// <param name="ignoreMembers">忽略映射成员集</param>
        /// <returns>目标类型实例</returns>
        TTarget Map<TSource, TTarget>(TSource instance, params Expression<Func<TTarget, object>>[] ignoreMembers);
    }
}
