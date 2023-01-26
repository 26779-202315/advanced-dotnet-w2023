using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Week04_2_ExpressionVisitors
{
    /// <summary>
    /// Rewrite an expression tree replacing case  with OrElse
    /// </summary>
    class AndAlsoExpressionVisitor : ExpressionVisitor
    {

        /// <summary>
        /// Dispatches the expression to one of the more specialized visit methods in this class.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        public override Expression Visit(Expression node)
        {
            // if the node is null we want to exit early
            if (node == null)
            {
                return null;
            }

            // we only want to visit binary when the node type is AndAlso
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:
                    // cast the expression to a binary expression
                    return this.VisitBinary((BinaryExpression)node);
                default:
                    return base.Visit(node);
            }
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression"></see>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.AndAlso)
            {
                return base.VisitBinary(node);
            }

            // visit the left side of the tree
            var left = this.Visit(node.Left);

            // visit the right side of the tree
            var right = this.Visit(node.Right);

            if (left == null || right == null)
            {
                throw new InvalidOperationException("Unable to make a binary expression from a null node");
            }

            return Expression.MakeBinary(ExpressionType.OrElse, left, right, node.IsLiftedToNull, node.Method);
        }

    }
}
