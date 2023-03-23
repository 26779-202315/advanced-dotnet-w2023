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
	public class ComplexObserver : IObservable<Luggage>
	{

		// maintains a list of observers
		private List<IObserver<Luggage>> observers;

		// maintain a list of luggage
		private List<Luggage> flights;

		public ComplexObserver()
		{
			this.observers = new List<IObserver<Luggage>>();
			this.flights = new List<Luggage>();
		}

		public IDisposable Subscribe(IObserver<Luggage> observer)
		{
			// check if the observer is contain within the observers

			// if the observer has not already been added
			// we want to add the observer
			if (!this.observers.Contains(observer))
			{
				// add the observer
				this.observers.Add(observer);

				foreach (var luggage in this.flights)
				{
					// call the next invocation
					// invoke the on next method, to notify the observer of new information
					// and that new information is the object luggage
					observer.OnNext(luggage);
				}
			}

			// return a new instance of the unsubscriber
			// providing the list of observers
			// and the new observer that was added
			return new Unsubscriber<Luggage>(this.observers, observer);
		}
	}

	// represents a class to unsubscribe/unregister and dispose of observers
	// cleanup the list of observers when there is a need to unregister an observer
	public class Unsubscriber<T> : IDisposable
	{
		private List<IObserver<T>> observers;
		private IObserver<T> observer;

		/// <summary>
		/// Initializes a new instance of the <see cref="Unsubscriber" /> class.
		/// </summary>
		/// <param name="observers">The observers.</param>
		/// <param name="observer">The observer.</param>
		internal Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
		{
			this.observers = observers;
			this.observer = observer;
		}

		public void Dispose()
		{
			// if the observer is contained within the list
			// remove the observer
			if (this.observers.Contains(this.observer))
			{
				this.observers.Remove(this.observer);
			}
		}
	}

	// represents our observer
	// this class will receive the updates from the subject
	public class LuggageObserver : IObserver<Luggage>
	{
		// maintains a list of data
		// that we want to handle in our observer class
		private List<Luggage> luggageContent = new List<Luggage>();

		private IDisposable cancellation;

		/// <summary>
		/// Initializes a new instance of the <see cref="LuggageObserver"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public LuggageObserver(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; }

		public void Subscribe(ComplexObserver observer)
		{
			// register a new observer
			this.cancellation = observer.Subscribe(this);
		}

		public void Unsubscribe()
		{
			// call dispose on the observer
			// this cleans up and un-registers the observer
			this.cancellation.Dispose();
			// clear the content list

			this.luggageContent.Clear();
		}

		/// <summary>
		/// Notifies the observer that the provider has finished sending push-based notifications.
		/// </summary>
		public void OnCompleted()
		{
			// clear luggage
			this.luggageContent.Clear();
		}

		/// <summary>
		/// Notifies the observer that the provider has experienced an error condition.
		/// </summary>
		/// <param name="error">An object that provides additional information about the error.</param>
		public void OnError(Exception error)
		{
			// do nothing for now
		}

		/// <summary>
		/// Provides the observer with new data.
		/// </summary>
		/// <param name="value">The current notification information.</param>
		
		public void OnNext(Luggage value)
		{
			Console.WriteLine(value);
		}
	}



	public class Luggage
	{
		public int FlightNumber { get; set; }

		public string Source { get; set; }

		public string Destination { get; set; }
	}
}
