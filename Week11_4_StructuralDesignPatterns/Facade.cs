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
 * Date: 2019-4-4
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Week11_4_StructuralDesignPatterns
{
	/// <summary>
	/// Represents a facade pattern demo.
	/// </summary>
	public class Facade
	{
		public Facade()
		{
			
		}

		// wrapped the functionality of the complex interface and complex implementation
		// into a simpler implementation where, values that can be generated
		// or otherwise determined without external input, can now be accessible to client code
		public void AddPersonSimple(string name, DateTimeOffset dateOfBirth)
		{
			var complexImplementation = new ComplexImplementation();

			complexImplementation.CreatePersonComplex(Guid.NewGuid(), DateTimeOffset.Now, name, dateOfBirth);
		}
	}

	public class FacadeClient
	{
		public void DoWork()
		{
			var facade = new Facade();

			facade.AddPersonSimple("dave", DateTimeOffset.MinValue);
		}
	}

	public interface IComplexInterface
	{
		// creates a person with all available properties
		Person CreatePersonComplex(Guid id, DateTimeOffset creationTime, string name, DateTimeOffset dateOfBirth);
	}

	public class ComplexImplementation : IComplexInterface
	{
		private static readonly List<Person> persons = new List<Person>();

		public Person CreatePersonComplex(Guid id, DateTimeOffset creationTime, string name, DateTimeOffset dateOfBirth)
		{
			var person = new Person(id, creationTime, name, dateOfBirth);

			persons.Add(person);

			return person;
		}
	}

	public class Person
	{
		public Person(Guid id, DateTimeOffset creationTime, string name, DateTimeOffset dateOfBirth)
		{
			this.Id = id;
			this.CreationTime = creationTime;
			this.Name = name;
			this.DateOfBirth = dateOfBirth;
		}

		public Guid Id { get; set; }

		public DateTimeOffset CreationTime { get; set; }

		public string Name { get; set; }

		public DateTimeOffset DateOfBirth { get; set; }
	}
}
