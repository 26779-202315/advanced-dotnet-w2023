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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Week07_3_KestrelWebServer.Data;
using Week07_3_KestrelWebServer.Handlers;
using Week07_3_KestrelWebServer.Logging;
using Week07_3_KestrelWebServer.Middleware;
using Week07_3_KestrelWebServer.Services;

namespace Week07_3_KestrelWebServer
{
	/// <summary>
	/// Represents application startup.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		/// The hosting environment.
		/// </summary>
		private readonly IHostingEnvironment hostingEnvironment;

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup" /> class.
		/// </summary>
		public Startup(IHostingEnvironment hostingEnvironment)
		{
			// get the working directory
			var workingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			Directory.SetCurrentDirectory(workingDirectory);

			// set the configuration instance
			this.configuration = new ConfigurationBuilder().SetBasePath(workingDirectory)
														   .AddXmlFile($"{workingDirectory}{Path.DirectorySeparatorChar}app.config", false, true)
														   .Build();

			// set the hosting environment
			// this indicates to the runtime what environment the application should run as
			this.hostingEnvironment = hostingEnvironment;

			var environment = this.configuration.GetValue<string>("environment:value")?.ToLowerInvariant();

			switch (environment)
			{
				case "development":
				case "staging":
				case "production":
					this.hostingEnvironment.EnvironmentName = environment;
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(environment), $"Invalid environment: {environment}. Valid values are: Development, Staging, Production");
			}
		}

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		public void Configure(IApplicationBuilder app)
		{
			if (this.hostingEnvironment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else if (this.hostingEnvironment.IsStaging() || this.hostingEnvironment.IsProduction())
			{
				app.UseExceptionHandler("/Error/Index");
			}

			app.UseHsts();

			app.Use(async (context, next) =>
			{
				// accesses the response object
				// and on the event of the response starting
				// we want to set some HTTP headers before our response
				// is sent back to our client application
				context.Response.OnStarting((state) =>
				{
					context.Response.Headers["X-Frame-Options"] = "deny";
					context.Response.Headers["X-Content-Type-Options"] = "nosniff";
					context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
					context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
					context.Response.Headers["Pragma"] = "no-cache";

					return Task.CompletedTask;
				}, context);

				// invoke the next processor in HTTP request-response pipeline
				await next.Invoke();
			});

			// use the request logging middleware to log information about requests
			app.UseMiddleware<RequestLogMiddleware>();

			app.MapWhen(c => c.Request.Path.Value == "/person" && c.Request.Method.ToUpperInvariant() == "GET", app.ApplicationServices.GetService<IPersonHandler>().HandleGet);
			app.MapWhen(c => c.Request.Path.Value == "/person" && c.Request.Method.ToUpperInvariant() == "POST", app.ApplicationServices.GetService<IPersonHandler>().HandlePost);

			// any request where the path of the request
			// is not mapped to any handler
			// the requests will be processed below
			app.Run(async context =>
			{
				// requests which do not match any of the
				// expected 'handlers' will end up being processed here

				// set the content type header, to indicate to the client application
				// how to interpret the response
				context.Response.Headers["Content-Type"] = "text/plain";

				// write the response to the client
				await context.Response.WriteAsync($"{DateTime.Now:o}");
			});
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			// adds various logging providers to our application
			services.AddLogging(logging =>
			{
				logging.ClearProviders();

				// add a console log provider to our application
				logging.AddConsole();

				logging.SetMinimumLevel(Enum.Parse<LogLevel>(this.configuration.GetValue<string>("logging:minimumLevel")));

				// add a file logging provider to our application
				logging.AddFile(options =>
				{
					options.FileLogSwitches = this.configuration.GetSection("logging:switches:switch").GetChildren().Select(c => new FileLogSwitch(c.Key, Enum.Parse<LogLevel>(c.Value)));
					options.FilePath = this.configuration.GetValue<string>("logging:path");
				});
			});

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				//options.UseSqlServer("Data source=.;Initial Catalog=Assignment3Db;")
				options.UseInMemoryDatabase("Week8Db");
			});

			services.AddTransient<IPersonHandler, PersonHandler>();

			// register our services for the application
			// our person service handles all the database interactions for the application
			services.AddTransient<IPersonService, PersonService>();

			// register the HttpContextAccessor that allows us to
			// access the HTTP context instance for every request that our application
			// receives in any class with HttpContext in the constructor
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}
	}
}