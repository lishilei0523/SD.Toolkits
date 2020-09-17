using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SD.Toolkits.NCalc.Tests
{
    /// <summary>
    /// 公式表达式测试
    /// </summary>
    [TestClass]
    public class FormulaExpressionTest
    {
        /// <summary>
        /// 测试原始表达式
        /// </summary>
        [TestMethod]
        public void TestRawExpression()
        {
            string formula = "[Prefix]+[TimeFormat]+[Flow]";
            FormulaExpression expression = new FormulaExpression(formula);

            Assert.AreEqual(formula, expression.RawExpression);
        }

        /// <summary>
        /// 测试原始参数列表
        /// </summary>
        [TestMethod]
        public void TestRawParameters()
        {
            string formula = "[Prefix]+[TimeFormat]+[Flow]";
            FormulaExpression expression = new FormulaExpression(formula);

            Assert.IsTrue(expression.RawParameters.Contains("Prefix"));
            Assert.IsTrue(expression.RawParameters.Contains("TimeFormat"));
            Assert.IsTrue(expression.RawParameters.Contains("Flow"));
        }
    }
}
