using System.Collections.Generic;
using System.Linq.Expressions;

namespace SD.Toolkits.EntityFrameworkCore.Extensions
{
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
