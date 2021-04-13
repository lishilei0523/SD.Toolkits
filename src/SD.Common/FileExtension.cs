using System;
using System.IO;
using System.Net;
using System.Text;

namespace SD.Common
{
    /// <summary>
    /// 文件操作扩展
    /// </summary>
    public static class FileExtension
    {
        //本地操作

        #region # 读取文件方法 —— static string ReadFile(string path)
        /// <summary>
        /// 读取文件方法
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>内容字符串</returns>
        /// <exception cref="ArgumentNullException">路径为空</exception>
        public static string ReadFile(string path)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), @"路径不可为空！");
            }

            #endregion

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(path, Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            finally
            {
                reader?.Dispose();
            }
        }
        #endregion

        #region # 写入文件方法 —— static void WriteFile(string path, string text...
        /// <summary>
        /// 写入文件方法
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">文本</param>
        /// <param name="append">是否附加</param>
        public static void WriteFile(string path, string text, bool append = false)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "路径不可为空！");
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text), "文本不可为空！");
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

                if (string.IsNullOrEmpty(directory))
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

        #region # 复制文件夹方法 —— static void CopyFolderTo(this string sourcePath, string targetPath)
        /// <summary>
        /// 复制文件夹方法
        /// </summary>
        /// <param name="sourcePath">源路径</param>
        /// <param name="targetPath">目标路径</param>
        public static void CopyFolderTo(this string sourcePath, string targetPath)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentNullException(nameof(sourcePath), "源路径不可为空！");
            }

            if (string.IsNullOrWhiteSpace(targetPath))
            {
                throw new ArgumentNullException(nameof(targetPath), "目标路径不可为空！");
            }

            #endregion

            //01.创建目标文件夹
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            //02.拷贝文件
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            FileInfo[] fileArray = sourceDir.GetFiles();
            foreach (FileInfo file in fileArray)
            {
                file.CopyTo($"{targetPath}\\{file.Name}", true);
            }

            //03.递归循环子文件夹
            DirectoryInfo[] subDirArray = sourceDir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirArray)
            {
                CopyFolderTo(subDir.FullName, $"{targetPath}//{subDir.Name}");
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

        #region # 上传文件 —— static void Upload(this FileInfo fileInfo, string hostName...
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileInfo">文件</param>
        /// <param name="remoteDirectory">远程目录</param>
        /// <param name="hostName">主机名</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        public static void Upload(this FileInfo fileInfo, string hostName, string remoteDirectory, string loginId, string password)
        {
            #region # 验证

            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo), "文件不可为空！");
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

            string uri = $"ftp://{hostName}/{remoteDirectory}/{fileInfo.Name}";
            byte[] buffer = new byte[2048];

            FtpWebRequest request = CreateFtpRequest(uri, loginId, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.ContentLength = fileInfo.Length;

            using (FileStream fileStream = fileInfo.OpenRead())
            {
                using (Stream stream = request.GetRequestStream())
                {
                    int dataRead;
                    do
                    {
                        dataRead = fileStream.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, dataRead);
                    } while (!(dataRead < buffer.Length));
                }
            }
        }
        #endregion

        #region # 下载文件 —— static void DownloadFile(string uri, string localDirectory...
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="localDirectory">本地目录</param>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        public static void DownloadFile(string uri, string localDirectory, string loginId, string password)
        {
            string fileName = Path.GetFileName(uri);
            string localPath = $@"{localDirectory}\{fileName}";
            byte[] buffer = new byte[2048];

            FtpWebRequest request = CreateFtpRequest(uri, loginId, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = false;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (FileStream fileStream = new FileStream(localPath, FileMode.OpenOrCreate))
                    {
                        try
                        {

                            int read;
                            do
                            {
                                read = stream.Read(buffer, 0, buffer.Length);
                                fileStream.Write(buffer, 0, read);
                            } while (read != 0);
                        }
                        catch
                        {
                            if (File.Exists(localPath))
                            {
                                File.Delete(localPath);
                            }
                            throw;
                        }
                    }

                    stream.Close();
                }

                response.Close();
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

            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                {
                    StringBuilder builder = new StringBuilder();
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        builder.Append(line);
                        builder.Append(newLine);
                        line = reader.ReadLine();
                    }

                    builder.Remove(builder.ToString().LastIndexOf(newLine), 1);
                    string[] files = builder.ToString().Split(newLine);

                    response.Close();

                    return files;
                }
            }
        }
        #endregion
    }
}
