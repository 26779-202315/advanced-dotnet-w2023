using System.Linq.Expressions;

namespace Week01_4_Lambda
{
    class Program
    {
        // declare out delegate with the name Square
        delegate int Square(int i);
        static void Main(string[] args)
        {
            // 'x' in this example, represents the input value of the function
            Square squareDelegate = x => (int)Math.Pow(x, 2);

            // invoke our delegate
            var result = squareDelegate(10);

            // print the result
            Console.WriteLine($"The result is: {result}");

            Func<int, bool> isEvenFunc = x => x % 2 == 0;
            Expression<Func<int, bool>> isEvenExpression = x => x % 2 == 0;

            var funcResult = isEvenFunc(5);
            // isEvenExpression(5);
            var compiledExpression = isEvenExpression.Compile();
            var expressionResult = compiledExpression.Invoke(10);

            Console.WriteLine($"The funcResult is: {funcResult}");

            Console.WriteLine($"The expressionResult is: {expressionResult}");

            // x - is the 'temp' variable that
            // represents the input variable to the function
            // '=>' lambda operator
            // the 'left' side of the lambda operator is at least one or more
            // input variables to the function
            // the 'right' side of the lambda operator is the actual body
            // of the function to execute
            Expression<Func<double, bool>> isOddExpression = x => x % 2 != 0;

            // this expression has 2 parameters
            // the first in an integer
            // the second is an integer
            // the return type is bool
            // the return type of a Func<..> is always the type of the last listed parameter
            Expression<Func<int, int, bool>> multiInputExpression = (x, y) => x > 5 && y > 5;

            // this is a no input expression
            // the return type the first type of the list of parameters
            Expression<Func<bool>> noInputExpression = () => true;

            // using the var keyword allows us to infer the type
            // without the explicit declaration
            var myString = "test";

            // the following line will result in an error because
            // it's impossible for the compiler to determine the type of
            // our variable 'unknownData'
            //var unknownData = null;

            // an async lambda cannot be an Expression
            // because the value of the func, must be re-evaluated each time
            // the function is run
            Func<Task<bool>> asyncLambda = async () =>
            {
                await Task.Delay(2000);
                //Console.WriteLine("after 2 seconds of delay");
                return true;
            };
        }
    }

}