using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO.MemoryMappedFiles;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 内存测试
    /// </summary>
    [TestClass]
    public class MemoryTests
    {
        #region # 测试共享内存 —— void TestSharedMemory()
        /// <summary>
        /// 测试共享内存
        /// </summary>
        [TestMethod]
        public void TestSharedMemory()
        {
            string key = Guid.NewGuid().ToString();

            byte[] data = new byte[65536];
            for (int index = 0; index < 65536; index++)
            {
                data[index] = (byte)(index % 100);
            }

            MemoryExtension.SetSharedMemory(key, data, out MemoryMappedFile memoryMappedFile1);
            byte[] sharedBytes = MemoryExtension.GetSharedMemory(key, data.Length, out MemoryMappedFile memoryMappedFile2);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(sharedBytes.EqualsTo(data));

            memoryMappedFile1.Dispose();
            memoryMappedFile2.Dispose();
        }
        #endregion
    }
}
