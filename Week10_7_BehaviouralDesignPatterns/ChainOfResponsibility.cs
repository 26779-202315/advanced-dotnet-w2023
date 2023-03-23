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
 * Date: 2019-3-28
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Week10_7_BehaviouralDesignPatterns
{
	// TODO: show handler pattern for datatypes, int, string, double, bool
	// create an interface to represent a generic handler
	// contains a method to actually process the request at hand
	// contains a property that is used as a reference to point to the next handler in the chain
	// of handlers
	public interface IHandler
	{
		// handles any request
		void HandleRequest(object request);

		// indicates the successor handler in the chain
		IHandler Successor { get; set; }
	}

	// represents out abstract handler
	// all handlers will derive from this handler
	public abstract class Handler : IHandler
	{
		// we have our default constructor here,
		// so that, we can have a handler which has no successor
		protected Handler()
		{
		}

		// this constructor allows us to specify the next handler in the chain of handlers
		// during the creation of the handler instance
		protected Handler(IHandler successor)
		{
			this.Successor = successor;
		}

		public abstract void HandleRequest(object request);

		public IHandler Successor { get; set; }
	}

	public class IntegerHandler : Handler
	{
		public IntegerHandler()
		{
			
		}

		public IntegerHandler(IHandler successor) : base(successor)
		{
		}

		public override void HandleRequest(object request)
		{
			if (request is int)
			{
				Console.WriteLine($"Value: {Convert.ToInt32(request)}");
			}
			else
			{
				this.Successor.HandleRequest(request);
			}
		}
	}

	public class StringHandler : Handler
	{
		public StringHandler()
		{

		}

		public StringHandler(IHandler successor) : base(successor)
		{
		}


		public override void HandleRequest(object request)
		{
			if (request is string)
			{
				Console.WriteLine($"Value: {Convert.ToString(request)}");
			}
			else
			{
				this.Successor.HandleRequest(request);
			}
		}
	}

	public class DefaultHandler : Handler
	{
		public override void HandleRequest(object request)
		{
			throw new InvalidOperationException("This is the default handler, and does nothing, add more handlers to implement additional logic");
		}
	}
}
