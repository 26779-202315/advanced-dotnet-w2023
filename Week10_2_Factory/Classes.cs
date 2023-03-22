using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week10_2_Factory
{
    class Classes
    {
        // The 'Product' abstract class
        public abstract class IVehicle
        {
            public string ModeOfTransport { get; set; } //Air, Water, Land
        }


        // A 'ConcreteProduct' class
        public class Car : IVehicle
        {
            public Car()
            {
                ModeOfTransport = "Land";
            }
        }


        // A 'ConcreteProduct' class
        public class Boat : IVehicle
        {
            public Boat()
            {
                ModeOfTransport = "Water";
            }
        }


        // The 'Creator' class
        public class Creator
        {
            public IVehicle GetVehicle(string type)
            {
                switch (type)
                {
                    case "Car":
                        return new Car();
                    case "Boat":
                        return new Boat();
                    default:
                        throw new NotSupportedException();
                }

            }

        }
    }

}
