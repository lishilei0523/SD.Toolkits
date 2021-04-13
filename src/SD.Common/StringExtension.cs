using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SD.Common
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        #region # 忽略大小写比较字符串是否相等扩展方法 —— static bool EqualsTo(this string source...
        /// <summary>
        /// 忽略大小写比较字符串是否相等扩展方法
        /// </summary>
        /// <param name="source">当前字符串</param>
        /// <param name="target">要比较的字符串</param>
        /// <returns>是否相同</returns>
        public static bool EqualsTo(this string source, string target)
        {
            if (string.IsNullOrWhiteSpace(source) && string.IsNullOrWhiteSpace(target))
            {
                return true;
            }
            return string.Equals(source, target, StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion

        #region # 字符串过滤Html标签扩展方法 —— static string FilterHtml(this string text)
        /// <summary>
        /// 字符串过滤Html标签扩展方法
        /// </summary>
        /// <param name="text">待过虑的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FilterHtml(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            text = Regex.Replace(text, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<style[^>]*?>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"</style>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<p[^>]*?>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<div[^>]*?>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"</p>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"</div>", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"-->", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<!--.*", "", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, "<[^>]*>", "", RegexOptions.Compiled);
            text = Regex.Replace(text, @"([\r\n])[\s]+", " ", RegexOptions.Compiled);
            return text.Replace("&nbsp;", " ");
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

        #region # 字符串加密扩展方法 —— static string Encrypt(this string text...
        /// <summary>
        /// 字符串加密扩展方法
        /// </summary>
        public static string Encrypt(this string text, string key = null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "744FBCAD-3BA6-40FB-9A75-B6C81E25403E" : key;

            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                string keyHash8 = key.ToHash16().Substring(0, 8);
                desCryptoService.Key = Encoding.ASCII.GetBytes(keyHash8);
                desCryptoService.IV = Encoding.ASCII.GetBytes(keyHash8);

                byte[] inputByteArray = Encoding.Default.GetBytes(text);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();

                        StringBuilder stringBuilder = new StringBuilder();

                        foreach (byte byt in memoryStream.ToArray())
                        {
                            stringBuilder.AppendFormat("{0:X2}", byt);
                        }

                        return stringBuilder.ToString();
                    }
                }
            }
        }
        #endregion

        #region # 字符串解密扩展方法 —— static string Decrypt(this string text...
        /// <summary>
        /// 字符串解密扩展方法
        /// </summary>
        public static string Decrypt(this string text, string key = null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "744FBCAD-3BA6-40FB-9A75-B6C81E25403E" : key;
            int length = text.Length / 2;

            byte[] inputByteArray = new byte[length];

            for (int index = 0; index < length; index++)
            {
                inputByteArray[index] = Convert.ToByte(text.Substring(index * 2, 2), 16);
            }

            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                string keyHash8 = key.ToHash16().Substring(0, 8);
                desCryptoService.Key = Encoding.ASCII.GetBytes(keyHash8);
                desCryptoService.IV = Encoding.ASCII.GetBytes(keyHash8);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();

                        return Encoding.Default.GetString(memoryStream.ToArray());
                    }
                }
            }
        }
        #endregion
    }
}
