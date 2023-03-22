using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week10_3_FactoryMethod
{
    // The 'Product' abstract class
    public abstract class IProduct
    {
    }


    // A 'ConcreteProduct' class
    public class ConcreteProductA : IProduct
    {
    }


    // A 'ConcreteProduct' class
    public class ConcreteProductB : IProduct
    {
    }


    // The 'Creator' abstract class
    public abstract class Creator
    {
        public abstract IProduct FactoryMethod();
    }


    // A 'ConcreteCreator' class
    public class ConcreteCreatorA : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProductA();
        }
    }


    // A 'ConcreteCreator' class
    public class ConcreteCreatorB : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProductB();
        }
    }
}
