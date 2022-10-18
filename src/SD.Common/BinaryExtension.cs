using System;
using System.Text;

namespace SD.Common
{
    /// <summary>
    /// 二进制扩展
    /// </summary>
    public static class BinaryExtension
    {
        #region # 设置数值位 —— static ushort SetBit(this ushort number, int index, bool bit)
        /// <summary>
        /// 设置数值位
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="index">索引</param>
        /// <param name="bit">数值位</param>
        /// <returns>数值</returns>
        public static ushort SetBit(this ushort number, int index, bool bit)
        {
            int mask = 0x01 << index;
            if (bit)
            {
                number = (ushort)(number | mask);
            }
            else
            {
                mask = ~mask;
                number = (ushort)(number & mask);
            }

            return number;
        }
        #endregion

        #region # 获取数值位 —— static bool GetBit(this ushort number, int index)
        /// <summary>
        /// 获取数值位
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="index">索引</param>
        /// <returns>数值位</returns>
        public static bool GetBit(this ushort number, int index)
        {
            int mask = 0x01 << index;

            return (number & mask) != 0;
        }
        #endregion

        #region # 拆分高低字节 —— static void Split2Bytes(int number, out byte highByte, out byte lowByte)
        /// <summary>
        /// 拆分高低字节
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="highByte">高字节</param>
        /// <param name="lowByte">低字节</param>
        /// <returns>高低字节</returns>
        public static void Split2Bytes(int number, out byte highByte, out byte lowByte)
        {
            highByte = (byte)(number >> 8);
            lowByte = (byte)number;
        }
        #endregion

        #region # 合并高低字节 —— static int Merge2Bytes(byte highByte, byte lowByte)
        /// <summary>
        /// 合并高低字节
        /// </summary>
        /// <param name="highByte">高字节</param>
        /// <param name="lowByte">低字节</param>
        /// <returns>数值</returns>
        public static int Merge2Bytes(byte highByte, byte lowByte)
        {
            int number = (highByte << 8) + lowByte;

            return number;
        }
        #endregion

        #region # string转ushort数组 —— static ushort[] ToUShorts(this string text)
        /// <summary>
        /// string转ushort数组
        /// </summary>
        public static ushort[] ToUShorts(this string text)
        {
            byte[] bytesTemp = Encoding.UTF8.GetBytes(text);
            ushort[] dest = Bytes2UShorts(bytesTemp);

            return dest;
        }
        #endregion

        #region # ushort数组转string —— static string ToString(this ushort[] array)
        /// <summary>
        /// ushort数组转string
        /// </summary>
        public static string ToString(this ushort[] array)
        {
            byte[] bytes = UShorts2Bytes(array);
            string text = Encoding.UTF8.GetString(bytes).Trim('\0');

            return text;
        }
        #endregion

        #region # float转ushort数组 —— static ushort[] ToUShorts(this float number)
        /// <summary>
        /// float转ushort数组
        /// </summary>
        public static ushort[] ToUShorts(this float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] ushorts = Bytes2UShorts(bytes);

            return ushorts;
        }
        #endregion

        #region # ushort数组转float —— static float ToFloat(this ushort[] array)
        /// <summary>
        /// ushort数组转float
        /// </summary>
        public static float ToFloat(this ushort[] array)
        {
            ushort[] ushorts = new ushort[2];
            for (int index = 0; index < 2; index++)
            {
                ushorts[index] = array[index];
            }
            byte[] bytes = UShorts2Bytes(ushorts);
            float value = BitConverter.ToSingle(bytes, 0);

            return value;
        }
        #endregion

        #region # byte数组转ushort数组 —— static ushort[] Bytes2UShorts(this byte[] array)
        /// <summary>
        /// byte数组转ushort数组
        /// </summary>
        /// <param name="array">byte数组</param>
        /// <returns>ushort数组</returns>
        public static ushort[] Bytes2UShorts(this byte[] array)
        {
            int length = array.Length;
            int count = length >> 1;
            byte[] bytes = new byte[length + 1];
            array.CopyTo(bytes, 0);

            if (length % 2 != 0)
            {
                count += 1;
            }

            ushort[] ushorts = new ushort[count];
            for (int index = 0; index < count; index++)
            {
                ushorts[index] = (ushort)(bytes[index * 2] & 0xff | bytes[2 * index + 1] << 8);
            }

            return ushorts;
        }
        #endregion

        #region # ushort数组转byte数组 —— static byte[] UShorts2Bytes(this ushort[] array)
        /// <summary>
        /// ushort数组转byte数组
        /// </summary>
        /// <param name="array">ushort数组</param>
        /// <returns>byte数组</returns>
        public static byte[] UShorts2Bytes(this ushort[] array)
        {
            int length = array.Length;
            byte[] bytes = new byte[length << 1];
            for (int index = 0; index < length; index++)
            {
                bytes[index * 2] = (byte)(array[index] >> 0);
                bytes[index * 2 + 1] = (byte)(array[index] >> 8);
            }

            return bytes;
        }
        #endregion
    }
}
