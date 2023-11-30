using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 二进制测试
    /// </summary>
    [TestClass]
    public class BinaryTests
    {
        #region # Modbus CRC16校验码测试 —— void TestModbusCRC16()
        /// <summary>
        /// Modbus CRC16校验码测试
        /// </summary>
        [TestMethod]
        public void TestModbusCRC16()
        {
            byte[] bytes1 = { 0x01, 0x64, 0x01, 0x00, 0x00, 0x00, 0x51, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00 };
            byte[] bytes2 = { 0x01, 0x64, 0x01, 0x00, 0x00, 0x00, 0x51, 0xFF, 0xFF, 0xC0, 0x00, 0x00, 0x00, 0x00 };

            byte[] crc1 = bytes1.GetModbusCRC16();
            Assert.AreEqual(crc1[0], 0x1C);
            Assert.AreEqual(crc1[1], 0x18);

            byte[] crc2 = bytes2.GetModbusCRC16();
            Assert.AreEqual(crc2[0], 0x06);
            Assert.AreEqual(crc2[1], 0xC6);
        }
        #endregion
    }
}
