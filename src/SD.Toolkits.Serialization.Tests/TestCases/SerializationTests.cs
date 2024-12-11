using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Serialization.Tests.StubEntities;
using System;

namespace SD.Toolkits.Serialization.Tests.TestCases
{
    /// <summary>
    /// 序列化测试
    /// </summary>
    [TestClass]
    public class SerializationTests
    {
        #region # 测试序列化字节数组 —— void TestToBytes()
        /// <summary>
        /// 测试序列化字节数组
        /// </summary>
        [TestMethod]
        public void TestToBytes()
        {
            OrderInfo order = new OrderInfo
            {
                Id = Guid.NewGuid(),
                Number = "XC14220501001",
                Name = "销售发货单-001"
            };
            byte[] bytes = order.ToBytes();
            OrderInfo orderBack = bytes.AsBytesTo<OrderInfo>();

            Assert.AreEqual(order.Id, orderBack.Id);
            Assert.AreEqual(order.Number, orderBack.Number);
            Assert.AreEqual(order.Name, orderBack.Name);
        }
        #endregion

        #region # 测试序列化Base64文本 —— void TestToBase64String()
        /// <summary>
        /// 测试序列化Base64文本
        /// </summary>
        [TestMethod]
        public void TestToBase64String()
        {
            OrderInfo order = new OrderInfo
            {
                Id = Guid.NewGuid(),
                Number = "XC14220501001",
                Name = "销售发货单-001"
            };
            string base64String = order.ToBase64String();
            OrderInfo orderBack = base64String.AsBase64StringTo<OrderInfo>();

            Assert.AreEqual(order.Id, orderBack.Id);
            Assert.AreEqual(order.Number, orderBack.Number);
            Assert.AreEqual(order.Name, orderBack.Name);
        }
        #endregion

        #region # 测试深拷贝实例 —— void TestClone()
        /// <summary>
        /// 测试深拷贝实例
        /// </summary>
        [TestMethod]
        public unsafe void TestClone()
        {
            OrderInfo order = new OrderInfo
            {
                Id = Guid.NewGuid(),
                Number = "XC14220501001",
                Name = "销售发货单-001"
            };
            OrderInfo orderBack = order.Clone<OrderInfo>();

            OrderInfo* orderPtr = &order;
            OrderInfo* orderBackPtr = &orderBack;
            int orderAddr = (int)orderPtr;
            int orderBackAddr = (int)orderBackPtr;

            Assert.AreNotEqual(orderAddr, orderBackAddr);
            Assert.AreEqual(order.Id, orderBack.Id);
            Assert.AreEqual(order.Number, orderBack.Number);
            Assert.AreEqual(order.Name, orderBack.Name);
        }
        #endregion
    }
}
