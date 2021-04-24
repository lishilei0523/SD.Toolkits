using System.Collections.Generic;

namespace SD.Toolkits.EasyUI
{
    /// <summary>
    /// EasyUI TreeGrid接口
    /// </summary>
    public interface ITreeGrid<T>
    {
        /// <summary>
        /// 类型
        /// </summary>
        /// <remarks>folder/pack</remarks>
        string type { get; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        ICollection<T> children { get; }
    }
}
