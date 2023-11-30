using SD.Common;
using System;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading.Tasks;

namespace SD.Toolkits.SharedMemory.TestClientS
{
    class Program
    {
        static void Main()
        {
            string key = "Global\\AE43B24E-1A1A-45B1-8B50-1E1649445AB7";
            int length = 343644;
            Parallel.For(0, 100, index =>
            {
                byte[] sharedBytes = MemoryExtension.GetSharedMemory(key, length, out MemoryMappedFile memoryMappedFile);
                if (sharedBytes.Any(x => x != 0))
                {
                    Console.WriteLine($"读取成功！ —— {index}");
                }
                else
                {
                    Console.WriteLine($"读取失败！ —— {index}");
                }

                memoryMappedFile.Dispose();
            });
            for (int index = 0; index < 100; index++)
            {
                byte[] sharedBytes = MemoryExtension.GetSharedMemory(key, length, out MemoryMappedFile memoryMappedFile);
                if (sharedBytes.Any(x => x != 0))
                {
                    Console.WriteLine($"读取成功！ —— {index}");
                }
                else
                {
                    Console.WriteLine($"读取失败！ —— {index}");
                }

                memoryMappedFile.Dispose();
            }

            Console.ReadKey();
        }
    }
}
