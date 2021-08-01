using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SD.Toolkits.Sql
{
    /// <summary>
    /// SQL扩展
    /// </summary>
    public static class SqlExtension
    {
        #region # CLS值转数据库值 —— object ToDbValue(this object value)
        /// <summary>
        /// CLS值转数据库值
        /// </summary>
        /// <param name="value">CLS值</param>
        /// <returns>处理后的数据库值</returns>
        public static object ToDbValue(this object value)
        {
            return value ?? DBNull.Value;
        }
        #endregion

        #region # 数据库值转CLS值 —— object ToClsValue(this IDataReader reader...
        /// <summary>
        /// 数据库值转CLS值
        /// </summary>
        /// <param name="dataReader">IDataReader对象</param>
        /// <param name="columnName">列名</param>
        /// <returns>CLS值</returns>
        public static object ToClsValue(this IDataReader dataReader, string columnName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(columnName)))
            {
                return null;
            }

            return dataReader[columnName];
        }
        #endregion

        #region # 过滤SQL敏感字符 —— static string FilterSql(this string text)
        /// <summary>
        /// 过滤SQL敏感字符
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>过滤后的文本</returns>
        public static string FilterSql(this string text)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            #endregion

            text = text.Replace("'", string.Empty);

            return text;
        }
        #endregion

        #region # 格式化Guid列表字符串 —— static string FormatGuids(this IEnumerable<Guid> guids)
        /// <summary>
        /// 格式化Guid列表字符串
        /// </summary>
        /// <param name="guids">Guid列表</param>
        /// <returns>Guid列表字符串</returns>
        public static string FormatGuids(this IEnumerable<Guid> guids)
        {
            guids = guids?.ToArray() ?? new Guid[0];

            StringBuilder builder = new StringBuilder();
            foreach (Guid guid in guids)
            {
                builder.Append("'");
                builder.Append(guid);
                builder.Append("'");
                builder.Append(',');
            }
            if (builder.Length == 0)
            {
                return string.Empty;
            }

            return builder.ToString().Substring(0, builder.Length - 1);
        }
        #endregion
    }
}
