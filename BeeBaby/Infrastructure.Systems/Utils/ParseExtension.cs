using System;
using Parse;
using System.Reflection;
using Skahal.Infrastructure.Framework.PCL.Domain;
using Infrastructure.Parse;

namespace Infrastructure.Systems.Utils
{
	public static class ParseExtension
	{
		/// <summary>
		/// To the domain.
		/// </summary>
		/// <returns>The domain.</returns>
		/// <param name="source">Source.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T ToDomain<T>(this ParseObject source) where T : IParseDomain
		{
			var target = Activator.CreateInstance<T>();
			SetPropertyValue("ObjectId", target, source.ObjectId);
			SetPropertyValue("CreatedAt", target, source.CreatedAt);
			SetPropertyValue("UpdatedAt", target, source.UpdatedAt);
			foreach (var key in source.Keys)
			{
				object value;
				if (source.TryGetValue(key, out value))
				{
					SetPropertyValue(key, target, value);
				}
			}
			return target;
		}

		/// <summary>
		/// To the parse object.
		/// </summary>
		/// <returns>The parse object.</returns>
		/// <param name="source">Source.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T ToParseObject<T>(this object source) where T : ParseObject
		{
			T target = (T) Activator.CreateInstance(typeof(T), source.GetType().Name);
			foreach (var property in source.GetType().GetRuntimeProperties())
			{
				if (property.CanWrite)
				{
					var value = property.GetValue(source);
					var propertyName = property.Name;
					if ("ObjectId".Equals(propertyName))
					{
						target.ObjectId = (string) value;
					}
					else
					{
						target.Add(propertyName, value);
					}
				}
			}
			return target;
		}

		/// <summary>
		/// Set the property value.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="target">Target.</param>
		/// <param name="value">Value.</param>
		static void SetPropertyValue(string name, object target, object value)
		{
			var property = target.GetType().GetRuntimeProperty(name);
			if (property != null)
			{
				property.SetValue(target, value);
			}
		}
	}
}