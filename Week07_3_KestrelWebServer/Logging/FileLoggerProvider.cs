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
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;

namespace Week07_3_KestrelWebServer.Logging
{
	/// <summary>
	/// Represents a file logger provider.
	/// </summary>
	/// <seealso cref="Microsoft.Extensions.Logging.ILoggerProvider" />
	[ProviderAlias("File")]
	public class FileLoggerProvider : ILoggerProvider
	{
		/// <summary>
		/// The synchronize lock.
		/// </summary>
		private readonly object syncLock = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="FileLoggerProvider"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		public FileLoggerProvider(IOptions<FileLoggerOptions> options)
		{
			this.Options = options;
		}

		/// <summary>
		/// Gets the options.
		/// </summary>
		/// <value>The options.</value>
		public IOptions<FileLoggerOptions> Options { get; }

		/// <summary>
		/// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
		/// </summary>
		/// <param name="categoryName">The category name for messages produced by the logger.</param>
		/// <returns>ILogger.</returns>
		public ILogger CreateLogger(string categoryName)
		{
			return new FileLogger(this, categoryName);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
		}

		/// <summary>
		/// Writes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Write(string value)
		{
			lock (this.syncLock)
			{
				using (var fileStream = File.Open(this.Options.Value.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
				{
					fileStream.Seek(0, SeekOrigin.End);

					using (var streamWriter = new StreamWriter(fileStream))
					{
						streamWriter.Write($"{DateTime.Now} [@{Thread.CurrentThread.ManagedThreadId}] : {value}");
					}
				}
			}
		}
	}
}