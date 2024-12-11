using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            FileInfo fileInfo = new FileInfo(path);
            StreamWriter writer = null;

            if (fileInfo.Exists && !append)
            {
                fileInfo.Delete();
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
                    Directory.CreateDirectory(directory!);
                }

                writer = append ? fileInfo.AppendText() : new StreamWriter(path, false, Encoding.UTF8);
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
            FileInfo[] fileInfos = sourceDir.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                fileInfo.CopyTo(@$"{targetDirectory}\{fileInfo.Name}", true);
            }

            //递归子文件夹
            DirectoryInfo[] subDirs = sourceDir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirs)
            {
                CopyFolderTo(subDir.FullName, @$"{targetDirectory}\{subDir.Name}");
            }
        }
        #endregion


        //FTP远程操作

        #region # 创建FTP请求 —— static FtpWebRequest CreateFtpRequest(string uri...
        /// <summary>
        /// 创建FTP请求
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="loginId">账号</param>
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

        #region # 上传文件 —— static void UploadFile(string fileName, byte[] fileBytes...
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileBytes">文件数据</param>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="remoteDirectory">远程目录</param>
        /// <param name="loginId">账号</param>
        /// <param name="password">密码</param>
        public static void UploadFile(string fileName, byte[] fileBytes, string ip, int port, string remoteDirectory, string loginId, string password)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), "文件名不可为空！");
            }
            if (fileBytes == null || !fileBytes.Any())
            {
                throw new ArgumentNullException(nameof(fileBytes), "文件数据不可为空！");
            }
            if (string.IsNullOrWhiteSpace(remoteDirectory))
            {
                throw new ArgumentNullException(nameof(remoteDirectory), "远程目录不可为空！");
            }
            if (string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException(nameof(ip), "IP地址不可为空！");
            }

            #endregion

            //创建目录
            FileExtension.CreateRemoteDirectory(ip, port, remoteDirectory, loginId, password);

            string uri = $"ftp://{ip}:{port}/{remoteDirectory}/{fileName}";

            FtpWebRequest request = CreateFtpRequest(uri, loginId, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.ContentLength = fileBytes.Length;
            using Stream stream = request.GetRequestStream();
            stream.Write(fileBytes, 0, fileBytes.Length);
        }
        #endregion

        #region # 下载文件 —— static byte[] DownloadFile(string uri, string loginId...
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="loginId">账号</param>
        /// <param name="password">密码</param>
        /// <returns>文件数据</returns>
        public static byte[] DownloadFile(string uri, string loginId, string password)
        {
            FtpWebRequest request = CreateFtpRequest(uri, loginId, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = false;

            using FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using MemoryStream memoryStream = new MemoryStream();
            stream!.CopyTo(memoryStream);
            byte[] buffer = memoryStream.ToArray();

            return buffer;
        }
        #endregion

        #region # 创建远程目录 —— static void CreateRemoteDirectory(string ip, int port...
        /// <summary>
        /// 创建远程目录
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="remoteDirectory">远程目录</param>
        /// <param name="loginId">账号</param>
        /// <param name="password">密码</param>
        public static void CreateRemoteDirectory(string ip, int port, string remoteDirectory, string loginId, string password)
        {
            string uri = $"ftp://{ip}:{port}/{remoteDirectory}";
            FtpWebRequest ftp = CreateFtpRequest(uri, loginId, password);
            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

            using FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            response.Close();
        }
        #endregion

        #region # 读取远程文件列表 —— static string[] ReadRemoteFiles(string uri, string loginId...
        /// <summary>
        /// 读取远程文件列表
        /// </summary>
        /// <param name="uri">远程地址</param>
        /// <param name="loginId">账号</param>
        /// <param name="password">密码</param>
        /// <returns>文件列表</returns>
        public static string[] ReadRemoteFiles(string uri, string loginId, string password)
        {
            FtpWebRequest ftp = CreateFtpRequest(uri, loginId, password);
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            using FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader streamReader = new StreamReader(stream!, Encoding.Default);

            IList<string> files = new List<string>();
            string line = streamReader.ReadLine();
            while (line != null)
            {
                files.Add(line);
                line = streamReader.ReadLine();
            }

            return files.ToArray();
        }
        #endregion
    }
}
