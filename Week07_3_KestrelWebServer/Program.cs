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
 * Date: 2019-2-23
 */
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Week07_3_KestrelWebServer.Services;

namespace Week07_3_KestrelWebServer
{
	/// <summary>
	/// Represents the main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The working directory.
		/// </summary>
		private static string workingDirectory;

		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static async Task Main(string[] args)
		{
			workingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			Directory.SetCurrentDirectory(workingDirectory);

			IWebHost builder = CreateWebHostBuilder(args).Build();

			var entryAssembly = Assembly.GetEntryAssembly();

			var logger = builder.Services.GetService<ILogger<Program>>();

			logger.LogInformation($"Week 8 Demo: v{entryAssembly.GetName().Version}");
			logger.LogInformation($"Week 8 Demo Working Directory : {Path.GetDirectoryName(entryAssembly.Location)}");
			logger.LogInformation($"Operating System: {Environment.OSVersion.Platform} {Environment.OSVersion.VersionString}");
			logger.LogInformation($"CLI Version: {Environment.Version}");
			logger.LogInformation($"{entryAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright}");

			var personService = builder.Services.GetService<IPersonService>();

			// add a demo person
			await personService.CreatePersonAsync("test", "last name");

			// actually start the server implementation
			await builder.RunAsync();
		}

		/// <summary>
		/// Creates the web host builder.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns>Returns the web host builder.</returns>
		private static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var builder = WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureAppConfiguration(config =>
				{
					config.SetBasePath(workingDirectory);
					config.AddXmlFile($"{workingDirectory}{Path.DirectorySeparatorChar}app.config", false, true);
				})
				.ConfigureKestrel((context, options) =>
				{
					// indicate to kestrel that we want our server to listen 
					// on ALL available IP addresses on our computer
					// using port 19040
					options.Listen(IPAddress.Any, 19040);
				});

			return builder;
		}
	}
}