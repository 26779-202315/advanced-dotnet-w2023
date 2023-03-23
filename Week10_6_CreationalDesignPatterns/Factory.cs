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
 * Date: 2019-3-27
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Week10_6_CreationalDesignPatterns
{
	/// <summary>
	/// Represents a factory pattern demo.
	/// </summary>
	public class Factory
	{

		// our generic method, allows us to create a vehicle of any type
		// and return the created object
		public T CreateVehicle<T>() where T : Vehicle
		{
			var type = typeof(Vehicle).Assembly
									.DefinedTypes
									.FirstOrDefault(c => typeof(Vehicle).IsAssignableFrom(typeof(T)));


			if (type == null)
			{
				throw new InvalidOperationException($"Unable to locate type: {typeof(T).AssemblyQualifiedName}");
			}

			var constructor = type.GetConstructor(Type.EmptyTypes);

			if (constructor == null)
			{
				throw new InvalidOperationException($"Type {typeof(T).AssemblyQualifiedName} does not have a public default constructor");
			}

			// invoke the default constructor on the given object
			return (T)constructor.Invoke(null);
		}

		public T CreateVehicle<T>(string make, string model, int year) where T : Vehicle
		{
			var type = typeof(Vehicle).Assembly
									.DefinedTypes
									.FirstOrDefault(c => typeof(Vehicle).IsAssignableFrom(typeof(T)));

			if (type == null)
			{
				throw new InvalidOperationException($"Unable to locate type: {typeof(T).AssemblyQualifiedName}");
			}

			var constructor = type.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(int) });

			if (constructor == null)
			{
				throw new InvalidOperationException($"Type {typeof(T).AssemblyQualifiedName} does not have a constructor matching the given parameters");
			}

			// invoke the parameterized constructor on the given object, using the provided parameters
			return (T)constructor.Invoke(new object[] { make, model, year });
		}
	}

	public class Vehicle
	{
		public Vehicle()
		{
		}

		public Vehicle(string make, string model, int year)
		{
			this.Make = make;
			this.Model = model;
			this.Year = year;
		}

		public string Make { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public int WheelCount { get; set; }
		public double FuelCapacity { get; set; }

		public double EngineSpeed { get; set; }
	}

	public class Car : Vehicle
	{
		public Car()
		{
			
		}

		public Car(string make, string model, int year) : base(make, model, year)
		{
		}
	}
}
