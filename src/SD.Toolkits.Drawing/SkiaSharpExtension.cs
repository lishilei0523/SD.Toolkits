using SkiaSharp;
using System;
using System.Drawing;

namespace SD.Toolkits.Drawing
{
    /// <summary>
    /// SkiaSharp扩展
    /// </summary>
    public static class SkiaSharpExtension
    {
        #region # 点映射 —— static Point ToPoint(this SKPoint skPoint)
        /// <summary>
        /// 点映射
        /// </summary>
        public static Point ToPoint(this SKPoint skPoint)
        {
            int x = (int)Math.Ceiling(skPoint.X);
            int y = (int)Math.Ceiling(skPoint.Y);
            Point point = new Point(x, y);

            return point;
        }
        #endregion

        #region # 点映射 —— static Point ToPoint(this SKPointI skPoint)
        /// <summary>
        /// 点映射
        /// </summary>
        public static Point ToPoint(this SKPointI skPoint)
        {
            Point point = new Point(skPoint.X, skPoint.Y);

            return point;
        }
        #endregion

        #region # 点映射 —— static PointF ToPointF(this SKPoint skPoint)
        /// <summary>
        /// 点映射
        /// </summary>
        public static PointF ToPointF(this SKPoint skPoint)
        {
            PointF point = new PointF(skPoint.X, skPoint.Y);

            return point;
        }
        #endregion

        #region # 点映射 —— static PointF ToPointF(this SKPointI skPoint)
        /// <summary>
        /// 点映射
        /// </summary>
        public static PointF ToPointF(this SKPointI skPoint)
        {
            PointF point = new PointF(skPoint.X, skPoint.Y);

            return point;
        }
        #endregion

        #region # 点映射 —— static SKPoint ToSKPoint(this Point point)
        /// <summary>
        /// 点映射
        /// </summary>
        public static SKPoint ToSKPoint(this Point point)
        {
            SKPoint skPoint = new SKPoint(point.X, point.Y);

            return skPoint;
        }
        #endregion

        #region # 点映射 —— static SKPoint ToSKPoint(this PointF point)
        /// <summary>
        /// 点映射
        /// </summary>
        public static SKPoint ToSKPoint(this PointF point)
        {
            SKPoint skPoint = new SKPoint(point.X, point.Y);

            return skPoint;
        }
        #endregion

        #region # 点映射 —— static SKPointI ToSKPointI(this Point point)
        /// <summary>
        /// 点映射
        /// </summary>
        public static SKPointI ToSKPointI(this Point point)
        {
            SKPointI skPoint = new SKPointI(point.X, point.Y);

            return skPoint;
        }
        #endregion

        #region # 点映射 —— static SKPointI ToSKPointI(this PointF point)
        /// <summary>
        /// 点映射
        /// </summary>
        public static SKPointI ToSKPointI(this PointF point)
        {
            int x = (int)Math.Ceiling(point.X);
            int y = (int)Math.Ceiling(point.Y);
            SKPointI skPoint = new SKPointI(x, y);

            return skPoint;
        }
        #endregion

        #region # 尺寸映射 —— static Size ToSize(this SKSize skSize)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static Size ToSize(this SKSize skSize)
        {
            int width = (int)Math.Ceiling(skSize.Width);
            int height = (int)Math.Ceiling(skSize.Height);
            Size size = new Size(width, height);

            return size;
        }
        #endregion

        #region # 尺寸映射 —— static Size ToSize(this SKSizeI skSize)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static Size ToSize(this SKSizeI skSize)
        {
            int width = skSize.Width;
            int height = skSize.Height;
            Size size = new Size(width, height);

            return size;
        }
        #endregion

        #region # 尺寸映射 —— static SizeF ToSizeF(this SKSize skSize)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static SizeF ToSizeF(this SKSize skSize)
        {
            SizeF size = new SizeF(skSize.Width, skSize.Height);

            return size;
        }
        #endregion

        #region # 尺寸映射 —— static SizeF ToSizeF(this SKSizeI skSize)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static SizeF ToSizeF(this SKSizeI skSize)
        {
            SizeF size = new SizeF(skSize.Width, skSize.Height);

            return size;
        }
        #endregion

        #region # 尺寸映射 —— static SKSize ToSKSize(this Size size)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static SKSize ToSKSize(this Size size)
        {
            SKSize skSize = new SKSize(size.Width, size.Height);

            return skSize;
        }
        #endregion

        #region # 尺寸映射 —— static SKSize ToSKSize(this SizeF size)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static SKSize ToSKSize(this SizeF size)
        {
            SKSize skSize = new SKSize(size.Width, size.Height);

            return skSize;
        }
        #endregion

        #region # 尺寸映射 —— static SKSizeI ToSKSizeI(this Size size)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static SKSizeI ToSKSizeI(this Size size)
        {
            SKSizeI skSize = new SKSizeI(size.Width, size.Height);

            return skSize;
        }
        #endregion

