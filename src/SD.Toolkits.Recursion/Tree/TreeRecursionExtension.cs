using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.Toolkits.Recursion.Tree
{
    /// <summary>
    /// 树形结构递归扩展方法工具类
    /// </summary>
    public static class TreeRecursionExtension
    {
        //Public

        #region # 深度获取上级节点集 —— static ICollection<T> GetDeepParentNodes<T>(this T vertex)
        /// <summary>
        /// 深度获取上级节点集
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>上级节点集</returns>
        /// <remarks>包含上级的上级的上级...</remarks>
        public static ICollection<T> GetDeepParentNodes<T>(this T vertex) where T : ITree<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecurseParentNodes(vertex, collection);

            return collection;
        }
        #endregion

        #region # 深度获取下级节点集 —— static ICollection<T> GetDeepSubNodes<T>(this T vertex)
        /// <summary>
        /// 深度获取下级节点集
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>下级节点集</returns>
        /// <remarks>包含下级的下级的下级...</remarks>
        public static ICollection<T> GetDeepSubNodes<T>(this T vertex) where T : ITree<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecurseSubNodes(vertex, collection);

            return collection;
        }
        #endregion

        #region # 尾递归上级扩展方法 —— static ICollection<T> TailRecurseParentNodes<T>(this IEnumerable<T>...
        /// <summary>
        /// 尾递归上级扩展方法
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="nodes">节点集</param>
        /// <returns>给定节点集 + 所有上级节点集</returns>
        public static ICollection<T> TailRecurseParentNodes<T>(this IEnumerable<T> nodes) where T : ITree<T>
        {
            T[] nodeArray = nodes?.ToArray() ?? new T[0];

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

        #region # 获取树路径扩展方法 —— static string GetTreePath<T>(this T vertex)
        /// <summary>
        /// 获取树路径扩展方法
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>树路径</returns>
        public static string GetTreePath<T>(this T vertex) where T : ITree<T>
        {
            StringBuilder pathBuilder = new StringBuilder();
            RecurseTreePath(vertex, pathBuilder);

            return pathBuilder.ToString();
        }
        #endregion


        //Private

        #region # 递归上级 —— static void RecurseParentNodes<T>(T vertex...
        /// <summary>
        /// 递归上级
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecurseParentNodes<T>(T vertex, ICollection<T> collection) where T : ITree<T>
        {
            #region # 验证参数

            if (collection == null)
            {
                collection = new List<T>();
            }
            if (vertex == null)
            {
                return;
            }

            #endregion

            if (vertex.ParentNode != null)
            {
                collection.Add(vertex.ParentNode);
                RecurseParentNodes(vertex.ParentNode, collection);
            }
        }
        #endregion

        #region # 递归下级 —— static void RecurseSubNodes<T>(T vertex...
        /// <summary>
        /// 递归下级
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecurseSubNodes<T>(T vertex, ICollection<T> collection) where T : ITree<T>
        {
            #region # 验证参数

            if (collection == null)
            {
                collection = new List<T>();
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
                RecurseSubNodes(subNode, collection);
            }
        }
        #endregion

        #region # 递归路径 —— static void RecurseTreePath<T>(T vertex...
        /// <summary>
        /// 递归路径
        /// </summary>
        /// <typeparam name="T">树形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="pathBuilder">路径建造者</param>
        private static void RecurseTreePath<T>(T vertex, StringBuilder pathBuilder) where T : ITree<T>
        {
            if (vertex.ParentNode != null)
            {
                RecurseTreePath(vertex.ParentNode, pathBuilder);
            }
            pathBuilder.Append("/");
            pathBuilder.Append(vertex);
        }
        #endregion
    }
}
