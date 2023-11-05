using SkiaSharp;
using System;
using System.IO;
using System.Text;

namespace SD.Toolkits.Drawing
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public static class ValidCodeGenerator
    {
        #region # 生成验证码文本 —— static string GenerateCode(int length)
        /// <summary>
        /// 生成验证码文本
        /// </summary>
        /// <param name="length">文本长度</param>
        /// <returns>验证码文本</returns>
        /// <remarks>最小长度1，最大长度10</remarks>
        public static string GenerateCode(int length)
        {
            #region # 验证

            const int minLength = 1;
            const int maxLength = 10;
            if (length > maxLength)
            {
                length = maxLength;
            }
            if (length < minLength)
            {
                length = minLength;
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

        #region # 生成验证码图像 —— static byte[] GenerateStream(string validCode)
        /// <summary>
        /// 生成验证码图像
        /// </summary>
        /// <param name="validCode">验证码文本</param>
        /// <returns>验证码图像</returns>
        public static byte[] GenerateStream(string validCode)
        {
            Random random = new Random();
            using SKTypeface typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.BoldItalic);
            using SKFont font = new SKFont(typeface, 16);
            using SKTextBlob validCodeBlob = SKTextBlob.Create(validCode, font);

            using SKBitmap bitmap = new SKBitmap((int)Math.Ceiling(validCodeBlob.Bounds.Width - 11), 22);
            using SKCanvas canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);

            SKPoint brushStartPoint = new SKPoint(0, 0);
            SKPoint brushEndPoint = new SKPoint(bitmap.Width, bitmap.Height);
            SKColor[] gradientColors = { SKColors.Blue, SKColors.DarkRed };
            using SKShader brush = SKShader.CreateLinearGradient(brushStartPoint, brushEndPoint, gradientColors, SKShaderTileMode.Clamp);
            using SKPaint textPaint = new SKPaint
            {
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                TextSize = 16,
                Typeface = typeface,
                Shader = brush
            };
            using SKPaint silverPaint = new SKPaint
            {
                Color = SKColors.Silver,
                Style = SKPaintStyle.Stroke
            };

            //画验证码
            canvas.DrawText(validCodeBlob, 5, 17, textPaint);

            //画干扰线
            for (int index = 0; index < 15; index++)
            {
                int x0 = random.Next(bitmap.Width);
                int y0 = random.Next(bitmap.Height);
                int x1 = random.Next(bitmap.Width);
                int y1 = random.Next(bitmap.Height);
                canvas.DrawLine(x0, y0, x1, y1, silverPaint);
            }

            //画干扰点
            for (int index = 0; index < 100; index++)
            {
                int x = random.Next(bitmap.Width);
                int y = random.Next(bitmap.Height);
                SKColor pointColor = new SKColor((uint)random.Next());
                canvas.DrawPoint(x, y, pointColor);
            }

            //画边框线
            canvas.DrawRect(0, 0, bitmap.Width - 1, bitmap.Height - 1, silverPaint);

            //转换图片字节
            using MemoryStream outputStream = new MemoryStream();
            using SKData skData = bitmap.Encode(SKEncodedImageFormat.Jpeg, 80);
            skData.SaveTo(outputStream);
            byte[] imageBytes = outputStream.ToArray();

            return imageBytes;
        }
        #endregion
    }
}
