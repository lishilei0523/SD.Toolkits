using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace SD.Toolkits.Excel
{
    /// <summary>
    /// 构建器
    /// </summary>
    public static class ExcelConductor
    {
        #region # 字段及构造器

        /// <summary>
        /// Excel 97-03文件扩展名
        /// </summary>
        public const string Excel03ExtensionName = ".xls";

        /// <summary>
        /// Excel 2007文件扩展名
        /// </summary>
        public const string Excel07ExtensionName = ".xlsx";

        /// <summary>
        /// Excel版本字典
        /// </summary>
        private static readonly IDictionary<ExcelVersion, string> _ExcelVersions;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ExcelConductor()
        {
            _ExcelVersions = new Dictionary<ExcelVersion, string>
            {
                { ExcelVersion.Excel03, Excel03ExtensionName },
                { ExcelVersion.Excel07, Excel07ExtensionName }
            };
        }

        #endregion

        #region # Excel版本字典 —— static {ExcelVersion, string} ExcelVersions
        /// <summary>
        /// Excel版本字典
        /// </summary>
        public static IDictionary<ExcelVersion, string> ExcelVersions
        {
            get { return _ExcelVersions; }
        }
        #endregion

        #region # 创建工作簿 —— static IWorkbook CreateWorkbook(string extensionName)
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <param name="extensionName">扩展名</param>
        /// <returns>工作簿</returns>
        public static IWorkbook CreateWorkbook(string extensionName)
        {
            IWorkbook workbook;
            if (extensionName == Excel03ExtensionName)
            {
                workbook = new HSSFWorkbook();
            }
            else if (extensionName == Excel07ExtensionName)
            {
                workbook = new XSSFWorkbook();
            }
            else
            {
                throw new NotSupportedException($"不支持扩展名\"{extensionName}\"！");
            }

            return workbook;
        }
        #endregion

        #region # 创建工作簿 —— static IWorkbook CreateWorkbook(string extensionName, Stream stream)
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <param name="extensionName">扩展名</param>
        /// <param name="stream">流</param>
        /// <returns>工作簿</returns>
        public static IWorkbook CreateWorkbook(string extensionName, Stream stream)
        {
            IWorkbook workbook;
            if (extensionName == Excel03ExtensionName)
            {
                workbook = new HSSFWorkbook(stream);
            }
            else if (extensionName == Excel07ExtensionName)
            {
                workbook = new XSSFWorkbook(stream);
            }
            else
            {
                throw new NotSupportedException($"不支持扩展名\"{extensionName}\"！");
            }

            return workbook;
        }
        #endregion

        #region # 创建工作簿公式求值程序 —— static IFormulaEvaluator CreateFormulaEvaluator(IWorkbook workbook)
        /// <summary>
        /// 创建工作簿公式求值程序
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <returns>公式求值程序</returns>
        public static IFormulaEvaluator CreateFormulaEvaluator(IWorkbook workbook)
        {
            IFormulaEvaluator formulaEvaluator;
            if (workbook is HSSFWorkbook)
            {
                formulaEvaluator = new HSSFFormulaEvaluator(workbook);
            }
            else if (workbook is XSSFWorkbook)
            {
                formulaEvaluator = new XSSFFormulaEvaluator(workbook);
            }
            else
            {
                throw new NotSupportedException("不支持的工作簿！");
            }

            return formulaEvaluator;
        }
        #endregion
    }
}
