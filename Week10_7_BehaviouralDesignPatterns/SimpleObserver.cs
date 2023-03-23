using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week10_7_BehaviouralDesignPatterns
{
	public class Subject
	{
		private readonly List<SimpleObserver> observers;

		public Subject()
		{
			// maintain a list of observers
			this.observers = new List<SimpleObserver>();
		}

		public void AddObserver(SimpleObserver observer)
		{
			// only add the observer
			// if there is no observer already added with the given name
			if (this.observers.All(c => c.Name != observer.Name))
			{
				this.observers.Add(observer);
			}
		}

		public void RemoveObserver(string name)
		{
			// remove all the observers from the list
			// where the name of the observer matches the name provided
			this.observers.RemoveAll(c => c.Name == name);
		}

		public void NotifyAll()
		{
			// for each observer, invoke the notify method
			// on the given observer
			foreach (var simpleObserver in this.observers)
			{
				simpleObserver.Notify();
			}
		}
	}

	public abstract class SimpleObserver
	{
		protected SimpleObserver(string name)
		{
			this.Name = name;
		}

		// the name allows us to provide a unique way of identifying
		// the observer
		public string Name { get; }

		// provide a notify method,
		// so that the subject can notify all the observers
		// and that the sub-class observers are implemented
		public abstract void Notify();
	}

	public class SimpleObserverA : SimpleObserver
	{
		public SimpleObserverA(string name) : base(name)
		{
		}

		public override void Notify()
		{
			// called when the notify method on the super class observer is called
			Console.WriteLine("I am observer A");
		}
	}

	public class SimpleObserverB : SimpleObserver
	{
		public SimpleObserverB(string name) : base(name)
		{
		}

		public override void Notify()
		{
			// called when the notify method on the super class observer is called
			Console.WriteLine("I am observer B");
		}
	}

	public class SimpleObserverC : SimpleObserver
	{
		public SimpleObserverC(string name) : base(name)
		{
		}

		public override void Notify()
		{
			// called when the notify method on the super class observer is called
			Console.WriteLine("I am observer C");
		}
	}


}
