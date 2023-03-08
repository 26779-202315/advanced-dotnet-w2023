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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Week07_3_KestrelWebServer.Data;
using Week07_3_KestrelWebServer.Services;
using Week07_3_KestrelWebServer.Shared;

namespace Week07_3_KestrelWebServer.Handlers
{
	/// <summary>
	/// Represents a person handler.
	/// </summary>
	public class PersonHandler : IPersonHandler
	{
		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<PersonHandler> logger;

		/// <summary>
		/// The person service.
		/// </summary>
		private readonly IPersonService personService;

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonHandler" /> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="personService">The person service.</param>
		public PersonHandler(ILogger<PersonHandler> logger, IPersonService personService)
		{
			this.logger = logger;
			this.personService = personService;
		}

		/// <summary>
		/// Handles a GET request for a person.
		/// </summary>
		/// <param name="app">The application.</param>
		public void HandleGet(IApplicationBuilder app)
		{
			this.logger.LogInformation("Handling GET request for person");

			app.Run(async context =>
			{
				// attempt to retrieve the name parameter from our query string
				context.Request.Query.TryGetValue("name", out var values);

				// set the name
				var name = values.FirstOrDefault();

				// query the database for our person with a name of the name provided in the GET request query string
				var resultTask = personService.QueryPersonAsync(
					c => c.FirstName.ToLowerInvariant() == name.ToLowerInvariant()
						|| c.LastName.ToLowerInvariant() == name.ToLowerInvariant());

				// serialize the results returned, and write the result to the response
				var serializer = new XmlSerializer(typeof(List<PersonViewModel>));
				var memoryStream = new MemoryStream();

				// set the content type
				context.Response.ContentType = "application/xml";

				var results = await resultTask;

				serializer.Serialize(memoryStream, results.Select(c => new PersonViewModel(c.CreationTime, c.Id, c.FirstName, c.LastName)).ToList());

				// writing the response to the client
				await context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));

				// no other code, except for maybe logging would
				// be written after you write the response to the client
			});
		}

		/// <summary>
		/// Handles a POST request for a person.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void HandlePost(IApplicationBuilder app)
		{
			this.logger.LogInformation("Handling POST request for person");

			app.Run(async context =>
			{
				var serializer = new XmlSerializer(typeof(PersonCreateModel));

				var model = (PersonCreateModel)serializer.Deserialize(context.Request.Body);

				var createTask = this.personService.CreatePersonAsync(model.FirstName, model.LastName);

				// create the memory stream to hold the serialized person instance
				var memoryStream = new MemoryStream();

				// set the content type
				context.Response.ContentType = "application/xml";

				serializer = new XmlSerializer(typeof(PersonViewModel));

				var result = await createTask;

				var personViewModel = new PersonViewModel(result.CreationTime, result.Id, result.FirstName, result.LastName);

				serializer.Serialize(memoryStream, personViewModel);

				// writing the response to the client
				await context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));

				// no other code, except for maybe logging would
				// be written after you write the response to the client
			});
		}
	}
}