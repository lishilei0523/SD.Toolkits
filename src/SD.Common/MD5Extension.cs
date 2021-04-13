using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SD.Common
{
    /// <summary>
    /// MD5扩展
    /// </summary>
    public static class MD5Extension
    {
        #region # 计算文本MD5值 —— static string ToMD5(this string text)
        /// <summary>
        /// 计算文本MD5值
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>MD5值</returns>
        public static string ToMD5(this string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
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

        #region # 计算文本16位MD5值 —— static string ToHash16(this string text)
        /// <summary>
        /// 计算文本16位MD5值
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>16位MD5值</returns>
        public static string ToHash16(this string text)
        {
            MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
            byte[] buffer = md5Crypto.ComputeHash(Encoding.Default.GetBytes(text));
            string hash = BitConverter.ToString(buffer, 4, 8);
            hash = hash.Replace("-", "");

            return hash;
        }
        #endregion

        #region # 计算流MD5值 —— static string ToMD5(this Stream stream)
        /// <summary>
        /// 计算流MD5值
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>MD5值</returns>
        public static string ToMD5(this Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] buffer = md5.ComputeHash(stream);
                StringBuilder md5Builder = new StringBuilder();
                foreach (byte @byte in buffer)
                {
                    md5Builder.Append(@byte.ToString("x2"));
                }

                return md5Builder.ToString();
            }
        }

        #endregion

        #region # 计算byte数组MD5值 —— static string ToMD5(this byte[] bytes)
        /// <summary>
        /// 计算byte数组MD5值
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>MD5值</returns>
        public static string ToMD5(this byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] buffer = md5.ComputeHash(bytes);
                StringBuilder md5Builder = new StringBuilder();
                foreach (byte @byte in buffer)
                {
                    md5Builder.Append(@byte.ToString("x2"));
                }

                return md5Builder.ToString();
            }
        }

        #endregion
    }
}
