﻿using Microsoft.AspNetCore.Mvc;
using SD.Toolkits.WebApiCore.Extensions;
using SD.Toolkits.WebApiCore.Tests.Models;
using System;
using System.Collections.Generic;

namespace SD.Toolkits.WebApiCore.Tests.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public void TestException()
        {
            string errorMessage = "{\"ErrorMessage\":\"登录失败，密码错误！\",\"LogId\":\"09531f51-2412-4423-ae00-20f0792e5545\"}";
            throw new InvalidOperationException(errorMessage);
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
        public void TestMultipleParamsWithObject([FromQuery] Person dad, [FromQuery] IEnumerable<Person> sons, [FromQuery] IEnumerable<Person> daughters)
        {
            Console.WriteLine(dad);
            Console.WriteLine(sons);
            Console.WriteLine(daughters);
        }
    }
}