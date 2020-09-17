using NCalc;
using NCalc.Domain;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SD.Toolkits.NCalc
{
    /// <summary>
    /// 公式表达式
    /// </summary>
    public class FormulaExpression : Expression
    {
        #region # 常量与构造器

        /// <summary>
        /// 左方括号
        /// </summary>
        public const string BracketLeft = "[";

        /// <summary>
        /// 右方括号
        /// </summary>
        public const string BracketRight = "]";

        /// <summary>
        /// 创建公式表达式构造器
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        public FormulaExpression(string expression)
            : base(expression)
        {

        }

        /// <summary>
        /// 创建公式表达式构造器
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <param name="options">求值设定</param>
        public FormulaExpression(string expression, EvaluateOptions options)
            : base(expression, options)
        {

        }

        /// <summary>
        /// 创建公式表达式构造器
        /// </summary>
        /// <param name="expression">逻辑表达式</param>
        public FormulaExpression(LogicalExpression expression)
            : base(expression)
        {

        }

        /// <summary>
        /// 创建公式表达式构造器
        /// </summary>
        /// <param name="expression">逻辑表达式</param>
        /// <param name="options">求值设定</param>
        public FormulaExpression(LogicalExpression expression, EvaluateOptions options)
            : base(expression, options)
        {

        }

        #endregion

        #region # 属性

        #region 原始表达式 —— string RawExpression
        /// <summary>
        /// 原始表达式
        /// </summary>
        public string RawExpression
        {
            get { return base.OriginalExpression; }
        }
        #endregion

        #region 原始参数列表 —— ICollection<string> RawParameters
        /// <summary>
        /// 原始参数列表
        /// </summary>
        public ICollection<string> RawParameters
        {
            get
            {
                string pattern = $@"\{FormulaExpression.BracketLeft}.*?\{FormulaExpression.BracketRight}";
                MatchCollection matches = Regex.Matches(this.RawExpression, pattern);

                ICollection<string> parameters = new HashSet<string>();
                foreach (Match match in matches)
                {
                    string parameter = match.Value;
                    parameter = parameter.Replace(FormulaExpression.BracketLeft, string.Empty);
                    parameter = parameter.Replace(FormulaExpression.BracketRight, string.Empty);
                    parameters.Add(parameter);
                }

                return parameters;
            }
        }
        #endregion

        #endregion
    }
}
