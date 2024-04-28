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

        #region 格式化表达式 —— string FormativeExpression
        /// <summary>
        /// 格式化表达式
        /// </summary>
        public string FormativeExpression
        {
            get
            {
                string formativeExpression = this.RawExpression;
                foreach (KeyValuePair<string, string> kv in this.FormativeParameters)
                {
                    if (this.Parameters.TryGetValue(kv.Key, out object value))
                    {
                        string parameterValue = value.ToString();
                        formativeExpression = formativeExpression.Replace(kv.Value, parameterValue);
                    }
                }

                return formativeExpression;
            }
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
                string pattern = $@"\{BracketLeft}.*?\{BracketRight}";
                MatchCollection matches = Regex.Matches(this.RawExpression, pattern);

                ICollection<string> parameters = new HashSet<string>();
                foreach (Match match in matches)
                {
                    string parameter = match.Value;
                    parameter = parameter.Replace(BracketLeft, string.Empty);
                    parameter = parameter.Replace(BracketRight, string.Empty);
                    parameters.Add(parameter);
                }

                return parameters;
            }
        }
        #endregion

        #region 格式化参数字典 —— IDictionary<string, string> FormativeParameters
        /// <summary>
        /// 格式化参数字典
        /// </summary>
        /// <remarks>键：参数名，值：格式化参数名</remarks>
        public IDictionary<string, string> FormativeParameters
        {
            get
            {
                string pattern = $@"\{BracketLeft}.*?\{BracketRight}";
                MatchCollection matches = Regex.Matches(this.RawExpression, pattern);

                IDictionary<string, string> parameters = new Dictionary<string, string>();
                foreach (Match match in matches)
                {
                    string parameter = match.Value;
                    string formativeParameter = parameter;
                    formativeParameter = formativeParameter.Replace(BracketLeft, string.Empty);
                    formativeParameter = formativeParameter.Replace(BracketRight, string.Empty);
                    parameters.Add(formativeParameter, parameter);
                }

                return parameters;
            }
        }
        #endregion

        #endregion
    }
}
