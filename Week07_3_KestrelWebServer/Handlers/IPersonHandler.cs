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
 * Date: 2019-3-7
 */

using Microsoft.AspNetCore.Builder;

namespace Week07_3_KestrelWebServer.Handlers
{
	/// <summary>
	/// Represents a person handler.
	/// </summary>
	public interface IPersonHandler
	{
		/// <summary>
		/// Handles a GET request for a person.
		/// </summary>
		/// <param name="app">The application.</param>
		void HandleGet(IApplicationBuilder app);

		/// <summary>
		/// Handles a POST request for a person.
		/// </summary>
		/// <param name="app">The application.</param>
		void HandlePost(IApplicationBuilder app);
	}
}
