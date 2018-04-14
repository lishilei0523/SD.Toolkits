using System.Collections.Generic;

namespace SD.Toolkits.Recursion.Diagram
{
    /// <summary>
    /// 图形结构接口
    /// </summary>
    public interface IDiagram<T>
    {
        #region 上级节点集 —— ICollection<T> ParentNodes
        /// <summary>
        /// 上级节点集
        /// </summary>
        ICollection<T> ParentNodes { get; }
        #endregion

        #region 下级节点集 —— ICollection<T> SubNodes
        /// <summary>
        /// 下级节点集
        /// </summary>
        ICollection<T> SubNodes { get; }
        #endregion
    }
}
