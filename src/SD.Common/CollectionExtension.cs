﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SD.Common
{
    /// <summary>
    /// 集合扩展
    /// </summary>
    public static class CollectionExtension
    {
        #region # 遍历操作 —— static void ForEach<T>(this IEnumerable<T> enumerable...
        /// <summary>
        /// 遍历操作
        /// </summary>
        /// <param name="enumerable">集合</param>
        /// <param name="action">委托</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            #region # 验证

            if (enumerable == null)
            {
                return;
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "操作表达式不可为空！");
            }

            #endregion

            foreach (T item in enumerable)
            {
                action(item);
            }
        }
        #endregion

        #region # 判断两个集合中的元素是否相等 —— static bool EqualsTo<T>(this IEnumerable<T>...
        /// <summary>
        /// 判断两个集合中的元素是否相等
        /// </summary>
        /// <param name="sourceList">源集合</param>
        /// <param name="targetList">目标集合</param>
        /// <returns>是否相等</returns>
        public static bool EqualsTo<T>(this IEnumerable<T> sourceList, IEnumerable<T> targetList)
        {
            #region # 验证

            if (sourceList == null)
            {
                throw new ArgumentNullException(nameof(sourceList), $"源{typeof(T).Name}集合对象不可为空！");
            }
            if (targetList == null)
            {
                throw new ArgumentNullException(nameof(targetList), $"目标{typeof(T).Name}集合对象不可为空！");
            }

            #endregion

            //转数组
            sourceList = sourceList.ToArray();
            targetList = targetList.ToArray();

            #region 01.长度对比

            //长度不相等
            if (sourceList.Count() != targetList.Count())
            {
                return false;
            }

            //长度都为0
            if (!sourceList.Any() && !targetList.Any())
            {
                return true;
            }

            #endregion

            #region 02.浅度对比

            //元素对比
            if (!sourceList.Except(targetList).Any() && !targetList.Except(sourceList).Any())
            {
                return true;
            }

            #endregion

            #region 03.深度对比

            //将集合序列化为字符串
            string sourceListStr = sourceList.ToXml().Trim();
            string targetListStr = targetList.ToXml().Trim();

            //对比字符串是否相同
            if (sourceListStr == targetListStr)
            {
                return true;
            }

            #endregion

            return false;
        }
        #endregion

        #region # 判断两个字典中的元素是否相等 —— static bool EqualsTo<TKey, TValue>(this IDictionary<TKey...
        /// <summary>
        /// 判断两个字典中的元素是否相等
        /// </summary>
        /// <param name="sourceDict">源字典</param>
        /// <param name="targetDict">目标字典</param>
        /// <returns>是否相等</returns>
        public static bool EqualsTo<TKey, TValue>(this IDictionary<TKey, TValue> sourceDict, IDictionary<TKey, TValue> targetDict)
        {
            #region # 验证

            if (sourceDict == null)
            {
                throw new ArgumentNullException(nameof(sourceDict), "源字典对象不可为空！");
            }
            if (targetDict == null)
            {
                throw new ArgumentNullException(nameof(targetDict), "目标字典对象不可为空！");
            }

            #endregion

            #region 01.长度对比

            //长度不相等
            if (sourceDict.Count != targetDict.Count)
            {
                return false;
            }

            //长度都为0
            if (!sourceDict.Any() && !targetDict.Any())
            {
                return true;
            }

            #endregion

            #region 02.深度对比

            KeyValuePair<TKey, TValue>[] sourceKeyValues = sourceDict.OrderBy(x => x.Key).ToArray();
            KeyValuePair<TKey, TValue>[] targetKeyValues = targetDict.OrderBy(x => x.Key).ToArray();

            //获取键集合
            IEnumerable<TKey> sourceKeys = sourceKeyValues.Select(x => x.Key);
            IEnumerable<TKey> targetKeys = targetKeyValues.Select(x => x.Key);

            //获取值集合
            IEnumerable<TValue> sourceValues = sourceKeyValues.Select(x => x.Value);
            IEnumerable<TValue> targetValues = targetKeyValues.Select(x => x.Value);

            //判断是否键值都相等
            if (sourceKeys.EqualsTo(targetKeys) && sourceValues.EqualsTo(targetValues))
            {
                return true;
            }

            #endregion

            return false;
        }
        #endregion

        #region # 判断集合是否为空或null —— static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        /// <summary>
        /// 判断集合是否为空或null
        /// </summary>
        /// <param name="enumerable">集合对象</param>
        /// <returns>是否为空或null</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }
            return !enumerable.Any();
        }
        #endregion

        #region # 根据一个字段去重 —— static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable...
        /// <summary>
        /// 根据一个字段去重
        /// </summary>
        /// <typeparam name="TKey">去重的参照字段</typeparam>
        /// <param name="enumerable">源集合</param>
        /// <param name="keySelector">字段选择委托</param>
        /// <returns>去重后的集合</returns>
        /// <exception cref="ArgumentNullException">源集合对象为空、参照字段表达式为空</exception>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        {
            #region # 验证参数

            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), $"源{typeof(T).Name}集合对象不可为空！");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector), @"参照字段表达式不可为空！");
            }

            #endregion

            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return enumerable.Where(item => seenKeys.Add(keySelector(item)));
        }
        #endregion

        #region # 添加元素集 —— static void AddRange<T>(this ICollection<T> collection...
        /// <summary>
        /// 添加元素集
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="enumerable">元素集</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            #region # 验证

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), $"源{typeof(T).Name}集合对象不可为空！");
            }

            #endregion

            enumerable = enumerable?.ToArray() ?? new T[0];
            if (!enumerable.Any())
            {
                return;
            }

            enumerable.ForEach(collection.Add);
        }
        #endregion

        #region # 分页 —— static IEnumerable<T> FindByPage<T, TKeyOne, TKeyTwo>...
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TKeyOne">排序键1</typeparam>
        /// <typeparam name="TKeyTwo">排序键2</typeparam>
        /// <param name="enumerable">集合对象</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelectorOne">排序键选择器1</param>
        /// <param name="keySelectorTwo">排序键选择器2</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>对象集合</returns>
        public static IEnumerable<T> FindByPage<T, TKeyOne, TKeyTwo>(this IEnumerable<T> enumerable, Func<T, bool> predicate, Func<T, TKeyOne> keySelectorOne, Func<T, TKeyTwo> keySelectorTwo, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            #region # 验证

            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), "源集合对象不可为空！");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "查询条件对象不可为空！");
            }
            if (keySelectorOne == null || keySelectorTwo == null)
            {
                throw new ArgumentNullException(nameof(keySelectorOne), "参照字段表达式不可为空！");
            }
            if (pageIndex.IsZeroOrMinus())
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "页码不可为0或负数！");
            }
            if (pageSize.IsZeroOrMinus())
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "页容量不可为0或负数！");
            }

            #endregion

            T[] list = enumerable.Where(predicate).ToArray();
            rowCount = list.Length;
            pageCount = (int)Math.Ceiling(rowCount * 1.0 / pageSize);
            return list.OrderByDescending(keySelectorOne).ThenByDescending(keySelectorTwo).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region # 分页 —— static IQueryable<T> FindByPage<T, TKeyOne, TKeyTwo>...
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TKeyOne">排序键1</typeparam>
        /// <typeparam name="TKeyTwo">排序键2</typeparam>
        /// <param name="queryable">集合对象</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelectorOne">排序键选择器1</param>
        /// <param name="keySelectorTwo">排序键选择器2</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>对象集合</returns>
        public static IQueryable<T> FindByPage<T, TKeyOne, TKeyTwo>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate, Expression<Func<T, TKeyOne>> keySelectorOne, Expression<Func<T, TKeyTwo>> keySelectorTwo, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            #region # 验证

            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable), "源集合对象不可为空！");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "查询条件对象不可为空！");
            }
            if (keySelectorOne == null || keySelectorTwo == null)
            {
                throw new ArgumentNullException(nameof(keySelectorOne), "参照字段表达式不可为空！");
            }
            if (pageIndex.IsZeroOrMinus())
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "页码不可为0或负数！");
            }
            if (pageSize.IsZeroOrMinus())
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "页容量不可为0或负数！");
            }

            #endregion

            IQueryable<T> list = queryable.Where(predicate);
            rowCount = list.Count();
            pageCount = (int)Math.Ceiling(rowCount * 1.0 / pageSize);
            return list.OrderByDescending(keySelectorOne).ThenByDescending(keySelectorTwo).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region # DataTable转换集合 —— static IList<T> ToList<T>(this DataTable...
        /// <summary>
        /// DataTable转换集合
        /// </summary>
        public static IList<T> ToList<T>(this DataTable dataTable)
        {
            //获取类型与属性信息
            Type currentType = typeof(T);
            PropertyInfo[] properties = currentType.GetProperties();

            //获取无参构造函数
            ConstructorInfo[] constructors = currentType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ConstructorInfo noParamCtor = constructors.Single(x => x.GetParameters().Length == 0);

            #region # 验证

            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable), "数据表不可为null！");
            }

            #endregion

            IList<T> collection = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T instance = (T)noParamCtor.Invoke(null);

                foreach (PropertyInfo property in properties)
                {
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        MethodInfo setter = property.GetSetMethod(true);
                        if (setter != null)
                        {
                            object value = row[property.Name] == DBNull.Value ? null : row[property.Name];
                            setter.Invoke(instance, new[] { value });
                        }
                    }
                }
                collection.Add(instance);
            }
            return collection;
        }
        #endregion

        #region # 集合转换DataTable —— static DataTable ToDataTable<T>(this IEnumerable...
        /// <summary>
        /// 集合转换DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable)
        {
            #region # 验证

            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), "集合不可为null！");
            }

            #endregion

            //获取类型与属性信息
            Type currentType = typeof(T);
            PropertyInfo[] properties = currentType.GetProperties();

            DataTable dataTable = new DataTable();

            //创建列
            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(new DataColumn(property.Name));
            }

            //创建行
            T[] array = enumerable.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    dataTable.Rows[i][property.Name] = property.GetValue(array[i]);
                }
            }

            return dataTable;
        }
        #endregion

        #region # 集合转换为分割字符串 —— static string ToSplicString<T>(this IEnumerable<T>...
        /// <summary>
        /// 集合转换为分割字符串
        /// </summary>
        /// <param name="enumerable">集合</param>
        /// <returns>分割字符串</returns>
        /// <remarks>以“,”分隔</remarks>
        public static string ToSplicString<T>(this IEnumerable<T> enumerable)
        {
            #region # 验证

            enumerable = enumerable?.ToArray() ?? new T[0];
            if (!enumerable.Any())
            {
                return null;
            }

            #endregion

            IEnumerable<string> strings = enumerable.Select(x => x.ToString());
            string result = strings.Aggregate((x, y) => $"{x},{y}");

            return result;
        }
        #endregion
    }
}
