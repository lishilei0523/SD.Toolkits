using SD.Toolkits.SkiaSharp.Enums;
using SkiaSharp;
using System;

namespace SD.Toolkits.SkiaSharp
{
    /// <summary>
    /// 图像扩展
    /// </summary>
    public static class ImageExtension
    {
        #region # 制作缩略图 —— static SKBitmap MakeThumbnail(this SKBitmap bitmap...
        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="quality">质量</param>
        /// <param name="thumbnailMode">缩略图模式</param>    
        public static SKBitmap MakeThumbnail(this SKBitmap bitmap, int width, int height, SKFilterQuality quality = SKFilterQuality.Medium, ThumbnailMode thumbnailMode = ThumbnailMode.WidthAndHeight)
        {
            int targetWidth = width;
            int targetHeight = height;
            switch (thumbnailMode)
            {
                case ThumbnailMode.WidthAndHeight:
                    break;
                case ThumbnailMode.Width:
                    targetHeight = bitmap.Height * width / bitmap.Width;
                    break;
                case ThumbnailMode.Height:
                    targetWidth = bitmap.Width * height / bitmap.Height;
                    break;
                default:
                    throw new NotSupportedException();
            }

            SKSizeI size = new SKSizeI(targetWidth, targetHeight);
            SKBitmap thumbnail = bitmap.Resize(size, quality);

            return thumbnail;
        }
        #endregion

        #region # 制作文字水印 —— static SKBitmap MakeTextWatermark(this SKBitmap bitmap...
        /// <summary>
        /// 制作文字水印
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="location">文字位置</param>
        public static SKBitmap MakeTextWatermark(this SKBitmap bitmap, string watermarkText, SKColor fontColor, float fontSize = 60F, string fontName = "微软雅黑", SKPoint? location = null)
        {
            SKBitmap copyBitmap = bitmap.Copy();
            using SKCanvas canvas = new SKCanvas(copyBitmap);

            using SKTypeface typeface = SKTypeface.FromFamilyName(fontName);
            using SKFont font = new SKFont(typeface, fontSize);
            using SKTextBlob textBlob = SKTextBlob.Create(watermarkText, font);
            float x;
            float y;
            if (location.HasValue)
            {
                x = location.Value.X;
                y = location.Value.Y;
            }
            else
            {
                x = copyBitmap.Width - textBlob.Bounds.Width;
                y = copyBitmap.Height - textBlob.Bounds.Height;
            }
            using SKPaint paint = new SKPaint
            {
                Color = fontColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = fontSize,
                Typeface = typeface
            };

            canvas.DrawText(textBlob, x, y, paint);

            return copyBitmap;
        }
        #endregion
    }
}
