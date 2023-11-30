using System;
using System.IO;

namespace SD.Common
{
    /// <summary>
    /// 临时文件流
    /// </summary>
    public class TemporaryFileStream : IDisposable
    {
        #region # 字段及构造器

        /// <summary>
        /// 文件流
        /// </summary>
        private readonly FileStream _fileStream;

        /// <summary>
        /// 创建临时文件流构造器
        /// </summary>
        /// <param name="path">路径</param>
        public TemporaryFileStream(string path)
        {
            this._fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        #endregion

        #region # 属性

        #region 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => this._fileStream.Name;
        }
        #endregion

        #endregion

        #region # 方法

        #region 创建临时文件流 —— static TemporaryFileStream Create(string content)
        /// <summary>
        /// 创建临时文件流
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>临时文件流</returns>
        public static TemporaryFileStream Create(string content)
        {
            string path = Path.GetTempFileName();
            File.WriteAllText(path, content);

            return new TemporaryFileStream(path);
        }
        #endregion

        #region 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this._fileStream?.Dispose();
        }
        #endregion 

        #endregion
    }
}
