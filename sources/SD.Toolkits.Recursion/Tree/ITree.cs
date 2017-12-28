using System.Collections.Generic;

namespace SD.Toolkits.Recursion.Tree
{
    /// <summary>
    /// 树形结构接口
    /// </summary>
    public interface ITree<T>
    {
        #region 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 是否是根级节点
        /// </summary>
        bool IsRoot { get; }
        #endregion

        #region 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 是否是叶子级节点
        /// </summary>
        bool IsLeaf { get; }
        #endregion

        #region 上级节点 —— T ParentNode
        /// <summary>
        /// 上级节点
        /// </summary>
        T ParentNode { get; }
        #endregion

        #region 下级节点集 —— ICollection<T> SubNodes
        /// <summary>
        /// 下级节点集
        /// </summary>
        ICollection<T> SubNodes { get; }
        #endregion
    }
}
