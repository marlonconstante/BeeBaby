using System;
using Parse;
using Skahal.Infrastructure.Framework.PCL.Domain;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Infrastructure.Parse;

namespace Infrastructure.Systems.Domain
{
	public abstract class ParseShared : ParseObject
	{
		public ParseShared(string className) : base(className)
		{
		}

		public ParseShared(IParseable parseable) : base(string.Concat(parseable.GetType().Name, "Shared"))
		{
			Convert(parseable);
		}

		public ParseShared(ParseObject parseObject) : base(parseObject.ClassName)
		{
			Copy(parseObject);
		}

		/// <summary>
		/// Gets the type of the parseable.
		/// </summary>
		/// <returns>The parseable type.</returns>
		/// <param name="value">Value.</param>
		Type GetParseableType(object value)
		{
			var valueTypeInfo = value.GetType().GetTypeInfo();
			var parseableTypeInfo = typeof(IParseable).GetTypeInfo();
			foreach (var type in valueTypeInfo.ImplementedInterfaces)
			{
				var typeInfo = type.GetTypeInfo();
				if (!parseableTypeInfo.Equals(typeInfo) && parseableTypeInfo.IsAssignableFrom(typeInfo))
				{
					return type;
				}
			}
			return null;
		}

		/// <summary>
		/// Convert the specified parseable.
		/// </summary>
		/// <param name="parseable">Parseable.</param>
		public void Convert(IParseable parseable)
		{
			var type = GetParseableType(this);
			if (type.Equals(GetParseableType(parseable)))
			{
				ObjectId = parseable.ObjectId;
				foreach (var property in type.GetRuntimeProperties())
				{
					var value = property.GetValue(parseable);
					if (value is Enum)
					{
						value = value.ToString();
					}	
					Add(property.Name, value);
				}
			}
			else
			{
				throw new InvalidOperationException(string.Concat("Objeto informado deve implementar a classe \"", type.Name, "\""));
			}
		}

		/// <summary>
		/// Copy the specified parseObject.
		/// </summary>
		/// <param name="parseObject">Parse object.</param>
		public void Copy(ParseObject parseObject)
		{
			if (ClassName.Equals(parseObject.ClassName))
			{
				foreach (var key in parseObject.Keys)
				{
					object value;
					if (parseObject.TryGetValue(key, out value))
					{
						Add(key, value);
					}
				}
			}
			else
			{
				throw new InvalidOperationException(string.Concat("ParseObject informado deve ser da classe \"", ClassName, "\""));
			}
		}

		/// <summary>
		/// Creates the query.
		/// </summary>
		/// <returns>The query.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static ParseQuery<ParseObject> CreateQuery<T>() where T : ParseShared
		{
			return new ParseQuery<ParseObject>(typeof(T).Name);
		}

		/// <summary>
		/// Finds the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static async Task<IEnumerable<T>> FindAsync<T>(ParseQuery<ParseObject> query) where T : ParseShared
		{
			return (await query.FindAsync()).Select((value) => (T) Activator.CreateInstance(typeof(T), value));
		}
	}
}