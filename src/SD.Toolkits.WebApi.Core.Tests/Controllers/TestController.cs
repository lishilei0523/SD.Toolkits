using Microsoft.AspNetCore.Mvc;
using SD.Toolkits.WebApi.Core.Extensions;
using SD.Toolkits.WebApi.Core.Tests.Models;
using System;
using System.Collections.Generic;

namespace SD.Toolkits.WebApi.Core.Tests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        [WrapPostParameters]
        [Route("[action]")]
        public void TestMultipleParams(int param1, double param2, string param3, DateTime param4)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
            Console.WriteLine(param4);
        }

        [HttpPost]
        [WrapPostParameters]
        [Route("[action]")]
        public void TestMultipleParamsWithList(int param1, double param2, IEnumerable<string> param3)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
        }

        [HttpPost]
        [WrapPostParameters]
        [Route("[action]")]
        public void TestMultipleParamsWithDictionary(int param1, Gender param2, IDictionary<string, int> param3)
        {
            Console.WriteLine(param1);
            Console.WriteLine(param2);
            Console.WriteLine(param3);
        }

        [HttpPost]
        [WrapPostParameters]
        [Route("[action]")]
        public void TestMultipleParamsWithObject([FromQuery] Person dad, [FromQuery] IEnumerable<Person> sons, [FromQuery] IEnumerable<Person> daughters)
        {
            Console.WriteLine(dad);
            Console.WriteLine(sons);
            Console.WriteLine(daughters);
        }
    }
}
