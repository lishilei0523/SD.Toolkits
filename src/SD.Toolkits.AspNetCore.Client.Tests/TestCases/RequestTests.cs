using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using SD.Toolkits.AspNetCore.Client.Tests.Models;
using SD.Toolkits.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SD.Toolkits.AspNetCore.Client.Tests.TestCases
{
    /// <summary>
    /// 请求测试
    /// </summary>
    [TestClass]
    public class RequestTests
    {
        #region # 测试异常 —— void TestException()
        /// <summary>
        /// 测试异常
        /// </summary>
        [TestMethod]
        public void TestException()
        {
            const string url = "http://localhost:33101/Api/Home/TestException";
            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Get);

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试数组 —— void TestArray()
        /// <summary>
        /// 测试数组
        /// </summary>
        [TestMethod]
        public void TestArray()
        {
            const string url = "http://localhost:33101/Api/Home/TestArray";
            using RestClient httpClient = new RestClient();

            RestRequest request = new RestRequest(url, Method.Get);
            string[] numbers = ["A", "B", "C", "D"];
            request.AddQueryParameter("name", "数组");
            request.AddQueryParameter("numbers", numbers.ToJson());

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试字典 —— void TestDictionary()
        /// <summary>
        /// 测试字典
        /// </summary>
        [TestMethod]
        public void TestDictionary()
        {
            const string url = "http://localhost:33101/Api/Home/TestDictionary";
            using RestClient httpClient = new RestClient();
            IDictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" }
            };
            RestRequest request = new RestRequest(url, Method.Get);
            request.AddQueryParameter("name", "字典");
            request.AddQueryParameter("keyValues", dictionary.ToJson());

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试多参数 —— void TestMultipleParams()
        /// <summary>
        /// 测试多参数
        /// </summary>
        [TestMethod]
        public void TestMultipleParams()
        {
            const string url = "http://localhost:33101/Api/Home/TestMultipleParams";
            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddParameter("param1", 1);
            request.AddParameter("param2", 0.5);
            request.AddParameter("param3", "Hello World");
            request.AddParameter("param4", DateTime.Now);

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试可空参数 —— void TestNullableParams()
        /// <summary>
        /// 测试可空参数
        /// </summary>
        [TestMethod]
        public void TestNullableParams()
        {
            const string url = "http://localhost:33101/Api/Home/TestNullableParams";
            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddParameter("param1", null);
            request.AddParameter("param2", 10.5);
            request.AddParameter("param3", "Hello");
            request.AddParameter("param4", DateTime.Now);

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试多参数带集合 —— void TestMultipleParamsWithList()
        /// <summary>
        /// 测试多参数带集合
        /// </summary>
        [TestMethod]
        public void TestMultipleParamsWithList()
        {
            const string url = "http://localhost:33101/Api/Home/TestMultipleParamsWithList";
            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            string[] numbers = ["A", "B", "C", "D"];
            request.AddParameter("param1", 20);
            request.AddParameter("param2", 10.5);
            request.AddParameter("param3", numbers.ToJson());

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试多参数带字典 —— void TestMultipleParamsWithDictionary()
        /// <summary>
        /// 测试多参数带字典
        /// </summary>
        [TestMethod]
        public void TestMultipleParamsWithDictionary()
        {
            const string url = "http://localhost:33101/Api/Home/TestMultipleParamsWithDictionary";
            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            IDictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "key1", 1 },
                { "key2", 2 },
                { "key3", 3 }
            };
            request.AddParameter("param1", 20);
            request.AddParameter("param2", (int)Gender.Female);
            request.AddParameter("param3", dictionary.ToJson());

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试多参数带对象 —— void TestMultipleParamsWithObject()
        /// <summary>
        /// 测试多参数带对象
        /// </summary>
        [TestMethod]
        public void TestMultipleParamsWithObject()
        {
            const string url = "http://localhost:33101/Api/Home/TestMultipleParamsWithObject";
            using RestClient httpClient = new RestClient();

            RestRequest request = new RestRequest(url, Method.Post);
            Person dad = new Person("父亲", 40, new DateTime(1980, 12, 10));
            IList<Person> sons = new List<Person>
            {
                new Person("儿子1", 10, new DateTime(2004, 2, 5)),
                new Person("儿子2", 9, new DateTime(2005, 2, 5)),
                new Person("儿子3", 8, new DateTime(2006, 2, 5))
            };
            IList<Person> daughters = new List<Person>
            {
                new Person("姑娘1", 10, new DateTime(2004, 2, 5)),
                new Person("姑娘2", 9, new DateTime(2005, 2, 5)),
                new Person("姑娘3", 8, new DateTime(2006, 2, 5))
            };
            request.AddParameter("dad", dad.ToJson());
            request.AddParameter("sons", sons.ToJson());
            request.AddParameter("daughters", daughters.ToJson());

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("请求成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("请求失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试单文件上传 —— void TestSingleFile()
        /// <summary>
        /// 测试单文件上传
        /// </summary>
        [TestMethod]
        public void TestSingleFile()
        {
            string filePath = CreateFile();
            string fileName = Path.GetFileName(filePath);
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            const string url = "http://localhost:33101/Api/Home/TestSingleFile";

            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");
            request.AddFile("formFile", buffer, fileName);

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("上传失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion

        #region # 测试多文件上传 —— void TestMultiFiles()
        /// <summary>
        /// 测试多文件上传
        /// </summary>
        [TestMethod]
        public void TestMultiFiles()
        {
            IDictionary<string, byte[]> buffers = new Dictionary<string, byte[]>();
            for (int index = 0; index < 3; index++)
            {
                string filePath = CreateFile();
                string fileName = Path.GetFileName(filePath);
                using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                buffers.Add(fileName, buffer);
            }

            const string url = "http://localhost:33101/Api/Home/TestMultiFiles";

            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");
            foreach (KeyValuePair<string, byte[]> kv in buffers)
            {
                request.AddFile("formFiles", kv.Value, kv.Key);
            }

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine("上传失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }
        #endregion


        //Private

        #region # 创建文件 —— static string CreateFile()
        /// <summary>
        /// 创建文件
        /// </summary>
        private static string CreateFile()
        {
            string fileName = Guid.NewGuid().ToString();
            string filePath = $@"D:\{fileName}.txt";

            File.WriteAllText(filePath, fileName);

            return filePath;
        }
        #endregion
    }
}
