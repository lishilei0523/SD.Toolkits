using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.Recursion
{
    /// <summary>
    /// 递归扩展方法工具类
    /// </summary>
    public static class RecursionExtension
    {
        #region # 递归扩展方法 —— static void Recursion<T>(this T node, ICollection<T> collection)
        /// <summary>
        /// 递归扩展方法，
        /// 自上向下递归
        /// </summary>
        /// <typeparam name="T">可递归类型</typeparam>
        /// <param name="targetTode">目标节点</param>
        /// <param name="collection">可递归类型集</param>
        public static void Recursion<T>(this T targetTode, ICollection<T> collection) where T : IRecursive<T>
        {
            #region # 验证参数

            if (collection == null)
            {
                collection = new HashSet<T>();
            }
            if (targetTode == null)
            {
                return;
            }
            if (targetTode.SubNodes == null || !targetTode.SubNodes.Any())
            {
                collection.Add(targetTode);
                return;
            }

            #endregion

            foreach (T subNode in targetTode.SubNodes)
            {
                collection.Add(subNode);
                Recursion(subNode, collection);
            }
            collection.Add(targetTode);
        }
        #endregion

        #region # 尾递归扩展方法 —— static void TailRecursion<T>(this IEnumerable<T> nodes...
        /// <summary>
        /// 尾递归扩展方法，
        /// 自下向上递归
        /// </summary>
        /// <typeparam name="T">可递归类型</typeparam>
        /// <param name="nodes">节点集</param>
        /// <param name="collection">可递归类型集</param>
        public static void TailRecursion<T>(this IEnumerable<T> nodes, HashSet<T> collection) where T : IRecursive<T>
        {
            List<T> nodeList = nodes == null ? null : nodes.ToList();

            #region # 验证参数

            if (collection == null)
            {
                collection = new HashSet<T>();
            }
            if (nodes == null || !nodeList.Any())
            {
                return;
            }

            #endregion

            foreach (T node in nodeList)
            {
                if (node.ParentNode != null)
                {
                    TailRecursion(nodeList.Select(x => x.ParentNode), collection);
                }
            }

            nodeList.ForEach(item => collection.Add(item));
        }
        #endregion
    }
}
