using SD.Common;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace SD.Toolkits.SharedMemory.TestClientM
{
    class Program
    {
        static void Main()
        {
            string key = "Global\\AE43B24E-1A1A-45B1-8B50-1E1649445AB7";

            string imagePath = "Images/Earth.jpg";
            using FileStream fileStream = File.OpenRead(imagePath);

            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Seek(0, SeekOrigin.Begin);

            MemoryExtension.SetSharedMemory(key, data, out MemoryMappedFile memoryMappedFile);

            Console.WriteLine("共享内存写入成功！");
            Console.ReadKey();

            memoryMappedFile.Dispose();
        }
    }
}
