﻿/*
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

namespace Week10_7_BehaviouralDesignPatterns
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
			// the successor to our integer handler is the string handler
			// and the successor to our string handler is the default handler
			var integerHandler = new IntegerHandler(new StringHandler(new DefaultHandler()));

			

			// this will be handled by our integer handler
			integerHandler.HandleRequest(5); 

			// this will not be handler by our integer handler
			integerHandler.HandleRequest("this is not an integer");

			Console.WriteLine("Program complete");
			Console.ReadKey();
		}
	}
}
