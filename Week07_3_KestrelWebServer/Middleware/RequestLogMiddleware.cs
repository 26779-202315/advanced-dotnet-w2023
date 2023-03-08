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
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Week07_3_KestrelWebServer.Middleware
{
	/// <summary>
	/// Represents a request log middleware.
	/// </summary>
	public class RequestLogMiddleware
	{
		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<RequestLogMiddleware> logger;

		/// <summary>
		/// The request delegate.
		/// </summary>
		private readonly RequestDelegate next;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestLogMiddleware" /> class.
		/// </summary>
		/// <param name="next">The next.</param>
		/// <param name="logger">The logger.</param>
		public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		/// <summary>
		/// Invokes the middleware asynchronously.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>Returns a task.</returns>
		public async Task InvokeAsync(HttpContext context)
		{

			try
			{
				this.logger.LogInformation($"Trace Identifier: {context.TraceIdentifier}");
				this.logger.LogInformation($"Request from {context.Connection.RemoteIpAddress} on port {context.Connection.RemotePort}");
				this.logger.LogDebug($"Content Length: {context.Request.ContentLength}");
				this.logger.LogDebug($"Content Type: {context.Request.ContentType}");
				this.logger.LogDebug($"HTTP Method: {context.Request.Method}");
				this.logger.LogDebug($"Host: {context.Request.Host}");
				this.logger.LogDebug($"HTTPS: {context.Request.IsHttps}");
				this.logger.LogDebug($"Path: {context.Request.Path}");
				this.logger.LogDebug($"Query: {context.Request.QueryString}");
			}
			catch (Exception e)
			{
				this.logger.LogError($"Unable to log request details: {e}");
			}

			await this.next(context);
		}
	}
}
