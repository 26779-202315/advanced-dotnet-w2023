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
using System.Xml.Serialization;

namespace Week07_3_KestrelWebServer.Shared
{
	/// <summary>
	/// Represents a person view model.
	/// </summary>
	[XmlRoot("Person", Namespace = "http://advprogrammingdotnet.mohawkcollege.ca/model")]
	[XmlType("Person", Namespace = "http://advprogrammingdotnet.mohawkcollege.ca/model")]
	public class PersonViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonViewModel" /> class.
		/// </summary>
		public PersonViewModel()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonViewModel" /> class.
		/// </summary>
		/// <param name="creationTime">The creation time.</param>
		/// <param name="id">The identifier.</param>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		public PersonViewModel(DateTimeOffset creationTime, Guid id, string firstName, string lastName)
		{
			this.CreationTime = creationTime;
			this.Id = id;
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>The creation time.</value>
		[XmlIgnore]
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[XmlElement]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the creation time XML.
		/// </summary>
		/// <value>The creation time XML.</value>
		[XmlElement("CreationTime")]
		public string CreationTimeXml
		{
			get => this.CreationTime.ToString("o");
			set => this.CreationTime = DateTimeOffset.Parse(value);
		}

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		[XmlElement]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		[XmlElement]
		public string LastName { get; set; }
	}
}
