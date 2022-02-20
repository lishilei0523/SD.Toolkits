using SD.Toolkits.WebApi.Attributes;
using SD.Toolkits.WebApi.Tests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace SD.Toolkits.WebApi.Tests.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "Hello World!";
        }

        [HttpGet]
        public void TestException()
        {
            string errorMessage = "{\"ErrorMessage\":\"登录失败，密码错误！\",\"LogId\":\"09531f51-2412-4423-ae00-20f0792e5545\"}";
            throw new InvalidOperationException(errorMessage);
        }

        [HttpGet]
        public void TestArray(string name, [FromJson] IEnumerable<string> numbers)
        {
            Trace.WriteLine(name);
            Trace.WriteLine(numbers);
        }

        [HttpGet]
        public void TestDictionary(string name, [FromJson] IDictionary<string, string> keyValues)
        {
            Trace.WriteLine(name);
            Trace.WriteLine(keyValues);
        }

        [HttpPost]
        [WrapPostParameters]
        public void TestMultipleParams(int param1, double param2, string param3, DateTime param4)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
            Console.WriteLine(param4);
        }

        [HttpPost]
        [WrapPostParameters]
        public void TestNullableParams(int? param1, double? param2, string param3, DateTime? param4)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
            Console.WriteLine(param4);
        }

        [HttpPost]
        [WrapPostParameters]
        public void TestMultipleParamsWithList(int param1, double param2, IEnumerable<string> param3)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
        }

        [HttpPost]
        [WrapPostParameters]
        public void TestMultipleParamsWithDictionary(int param1, Gender param2, IDictionary<string, int> param3)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
        }

        [HttpPost]
        [WrapPostParameters]
        public void TestMultipleParamsWithObject(Person dad, IEnumerable<Person> sons, IEnumerable<Person> daughters)
        {
            Console.WriteLine(dad);
            Console.WriteLine(sons);
            Console.WriteLine(daughters);
        }
    }
}
