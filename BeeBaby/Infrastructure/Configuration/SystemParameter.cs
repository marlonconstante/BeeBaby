﻿using System;
using Skahal.Infrastructure.Framework.PCL.Domain;

namespace Infrastructure.Configuration
{
	public class SystemParameter: EntityWithIdBase<string>, IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value
		{
			get;
			set;
		}	
	}
}

