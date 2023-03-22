using static Week10_3_FactoryMethod.Classes;

namespace Week10_3_FactoryMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting 'Factory Method' Design Pattern Application!");

            // An array of creators

            VehicleCreator[] vCreators = new VehicleCreator[2];
            vCreators[0] = new BoatCreator();
            vCreators[1] = new CarCreator();

            // Iterate over creators and create products

            foreach (VehicleCreator vCreator in vCreators)
            {
                IVehicle product = vCreator.GetVehicle();
                Console.WriteLine("Created a new {0}, The mode of trnasport is {1}",
                  product.GetType().Name, product.ModeOfTransport);
            }

            Console.ReadLine();
        }
    }
}