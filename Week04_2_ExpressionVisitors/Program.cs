using System.Linq.Expressions;

namespace Week04_2_ExpressionVisitors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // declare and initialize a new expression that takes 1 string
            // and returns the result as a boolean
            Expression<Func<string, bool>> andAlsoExp = e => e.Length > 10 && e.StartsWith("A");
            Console.WriteLine($"Original Exp.: {andAlsoExp}");

            var result = andAlsoExp.Compile()("Apple");
            Console.WriteLine($"Calling the compiled original AndAlso expression with the word Apple: {result}");

            var andAlsoExpressionVisitor = new AndAlsoExpressionVisitor();
            // Use the visitor to visit the expression nodes to rewrite all the operators as OrElse
            var updatedAndAlsoExp = andAlsoExpressionVisitor.Visit(andAlsoExp);

            Console.WriteLine($"Updated Exp.: {updatedAndAlsoExp}");

            var compiledUpdatedExp = (Expression<Func<string, bool>>)updatedAndAlsoExp;
            var updatedResult = compiledUpdatedExp.Compile()("Apple");
            Console.WriteLine($"Calling the compiled modified OrElse expression with the word Apple: {updatedResult}");

            Console.WriteLine("\n\n");

            // declare and initialize a new expression that takes 3 doubles
            // and returns the result as a double
            // the first 3 arguments are the input parameters
            // and the last argument is the return type
            Expression<Func<double, double, double, double>> mathExp = (a, b, c) => a + b - c;

            Console.WriteLine($"Original Math Exp.: {mathExp}");

            var mathVisitor = new MathExpressionVisitor();

            var updatedMathExp = mathVisitor.Visit(mathExp);
            Console.WriteLine($"Updated Math Exp.: {updatedMathExp}");
        }
    }
}