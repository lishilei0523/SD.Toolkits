using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.Recursion.Tree
{
    /// <summary>
    /// 树形结构递归扩展方法工具类
    /// </summary>
    public static class TreeRecursionExtension
    {
        //Public

        #region # 深度获取上级节点集 —— static IEnumerable<T> GetDeepParentNodes<T>(this T vertex)
        /// <summary>
        /// 深度获取上级节点集
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>上级节点集</returns>
        /// <remarks>包含上级的上级的上级...</remarks>
        public static IEnumerable<T> GetDeepParentNodes<T>(this T vertex) where T : ITree<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecursionParentNodes(vertex, collection);

            return collection;
        }
        #endregion

        #region # 深度获取下级节点集 —— static IEnumerable<T> GetDeepSubNodes<T>(this T vertex)
        /// <summary>
        /// 深度获取下级节点集
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>下级节点集</returns>
        /// <remarks>包含下级的下级的下级...</remarks>
        public static IEnumerable<T> GetDeepSubNodes<T>(this T vertex) where T : ITree<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecursionSubNodes(vertex, collection);

            return collection;
        }
        #endregion

        #region # 尾递归上级扩展方法 —— static void TailRecursionParentNodes<T>(this IEnumerable<T>...
        /// <summary>
        /// 尾递归上级扩展方法
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="nodes">节点集</param>
        /// <returns>给定节点集 + 所有上级节点集</returns>
        public static IEnumerable<T> TailRecursionParentNodes<T>(this IEnumerable<T> nodes) where T : ITree<T>
        {
            T[] nodeArray = nodes == null ? new T[0] : nodes.ToArray();

            HashSet<T> collection = new HashSet<T>();

            foreach (T node in nodeArray)
            {
                foreach (T parentNode in node.GetDeepParentNodes())
                {
                    collection.Add(parentNode);
                }
                collection.Add(node);
            }

            return collection;
        }
        #endregion


        //Private

        #region # 递归上级 —— static void RecursionParentNodes<T>(T vertex...
        /// <summary>
        /// 递归上级
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecursionParentNodes<T>(T vertex, ICollection<T> collection) where T : ITree<T>
        {
            #region # 验证参数

            if (collection == null)
            {
                collection = new HashSet<T>();
            }
            if (vertex == null)
            {
                return;
            }

            #endregion

            if (vertex.ParentNode != null)
            {
                collection.Add(vertex.ParentNode);
                RecursionParentNodes(vertex.ParentNode, collection);
            }
        }
        #endregion

        #region # 递归下级 —— static void RecursionSubNodes<T>(T vertex...
        /// <summary>
        /// 递归下级
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecursionSubNodes<T>(T vertex, ICollection<T> collection) where T : ITree<T>
        {
            #region # 验证参数

            if (collection == null)
            {
                collection = new HashSet<T>();
            }
            if (vertex == null)
            {
                return;
            }
            if (vertex.SubNodes == null || !vertex.SubNodes.Any())
            {
                return;
            }

            #endregion

            foreach (T subNode in vertex.SubNodes)
            {
                collection.Add(subNode);
                RecursionSubNodes(subNode, collection);
            }
        }
        #endregion
    }
}
