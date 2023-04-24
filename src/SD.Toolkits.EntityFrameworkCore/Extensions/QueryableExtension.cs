using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SD.Toolkits.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// IQueryable集合扩展
    /// </summary>
    public static class QueryableExtension
    {
        //Public

        #region # IQueryable集合转换为SQL语句 —— static string ParseSql(this IQueryable...
        /// <summary>
        /// IQueryable集合转换为SQL语句
        /// </summary>
        /// <param name="queryable">IQueryable集合对象</param>
        /// <returns>SQL语句</returns>
        public static string ParseSql(this IQueryable queryable)
        {
#if NETSTANDARD2_0
            throw new NotSupportedException();
#endif
#if NETSTANDARD2_1 || NET6_0
            return queryable.ToQueryString();
#endif
        }
        #endregion

        #region # IQueryable集合转换为SQL语句 —— static bool TryParseSQl(this IQueryable...
        /// <summary>
        /// IQueryable集合转换为SQL语句
        /// </summary>
        /// <param name="queryable">IQueryable集合</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>是否转换成功</returns>
        public static bool TryParseSQl(this IQueryable queryable, out string sql)
        {
            try
            {
                sql = ParseSql(queryable);
                return true;
            }
            catch (InvalidOperationException)
            {
                sql = string.Empty;
                return false;
            }
        }
        #endregion

        #region # 判断IQueryable集合能否转换为SQL语句 —— static bool CanParseSQl(this IQueryable...
        /// <summary>
        /// 判断IQueryable集合能否转换为SQL语句
        /// </summary>
        /// <param name="queryable">IQueryable集合</param>
        /// <returns>能否转换</returns>
        public static bool CanParseSQl(this IQueryable queryable)
        {
            try
            {
                ParseSql(queryable);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        #endregion

        #region # IQueryable集合多列动态多条件排序 —— static IQueryable<T> OrderBy<T>(this IQueryable<T>...
        /// <summary>
        /// IQueryable集合多列动态多条件排序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="queryable">可查询集合</param>
        /// <param name="keySelectors">排序键</param>
        /// <returns>IQueryable集合</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, IDictionary<string, bool> keySelectors)
        {
            int resetCount = 0;

            Type type = typeof(T);

            ParameterExpression param = Expression.Parameter(type, "x");

            foreach (KeyValuePair<string, bool> selector in keySelectors)
            {
                PropertyInfo property = type.GetProperty(selector.Key);

                #region # 验证

                if (property == null)
                {
                    throw new InvalidOperationException(string.Format("属性\"{0}\"不存在！", selector.Key));
                }

                #endregion

                Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
                LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

                string methodName = resetCount > 0
                    ? (selector.Value ? "ThenBy" : "ThenByDescending")
                    : (selector.Value ? "OrderBy" : "OrderByDescending");

                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, queryable.Expression, Expression.Quote(orderByExpression));

                queryable = queryable.Provider.CreateQuery<T>(resultExp);

                resetCount++;
            }

            return queryable;
        }
        #endregion

        #region # IQueryable集合包含导航属性 —— static IQueryable<T> IncludeNavigationProperties<T>(...
        /// <summary>
        /// IQueryable集合包含导航属性
        /// </summary>
        /// <param name="queryable">可查询集合</param>
        /// <returns>可查询集合</returns>
        public static IQueryable<T> IncludeNavigationProperties<T>(this IQueryable<T> queryable) where T : class
        {
            queryable = IncludeRecursively(queryable, typeof(T), null, null);

            return queryable;
        }
        #endregion

        #region # 执行SQL查询 —— static T ExecuteScalar<T>(this DbContext dbContext...
        /// <summary>
        /// 执行SQL查询
        /// </summary>
        /// <param name="dbContext">EF上下文</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数集</param>
        /// <returns>实体对象列表</returns>
        public static T ExecuteScalar<T>(this DbContext dbContext, string sql, params object[] parameters)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), "SQL语句不可为空！");
            }

            #endregion

            DbConnection dbConnection = dbContext.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sql;
            if (parameters != null && parameters.Any())
            {
                dbCommand.Parameters.AddRange(parameters);
            }

            DbDataReader dataReader = dbCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            dataReader.Close();

            if (dataTable.Rows.Count > 0)
            {
                object result = dataTable.Rows[0][0];
                return (T)result;
            }

            return default(T);
        }
        #endregion

        #region # 执行SQL查询 —— static ICollection<T> ExecuteSqlQuery<T>(this DbContext...
        /// <summary>
        /// 执行SQL查询
        /// </summary>
        /// <param name="dbContext">EF上下文</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数集</param>
        /// <returns>实体对象列表</returns>
        public static ICollection<T> ExecuteSqlQuery<T>(this DbContext dbContext, string sql, params object[] parameters)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql), "SQL语句不可为空！");
            }

            #endregion

            DbConnection dbConnection = dbContext.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
            }
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sql;
            if (parameters != null && parameters.Any())
            {
                dbCommand.Parameters.AddRange(parameters);
            }

            DbDataReader dataReader = dbCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            dataReader.Close();

            //获取类型与属性列表
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();

            //获取无参构造函数
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            ConstructorInfo noParamConstructor = constructors.Single(ctor => ctor.GetParameters().Length == 0);

            IList<T> entities = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                T entity = (T)noParamConstructor.Invoke(null);
                foreach (PropertyInfo property in propertyInfos)
                {
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        MethodInfo propertySetter = property.GetSetMethod(true);
                        if (propertySetter != null)
                        {
                            object value = dataRow[property.Name] == DBNull.Value
                                ? null
                                : dataRow[property.Name];
                            propertySetter.Invoke(entity, new[] { value });
                        }
                    }
                }
                entities.Add(entity);
            }

            return entities;
        }
        #endregion


        //Private

        #region # IQueryable集合递归包含导航属性 —— static IQueryable IncludeRecursively(IQueryable queryable...
        /// <summary>
        /// IQueryable集合递归包含导航属性
        /// </summary>
        /// <param name="queryable">可查询集合</param>
        /// <param name="classType">实体类型</param>
        /// <param name="excludePropertyType">排除属性类型</param>
        /// <param name="pathPrefix">路径前缀</param>
        /// <returns>可查询集合</returns>
        private static IQueryable<T> IncludeRecursively<T>(IQueryable<T> queryable, Type classType, Type excludePropertyType, string pathPrefix) where T : class
        {
            //加载所有导航属性
            Func<PropertyInfo, bool> navPropertySelector = typ =>
            {
                MethodInfo getMethod = typ.GetGetMethod(false) == null ? typ.GetGetMethod(true) : typ.GetGetMethod(false);
                MethodInfo setMethod = typ.GetSetMethod(false) == null ? typ.GetSetMethod(true) : typ.GetSetMethod(false);

                if (setMethod == null)
                {
                    return false;
                }
                if (getMethod == null)
                {
                    return false;
                }
                if (getMethod.IsVirtual && (setMethod.IsPrivate || setMethod.IsVirtual))
                {
                    if (getMethod.IsVirtual && getMethod.IsFinal)
                    {
                        return false;
                    }

                    return true;
                }

                return false;
            };

            IEnumerable<PropertyInfo> navProperties = classType.GetProperties().Where(navPropertySelector);

            foreach (PropertyInfo propertyInfo in navProperties)
            {
                if (propertyInfo.PropertyType == excludePropertyType)
                {
                    continue;
                }

                //构造路径
                string path = string.IsNullOrWhiteSpace(pathPrefix)
                    ? propertyInfo.Name
                    : $"{pathPrefix}.{propertyInfo.Name}";

                queryable = queryable.Include(path);

                Type propertyType = propertyInfo.PropertyType;
                if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                    if (propertyType == excludePropertyType)
                    {
                        continue;
                    }
                }

                string pathPrefix2 = string.IsNullOrWhiteSpace(pathPrefix)
                    ? propertyInfo.Name
                    : $"{pathPrefix}.{propertyInfo.Name}";

                IncludeRecursively(queryable, propertyType, classType, pathPrefix2);
            }

            return queryable;
        }
        #endregion
    }
}
