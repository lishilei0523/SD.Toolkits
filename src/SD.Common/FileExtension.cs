using System;
using System.IO;
using System.Net;
using System.Text;

namespace SD.Common
{
    /// <summary>
    /// 文件扩展
    /// </summary>
    public static class FileExtension
    {
        //本地操作

        #region # 读取文件 —— static string ReadFile(string path)
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文本</returns>
        public static string ReadFile(string path)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), @"路径不可为空！");
            }

            #endregion

            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(path, Encoding.UTF8);
                string content = streamReader.ReadToEnd();
                return content;
            }
            finally
            {
                streamReader?.Dispose();
            }
        }
        #endregion

        #region # 写入文件 —— static void WriteFile(string path, string text...
        /// <summary>
        /// 写入文件方法
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">文本</param>
        /// <param name="append">是否附加</param>
        public static void WriteFile(string path, string text, bool append = false)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "路径不可为空！");
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                text = string.Empty;
            }

            #endregion

            FileInfo file = new FileInfo(path);
            StreamWriter writer = null;

            if (file.Exists && !append)
            {
                file.Delete();
            }
            try
            {
                //获取文件目录并判断是否存在
                string directory = Path.GetDirectoryName(path);
                if (string.IsNullOrWhiteSpace(directory))
                {
                    throw new ArgumentNullException(nameof(path), "目录不可为空！");
                }
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                writer = append ? file.AppendText() : new StreamWriter(path, false, Encoding.UTF8);
                writer.Write(text);
            }
            finally
            {
                writer?.Dispose();
            }
        }
        #endregion

        #region # 复制文件夹 —— static void CopyFolderTo(this string sourcePath, string targetPath)
        /// <summary>
        /// 复制文件夹方法
        /// </summary>
        /// <param name="sourceDirectory">源文件夹</param>
        /// <param name="targetDirectory">目标文件夹</param>
        public static void CopyFolderTo(this string sourceDirectory, string targetDirectory)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(sourceDirectory))
            {
                throw new ArgumentNullException(nameof(sourceDirectory), "源文件夹不可为空！");
            }
            if (string.IsNullOrWhiteSpace(targetDirectory))
            {
                throw new ArgumentNullException(nameof(targetDirectory), "目标文件夹不可为空！");
            }

            #endregion

            //创建目标文件夹
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            //拷贝文件
            DirectoryInfo sourceDir = new DirectoryInfo(sourceDirectory);
            FileInfo[] fileArray = sourceDir.GetFiles();
            foreach (FileInfo file in fileArray)
            {
                file.CopyTo($"{targetDirectory}\\{file.Name}", true);
            }

            //递归子文件夹
            DirectoryInfo[] subDirArray = sourceDir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirArray)
            {
                CopyFolderTo(subDir.FullName, $"{targetDirectory}//{subDir.Name}");
            }
        }
        #endregion


        //FTP远程操作

        #region # 创建FTP请求 —— static FtpWebRequest CreateFtpRequest(string uri...
        /// <summary>
        /// 创建FTP请求
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>FTP请求</returns>
        public static FtpWebRequest CreateFtpRequest(string uri, string loginId, string password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

            if (!string.IsNullOrWhiteSpace(loginId) && !string.IsNullOrWhiteSpace(password))
            {
                //提供身份验证信息
                request.Credentials = new NetworkCredential(loginId, password);
            }

            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            request.KeepAlive = false;

            return request;
        }
        #endregion

        #region # 上传文件 —— static void Upload(string fileName, byte[] fileDatas...
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileDatas">文件数据</param>
        /// <param name="remoteDirectory">远程目录</param>
        /// <param name="hostName">主机名</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        public static void Upload(string fileName, byte[] fileDatas, string hostName, string remoteDirectory, string loginId, string password)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), "文件名不可为空！");
            }
            if (fileDatas == null)
            {
                throw new ArgumentNullException(nameof(fileDatas), "文件数据不可为空！");
            }
            if (string.IsNullOrWhiteSpace(remoteDirectory))
            {
                throw new ArgumentNullException(nameof(remoteDirectory), "目标路径不可为空！");
            }
            if (string.IsNullOrWhiteSpace(hostName))
            {
                throw new ArgumentNullException(nameof(hostName), "host地址不可为空！");
            }

            #endregion

            //创建目录
            CreateRemoteDirectoryIfNotExists(hostName, remoteDirectory, loginId, password);

            string uri = $"ftp://{hostName}/{remoteDirectory}/{fileName}";

            FtpWebRequest request = CreateFtpRequest(uri, loginId, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.ContentLength = fileDatas.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(fileDatas, 0, fileDatas.Length);
            }
        }
        #endregion

        #region # 下载文件 —— static byte[] DownloadFile(string uri, string loginId...
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>文件数据</returns>
        public static byte[] DownloadFile(string uri, string loginId, string password)
        {
            FtpWebRequest request = CreateFtpRequest(uri, loginId, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = false;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    byte[] buffer = stream.ToByteArray();
                    return buffer;
                }
            }
        }
        #endregion

        #region # 创建远程目录 —— static void CreateRemoteDirectoryIfNotExists(string hostName, string directoryName...
        /// <summary>
        /// 创建远程目录
        /// </summary>
        /// <param name="hostName">主机名</param>
        /// <param name="directoryName">目录名</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        public static void CreateRemoteDirectoryIfNotExists(string hostName, string directoryName, string loginId, string password)
        {
            try
            {
                string uri = "ftp://" + hostName + "/" + directoryName;
                FtpWebRequest ftp = CreateFtpRequest(uri, loginId, password);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception exception)
            {
                if (exception is WebException webException)
                {
                    FtpWebResponse response = (FtpWebResponse)webException.Response;

                    if (response.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion

        #region # 读取远程地址文件列表 —— static string[] ReadRemoteFiles(string uri, string loginId...
        /// <summary>
        /// 读取远程地址文件列表
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>远程目录文件列表</returns>
        public static string[] ReadRemoteFiles(string uri, string loginId, string password)
        {
            const char newLine = '\n';
            FtpWebRequest ftp = CreateFtpRequest(uri, loginId, password);
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
                    {
                        StringBuilder builder = new StringBuilder();
                        string line = streamReader.ReadLine();
                        while (line != null)
                        {
                            builder.Append(line);
                            builder.Append(newLine);
                            line = streamReader.ReadLine();
                        }

                        builder.Remove(builder.ToString().LastIndexOf(newLine), 1);
                        string[] files = builder.ToString().Split(newLine);

                        return files;
                    }
                }
            }
        }
        #endregion
    }


    /// <summary>
    /// 临时文件流
    /// </summary>
    public class TemporaryFileStream : IDisposable
    {
        /// <summary>
        /// 路径
        /// </summary>
        private readonly string _path;

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
            this._path = path;
            this._fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        }

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

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return this._fileStream.Name; }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this._fileStream.Dispose();
            File.Delete(this._path);
        }
    }
}
