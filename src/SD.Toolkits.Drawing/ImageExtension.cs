﻿using SD.Toolkits.Drawing.Enums;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.Drawing
{
    /// <summary>
    /// 图像扩展
    /// </summary>
    public static class ImageExtension
    {
        #region # 调整质量 —— static SKBitmap TuneQuality(this SKBitmap bitmap...
        /// <summary>
        /// 调整质量
        /// </summary>
        /// <param name="bitmap">图像</param>
        /// <param name="quality">质量，0~100，0最差，100最佳</param>
        /// <remarks>不改变图像尺寸</remarks>
        public static SKBitmap TuneQuality(this SKBitmap bitmap, byte quality)
        {
            using SKData imageData = bitmap.Encode(SKEncodedImageFormat.Jpeg, quality);
            SKBitmap tunedBitmap = SKBitmap.Decode(imageData);

            return tunedBitmap;
        }
        #endregion

        #region # 剪裁图像 —— static SKBitmap ClipBitmap(this SKBitmap bitmap...
        /// <summary>
        /// 剪裁图像
        /// </summary>
        /// <param name="bitmap">图像</param>
        /// <param name="rectangle">剪裁区域</param>
        /// <returns>剪裁后图像</returns>
        public static SKBitmap ClipBitmap(this SKBitmap bitmap, SKRectI rectangle)
        {
            SKImageInfo clippedImageInfo = new SKImageInfo(rectangle.Width, rectangle.Height);
            SKBitmap clippedBitmap = new SKBitmap(clippedImageInfo);
            using SKCanvas canvas = new SKCanvas(clippedBitmap);
            canvas.Clear(SKColors.White);
            canvas.DrawBitmap(bitmap, rectangle, SKRect.Create(0, 0, rectangle.Width, rectangle.Height));

            return clippedBitmap;
        }
        #endregion

        #region # 制作缩略图 —— static SKBitmap MakeThumbnail(this SKBitmap bitmap...
        /// <summary>
        /// 制作缩略图
        /// </summary>
        /// <param name="bitmap">图像</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="quality">质量</param>
        /// <param name="thumbnailMode">缩略图模式</param>
        /// <returns>缩略图</returns>
        public static SKBitmap MakeThumbnail(this SKBitmap bitmap, int width, int height, SKFilterQuality quality = SKFilterQuality.Medium, ThumbnailMode thumbnailMode = ThumbnailMode.WidthAndHeight)
        {
            #region # 验证

            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap), "图像不可为空！");
            }

            #endregion

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
        /// <param name="bitmap">图像</param>
        /// <param name="watermark">水印文字</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="location">水印位置</param>
        /// <returns>水印图像</returns>
        public static SKBitmap MakeTextWatermark(this SKBitmap bitmap, string watermark, SKColor fontColor, float fontSize = 60F, string fontName = "微软雅黑", SKPoint? location = null)
        {
            #region # 验证

            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap), "图像不可为空！");
            }

            #endregion

            SKBitmap copyBitmap = bitmap.Copy();
            using SKCanvas canvas = new SKCanvas(copyBitmap);

            using SKTypeface typeface = SKTypeface.FromFamilyName(fontName);
            using SKFont font = new SKFont(typeface, fontSize);
            using SKTextBlob textBlob = SKTextBlob.Create(watermark, font);
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

        #region # 制作图像水印 —— static SKBitmap MakeImageWatermark(this SKBitmap bitmap...
        /// <summary>
        /// 制作图像水印
        /// </summary>
        /// <param name="bitmap">图像</param>
        /// <param name="watermark">水印图像</param>
        /// <param name="watermarkWidth">水印宽度</param>
        /// <param name="watermarkHeight">水印高度</param>
        /// <param name="watermarkQuality">水印质量</param>
        /// <param name="location">水印位置</param>
        /// <returns>水印图像</returns>
        public static SKBitmap MakeImageWatermark(this SKBitmap bitmap, SKBitmap watermark, int watermarkWidth, int watermarkHeight, SKFilterQuality watermarkQuality = SKFilterQuality.Medium, SKPoint? location = null)
        {
            #region # 验证

            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap), "图像不可为空！");
            }
            if (watermark == null)
            {
                throw new ArgumentNullException(nameof(watermark), "水印图像不可为空！");
            }

            #endregion

            SKBitmap copyBitmap = bitmap.Copy();
            using SKCanvas canvas = new SKCanvas(copyBitmap);

            SKSizeI size = new SKSizeI(watermarkWidth, watermarkHeight);
            using SKBitmap resizedWatermark = watermark.Resize(size, watermarkQuality);

            float x;
            float y;
            if (location.HasValue)
            {
                x = location.Value.X;
                y = location.Value.Y;
            }
            else
            {
                x = copyBitmap.Width - resizedWatermark.Width - 5;
                y = copyBitmap.Height - resizedWatermark.Height - 5;
            }

            canvas.DrawBitmap(resizedWatermark, x, y);

            return copyBitmap;
        }
        #endregion

        #region # 横向拼接图像 —— static SKBitmap MergeBitmapsHorizontally(this IEnumerable<SKBitmap>...
        /// <summary>
        /// 横向拼接图像
        /// </summary>
        /// <param name="bitmaps">图像集</param>
        /// <returns>拼接图像</returns>
        public static SKBitmap MergeBitmapsHorizontally(this IEnumerable<SKBitmap> bitmaps)
        {
            #region # 验证

            bitmaps = bitmaps?.ToArray() ?? Array.Empty<SKBitmap>();
            if (!bitmaps.Any())
            {
                throw new ArgumentNullException(nameof(bitmaps), "要合并的图像集不可为空！");
            }
            if (bitmaps.Count() == 1)
            {
                return bitmaps.Single();
            }

            #endregion

            int width = 0;
            int height = 0;
            foreach (SKBitmap bitmap in bitmaps)
            {
                width += bitmap.Width;
                if (height < bitmap.Height)
                {
                    height = bitmap.Height;
                }
            }

            SKBitmap mergedBitmap = new SKBitmap(width, height);
            using SKCanvas canvas = new SKCanvas(mergedBitmap);
            canvas.Clear(SKColors.White);
            int startX = 0;
            foreach (SKBitmap bitmap in bitmaps)
            {
                int startY = (height - bitmap.Height) / 2;
                canvas.DrawBitmap(bitmap, startX, startY);
                startX += bitmap.Width;
            }

            return mergedBitmap;
        }
        #endregion

        #region # 纵向拼接图像 —— static SKBitmap MergeBitmapsVertically(this IEnumerable<SKBitmap>...
        /// <summary>
        /// 纵向拼接图像
        /// </summary>
        /// <param name="bitmaps">图像集</param>
        /// <returns>拼接图像</returns>
        public static SKBitmap MergeBitmapsVertically(this IEnumerable<SKBitmap> bitmaps)
        {
            #region # 验证

            bitmaps = bitmaps?.ToArray() ?? Array.Empty<SKBitmap>();
            if (!bitmaps.Any())
            {
                throw new ArgumentNullException(nameof(bitmaps), "要合并的图像集不可为空！");
            }
            if (bitmaps.Count() == 1)
            {
                return bitmaps.Single();
            }

            #endregion

            int width = 0;
            int height = 0;
            foreach (SKBitmap bitmap in bitmaps)
            {
                height += bitmap.Height;
                if (width < bitmap.Width)
                {
                    width = bitmap.Width;
                }
            }

            SKBitmap mergedBitmap = new SKBitmap(width, height);
            using SKCanvas canvas = new SKCanvas(mergedBitmap);
            canvas.Clear(SKColors.White);
            int startY = 0;
            foreach (SKBitmap bitmap in bitmaps)
            {
                int startX = (width - bitmap.Width) / 2;
                canvas.DrawBitmap(bitmap, startX, startY);
                startY += bitmap.Height;
            }

            return mergedBitmap;
        }
        #endregion
    }
}
