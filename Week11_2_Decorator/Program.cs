namespace Week11_2_Decorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pizza pizzaA = new PanPizza();
            pizzaA = new Cheese(pizzaA);
            //pizzaA = new Cheese(pizzaA);
            pizzaA = new Olives(pizzaA);

            Console.WriteLine(pizzaA.GetOrderDetails());
            Console.WriteLine(pizzaA.GetOrderTotal());

            Pizza pizzaB = new PanPizza();
            pizzaB = new Cheese(pizzaB);

            Console.WriteLine(pizzaB.GetOrderDetails());
            Console.WriteLine(pizzaB.GetOrderTotal());

        }
    }
}