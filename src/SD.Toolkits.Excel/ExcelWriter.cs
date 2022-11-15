using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SD.Toolkits.Excel
{
    /// <summary>
    /// Excel写入器
    /// </summary>
    public static class ExcelWriter
    {
        //Public

        #region # 将集合写入Excel文件 —— static void WriteFile<T>(IEnumerable<T> enumerable...
        /// <summary>
        /// 将集合写入Excel文件
        /// </summary>
        /// <param name="enumerable">集合对象</param>
        /// <param name="path">写入路径</param>
        /// <param name="titles">标题列表</param>
        public static void WriteFile<T>(IEnumerable<T> enumerable, string path, string[] titles = null)
        {
            T[] array = enumerable == null ? new T[0] : enumerable.ToArray();

            #region # 验证

            if (!array.Any())
            {
                throw new ArgumentNullException(nameof(enumerable), $"源{typeof(T).Name}类型集合不可为空！");
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "写入路径不可为空！");
            }

            #endregion

            //创建工作簿
            string extensionName = Path.GetExtension(path);
            IWorkbook workbook = CreateWorkbook(extensionName, array, titles);

            //写入文件
            using (FileStream stream = File.OpenWrite(path))
            {
                workbook.Write(stream);
            }
        }
        #endregion

        #region # 将集合写入Excel字节数组 —— static byte[] WriteStream<T>(IEnumerable<T>...
        /// <summary>
        /// 将集合写入Excel字节数组
        /// </summary>
        /// <param name="enumerable">集合对象</param>
        /// <param name="excelVersion">Excel版本</param>
        /// <param name="titles">标题列表</param>
        /// <returns>字节数组</returns>
        public static byte[] WriteStream<T>(IEnumerable<T> enumerable, ExcelVersion excelVersion, string[] titles = null)
        {
            T[] array = enumerable == null ? new T[0] : enumerable.ToArray();

            #region # 验证

            if (!array.Any())
            {
                throw new ArgumentNullException(nameof(enumerable), $"源{typeof(T).Name}类型集合不可为空！");
            }

            #endregion

            string extensionName = ExcelConductor.ExcelVersions[excelVersion];
            IWorkbook workbook = CreateWorkbook(extensionName, array, titles);

            //写入内存流
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Write(stream);
                byte[] buffer = stream.ToArray();

                return buffer;
            }
        }
        #endregion


        //Private

        #region # 创建工作簿 —— static IWorkbook CreateWorkbook<T>(string extensionName...
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="extensionName">扩展名</param>
        /// <param name="array">对象数组</param>
        /// <param name="titles">标题集</param>
        /// <returns>工作簿</returns>
        private static IWorkbook CreateWorkbook<T>(string extensionName, T[] array, string[] titles = null)
        {
            //创建工作簿
            IWorkbook workbook = ExcelConductor.CreateWorkbook(extensionName);

            //默认字体
            IFont font = workbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 11;

            //标题样式
            ICellStyle titleStyle = workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            titleStyle.BorderLeft = BorderStyle.Thin;
            titleStyle.BorderTop = BorderStyle.Thin;
            titleStyle.BorderRight = BorderStyle.Thin;
            titleStyle.BorderBottom = BorderStyle.Thin;
            titleStyle.SetFont(font);

            //内容样式
            ICellStyle contentStyle = workbook.CreateCellStyle();
            contentStyle.Alignment = HorizontalAlignment.Left;
            contentStyle.VerticalAlignment = VerticalAlignment.Center;
            contentStyle.BorderLeft = BorderStyle.Thin;
            contentStyle.BorderTop = BorderStyle.Thin;
            contentStyle.BorderRight = BorderStyle.Thin;
            contentStyle.BorderBottom = BorderStyle.Thin;
            contentStyle.SetFont(font);

            //创建工作表
            ISheet sheet = workbook.CreateSheet(typeof(T).Name);

            //创建标题行
            IRow rowTitle = sheet.CreateRow(0);
            rowTitle.Height = 15 * 20;
            string[] defaultTitles = typeof(T).GetProperties().Select(x => x.Name).ToArray();
            if (titles == null)
            {
                CreateTitleRow(defaultTitles, rowTitle, titleStyle);
            }
            else
            {
                #region # 验证

                if (titles.Length != defaultTitles.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(titles), "标题列数与数据列数不一致！");
                }

                #endregion

                CreateTitleRow(titles, rowTitle, titleStyle);
            }

            //创建数据行
            CreateDataRows(array, sheet, contentStyle);

            //自适应列宽
            for (int index = 0; index < rowTitle.LastCellNum; index++)
            {
                sheet.AutoSizeColumn(index);
                int columnWidth = sheet.GetColumnWidth(index);
                sheet.SetColumnWidth(index, columnWidth + 3 * 256);
            }

            return workbook;
        }
        #endregion

        #region # 创建标题行 —— static void CreateTitleRow(string[] titles, IRow rowTitle, ICellStyle cellStyle)
        /// <summary>
        /// 创建标题行
        /// </summary>
        /// <param name="titles">标题数组</param>
        /// <param name="rowTitle">标题行</param>
        /// <param name="cellStyle">单元格样式</param>
        private static void CreateTitleRow(string[] titles, IRow rowTitle, ICellStyle cellStyle)
        {
            for (int index = 0; index < titles.Length; index++)
            {
                ICell cell = rowTitle.CreateCell(index);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(titles[index]);
            }
        }
        #endregion

        #region # 创建数据行 —— static void CreateDataRows<T>(T[] array, ISheet sheet, ICellStyle cellStyle)
        /// <summary>
        /// 创建数据行
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <param name="sheet">工作表对象</param>
        /// <param name="cellStyle">单元格样式</param>
        private static void CreateDataRows<T>(T[] array, ISheet sheet, ICellStyle cellStyle)
        {
            for (int index = 0; index < array.Length; index++)
            {
                T item = array[index];
                PropertyInfo[] properties = typeof(T).GetProperties();
                IRow row = sheet.CreateRow(index + 1);
                row.Height = 15 * 20;

                for (int j = 0; j < properties.Length; j++)
                {
                    //创建单元格
                    ICell cell = row.CreateCell(j);
                    cell.CellStyle = cellStyle;

                    //单元格赋值
                    if (properties[j].PropertyType == typeof(double))
                    {
                        cell.SetCellValue((double)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(float))
                    {
                        cell.SetCellValue((float)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(decimal))
                    {
                        cell.SetCellValue(Convert.ToDouble(properties[j].GetValueInternal(item)));
                    }
                    else if (properties[j].PropertyType == typeof(byte))
                    {
                        cell.SetCellValue((byte)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(short))
                    {
                        cell.SetCellValue((short)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(int))
                    {
                        cell.SetCellValue((int)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(long))
                    {
                        cell.SetCellValue((long)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(bool))
                    {
                        cell.SetCellValue((bool)properties[j].GetValueInternal(item));
                    }
                    else if (properties[j].PropertyType == typeof(DateTime))
                    {
                        cell.SetCellValue(((DateTime)properties[j].GetValueInternal(item)).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        cell.SetCellValue(properties[j].GetValueInternal(item) == null
                            ? string.Empty
                            : properties[j].GetValueInternal(item).ToString());
                    }
                }
            }
        }
        #endregion
    }
}
