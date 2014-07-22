using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace Infrastructure.Configuration
{
	public static class SystemParameterService
	{
//		static IEnumerable<SystemParameter> s_parameters = new List<SystemParameter>();
		static ISystemParameterRepository s_repository;
		static IUnitOfWork s_unityOfWork;

		const string KeyCurrentCultureName = "CurrentCultureName";

		public static void Initialize(ISystemParameterRepository repository, IUnitOfWork unityOfWork)
		{
			s_repository = repository;
			s_unityOfWork = unityOfWork;
		}


		/// <summary>
		/// Determines if has current culture changed the specified cultureName.
		/// </summary>
		/// <returns><c>true</c> if has current culture changed the specified cultureName; otherwise, <c>false</c>.</returns>
		/// <param name="cultureName">Culture name.</param>
		public static bool HasCurrentCultureChanged(string cultureName)
		{
			var savedCultureName  = s_repository.FindFirst(p => p.Name.Equals(KeyCurrentCultureName));

			if (savedCultureName == null)
			{
				SaveParameter(KeyCurrentCultureName, cultureName);
				return true;
			}

			return !savedCultureName.Value.Equals(cultureName);
		}

		public static void SaveEntityVersion(string entityName, string version)
		{
			SaveParameter(entityName, version);
		}

		public static bool IsDataStructureUpdate(string entityName, int dataVersion)
		{
			return s_repository.CountAll(v => v.Name.Equals(entityName) && Convert.ToInt32(v.Value) >= dataVersion) > 0;
		}

		static void SaveParameter(string entityName, string version)
		{
			var item = new SystemParameter {
				Name = entityName,
				Value = version
			};
			s_repository[item.Id] = item;

			s_unityOfWork.Commit();
		}
	}
}

