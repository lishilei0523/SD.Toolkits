using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.Toolkits.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// 查询建造者
    /// </summary>
    public sealed class QueryBuilder<T> where T : class
    {
        #region # 字段及构造器

        /// <summary>
        /// 种子条件表达式
        /// </summary>
        private Expression<Func<T, bool>> _seedCondition;

        /// <summary>
        /// 创建查询建造者构造器
        /// </summary>
        /// <param name="seedPredicate">种子条件表达式</param>
        public QueryBuilder(Expression<Func<T, bool>> seedPredicate)
        {
            #region # 验证

            if (seedPredicate == null)
            {
                throw new ArgumentNullException(nameof(seedPredicate), "种子条件表达式不可为空！");
            }

            #endregion

            this._seedCondition = seedPredicate;
        }

        /// <summary>
        /// 创建以肯定开始查询建造者
        /// </summary>
        /// <returns>查询建造者</returns>
        /// <remarks>适用于拼接And条件</remarks>
        public static QueryBuilder<T> Affirm()
        {
            return new QueryBuilder<T>(x => true);
        }

        /// <summary>
        /// 创建以否定开始查询建造者
        /// </summary>
        /// <returns>查询建造者</returns>
        /// <remarks>适用于拼接Or条件</remarks>
        public static QueryBuilder<T> Negate()
        {
            return new QueryBuilder<T>(x => false);
        }

        #endregion

        #region # 逻辑与运算 —— void And(Expression<Func<T, bool>> condition)
        /// <summary>
        /// 逻辑与运算
        /// </summary>
        /// <param name="condition">条件表达式</param>
        public void And(Expression<Func<T, bool>> condition)
        {
            this._seedCondition = Merge(this._seedCondition, condition, Expression.AndAlso);
        }
        #endregion

        #region # 逻辑或运算 —— void Or(Expression<Func<T, bool>> condition)
        /// <summary>
        /// 逻辑或运算
        /// </summary>
        /// <param name="condition">条件表达式</param>
        public void Or(Expression<Func<T, bool>> condition)
        {
            this._seedCondition = Merge(this._seedCondition, condition, Expression.OrElse);
        }
        #endregion

        #region # 建造条件表达式 —— Expression<Func<T, bool>> Build()
        /// <summary>
        /// 建造条件表达式
        /// </summary>
        /// <returns>条件表达式</returns>
        public Expression<Func<T, bool>> Build()
        {
            return this._seedCondition;
        }
        #endregion


        //Private

        #region # 合并表达式 —— static Expression<TExpression> Merge<TExpression>(...
        /// <summary>
        /// 合并表达式
        /// </summary>
        /// <typeparam name="TExpression">表达式类型</typeparam>
        /// <param name="left">左表达式</param>
        /// <param name="right">右表达式</param>
        /// <param name="operator">操作符</param>
        /// <returns>Lambda表达式</returns>
        private static Expression<TExpression> Merge<TExpression>(Expression<TExpression> left, Expression<TExpression> right, Func<Expression, Expression, Expression> @operator)
        {
            // zip parameters (map from parameters of right to parameters of left)
            IDictionary<ParameterExpression, ParameterExpression> map = left.Parameters
                .Select((expression, index) => new { expression, s = right.Parameters[index] })
                .ToDictionary(p => p.s, p => p.expression);

            // replace parameters in the right lambda expression with the parameters in the left
            ParameterExpressionVisitor parameterExpressionVisitor = new ParameterExpressionVisitor(map);
            Expression rightBody = parameterExpressionVisitor.Visit(right.Body);

            // create a merged lambda expression with parameters from the left expression
            return Expression.Lambda<TExpression>(@operator.Invoke(left.Body, rightBody), left.Parameters);
        }
        #endregion
    }


    /// <summary>
    /// 参数表达式访问者
    /// </summary>
    internal class ParameterExpressionVisitor : ExpressionVisitor
    {
        #region # 字段及构造器

        /// <summary>
        /// 参数表达式映射
        /// </summary>
        private readonly IDictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="map">参数表达式映射</param>
        public ParameterExpressionVisitor(IDictionary<ParameterExpression, ParameterExpression> map)
        {
            this._map = map;
        }

        #endregion

        #region # 访问 —— override Expression VisitParameter(ParameterExpression expression)
        /// <summary>
        /// 访问
        /// </summary>
        /// <param name="expression">参数表达式</param>
        /// <returns>表达式</returns>
        protected override Expression VisitParameter(ParameterExpression expression)
        {
            if (this._map.TryGetValue(expression, out ParameterExpression replacement))
            {
                expression = replacement;
            }

            return base.VisitParameter(expression);
        }
        #endregion
    }
}
