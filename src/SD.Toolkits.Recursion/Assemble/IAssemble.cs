using System.Collections.Generic;

namespace SD.Toolkits.Recursion.Assemble
{
    /// <summary>
    /// 可装配接口
    /// </summary>
    public interface IAssemble<T>
    {
        #region 是否是成品件 —— bool IsFinal
        /// <summary>
        /// 是否是成品件
        /// </summary>
        bool IsFinal { get; }
        #endregion

        #region 是否是元件 —— bool IsCell
        /// <summary>
        /// 是否是元件
        /// </summary>
        bool IsCell { get; }
        #endregion

        #region 上级元素 —— T ParentElement
        /// <summary>
        /// 上级元素
        /// </summary>
        T ParentElement { get; }
        #endregion

        #region 下级元素集 —— ICollection<T> SubElements
        /// <summary>
        /// 下级元素集
        /// </summary>
        ICollection<T> SubElements { get; }
        #endregion
    }
}
