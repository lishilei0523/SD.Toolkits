using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace SD.Toolkits.EntityFramework.Extensions
{
    /// <summary>
    /// IQueryable集合扩展
    /// </summary>
    public static class QueryableExtension
    {
        #region # IQueryable集合转换为SQL语句 —— static string ParseSql(this IQueryable...
        /// <summary>
        /// IQueryable集合转换为SQL语句
        /// </summary>
        /// <param name="queryable">IQueryable集合对象</param>
        /// <returns>SQL语句</returns>
        public static string ParseSql(this IQueryable queryable)
        {
            // CHECK ObjectQuery
            ObjectQuery objectQuery = queryable as ObjectQuery;

            if (objectQuery != null)
            {
                return objectQuery.ToTraceString();
            }

            // CHECK DbQuery
            DbQuery dbQuery = queryable as DbQuery;

            if (dbQuery != null)
            {
                PropertyInfo internalQueryProperty = dbQuery.GetType().GetProperty("InternalQuery", BindingFlags.NonPublic | BindingFlags.Instance);
                object internalQuery = internalQueryProperty.GetValue(dbQuery, null);
                PropertyInfo objectQueryContextProperty = internalQuery.GetType().GetProperty("ObjectQuery", BindingFlags.Public | BindingFlags.Instance);
                object objectQueryContext = objectQueryContextProperty.GetValue(internalQuery, null);

                objectQuery = objectQueryContext as ObjectQuery;

                return objectQuery == null ? string.Empty : objectQuery.ToTraceString();
            }

            Type type = queryable.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DbQuery<>))
            {
                PropertyInfo internalQueryProperty = typeof(DbQuery<>).MakeGenericType(queryable.ElementType).GetProperty("InternalQuery", BindingFlags.NonPublic | BindingFlags.Instance);
                object internalQuery = internalQueryProperty.GetValue(queryable, null);
                PropertyInfo objectQueryContextProperty = internalQuery.GetType().GetProperty("ObjectQuery", BindingFlags.Public | BindingFlags.Instance);
                object objectQueryContext = objectQueryContextProperty.GetValue(internalQuery, null);

                objectQuery = objectQueryContext as ObjectQuery;

                return objectQuery == null ? string.Empty : objectQuery.ToTraceString();
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DbSet<>))
            {
                PropertyInfo internalQueryProperty = typeof(DbSet<>).MakeGenericType(queryable.ElementType).GetProperty("InternalQuery", BindingFlags.NonPublic | BindingFlags.Instance);
                object internalQuery = internalQueryProperty.GetValue(queryable, null);
                PropertyInfo objectQueryContextProperty = internalQuery.GetType().GetProperty("ObjectQuery", BindingFlags.Public | BindingFlags.Instance);
                object objectQueryContext = objectQueryContextProperty.GetValue(internalQuery, null);

                objectQuery = objectQueryContext as ObjectQuery;

                return objectQuery == null ? string.Empty : objectQuery.ToTraceString();
            }

            throw new Exception("Error");
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
            catch (NotSupportedException)
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
            catch (NotSupportedException)
            {
                return false;
            }
        }
        #endregion
    }
}
