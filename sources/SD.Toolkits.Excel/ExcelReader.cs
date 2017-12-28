using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace SD.Toolkits.Excel
{
    /// <summary>
    /// Excel读取器
    /// </summary>
    public static class ExcelReader
    {
        //Public

        #region # 读取Excel并转换为给定类型数组 —— static T[] ReadFile<T>(string path, int sheetIndex...
        /// <summary>
        /// 读取Excel并转换为给定类型数组
        /// </summary>
        /// <param name="path">读取路径</param>
        /// <param name="sheetIndex">工作表索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns>给定类型数组</returns>
        /// <remarks>默认读取第一张工作表，第二行</remarks>
        public static T[] ReadFile<T>(string path, int sheetIndex = 0, int rowIndex = 1)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path", @"文件路径不可为空！");
            }
            if (Path.GetExtension(path) != Constant.Excel97ExtensionName)
            {
                throw new ArgumentOutOfRangeException("path", string.Format("文件格式不正确，只能是\"{0}\"格式！", Constant.Excel97ExtensionName));
            }
            if (sheetIndex < 0)
            {
                sheetIndex = 0;
            }
            if (rowIndex < 0)
            {
                rowIndex = 0;
            }

            #endregion

            //01.创建文件流
            using (FileStream stream = File.OpenRead(path))
            {
                //02.创建工作薄
                IWorkbook workbook = new HSSFWorkbook(stream);

                #region # 验证逻辑

                if (sheetIndex + 1 > workbook.NumberOfSheets)
                {
                    throw new InvalidOperationException("给定工作表索引超出了Excel有效工作表数！");
                }

                #endregion

                //03.读取给定工作表
                ISheet sheet = workbook.GetSheetAt(sheetIndex);

                //04.返回集合
                return SheetToArray<T>(sheet, rowIndex);
            }
        }
        #endregion

        #region # 读取Excel并转换为给定类型数组 —— static T[] ReadFile<T>(string path, string...
        /// <summary>
        /// 读取Excel并转换为给定类型数组
        /// </summary>
        /// <param name="path">读取路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns>给定类型数组</returns>
        /// <remarks>默认读取第二行</remarks>
        public static T[] ReadFile<T>(string path, string sheetName, int rowIndex = 1)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path", @"文件路径不可为空！");
            }
            if (string.IsNullOrWhiteSpace(sheetName))
            {
                throw new ArgumentNullException("sheetName", @"工作表名称不可为空！");
            }
            if (Path.GetExtension(path) != Constant.Excel97ExtensionName)
            {
                throw new ArgumentOutOfRangeException("path", string.Format("文件格式不正确，只能是\"{0}\"格式！", Constant.Excel97ExtensionName));
            }
            if (rowIndex < 0)
            {
                rowIndex = 0;
            }

            #endregion

            //01.创建文件流
            using (FileStream stream = File.OpenRead(path))
            {
                //02.创建工作薄
                IWorkbook workbook = new HSSFWorkbook(stream);

                //03.读取给定工作表
                ISheet sheet = workbook.GetSheet(sheetName);

                //04.返回集合
                return SheetToArray<T>(sheet, rowIndex);
            }
        }
        #endregion


        //Private

        #region # 将工作表数据填充至数组 —— static T[] SheetToArray<T>(ISheet sheet, int rowIndex)
        /// <summary>
        /// 将工作表数据填充至数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sheet">工作表</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns>泛型集合</returns>
        private static T[] SheetToArray<T>(ISheet sheet, int rowIndex)
        {
            #region # 验证逻辑

            if (rowIndex > sheet.LastRowNum)
            {
                throw new InvalidOperationException(string.Format("给定行索引\"{0}\"超出了Excel有效行数！", rowIndex));
            }

            #endregion

            ICollection<T> collection = new List<T>();

            //读取给定行索引后的每一行
            for (int index = rowIndex; index <= sheet.LastRowNum; index++)
            {
                Type sourceType = typeof(T);
                T sourceObj = (T)Activator.CreateInstance(sourceType);
                PropertyInfo[] properties = sourceType.GetProperties();

                //读取每行并为对象赋值
                FillInstanceValue(sheet, index, properties, sourceObj);
                collection.Add(sourceObj);
            }
            return collection.ToArray();
        }
        #endregion

        #region # 读取每一行，并填充对象属性值 —— static void FillInstanceValue<T>(ISheet sheet, int index...
        /// <summary>
        /// 读取每一行，并填充对象属性值
        /// </summary>
        /// <param name="sheet">工作表对象</param>
        /// <param name="index">行索引</param>
        /// <param name="properties">对象属性集合</param>
        /// <param name="instance">对象实例</param>
        private static void FillInstanceValue<T>(ISheet sheet, int index, PropertyInfo[] properties, T instance)
        {
            IRow row = sheet.GetRow(index);

            #region # 验证

            if (properties.Length != row.Cells.Count)
            {
                throw new InvalidOperationException(string.Format("模型与Excel表格不兼容：第{0}行 列数不一致！", (index + 1)));
            }

            #endregion

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType == typeof(double))
                {
                    properties[i].SetValue(instance, Convert.ToDouble(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(float))
                {
                    properties[i].SetValue(instance, Convert.ToSingle(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(decimal))
                {
                    properties[i].SetValue(instance, Convert.ToDecimal(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(byte))
                {
                    properties[i].SetValue(instance, Convert.ToByte(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(short))
                {
                    properties[i].SetValue(instance, Convert.ToInt16(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(int))
                {
                    properties[i].SetValue(instance, Convert.ToInt32(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(long))
                {
                    properties[i].SetValue(instance, Convert.ToInt64(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(bool))
                {
                    properties[i].SetValue(instance, Convert.ToBoolean(row.GetCell(i).ToString().Trim()));
                }
                else if (properties[i].PropertyType == typeof(DateTime))
                {
                    properties[i].SetValue(instance, Convert.ToDateTime(row.GetCell(i).ToString().Trim()));
                }
                else
                {
                    properties[i].SetValue(instance, row.GetCell(i).ToString().Trim());
                }
            }
        }
        #endregion
    }
}
