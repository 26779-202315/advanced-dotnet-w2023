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
using System.Runtime.InteropServices;
using System.Text;

namespace Week10_6_CreationalDesignPatterns
{
	/// <summary>
	/// Represents a builder pattern demo.
	/// </summary>
	// represents a generic vehicle builder
	// where the type T, is the type of class or instance to be created by
	// any derived builder
	public interface IVehicleBuilder<out T> where T : Vehicle
	{
		// support the addition of wheels to our vehicle
		IVehicleBuilder<T> AddWheels(int count);

		T Build();
		// Car Build();

		// support the setting of the engine speed
		IVehicleBuilder<T> SetEngineSpeed(double speed);

		// support the setting of the fuel capacity
		IVehicleBuilder<T> SetFuelCapacity(double capacity);
	}

	// create our car builder class, which implements our vehicle builder interface
	// the type of object we are creating using this builder pattern, is a car
	public class CarBuilder : IVehicleBuilder<Car>
	{
		// in our car builder class, we need a member to maintain the current object
		// that our builder is creating
		private readonly Car car;

		public CarBuilder()
		{
			// initialize an instance of our car members
			this.car = new Car();
		}

		public IVehicleBuilder<Car> AddWheels(int count)
		{
			this.car.WheelCount += count;
			return this;
		}

		public Car Build()
		{
			return this.car;
		}

		public IVehicleBuilder<Car> SetEngineSpeed(double speed)
		{
			this.car.EngineSpeed = speed;
			return this;
		}

		public IVehicleBuilder<Car> SetFuelCapacity(double capacity)
		{
			this.car.FuelCapacity = capacity;
			return this;
		}
	}

	public class VehicleDirector
	{
		public T Construct<T>(IVehicleBuilder<T> builder) where T : Vehicle
		{
			return builder.Build();
		}
	}
}
