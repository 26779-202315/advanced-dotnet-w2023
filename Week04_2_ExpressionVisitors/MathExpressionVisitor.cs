using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Week04_2_ExpressionVisitors
{
    /// <summary>
    /// rewrite math expressions, it will convert all math operators to muliplication.
    /// </summary>
    class MathExpressionVisitor : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                    // we can safely cast the expression to a binary expression
                    // because all the above types in our case statement
                    // are all expression types that only support a binary expression
                    return this.VisitBinary((BinaryExpression)node);

                // add the default case so that we can safely
                // ignore any expressions that do not have the above expression types
                default:
                    // always call base.methodname, in our case we are calling base.Visit(node);
                    // otherwise we will enter Infinite Recursion(Loop) 
                    return base.Visit(node);
            }
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:

                    // visit the right side of the binary expression
                    var right = this.Visit(node.Right);

                    // visit the left side of the binary expression
                    var left = this.Visit(node.Left);

                    // return a new binary expression
                    // with new expression type of Multiply
                    return Expression.MakeBinary(ExpressionType.Multiply, left, right);

                // add the default case so that we can safely
                // ignore any expressions that do not have the above expression types
                default:
                    // always call base.methodname, in our case
                    // we are calling base.VisitBinary(node);
                    return base.VisitBinary(node);
            }
        }
    }
}
