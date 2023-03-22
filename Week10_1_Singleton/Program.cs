namespace Week10_1_Singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting 'Singleton' Design Pattern Application.");

            DatabaseConnection A = DatabaseConnection.GetInstance();
            DatabaseConnection B = DatabaseConnection.GetInstance();

            if(A == B)
            {
                Console.WriteLine("it's the same object!");
            }
        }
    }
}