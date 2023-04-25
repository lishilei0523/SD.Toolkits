using SD.Toolkits.Drawing.Enums;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace SD.Toolkits.Drawing
{
    /// <summary>
    /// 图像扩展
    /// </summary>
    public static class ImageExtension
    {
        #region # 制作缩略图 —— static Bitmap MakeThumbnail(this Bitmap image...
        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">缩略图模式</param>    
        public static Bitmap MakeThumbnail(this Bitmap image, int width, int height, ThumbnailMode mode)
        {
            int targetWidth = width;
            int targetHeight = height;
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            int x = 0;
            int y = 0;
            switch (mode)
            {
                case ThumbnailMode.WidthAndHeight:
                    break;
                case ThumbnailMode.Width:
                    targetHeight = image.Height * width / image.Width;
                    break;
                case ThumbnailMode.Height:
                    targetWidth = image.Width * height / image.Height;
                    break;
                case ThumbnailMode.Cut:
                    if (image.Width / (double)image.Height > targetWidth / (double)targetHeight)
                    {
                        originalHeight = image.Height;
                        originalWidth = image.Height * targetWidth / targetHeight;
                        y = 0;
                        x = (image.Width - originalWidth) / 2;
                    }
                    else
                    {
                        originalWidth = image.Width;
                        originalHeight = image.Width * height / targetWidth;
                        x = 0;
                        y = (image.Height - originalHeight) / 2;
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }

            Bitmap thumbnail = new Bitmap(targetWidth, targetHeight);
            Graphics graphics = Graphics.FromImage(thumbnail);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, targetWidth, targetHeight), new Rectangle(x, y, originalWidth, originalHeight), GraphicsUnit.Pixel);

            //释放资源
            image.Dispose();
            graphics.Dispose();

            return thumbnail;
        }
        #endregion

        #region # 制作文字水印 —— static void MakeTextWatermark(this Bitmap image...
        /// <summary>
        /// 制作文字水印
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="fontSize">文字</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="fontStyle">字体样式</param>
        /// <param name="angle">旋转角度</param>
        public static void MakeTextWatermark(this Bitmap image, string watermarkText, Color fontColor, float fontSize = 60, string fontName = "微软雅黑", FontStyle fontStyle = FontStyle.Bold, float angle = 30)
        {
            #region # 验证

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "图片不可为空！");
            }

            #endregion

            using (Graphics graphics = Graphics.FromImage(image))
            {
                float imageWidth = image.Width;
                float imageHeight = image.Height;
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(0, -imageHeight / 2);

                float fontPanelHeight = fontSize * 14;

                //设置旋转角度
                Font font = new Font(fontName, fontSize, fontStyle, GraphicsUnit.Pixel);
                SizeF sizeF = graphics.MeasureString(watermarkText, font);
                for (int index = 0; index < (imageHeight + imageHeight / 2) / fontPanelHeight; index++)
                {
                    float xpos = 0;
                    float ypos = fontPanelHeight * index - fontSize;
                    graphics.DrawString(watermarkText, font, new SolidBrush(fontColor), xpos, ypos);

                    xpos = imageWidth / 2 - sizeF.Width / 2;
                    ypos = fontPanelHeight * index - fontSize;
                    graphics.DrawString(watermarkText, font, new SolidBrush(fontColor), xpos, ypos);

                    xpos = imageWidth - sizeF.Width;
                    ypos = fontPanelHeight * index - fontSize;
                    graphics.DrawString(watermarkText, font, new SolidBrush(fontColor), xpos, ypos);
                }
            }
        }
        #endregion

        #region # 制作图片水印 —— static void MakeImageWatermark(this Bitmap image...
        /// <summary>
        /// 制作图片水印
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="watermarkImage">水印图片</param>
        /// <param name="watermarkTransparency">水印透明度</param>
        /// <remarks>水印透明度：1~10，0完全透明，10不透明</remarks>
        public static void MakeImageWatermark(this Bitmap image, Bitmap watermarkImage, byte watermarkTransparency)
        {
            #region # 验证

            if (watermarkImage.Width >= image.Width || watermarkImage.Height >= image.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(watermarkImage), "水印图片尺寸过大！");
            }

            #endregion

            Graphics graphics = Graphics.FromImage(image);
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap
            {
                OldColor = Color.FromArgb(255, 0, 255, 0),
                NewColor = Color.FromArgb(0, 0, 0, 0)
            };
            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5f;
            if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
            {
                transparency = watermarkTransparency / 10.0f;
            }

            float[][] colorMatrixElements =
            {
                new[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                new[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                new[] {0.0f, 0.0f, 0.0f, transparency, 0.0f},
                new[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = (int)((image.Width * (float)0.99) - (watermarkImage.Width));
            int ypos = (int)((image.Height * (float)0.99) - watermarkImage.Height);

            graphics.DrawImage(watermarkImage, new Rectangle(xpos, ypos, watermarkImage.Width, watermarkImage.Height), 0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel, imageAttributes);

            graphics.Dispose();
            watermarkImage.Dispose();
            imageAttributes.Dispose();
        }
        #endregion

        #region # 压缩图片 —— static Bitmap Compress(this Bitmap image, byte level)
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="level">压缩等级</param>
        /// <remarks>压缩等级：0~100，0最差，100最佳</remarks>
        public static Bitmap Compress(this Bitmap bitmap, byte level)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo imageEncoder = imageEncoders.Single(x => x.MimeType == "image/jpeg");
            Encoder encoder = Encoder.Quality;
            EncoderParameters encoderParameters = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(encoder, level);
            encoderParameters.Param[0] = encoderParameter;

            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, imageEncoder, encoderParameters);

            Bitmap compressedBitmap = new Bitmap(stream);

            return compressedBitmap;
        }
        #endregion
    }
}
