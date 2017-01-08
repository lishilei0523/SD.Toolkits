using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SD.Toolkits.Text.Common
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        #region # 计算字符串MD5值 —— static string ToMD5(this string str)
        /// <summary>
        /// 计算字符串MD5值
        /// </summary>
        /// <param name="str">待转换的字符串</param>
        /// <returns>MD5值</returns>
        public static string ToMD5(this string str)
        {
            byte[] buffer = Encoding.Default.GetBytes(str);
            using (MD5 md5 = MD5.Create())
            {
                buffer = md5.ComputeHash(buffer);
                StringBuilder md5Builder = new StringBuilder();
                foreach (byte @byte in buffer)
                {
                    md5Builder.Append(@byte.ToString("x2"));
                }
                return md5Builder.ToString();
            }
        }
        #endregion

        #region # 字符串分词扩展方法 —— static string[] SplitWord(this string str)
        /// <summary>
        /// 字符串分词扩展方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>分词后的字符串数组</returns>
        public static string[] SplitWord(this string str)
        {
            //TODO 利用盘古分词实现

            //StringReader strReader = new StringReader(str);
            //Analyzer analyzer = new PanGuAnalyzer();
            //TokenStream tokenStream = analyzer.TokenStream(string.Empty, strReader);
            //Token token;
            //List<string> strs = new List<string>();
            //while ((token = tokenStream.Next()) != null)
            //{
            //    strs.Add(token.Term());
            //}


            return null;
        }
        #endregion

        #region # 字符串过滤Html标签扩展方法 —— static string FilterHtml(this string str)
        /// <summary>
        /// 字符串过滤Html标签扩展方法
        /// </summary>
        /// <param name="str">待过虑的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterHtml(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            str = Regex.Replace(str, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<style[^>]*?>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"</style>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<p[^>]*?>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<div[^>]*?>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"</p>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"</div>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"-->", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<!--.*", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "<[^>]*>", "", RegexOptions.Compiled);
            str = Regex.Replace(str, @"([\r\n])[\s]+", " ", RegexOptions.Compiled);
            return str.Replace("&nbsp;", " ");
        }
        #endregion

        #region # 字符串过滤SQL语句关键字扩展方法 —— static string FilterSql(this string sql)
        /// <summary>
        /// 字符串过滤SQL语句关键字扩展方法
        /// </summary>
        /// <param name="sql">SQL字符串</param>
        /// <returns>过滤后的SQL字符串</returns>
        public static string FilterSql(this string sql)
        {
            return sql.Replace("'", string.Empty);
        }
        #endregion
    }
}