        #region # 尺寸映射 —— static SKSizeI ToSKSizeI(this SizeF size)
        /// <summary>
        /// 尺寸映射
        /// </summary>
        public static SKSizeI ToSKSizeI(this SizeF size)
        {
            int width = (int)Math.Ceiling(size.Width);
            int height = (int)Math.Ceiling(size.Height);
            SKSizeI skSize = new SKSizeI(width, height);

            return skSize;
        }
        #endregion

        #region # 矩形映射 —— static Rectangle ToRectangle(this SKRect skRect)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static Rectangle ToRectangle(this SKRect skRect)
        {
            int x = (int)Math.Ceiling(skRect.Left);
            int y = (int)Math.Ceiling(skRect.Top);
            int width = (int)Math.Ceiling(skRect.Width);
            int height = (int)Math.Ceiling(skRect.Height);
            Rectangle rectangle = new Rectangle(x, y, width, height);

            return rectangle;
        }
        #endregion

        #region # 矩形映射 —— static Rectangle ToRectangle(this SKRectI skRect)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static Rectangle ToRectangle(this SKRectI skRect)
        {
            int x = skRect.Left;
            int y = skRect.Top;
            int width = skRect.Width;
            int height = skRect.Height;
            Rectangle rectangle = new Rectangle(x, y, width, height);

            return rectangle;
        }
        #endregion

        #region # 矩形映射 —— static RectangleF ToRectangleF(this SKRect skRect)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static RectangleF ToRectangleF(this SKRect skRect)
        {
            float x = skRect.Left;
            float y = skRect.Top;
            float width = skRect.Width;
            float height = skRect.Height;
            RectangleF rectangle = new RectangleF(x, y, width, height);

            return rectangle;
        }
        #endregion

        #region # 矩形映射 —— static RectangleF ToRectangleF(this SKRectI skRect)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static RectangleF ToRectangleF(this SKRectI skRect)
        {
            int x = skRect.Left;
            int y = skRect.Top;
            int width = skRect.Width;
            int height = skRect.Height;
            RectangleF rectangle = new RectangleF(x, y, width, height);

            return rectangle;
        }
        #endregion

        #region # 矩形映射 —— static SKRect ToSkRect(this Rectangle rectangle)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static SKRect ToSkRect(this Rectangle rectangle)
        {
            SKRect skRect = SKRect.Create(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

            return skRect;
        }
        #endregion

        #region # 矩形映射 —— static SKRect ToSkRect(this RectangleF rectangle)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static SKRect ToSkRect(this RectangleF rectangle)
        {
            SKRect skRect = SKRect.Create(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

            return skRect;
        }
        #endregion

        #region # 矩形映射 —— static SKRectI ToSkRectI(this Rectangle rectangle)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static SKRectI ToSkRectI(this Rectangle rectangle)
        {
            SKRectI skRect = SKRectI.Create(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

            return skRect;
        }
        #endregion

        #region # 矩形映射 —— static SKRectI ToSkRectI(this RectangleF rectangle)
        /// <summary>
        /// 矩形映射
        /// </summary>
        public static SKRectI ToSkRectI(this RectangleF rectangle)
        {
            int x = (int)Math.Ceiling(rectangle.Left);
            int y = (int)Math.Ceiling(rectangle.Top);
            int width = (int)Math.Ceiling(rectangle.Width);
            int height = (int)Math.Ceiling(rectangle.Height);
            SKRectI skRect = SKRectI.Create(x, y, width, height);

            return skRect;
        }
        #endregion

        #region # 颜色映射 —— static Color ToColor(this SKColor skColor)
        /// <summary>
        /// 颜色映射
        /// </summary>
        public static Color ToColor(this SKColor skColor)
        {
            Color color = Color.FromArgb(skColor.Alpha, skColor.Red, skColor.Green, skColor.Blue);

            return color;
        }
        #endregion

        #region # 颜色映射 —— static Color ToColor(this SKColorF skColor)
        /// <summary>
        /// 颜色映射
        /// </summary>
        public static Color ToColor(this SKColorF skColor)
        {
            int a = (int)Math.Ceiling(skColor.Alpha);
            int r = (int)Math.Ceiling(skColor.Red);
            int g = (int)Math.Ceiling(skColor.Green);
            int b = (int)Math.Ceiling(skColor.Blue);
            Color color = Color.FromArgb(a, r, g, b);

            return color;
        }
        #endregion

        #region # 颜色映射 —— static SKColor ToSKColor(this Color color)
        /// <summary>
        /// 颜色映射
        /// </summary>
        public static SKColor ToSKColor(this Color color)
        {
            SKColor skColor = new SKColor(color.R, color.G, color.B, color.A);

            return skColor;
        }
        #endregion

        #region # 颜色映射 —— static SKColorF ToSKColorF(this Color color)
        /// <summary>
        /// 颜色映射
        /// </summary>
        public static SKColorF ToSKColorF(this Color color)
        {
            SKColorF skColor = new SKColorF(color.R, color.G, color.B, color.A);

            return skColor;
        }
        #endregion
    }
}
