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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week07_3_KestrelWebServer.Logging
{
	/// <summary>
	/// Represents file log options.
	/// </summary>
	public class FileLoggerOptions
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FileLoggerOptions" /> class.
		/// </summary>
		public FileLoggerOptions() : this("system.log", new FileLogSwitch("Default", LogLevel.Trace))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileLoggerOptions" /> class.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="fileLogSwitches">The file log switches.</param>
		public FileLoggerOptions(string filePath, params FileLogSwitch[] fileLogSwitches)
		{
			this.FileLogSwitches = fileLogSwitches.ToList();
			this.FilePath = filePath;

			if (!Path.IsPathRooted(filePath))
			{
				this.FilePath = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), Path.GetFileName(filePath));
			}
		}

		/// <summary>
		/// Gets the categories.
		/// </summary>
		/// <value>The categories.</value>
		public IEnumerable<FileLogSwitch> FileLogSwitches { get; set; }

		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName => Path.Combine(Path.GetDirectoryName(this.FilePath), Path.GetFileNameWithoutExtension(this.FilePath) + "_" + DateTime.Today.ToString("yyyyMMdd") + Path.GetExtension(this.FilePath));

		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>The file path.</value>
		public string FilePath { get; set; }
	}
}