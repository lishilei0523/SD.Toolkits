using SD.Toolkits.CoreWCF.Client.Tests.TestCases;
using System;

namespace SD.Toolkits.CoreWCF.Client.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationTests configurationTests = new ConfigurationTests();
            configurationTests.TestSections();
            configurationTests.TestEndpoints();
            configurationTests.TestBindings();
            configurationTests.TestBehaviors();

            Console.WriteLine("------------------------------");

            ConnectionTests connectionTests = new ConnectionTests();
            connectionTests.TestLogin();
            connectionTests.TestGetUsers();

            Console.WriteLine("------------------------------");
            Console.ReadKey();
        }
    }
}
