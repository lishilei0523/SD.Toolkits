﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SD.Toolkits.AspNetCore.Attributes;
using SD.Toolkits.AspNetCore.Server.Tests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SD.Toolkits.AspNetCore.Server.Tests.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class HomeController : ControllerBase
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
        public void TestMultipleParamsWithObject([FromQuery] Person dad, [FromQuery] IEnumerable<Person> sons, [FromQuery] IEnumerable<Person> daughters)
        {
            Console.WriteLine(dad);
            Console.WriteLine(sons);
            Console.WriteLine(daughters);
        }

        [HttpPost]
        [FileParameters]
        [DisableRequestSizeLimit]
        public void TestSingleFile(string use, string description, IFormFile formFile)
        {
            #region # 验证

            if (!base.Request.ContentType!.StartsWith("multipart/form-data"))
            {
                throw new HttpRequestException(HttpStatusCode.UnsupportedMediaType.ToString());
            }
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile), "要上传的文件不可为空！");
            }

            #endregion

            Console.WriteLine(use);
            Console.WriteLine(description);
            Console.WriteLine(formFile);
        }

        [HttpPost]
        [FileParameters]
        [DisableRequestSizeLimit]
        public void TestMultiFiles(string use, string description, IFormFileCollection formFiles)
        {
            #region # 验证

            if (!base.Request.ContentType!.StartsWith("multipart/form-data"))
            {
                throw new HttpRequestException(HttpStatusCode.UnsupportedMediaType.ToString());
            }
            if (formFiles == null || !formFiles.Any())
            {
                throw new ArgumentNullException(nameof(formFiles), "要上传的文件集不可为空！");
            }

            #endregion

            Console.WriteLine(use);
            Console.WriteLine(description);
            Console.WriteLine(formFiles);
        }
    }
}
