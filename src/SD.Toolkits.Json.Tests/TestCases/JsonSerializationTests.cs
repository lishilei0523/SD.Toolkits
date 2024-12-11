using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Json.Tests.StubDTOs;
using System;

namespace SD.Toolkits.Json.Tests.TestCases
{
    /// <summary>
    /// JSON序列化测试
    /// </summary>
    [TestClass]
    public class JsonSerializationTests
    {
        #region # 测试序列化 —— void TestSerialize()
        /// <summary>
        /// 测试序列化
        /// </summary>
        [TestMethod]
        public void TestSerialize()
        {
            OrderInfo order = new OrderInfo
            {
                Id = Guid.NewGuid(),
                Number = "XC14220501001",
                Name = "销售发货单-001"
            };

            string json = order.ToJson();
            string text = "XC14220501001";

            Assert.IsTrue(json.Contains(text));
        }
        #endregion

        #region # 测试反序列化 —— void TestDeserialize()
        /// <summary>
        /// 测试反序列化
        /// </summary>
        [TestMethod]
        public void TestDeserialize()
        {
            OrderInfo order = new OrderInfo
            {
                Id = Guid.NewGuid(),
                Number = "XC14220501001",
                Name = "销售发货单-001"
            };
            string json = order.ToJson();
            OrderInfo orderBack = json.AsJsonTo<OrderInfo>();

            Assert.IsTrue(order.Id == orderBack.Id);
            Assert.IsTrue(order.Number == orderBack.Number);
            Assert.IsTrue(order.Name == orderBack.Name);
        }
        #endregion

        #region # 测试序列化日期时间 —— void TestSerializeDateTime()
        /// <summary>
        /// 测试序列化日期时间
        /// </summary>
        [TestMethod]
        public void TestSerializeDateTime()
        {
            StudentInfo studentInfo = new StudentInfo
            {
                Id = 1,
                Name = "张三",
                BirthDay = new DateTime(2016, 9, 1, 9, 33, 10)
            };

            string json = studentInfo.ToJson("yyyy-MM-dd HH:mm:ss");
            string dateText = "2016-09-01 09:33:10";

            Assert.IsTrue(json.Contains(dateText));
        }
        #endregion

        #region # 测试反序列化日期时间 —— void TestDeserializeDateTime()
        /// <summary>
        /// 测试反序列化日期时间
        /// </summary>
        [TestMethod]
        public void TestDeserializeDateTime()
        {
            StudentInfo studentInfo = new StudentInfo
            {
                Id = 1,
                Name = "张三",
                BirthDay = new DateTime(2016, 9, 1, 9, 33, 10)
            };

            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = studentInfo.ToJson(dateTimeFormat);
            StudentInfo student = json.AsJsonTo<StudentInfo>(dateTimeFormat);

            Assert.IsTrue(studentInfo.Id == student.Id);
            Assert.IsTrue(studentInfo.Name == student.Name);
            Assert.IsTrue(studentInfo.BirthDay == student.BirthDay);
        }
        #endregion

        #region # 测试序列化可空日期时间 —— void TestSerializeNullableDateTime()
        /// <summary>
        /// 测试序列化可空日期时间
        /// </summary>
        [TestMethod]
        public void TestSerializeNullableDateTime()
        {
            PersonInfo person1 = new PersonInfo
            {
                Id = 1,
                Name = "张三",
                BirthDay = new DateTime(2016, 9, 1, 9, 33, 10)
            };
            PersonInfo person2 = new PersonInfo
            {
                Id = 2,
                Name = "李四",
                BirthDay = null
            };

            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json1 = person1.ToJson(dateTimeFormat);
            string json2 = person2.ToJson(dateTimeFormat);
            string dateText = "2016-09-01 09:33:10";

            Assert.IsTrue(json1.Contains(dateText));
            Assert.IsTrue(json2.Contains("null"));
        }
        #endregion

        #region # 测试反序列化可空日期时间 —— void TestDeserializeNullableDateTime()
        /// <summary>
        /// 测试反序列化可空日期时间
        /// </summary>
        [TestMethod]
        public void TestDeserializeNullableDateTime()
        {
            PersonInfo person1 = new PersonInfo
            {
                Id = 1,
                Name = "张三",
                BirthDay = new DateTime(2016, 9, 1, 9, 33, 10)
            };
            PersonInfo person2 = new PersonInfo
            {
                Id = 2,
                Name = "李四",
                BirthDay = null
            };

            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json1 = person1.ToJson(dateTimeFormat);
            string json2 = person2.ToJson(dateTimeFormat);

            PersonInfo person1Back = json1.AsJsonTo<PersonInfo>(dateTimeFormat);
            PersonInfo person2Back = json2.AsJsonTo<PersonInfo>(dateTimeFormat);

            Assert.IsTrue(person1.Id == person1Back.Id);
            Assert.IsTrue(person1.Name == person1Back.Name);
            Assert.IsTrue(person1.BirthDay == person1Back.BirthDay);
            Assert.IsTrue(person2.Id == person2Back.Id);
            Assert.IsTrue(person2.Name == person2Back.Name);
            Assert.IsTrue(person2.BirthDay == person2Back.BirthDay);
        }
        #endregion
    }
}
