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
using System.Text;

namespace Week10_6_CreationalDesignPatterns
{
	/// <summary>
	/// Represents a singleton pattern demo.
	/// </summary>
	public class Singleton
	{
		// represents the current instance of our singleton class
		// from the perspective of any calling code
		private static Singleton instance;

		// ensure our constructor is private
		// to prevent instantiation of our singleton class
		// outside of the Singleton class
		private Singleton()
		{
			
		}

		// checks if the instance member is null
		// if it is null, then instantiate a new instance of our Singleton class
		// and assign the newly created object to our instance variable
		public static Singleton Current => instance ?? (instance = new Singleton());

		// the above property access and the below method
		// do the same thing
		// they check if the instance variable is null
		// if it is, then instantiate it, assign the result to instance
		// and return the instance variable
		public static Singleton GetInstance()
		{
			if (instance == null)
			{
				instance = new Singleton();
			}

			return instance;
		}
	}
}
