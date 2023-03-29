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
 * Date: 2019-4-3
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Week11_3_ConcurrencyDesignPatterns
{
	/// <summary>
	/// Represents a reactor pattern demo.
	/// </summary>
	public class Reactor
	{
		// holds a list of handlers we want to use to "handle" incoming requests/information
		private List<Handler> handlers = new List<Handler>();

		public Reactor()
		{
			// use reflection to load all the handlers in the assembly
			// then use the select method to invoke the constructor on each handler
			// return the invoked handlers to the add range method
			// and add all the handler to the list
			this.handlers.AddRange(typeof(Reactor).Assembly.DefinedTypes.Where(c => typeof(Handler).IsAssignableFrom(c)
																			&& !c.IsAbstract
																			&& c.IsClass)
			                                      .Select(t => (Handler)Activator.CreateInstance(t)));
		}

		public void Handle(Resource resource)
		{
			// find the first handler
			var handler = this.handlers.FirstOrDefault(c => c.Resource == resource.Name);

			if (handler == null)
			{
				throw new InvalidOperationException("Unable to locate handler for resource");
			}

			handler.Handle(resource);
		}
	}

	// represents our abstract handler, must be implemented by all handlers
	public abstract class Handler
	{
		protected Handler()
		{

		}

		// we defined our resource as abstract
		// so that all derived handler will define the resource they handle
		public abstract string Resource { get; }

		// define an abstract handle method
		// to be implemented by all derived handlers
		// the parameter, is the resource to handle
		public abstract void Handle(Resource resource);
	}

	// patient handler, specific to handling patients
	public class PatientHandler : Handler
	{
		public PatientHandler()
		{
			
		}

		public override string Resource => "Patient";

		public override void Handle(Resource resource)
		{
			Console.WriteLine($"Resource: {resource.Data}");
		}
	}

	public class Resource
	{
		public Priority Priority { get; set; }

		public string Name { get; set; }

		public string Data { get; set; }
	}

	public enum Priority
	{
		High,
		Standard
	}
}
