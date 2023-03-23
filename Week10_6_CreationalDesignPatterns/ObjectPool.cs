using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week10_6_CreationalDesignPatterns
{
	public class ObjectPool
	{
		private static ObjectPool instance;

		private Queue<Worker> availableInstances = new Queue<Worker>();

		private Queue<Worker> usedInstances = new Queue<Worker>();

		private Queue<Worker> waitingInstances = new Queue<Worker>();

		private ObjectPool()
		{

		}

		public static ObjectPool Instance => instance ?? (instance = new ObjectPool());

		public void QueueWorkItem(Func<int, int, int> function)
		{
			// if no available workers, we have to queue up another work item to wait
			// until a worker is available
			if (!this.availableInstances.Any())
			{
				this.waitingInstances.Enqueue(new Worker(function));
			}
			else
			{
				// remove a worker from the queue of available workers
				var workingInstance = this.availableInstances.Dequeue();

				//workingInstance.DoWork();

				workingInstance.Running += (sender, args) =>
				{
					Console.WriteLine("worker is running");
				};

				// add the removed worked from the available queue to the used queue
				this.usedInstances.Enqueue(workingInstance);

				workingInstance.Completed += (sender, args) =>
				{
					// remove the working instance from the used instances
					var innerInstance = this.usedInstances.Dequeue();

					// put the instance that just finished, back on to the available queue of working instances
					this.availableInstances.Enqueue(innerInstance);

				};
			}
		}
	}

	public class Worker
	{
		public EventHandler Running;
		public EventHandler Completed;

		private readonly Func<int, int, int> function;

		public Worker(Func<int, int, int> function)
		{
			// indicate to subscribed classes, that we have started the process
			this.Running?.Invoke(this, EventArgs.Empty);
			this.function = function;
		}

		public void DoWork(int value1, int value2)
		{
			this.function(value1, value2);
			Console.WriteLine("doing work...");

			// indicate to the subscribed classes, that we have completed the process
			this.Completed?.Invoke(this, EventArgs.Empty);
		}
	}
}
