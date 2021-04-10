using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.Toolkits.Recursion.Assemble
{
    /// <summary>
    /// 可装配对象递归扩展方法工具类
    /// </summary>
    public static class AssembleRecursionExtension
    {
        //Public

        #region # 深度获取上级元素集 —— static ICollection<T> GetDeepParentElements<T>(this T vertex)
        /// <summary>
        /// 深度获取上级元素集
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>上级元素集</returns>
        /// <remarks>包含上级的上级的上级...</remarks>
        public static ICollection<T> GetDeepParentElements<T>(this T vertex) where T : IAssemble<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecurseParentElements(vertex, collection);

            return collection;
        }
        #endregion

        #region # 深度获取下级元素集 —— static ICollection<T> GetDeepSubElements<T>(this T vertex)
        /// <summary>
        /// 深度获取下级元素集
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>下级元素集</returns>
        /// <remarks>包含下级的下级的下级...</remarks>
        public static ICollection<T> GetDeepSubElements<T>(this T vertex) where T : IAssemble<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecurseSubElements(vertex, collection);

            return collection;
        }
        #endregion

        #region # 尾递归上级扩展方法 —— static ICollection<T> TailRecurseParentElements<T>(this IEnumerable<T>...
        /// <summary>
        /// 尾递归上级扩展方法
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="elements">元素集</param>
        /// <returns>给定元素集 + 所有上级元素集</returns>
        public static ICollection<T> TailRecurseParentElements<T>(this IEnumerable<T> elements) where T : IAssemble<T>
        {
            T[] elementArray = elements?.ToArray() ?? new T[0];

            HashSet<T> collection = new HashSet<T>();

            foreach (T element in elementArray)
            {
                foreach (T parentElement in element.GetDeepParentElements())
                {
                    collection.Add(parentElement);
                }
                collection.Add(element);
            }

            return collection;
        }
        #endregion

        #region # 获取装配路径扩展方法 —— static string GetAssemblePath<T>(this T vertex)
        /// <summary>
        /// 获取装配路径扩展方法
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <returns>装配路径</returns>
        public static string GetAssemblePath<T>(this T vertex) where T : IAssemble<T>
        {
            StringBuilder pathBuilder = new StringBuilder();
            RecurseAssemblePath(vertex, pathBuilder);

            return pathBuilder.ToString();
        }
        #endregion


        //Private

        #region # 递归上级 —— static void RecurseParentElements<T>(T vertex...
        /// <summary>
        /// 递归上级
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecurseParentElements<T>(T vertex, ICollection<T> collection) where T : IAssemble<T>
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

            if (vertex.ParentElement != null)
            {
                collection.Add(vertex.ParentElement);
                RecurseParentElements(vertex.ParentElement, collection);
            }
        }
        #endregion

        #region # 递归下级 —— static void RecurseSubElements<T>(T vertex...
        /// <summary>
        /// 递归下级
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecurseSubElements<T>(T vertex, ICollection<T> collection) where T : IAssemble<T>
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
            if (vertex.SubElements == null || !vertex.SubElements.Any())
            {
                return;
            }

            #endregion

            foreach (T subElement in vertex.SubElements)
            {
                collection.Add(subElement);
                RecurseSubElements(subElement, collection);
            }
        }
        #endregion

        #region # 递归路径 —— static void RecurseAssemblePath<T>(T vertex...
        /// <summary>
        /// 递归路径
        /// </summary>
        /// <typeparam name="T">可装配对象类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="pathBuilder">路径建造者</param>
        private static void RecurseAssemblePath<T>(T vertex, StringBuilder pathBuilder) where T : IAssemble<T>
        {
            if (vertex.ParentElement != null)
            {
                RecurseAssemblePath(vertex.ParentElement, pathBuilder);
            }
            pathBuilder.Append("/");
            pathBuilder.Append(vertex);
        }
        #endregion
    }
}
