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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Week07_3_KestrelWebServer.Data;

namespace Week07_3_KestrelWebServer.Services
{
	/// <summary>
	/// Represents a person service.
	/// </summary>
	/// <seealso cref="IPersonService" />
	public class PersonService :  IPersonService
	{
		/// <summary>
		/// The context.
		/// </summary>
		private readonly ApplicationDbContext context;

		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<PersonService> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonService" /> class.
		/// </summary>
		public PersonService(ApplicationDbContext context, ILogger<PersonService> logger)
		{
			this.context = context;
			this.logger = logger;
		}

		/// <summary>
		/// Creates a person asynchronously.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <returns>Returns a task.</returns>
		public async Task<Person> CreatePersonAsync(string firstName, string lastName)
		{
			// create our person object to be saved to the database
			var person = new Person(firstName, lastName);

			// start the process of adding our created person instance to the entity context
			// the entity context being, a object or objects to be saved to the database at a later point in time
			var addTask = this.context.Persons.AddAsync(person);

			this.logger.LogInformation($"adding person {firstName}, {lastName} to entity context...");

			// await our add task to completion
			await addTask;

			// start the process of saving the changes to the database
			var saveTask = this.context.SaveChangesAsync();

			this.logger.LogInformation("saving changes...");

			// await our save task to completion
			await saveTask;

			// return the created person
			return person;
		}

		/// <summary>
		/// Queries for a person asynchronously.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <returns>Returns a list of persons which match the given predicate.</returns>
		public async Task<List<Person>> QueryPersonAsync(Expression<Func<Person, bool>> expression)
		{
			// query the database
			var results = this.context.Persons.Where(expression);

			// start the process of processing the results from the database
			// using the ToListAsync method
			var queryTask = results.OrderByDescending(c =>c.CreationTime).ToListAsync();

			this.logger.LogInformation("querying persons...");

			// await the task to completion
			return await queryTask;
		}
	}
}