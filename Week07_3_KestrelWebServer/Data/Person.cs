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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week07_3_KestrelWebServer.Data
{
	/// <summary>
	/// Represents a person.
	/// </summary>
	public class Person
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		public Person()
		{
			this.CreationTime = DateTimeOffset.Now;
			this.Id = Guid.NewGuid();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		public Person(string firstName, string lastName) : this()
		{
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>The creation time.</value>
		[Required]
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		[Required]
		[StringLength(128)]
		public string  FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		[Required]
		[StringLength(128)]
		public string LastName { get; set; }
	}
}
