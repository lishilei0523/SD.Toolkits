using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SD.Common
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        #region # 忽略大小写比较字符串是否相等 —— static bool EqualsTo(this string sourceText...
        /// <summary>
        /// 忽略大小写比较字符串是否相等
        /// </summary>
        /// <param name="sourceText">源文本</param>
        /// <param name="targetText">目标文本</param>
        /// <returns>是否相等</returns>
        public static bool EqualsTo(this string sourceText, string targetText)
        {
            if (string.IsNullOrWhiteSpace(sourceText) && string.IsNullOrWhiteSpace(targetText))
            {
                return true;
            }

            return string.Equals(sourceText, targetText, StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion

        #region # 过滤Html标签 —— static string FilterHtmlTags(this string text)
        /// <summary>
        /// 过滤Html标签
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>过滤后的文本</returns>
        public static string FilterHtmlTags(this string text)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            #endregion

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
            text = text.Replace("&nbsp;", " ");

            return text;
        }
        #endregion

        #region # 加密文本 —— static string Encrypt(this string plaintext...
        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="key">键</param>
        /// <returns>密文</returns>
        public static string Encrypt(this string plaintext, string key = null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "744FBCAD-3BA6-40FB-9A75-B6C81E25403E" : key;
            using (DESCryptoServiceProvider desCryptoService = new DESCryptoServiceProvider())
            {
                string keyHash8 = key.ToHash16().Substring(0, 8);
                desCryptoService.Key = Encoding.ASCII.GetBytes(keyHash8);
                desCryptoService.IV = Encoding.ASCII.GetBytes(keyHash8);

                byte[] inputByteArray = Encoding.Default.GetBytes(plaintext);

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

        #region # 解密文本 —— static string Decrypt(this string ciphertext...
        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key">键</param>
        /// <returns>明文</returns>
        public static string Decrypt(this string ciphertext, string key = null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "744FBCAD-3BA6-40FB-9A75-B6C81E25403E" : key;
            int length = ciphertext.Length / 2;

            byte[] inputByteArray = new byte[length];
            for (int index = 0; index < length; index++)
            {
                inputByteArray[index] = Convert.ToByte(ciphertext.Substring(index * 2, 2), 16);
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
