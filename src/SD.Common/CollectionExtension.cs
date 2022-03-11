using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        /// <param name="action">操作委托</param>
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
                action.Invoke(item);
            }
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
                throw new ArgumentNullException(nameof(collection), "源集合不可为空！");
            }

            #endregion

            enumerable = enumerable?.ToArray() ?? new T[0];
            foreach (T item in enumerable)
            {
                collection.Add(item);
            }
        }
        #endregion

        #region # 获取集合页数 —— static int GetPageCount<T>(this IEnumerable<T>...
        /// <summary>
        /// 获取集合页数
        /// </summary>
        /// <param name="enumerable">集合</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>页数</returns>
        public static int GetPageCount<T>(this IEnumerable<T> enumerable, int pageSize)
        {
            #region # 验证

            enumerable = enumerable?.ToArray() ?? new T[0];
            if (!enumerable.Any())
            {
                return 0;
            }

            #endregion

            int pageCount = (int)Math.Ceiling(enumerable.Count() * 1.0 / pageSize);

            return pageCount;
        }
        #endregion

        #region # 根据属性去重 —— static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T>...
        /// <summary>
        /// 根据属性去重
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TKey">参照属性</typeparam>
        /// <param name="enumerable">集合</param>
        /// <param name="keySelector">属性选择器</param>
        /// <returns>去重后的集合</returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        {
            #region # 验证

            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable), "源集合对象不可为空！");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector), "属性选择器不可为空！");
            }

            #endregion

            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return enumerable.Where(item => seenKeys.Add(keySelector(item)));
        }
        #endregion

        #region # 判断集合是否为空或null —— static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        /// <summary>
        /// 判断集合是否为空或null
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>是否为空或null</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null || !collection.Any())
            {
                return true;
            }

            return false;
        }
        #endregion

        #region # 判断两个集合中的元素是否相等 —— static bool EqualsTo<T>(this IEnumerable<T>...
        /// <summary>
        /// 判断两个集合中的元素是否相等
        /// </summary>
        /// <param name="sourceEnumerable">源集合</param>
        /// <param name="targetEnumerable">目标集合</param>
        /// <returns>是否相等</returns>
        public static bool EqualsTo<T>(this IEnumerable<T> sourceEnumerable, IEnumerable<T> targetEnumerable)
        {
            //转数组
            sourceEnumerable = sourceEnumerable?.ToArray() ?? new T[0];
            targetEnumerable = targetEnumerable?.ToArray() ?? new T[0];

            #region 01.长度对比

            //长度都为0
            if (!sourceEnumerable.Any() && !targetEnumerable.Any())
            {
                return true;
            }

            //长度不相等
            if (sourceEnumerable.Count() != targetEnumerable.Count())
            {
                return false;
            }

            #endregion

            #region 02.浅度对比

            //元素对比
            if (!sourceEnumerable.Except(targetEnumerable).Any() && !targetEnumerable.Except(sourceEnumerable).Any())
            {
                return true;
            }

            #endregion

            #region 03.深度对比

            //将集合序列化为字符串
            string sourceListStr = sourceEnumerable.ToXml().Trim();
            string targetListStr = targetEnumerable.ToXml().Trim();

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
            sourceDict = sourceDict ?? new Dictionary<TKey, TValue>();
            targetDict = targetDict ?? new Dictionary<TKey, TValue>();

            #region 01.长度对比

            //长度都为0
            if (!sourceDict.Any() && !targetDict.Any())
            {
                return true;
            }

            //长度不相等
            if (sourceDict.Count != targetDict.Count)
            {
                return false;
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

        #region # DataTable转换集合 —— static ICollection<T> ToCollection<T>(this DataTable...
        /// <summary>
        /// DataTable转换集合
        /// </summary>
        public static ICollection<T> ToCollection<T>(this DataTable dataTable)
        {
            #region # 验证

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return new List<T>();
            }

            #endregion

            //获取类型与属性列表
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            //获取无参构造函数
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ConstructorInfo noParamConstructor = constructors.Single(ctor => ctor.GetParameters().Length == 0);

            IList<T> collection = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                T instance = (T)noParamConstructor.Invoke(null);
                foreach (PropertyInfo property in properties)
                {
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        MethodInfo propertySetter = property.GetSetMethod(true);
                        if (propertySetter != null)
                        {
                            object value = dataRow[property.Name] == DBNull.Value
                                ? null
                                : dataRow[property.Name];
                            propertySetter.Invoke(instance, new[] { value });
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
        /// <param name="enumerable">集合</param>
        /// <param name="onlyPrimitiveType">是否只包含基元类型</param>
        /// <param name="tableName">表名</param>
        /// <param name="propertyQuery">属性查询条件</param>
        /// <remarks>
        /// 基元类型：string, bool, byte, short, int, long, float, double, decimal, Guid, DateTime, TimeSpan
        /// </remarks>
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable, bool onlyPrimitiveType = false, string tableName = null, Func<PropertyInfo, bool> propertyQuery = null)
        {
            T[] array = enumerable?.ToArray() ?? new T[0];
            tableName = string.IsNullOrWhiteSpace(tableName) ? typeof(T).Name : tableName;
            DataTable dataTable = new DataTable(tableName);

            #region 验证

            if (!array.Any())
            {
                return dataTable;
            }

            #endregion

            //获取类型属性列表
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (propertyQuery != null)
            {
                properties = properties.Where(propertyQuery).ToArray();
            }

            //过滤基元类型
            if (onlyPrimitiveType)
            {
                Type[] primitiveTypes =
                {
                    typeof(string),
                    typeof(bool),
                    typeof(byte),
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(decimal),
                    typeof(Guid),
                    typeof(DateTime),
                    typeof(TimeSpan),
                    typeof(Enum)
                };

                IList<PropertyInfo> primitiveProperties = new List<PropertyInfo>();
                foreach (PropertyInfo property in properties)
                {
                    Type propertyType = property.PropertyType;
                    if (primitiveTypes.Contains(propertyType) || primitiveTypes.Contains(propertyType.BaseType))
                    {
                        primitiveProperties.Add(property);
                    }
                    if (propertyType.IsNullable())
                    {
                        Type propertyOriginalType = propertyType.GetGenericArguments()[0];
                        if (primitiveTypes.Contains(propertyOriginalType) || primitiveTypes.Contains(propertyOriginalType.BaseType))
                        {
                            primitiveProperties.Add(property);
                        }
                    }
                }

                properties = primitiveProperties.ToArray();
            }

            //创建列
            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsNullable())
                {
                    Type propertyOriginalType = propertyType.GetGenericArguments()[0];
                    DataColumn dataColumn = new DataColumn(property.Name, propertyOriginalType);
                    dataTable.Columns.Add(dataColumn);
                }
                else
                {
                    DataColumn dataColumn = new DataColumn(property.Name, propertyType);
                    dataTable.Columns.Add(dataColumn);
                }
            }

            //创建行
            foreach (T instance in array)
            {
                IEnumerable<object> propertyValues =
                    from property in properties
#if NET40
                    let propertyValue = property.GetValue(instance, null)
#else
                    let propertyValue = property.GetValue(instance)
#endif

                    select propertyValue;

                dataTable.Rows.Add(propertyValues.ToArray());
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
