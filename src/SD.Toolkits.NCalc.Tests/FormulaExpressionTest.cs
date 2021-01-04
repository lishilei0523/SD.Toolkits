﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string formula = "[A]+[B]+[C]";
            FormulaExpression expression = new FormulaExpression(formula);

            Assert.AreEqual(formula, expression.RawExpression);
        }

        /// <summary>
        /// 测试格式化表达式
        /// </summary>
        [TestMethod]
        public void TestFormativeExpression()
        {
            string formula = "[A]+[B]+[C]";
            FormulaExpression expression = new FormulaExpression(formula);

            expression.Parameters["A"] = 10;
            expression.Parameters["B"] = 190;
            expression.Parameters["C"] = 0.25;

            Assert.AreEqual(expression.FormativeExpression, "10+190+0.25");
        }

        /// <summary>
        /// 测试原始参数列表
        /// </summary>
        [TestMethod]
        public void TestRawParameters()
        {
            string formula = "[A]+[B]+[C]";
            FormulaExpression expression = new FormulaExpression(formula);

            Assert.IsTrue(expression.RawParameters.Contains("A"));
            Assert.IsTrue(expression.RawParameters.Contains("B"));
            Assert.IsTrue(expression.RawParameters.Contains("C"));
        }

        /// <summary>
        /// 测试格式化参数列表
        /// </summary>
        [TestMethod]
        public void TestFormativeParameters()
        {
            string formula = "[A]+[B]+[C]";
            FormulaExpression expression = new FormulaExpression(formula);

            Assert.IsTrue(expression.FormativeParameters.Keys.Contains("A"));
            Assert.IsTrue(expression.FormativeParameters.Keys.Contains("B"));
            Assert.IsTrue(expression.FormativeParameters.Keys.Contains("C"));
            Assert.IsTrue(expression.FormativeParameters.Values.Contains("[A]"));
            Assert.IsTrue(expression.FormativeParameters.Values.Contains("[B]"));
            Assert.IsTrue(expression.FormativeParameters.Values.Contains("[C]"));
        }

        /// <summary>
        /// 测试求值
        /// </summary>
        [TestMethod]
        public void TestEvaluate()
        {
            string formula = "[A]+[B]+[C]";
            FormulaExpression expression = new FormulaExpression(formula);

            expression.Parameters["A"] = 10;
            expression.Parameters["B"] = 190;
            expression.Parameters["C"] = 0.25;

            object result = expression.Evaluate();

            Assert.AreEqual(result, 200.25);
        }
    }
}
