using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week11_2_Decorator
{
    public abstract class Component
    {
        public string SomeProperty { get; set; }

        public abstract string SomeMethod();
        public abstract int AnotherMethod();
    }

    public class ConcreteCompnent : Component
    {
        public ConcreteCompnent()
        {
            SomeProperty = " ";
        }

        public override int AnotherMethod()
        {
            throw new NotImplementedException();
        }

        public override string SomeMethod()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Decorator : Component
    {
        protected Component component;
    }

    public class ConcreteDecorator1 : Decorator
    {
        public ConcreteDecorator1(Component component)
        {
            this.component = component;
        }

        public override int AnotherMethod()
        {
            throw new NotImplementedException();
        }

        public override string SomeMethod()
        {
            component.SomeMethod();
            throw new NotImplementedException();
        }
    }

}
