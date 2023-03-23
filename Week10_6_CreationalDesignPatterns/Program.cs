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
using System.IO;

namespace Week10_6_CreationalDesignPatterns
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			// create our singleton instance
			Singleton instance = Singleton.Current;

			// create our factory
			Factory factory = new Factory();

			// create a instance of our car with default values
			Car car1 = factory.CreateVehicle<Car>();

			// create an instance of our car with provided values
			Car car2 = factory.CreateVehicle<Car>("Ford", "F150", 2019);

			VehicleDirector vehicleDirector = new VehicleDirector();

			IVehicleBuilder<Car> builder = new CarBuilder();

			builder.AddWheels(4);
			builder.SetEngineSpeed(200);
			builder.SetFuelCapacity(80);

			Car car3 = vehicleDirector.Construct(builder); // build the car using the construct method
			Car car4 = builder.Build(); // builds the car using the build method

			var car5 = new CarBuilder()
						.AddWheels(4)
						.SetEngineSpeed(100)
						.SetFuelCapacity(10)
						.Build();


			Console.WriteLine("Program complete");
			Console.ReadKey();
		}
	}
}
