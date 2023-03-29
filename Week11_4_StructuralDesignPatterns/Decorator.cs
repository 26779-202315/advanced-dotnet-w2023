/*
 * Copyright 2016-2019 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: Nityan Khanna
 * Date: 2019-4-4
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Week11_4_StructuralDesignPatterns
{
	public class Client
	{
		private readonly ICoffee coffee;
		private readonly MilkDecorator milkDecorator;

		public void DoWork()
		{
			// depend upon abstractions, not concrete implementations
			// i.e. use ICoffee instead of MilkDecorator in client code
			this.coffee.Make();
			this.milkDecorator.Make();
		}
	}

	public interface ICoffee
	{
		// the cost of the coffee
		double Cost { get; }

		// the description of the coffee
		string Description { get; }


		// our operation to invoke
		void Make();
	}

	/// <summary>
	/// Represents a decorator pattern demo.
	/// </summary>
	// implements our ICoffee interface
	// and contains a reference to a another implementation of the ICoffee interface
	public abstract class Decorator : ICoffee
	{
		private readonly ICoffee coffee;

		protected Decorator(ICoffee coffee)
		{
			this.coffee = coffee;
		}

		// marked as abstract so that derived classes are forced to implement the property
		public abstract double Cost { get; }

		// marked as abstract so that derived classes are forced to implement the property
		public abstract string Description { get; }

		// marked as abstract so that derived classes are forced to implement the method
		public abstract void Make();
	}

	public class MilkDecorator : Decorator
	{
		public MilkDecorator(ICoffee coffee) : base(coffee)
		{
		}

		// set the cost
		public override double Cost => 2.99;

		// set the description
		public override string Description => "Made with milk";


		public override void Make()
		{
			Console.WriteLine("Making coffee with milk");
		}

		// adding a churn method
		// to expose additional functionality
		// to implement the decorator pattern
		public void Churn()
		{
			Console.WriteLine("Churning coffee with milk");
		}
	}

	public class ChocolateDecorator: Decorator
	{
		public ChocolateDecorator(ICoffee coffee) : base(coffee)
		{
		}

		public override double Cost => 4.99;
		public override string Description => "Made with chocolate";

		public override void Make()
		{
			Console.WriteLine("Made with chocolate");
		}

		// add a melt method to
		// expose additional functionality to our decorator
		public void Melt()
		{
			Console.WriteLine("melting coffee with chocolate");
		}
	}
}
