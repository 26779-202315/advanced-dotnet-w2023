using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week11_2_Decorator
{
    public abstract class Pizza
    {
        public string OrderDetails { get; set; }
        public abstract string GetOrderDetails();
        public abstract double GetOrderTotal();
    }

    public class PanPizza : Pizza
    {
        public PanPizza()
        {
            OrderDetails = "Pan Pizza";
        }

        public override string GetOrderDetails()
        {
            return OrderDetails;
        }

        public override double GetOrderTotal()
        {
            return 5.0;
        }
    }

    public abstract class PizzaToppingDecorator : Pizza
    {
        public Pizza pizza;

        protected PizzaToppingDecorator(Pizza pizza)
        {
            this.pizza = pizza;
        }

        public abstract override string GetOrderDetails();
        public abstract override double GetOrderTotal();
    }

    public class Cheese : PizzaToppingDecorator
    {

        public Cheese(Pizza pizza) : base(pizza)
        {
        }
        public override string GetOrderDetails()
        {
            return pizza.GetOrderDetails() + " | Add Cheese";

        }

        public override double GetOrderTotal()
        {
            return pizza.GetOrderTotal() + 2.5;
        }
    }

    public class Olives : PizzaToppingDecorator
    {

        public Olives(Pizza pizza) : base(pizza)
        {
        }
        public override string GetOrderDetails()
        {
            return pizza.GetOrderDetails() + " | Add Olives";

        }

        public override double GetOrderTotal()
        {
            return pizza.GetOrderTotal() + .25;
        }
    }

}
