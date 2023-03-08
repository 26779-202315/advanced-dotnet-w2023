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
 * Date: 2019-3-3
 */
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;

namespace Week07_3_KestrelWebServer.Logging
{
	/// <summary>
	/// Represents a file logger.
	/// </summary>
	/// <seealso cref="Microsoft.Extensions.Logging.ILogger" />
	public class FileLogger : ILogger
	{
		/// <summary>
		/// The category name.
		/// </summary>
		private readonly string categoryName;

		/// <summary>
		/// The provider.
		/// </summary>
		private readonly FileLoggerProvider provider;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileLogger"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="categoryName">Name of the category.</param>
		public FileLogger(FileLoggerProvider provider, string categoryName)
		{
			this.provider = provider;
			this.categoryName = categoryName;
		}

		/// <summary>
		/// Begins a logical operation scope.
		/// </summary>
		/// <typeparam name="TState">The type of the t state.</typeparam>
		/// <param name="state">The identifier for the scope.</param>
		/// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		/// <summary>
		/// Checks if the given <paramref name="logLevel" /> is enabled.
		/// </summary>
		/// <param name="logLevel">level to be checked.</param>
		/// <returns><c>true</c> if enabled.</returns>
		public bool IsEnabled(LogLevel logLevel)
		{
			return this.provider.Options.Value.FileLogSwitches?.Any(c => this.categoryName.ToLowerInvariant().Contains(c.Category.ToLowerInvariant()) && logLevel >= c.LogLevel && c.LogLevel != LogLevel.None) == true || this.categoryName == "Default";
		}

		/// <summary>
		/// Writes a log entry.
		/// </summary>
		/// <typeparam name="TState">The type of the t state.</typeparam>
		/// <param name="logLevel">Entry will be written on this level.</param>
		/// <param name="eventId">Id of the event.</param>
		/// <param name="state">The entry to be written. Can be also an object.</param>
		/// <param name="exception">The exception related to this entry.</param>
		/// <param name="formatter">Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!this.IsEnabled(logLevel))
			{
				return;
			}

			var builder = new StringBuilder();

			builder.Append($"{logLevel} ");
			builder.Append($"{this.categoryName} ");
			builder.AppendLine(formatter(state, exception));

			if (exception != null)
			{
				builder.AppendLine(exception.ToString());
			}

			this.provider.Write(builder.ToString());
		}
	}
}