using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public static class ValidCodeGenerator
    {
        #region # 常量

        /// <summary>
        /// 验证码的最大长度
        /// </summary>
        private const int MaxLength = 10;

        /// <summary>
        /// 验证码的最小长度
        /// </summary>
        private const int MinLength = 1;

        #endregion

        #region # 生成验证码字符串 —— string GenerateCode(int length)
        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>验证码字符串</returns>
        public static string GenerateCode(int length)
        {
            #region # 验证

            if (length > MaxLength)
            {
                length = MaxLength;
            }
            if (length < MinLength)
            {
                length = MinLength;
            }

            #endregion

            int[] randomNumbers = new int[length];
            int[] validNumbers = new int[length];
            int[] seeks = new int[length];
            StringBuilder validCodeBuilder = new StringBuilder();

            //生成起始序列值
            int seek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seek);
            int beginSeek = seekRand.Next(0, int.MaxValue - length * 10000);
            for (int index = 0; index < length; index++)
            {
                beginSeek += 10000;
                seeks[index] = beginSeek;
            }

            //生成随机数字
            for (int index = 0; index < length; index++)
            {
                Random random = new Random(seeks[index]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randomNumbers[index] = random.Next(pownum, int.MaxValue);
            }

            //抽取随机数字
            for (int index = 0; index < length; index++)
            {
                string numberText = randomNumbers[index].ToString();
                int numberLength = numberText.Length;
                Random random = new Random();
                int numberPosition = random.Next(0, numberLength - 1);
                validNumbers[index] = int.Parse(numberText.Substring(numberPosition, 1));
            }

            //生成验证码
            for (int index = 0; index < length; index++)
            {
                validCodeBuilder.Append(validNumbers[index]);
            }

            return validCodeBuilder.ToString();
        }
        #endregion

        #region # 生成验证码图片 —— static byte[] GenerateStream(string validCode)
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="validCode">验证码字符串</param>
        /// <returns>验证码图片序列化后的字节数组</returns>
        public static byte[] GenerateStream(string validCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validCode.Length * 12.0), 22);
            Graphics graphic = Graphics.FromImage(image);
            try
            {
                MemoryStream stream = DrawValidCode(validCode, graphic, image);
                byte[] buffer = stream.ToArray();
                stream.Dispose();

                return buffer;
            }
            finally
            {
                graphic.Dispose();
                image.Dispose();
            }
        }
        #endregion

        #region # 绘制验证码 —— MemoryStream DrawValidCode(string validCode...
        /// <summary>
        /// 绘制验证码
        /// </summary>
        /// <param name="validCode">验证码</param>
        /// <param name="graphic">画刷</param>
        /// <param name="image">画布</param>
        /// <returns>内存流</returns>
        private static MemoryStream DrawValidCode(string validCode, Graphics graphic, Bitmap image)
        {
            //生成随机生成器
            Random random = new Random();

            //清空图片背景色
            graphic.Clear(Color.White);

            //画图片的干扰线
            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                Pen pen = new Pen(Color.Silver);
                graphic.DrawLine(pen, x1, y1, x2, y2);
            }

            Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rectangle, Color.Blue, Color.DarkRed, 1.2f, true);
            graphic.DrawString(validCode, font, brush, 3, 2);

            //画图片的前景干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //画图片的边框线
            graphic.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            //保存图片数据
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);

            return stream;
        }
        #endregion
    }
}
