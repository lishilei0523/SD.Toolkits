using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace SD.Common
{
    /// <summary>
    /// 内存扩展
    /// </summary>
    public static class MemoryExtension
    {
        #region # 设置共享内存 —— static void SetSharedMemory(string key, byte[] bytes...
        /// <summary>
        /// 设置共享内存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="bytes">数据</param>
        /// <param name="memoryMappedFile">内存映射文件</param>
        public static void SetSharedMemory(string key, byte[] bytes, out MemoryMappedFile memoryMappedFile)
        {
            #region # 验证

            bytes = bytes?.ToArray() ?? new byte[0];
            if (!bytes.Any())
            {
                throw new ArgumentNullException(nameof(bytes), "数据不可为空！");
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "键不可为空！");
            }

            #endregion

#if NET40_OR_GREATER
            memoryMappedFile = MemoryMappedFile.CreateOrOpen(key, bytes.Length, MemoryMappedFileAccess.ReadWriteExecute, MemoryMappedFileOptions.DelayAllocatePages, null, HandleInheritability.Inheritable);
#else
            memoryMappedFile = MemoryMappedFile.CreateOrOpen(key, bytes.Length, MemoryMappedFileAccess.ReadWriteExecute, MemoryMappedFileOptions.DelayAllocatePages, HandleInheritability.Inheritable);
#endif
            using MemoryMappedViewStream stream = memoryMappedFile.CreateViewStream(0, bytes.Length);
            stream.Write(bytes, 0, bytes.Length);
        }
        #endregion

        #region # 获取共享内存 —— static byte[] GetSharedMemory(string key, int length...
        /// <summary>
        /// 获取共享内存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="length">数据长度</param>
        /// <param name="memoryMappedFile">内存映射文件</param>
        /// <returns>数据</returns>
        public static byte[] GetSharedMemory(string key, int length, out MemoryMappedFile memoryMappedFile)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key), "键不可为空！");
            }
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "数据长度不可小于等于0！");
            }

            #endregion

            byte[] bytes = new byte[length];
            memoryMappedFile = MemoryMappedFile.OpenExisting(key, MemoryMappedFileRights.FullControl, HandleInheritability.Inheritable);
            using MemoryMappedViewStream stream = memoryMappedFile.CreateViewStream(0, length);
            stream.Read(bytes, 0, length);

            return bytes;
        }
        #endregion
    }
}
