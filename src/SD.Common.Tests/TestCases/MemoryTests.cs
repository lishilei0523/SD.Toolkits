using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;

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

            MemoryExtension.SetSharedMemory(key, data, out MemoryMappedFile memoryMappedFileW);

            Parallel.For(0, 100, index =>
            {
                byte[] sharedBytes = MemoryExtension.GetSharedMemory(key, data.Length, out MemoryMappedFile memoryMappedFileR);
                Assert.IsTrue(sharedBytes.EqualsTo(data));
                memoryMappedFileR.Dispose();
            });
            for (int index = 0; index < 100; index++)
            {
                byte[] sharedBytes = MemoryExtension.GetSharedMemory(key, data.Length, out MemoryMappedFile memoryMappedFileR);
                Assert.IsTrue(sharedBytes.EqualsTo(data));
                memoryMappedFileR.Dispose();
            }

            memoryMappedFileW.Dispose();
        }
        #endregion
    }
}
