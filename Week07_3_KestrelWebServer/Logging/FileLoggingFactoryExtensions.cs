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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Week07_3_KestrelWebServer.Logging
{
	/// <summary>
	/// Contains extensions for the <see cref="ILoggingBuilder"/> interface
	/// </summary>
	public static class FileLoggingFactoryExtensions
	{
		/// <summary>
		/// Adds a file logger.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>Returns a logging builder instance.</returns>
		public static ILoggingBuilder AddFile(this ILoggingBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder), "Value cannot be null");
			}

			builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
			return builder;
		}

		/// <summary>
		/// Adds a file logger.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="options">The options.</param>
		/// <returns>Returns a logging builder instance.</returns>
		public static ILoggingBuilder AddFile(this ILoggingBuilder builder, Action<FileLoggerOptions> options)
		{
			builder.AddFile();
			builder.Services.Configure(options);

			return builder;
		}
	}
}