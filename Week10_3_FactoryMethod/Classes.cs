using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week10_3_FactoryMethod
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


        // The 'Creator' abstract class
        public abstract class VehicleCreator
        {
            public abstract IVehicle GetVehicle();
        }


        // A 'ConcreteCreator' class
        public class CarCreator : VehicleCreator
        {
            public override IVehicle GetVehicle()
            {
                return new Car();
            }
        }


        // A 'ConcreteCreator' class
        public class BoatCreator : VehicleCreator
        {
            public override IVehicle GetVehicle()
            {
                return new Boat();
            }
        }
    }

}
