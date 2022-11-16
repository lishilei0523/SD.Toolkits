using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

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
        /// <param name="path">文件路径</param>
        /// <param name="sheetIndex">工作表索引</param>
        /// <param name="rowIndex">起始行索引</param>
        /// <returns>给定类型数组</returns>
        /// <remarks>默认读取第1张工作表，第2行</remarks>
        public static T[] ReadFile<T>(string path, int sheetIndex = 0, int rowIndex = 1)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "文件路径不可为空！");
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

            using (FileStream stream = File.OpenRead(path))
            {
                //读取工作薄
                string extensionName = Path.GetExtension(path);
                IWorkbook workbook = ExcelConductor.CreateWorkbook(extensionName, stream);

                #region # 验证

                if (sheetIndex + 1 > workbook.NumberOfSheets)
                {
                    throw new ArgumentOutOfRangeException(nameof(sheetIndex), "给定工作表索引超出了Excel有效工作表数！");
                }

                #endregion

                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                T[] array = SheetToArray<T>(sheet, rowIndex);

                //关闭工作薄
                workbook.Close();

                return array;
            }
        }
        #endregion

        #region # 读取Excel并转换为给定类型数组 —— static T[] ReadFile<T>(string path, string sheetName...
        /// <summary>
        /// 读取Excel并转换为给定类型数组
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="rowIndex">起始行索引</param>
        /// <returns>给定类型数组</returns>
        /// <remarks>默认读取第2行</remarks>
        public static T[] ReadFile<T>(string path, string sheetName, int rowIndex = 1)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), "文件路径不可为空！");
            }
            if (string.IsNullOrWhiteSpace(sheetName))
            {
                throw new ArgumentNullException(nameof(sheetName), "工作表名称不可为空！");
            }
            if (rowIndex < 0)
            {
                rowIndex = 0;
            }

            #endregion

            using (FileStream stream = File.OpenRead(path))
            {
                //读取工作薄
                string extensionName = Path.GetExtension(path);
                IWorkbook workbook = ExcelConductor.CreateWorkbook(extensionName, stream);

                ISheet sheet = workbook.GetSheet(sheetName);
                T[] array = SheetToArray<T>(sheet, rowIndex);

                //关闭工作薄
                workbook.Close();

                return array;
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
        /// <param name="rowIndex">起始行索引</param>
        /// <returns>实例数组</returns>
        private static T[] SheetToArray<T>(ISheet sheet, int rowIndex)
        {
            #region # 验证

            if (rowIndex > sheet.LastRowNum)
            {
                throw new InvalidOperationException($"给定行索引\"{rowIndex}\"超出了Excel有效行数！");
            }

            #endregion

            Type instanceType = typeof(T);
            PropertyInfo[] properties = instanceType.GetProperties();

            //读取起始行索引后的每一行为实例赋值
            IList<T> instances = new List<T>();
            for (int index = rowIndex; index <= sheet.LastRowNum; index++)
            {
                T instance = (T)Activator.CreateInstance(instanceType);
                FillInstanceValue(sheet, index, properties, instance);
                instances.Add(instance);
            }

            return instances.ToArray();
        }
        #endregion

        #region # 读取行填充实例属性值 —— static void FillInstanceValue<T>(ISheet sheet, int rowIndex...
        /// <summary>
        /// 读取行填充实例属性值
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="properties">属性集</param>
        /// <param name="instance">实例</param>
        private static void FillInstanceValue<T>(ISheet sheet, int rowIndex, PropertyInfo[] properties, T instance)
        {
            IFormulaEvaluator formulaEvaluator = ExcelConductor.CreateFormulaEvaluator(sheet.Workbook);
            IRow row = sheet.GetRow(rowIndex);

            #region # 验证

            if (properties.Length != row.Cells.Count)
            {
                throw new InvalidOperationException($"模型与Excel表格不兼容：第{rowIndex + 1}行 列数不一致！");
            }

            #endregion

            for (int propertyIndex = 0; propertyIndex < properties.Length; propertyIndex++)
            {
                PropertyInfo property = properties[propertyIndex];
                Type propertyType = property.PropertyType;

                ICell cell = row.GetCell(propertyIndex);
                string cellValue = cell.ToString().Trim();

                #region # 公式与日期时间处理

                //公式
                if (cell.CellType == CellType.Formula)
                {
                    CellValue formulaValue = formulaEvaluator.Evaluate(cell);
                    switch (formulaValue.CellType)
                    {
                        case CellType.Numeric:
                            cellValue = formulaValue.NumberValue.ToString();
                            break;
                        case CellType.String:
                            cellValue = formulaValue.StringValue;
                            break;
                        case CellType.Boolean:
                            cellValue = formulaValue.BooleanValue.ToString();
                            break;
                        default:
                            cellValue = formulaValue.StringValue;
                            break;
                    }
                }

                //日期时间
                if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                {
                    cellValue = cell.DateCellValue.ToString(CultureInfo.CurrentCulture);
                }

                #endregion

                if (propertyType == typeof(bool))
                {
                    property.SetValueInternal(instance, Convert.ToBoolean(cellValue));
                }
                else if (propertyType == typeof(byte))
                {
                    property.SetValueInternal(instance, Convert.ToByte(cellValue));
                }
                else if (propertyType == typeof(sbyte))
                {
                    property.SetValueInternal(instance, Convert.ToSByte(cellValue));
                }
                else if (propertyType == typeof(short))
                {
                    property.SetValueInternal(instance, Convert.ToInt16(cellValue));
                }
                else if (propertyType == typeof(ushort))
                {
                    property.SetValueInternal(instance, Convert.ToUInt16(cellValue));
                }
                else if (propertyType == typeof(int))
                {
                    property.SetValueInternal(instance, Convert.ToInt32(cellValue));
                }
                else if (propertyType == typeof(uint))
                {
                    property.SetValueInternal(instance, Convert.ToUInt32(cellValue));
                }
                else if (propertyType == typeof(long))
                {
                    property.SetValueInternal(instance, Convert.ToInt64(cellValue));
                }
                else if (propertyType == typeof(ulong))
                {
                    property.SetValueInternal(instance, Convert.ToUInt64(cellValue));
                }
                else if (propertyType == typeof(float))
                {
                    property.SetValueInternal(instance, Convert.ToSingle(cellValue));
                }
                else if (propertyType == typeof(double))
                {
                    property.SetValueInternal(instance, Convert.ToDouble(cellValue));
                }
                else if (propertyType == typeof(decimal))
                {
                    property.SetValueInternal(instance, Convert.ToDecimal(cellValue));
                }
                else if (propertyType == typeof(Guid))
                {
                    property.SetValueInternal(instance, Guid.Parse(cellValue));
                }
                else if (propertyType == typeof(DateTime))
                {
                    property.SetValueInternal(instance, Convert.ToDateTime(cellValue));
                }
                else if (propertyType == typeof(TimeSpan))
                {
                    property.SetValueInternal(instance, TimeSpan.Parse(cellValue));
                }
                else
                {
                    property.SetValueInternal(instance, cellValue);
                }
            }
        }
        #endregion
    }
}
