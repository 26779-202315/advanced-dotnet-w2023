using System;
using System.Linq.Expressions;

namespace Week04_1_Expressions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConstantExpressions();
            BinaryExpressions();
        }

        private static void BinaryExpressions()
        {
            // declare and initialize the left parameter expression, represented as the variable x
            ParameterExpression leftParameterExpression = Expression.Parameter(typeof(int), "x");

            // declare and initialize the right parameter expression, represented as the variable y
            ParameterExpression rightParameterExpression = Expression.Parameter(typeof(int), "y");

            // combine the two parameter expressions into a binary expression with NodeType set to ExpressionType.Multiply 
            BinaryExpression binaryExpression = Expression.Multiply(leftParameterExpression, rightParameterExpression);

            //It is "roughly" the same like this
            //var binaryExpression = Expression.MakeBinary(ExpressionType.Multiply,leftParameterExpression, rightParameterExpression);


            // create a lambda expression using the binary expression, the left parameter expression, and the right parameter expression
            // passing 'leftParameterExpression' and 'rightParameterExpression' allows us to actually input values into our function
            Expression<Func<int, int, int>> lambdaExpression = Expression.Lambda<Func<int, int, int>>(binaryExpression, leftParameterExpression, rightParameterExpression);

            //We can use the compiled expression directly as we have declared the types when building the expression
            var result = lambdaExpression.Compile()(7, 6);

            //If types are not known at compile time, we can use Expression.Lambda() instead
            LambdaExpression lambdaExpression2 = Expression.Lambda(binaryExpression, leftParameterExpression, rightParameterExpression);


            // In this case we needto  use the DynamicInvoke() on the compiled expression.This will not check for any parameter type at compile time; might throw an error at run time if the wrong argument types are passed in.
            var result2 = lambdaExpression2.Compile().DynamicInvoke(7, 6);

            // print the result
            Console.WriteLine($"The expression represented as a string: {lambdaExpression}");

            Console.WriteLine($"The result with Compile(): {result}");
            Console.WriteLine($"The result with DynamicInvoke(): {result2}");

            // the below line is equivalent to the above code
            Expression<Func<int, int, int>> multiplyExpression = (x, y) => x * y;
            // compile and invoke the expression
            Console.WriteLine(multiplyExpression.Compile().Invoke(7, 6));

            //Expression<Func<int, int, int>> multiplyExpression = (x, y) => x * y;
        }

        private static void ConstantExpressions()
        {
            // declare and initialize a constant expression
            // with the value "my value"
            ConstantExpression constantExpression = Expression.Constant("my value");

            // constant expressions can also hold integer values
            ConstantExpression constantIntegerExpression = Expression.Constant(5);

            // as well as null
            ConstantExpression constantNullExpression = Expression.Constant(null);

            // each of the constants expressions below
            // represent a single leaf in a tree structure
            var seven = Expression.Constant(7);
            var six = Expression.Constant(6);
            var three = Expression.Constant(3);

            Console.WriteLine(constantExpression);
            Console.WriteLine(constantIntegerExpression);
            Console.WriteLine(constantNullExpression);
            Console.WriteLine(seven);
            Console.WriteLine(six);
            Console.WriteLine(three);
        }
    }
}