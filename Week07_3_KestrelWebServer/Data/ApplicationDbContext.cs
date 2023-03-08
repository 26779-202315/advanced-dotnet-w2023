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
using Microsoft.EntityFrameworkCore;

namespace Week07_3_KestrelWebServer.Data
{
	/// <summary>
	/// Represents the database context for the application.
	/// </summary>
	/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
	public class ApplicationDbContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
		/// </summary>
		/// <param name="options">The options for this context.</param>
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		/// Gets or sets the persons.
		/// </summary>
		/// <value>The persons.</value>
		public DbSet<Person> Persons { get; set; }
	}
}