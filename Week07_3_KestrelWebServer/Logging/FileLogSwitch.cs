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

namespace Week07_3_KestrelWebServer.Logging
{
	/// <summary>
	/// Represents a file log switch.
	/// </summary>
	public class FileLogSwitch
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FileLogSwitch"/> class.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <param name="logLevel">The log level.</param>
		public FileLogSwitch(string category, LogLevel logLevel)
		{
			this.Category = category;
			this.LogLevel = logLevel;
		}

		/// <summary>
		/// Gets the category.
		/// </summary>
		/// <value>The category.</value>
		public string Category { get; }

		/// <summary>
		/// Gets the log level.
		/// </summary>
		/// <value>The log level.</value>
		public LogLevel LogLevel { get; }
	}
}