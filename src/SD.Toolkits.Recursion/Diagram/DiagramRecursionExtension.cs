﻿using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.Recursion.Diagram
{
    /// <summary>
    /// 图形结构递归扩展
    /// </summary>
    public static class DiagramRecursionExtension
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
        public static ICollection<T> GetDeepParentNodes<T>(this T vertex) where T : IDiagram<T>
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
        public static ICollection<T> GetDeepSubNodes<T>(this T vertex) where T : IDiagram<T>
        {
            HashSet<T> collection = new HashSet<T>();
            RecurseSubNodes(vertex, collection);

            return collection;
        }
        #endregion


        //Private

        #region # 递归上级 —— static void RecurseParentNodes<T>(T vertex...
        /// <summary>
        /// 递归上级
        /// </summary>
        /// <typeparam name="T">图形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecurseParentNodes<T>(T vertex, ICollection<T> collection) where T : IDiagram<T>
        {
            #region # 验证

            if (collection == null)
            {
                collection = new List<T>();
            }
            if (vertex == null)
            {
                return;
            }
            if (vertex.ParentNodes == null || !vertex.ParentNodes.Any())
            {
                return;
            }

            #endregion

            foreach (T parentNode in vertex.ParentNodes)
            {
                collection.Add(parentNode);
                RecurseParentNodes(parentNode, collection);
            }
        }
        #endregion

        #region # 递归下级 —— static void RecurseSubNodes<T>(T vertex...
        /// <summary>
        /// 递归下级
        /// </summary>
        /// <typeparam name="T">图形结构类型</typeparam>
        /// <param name="vertex">顶点</param>
        /// <param name="collection">目标集合容器</param>
        private static void RecurseSubNodes<T>(T vertex, ICollection<T> collection) where T : IDiagram<T>
        {
            #region # 验证

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
    }
}
