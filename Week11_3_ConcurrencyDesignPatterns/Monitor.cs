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
 * Date: 2019-4-4
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Week11_3_ConcurrencyDesignPatterns
{
	/// <summary>
	/// Represents a monitor pattern demo.
	/// </summary>
	public class MonitorSample
	{
		// acts as a synchronization block/handle that is acquired and released
		// as needed based on access to a critical section of code
		private static readonly object syncLock = new object();

		public void AccessBankAccount()
		{
			// monitor class allows us to acquire a lock and the object
			// we provide to the monitor is the key on which the section of code is locked
			Monitor.Enter(syncLock);

			try
			{
				var bankAccount = new BankAccount();

				bankAccount.Deposit(500);
				bankAccount.Withdraw(250);

				// pulse is used when there is only one other thread to notify
				Monitor.Pulse(syncLock);

				// pulse all is used when there are multiple threads to notify of a change
				// in an objects state
				Monitor.PulseAll(syncLock);

				// waits and blocks the current thread until the operation is completed
				Monitor.Wait(syncLock);
				
				// waits and blocks for a maximum of 5 seconds
				Monitor.Wait(syncLock, TimeSpan.FromSeconds(5));
			}
			finally
			{
				// release the lock on the section of code
				// we use the Monitor.Exit method to do this
				// we provide the key to be released
				Monitor.Exit(syncLock);
			}
		}

		public static void DoCriticalWork()
		{
			lock (syncLock)
			{
				var bankAccount = new BankAccount();

				bankAccount.Deposit(500);
				bankAccount.Withdraw(250);
			}
		}
	}

	public class BankAccount
	{
		public BankAccount()
		{
			
		}

		public double Balance { get; private set; }

		public void Deposit(double amount)
		{
			this.Balance += amount;
		}

		public double Withdraw(double amount)
		{
			if (amount > this.Balance)
			{
				throw new InvalidOperationException($"Cannot withdraw: {amount} dollars, there is not enough money in the account");
			}

			this.Balance -= amount;
			return amount;

		}
	}
}
