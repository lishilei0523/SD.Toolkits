using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;
using System;

namespace SD.Toolkits.EntityFrameworkCore.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            DbSession dbSession = new DbSession();

            Student student = new Student("001", "张三", true, 20, Guid.NewGuid());

            dbSession.Add(student);

            dbSession.SaveChanges();

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
