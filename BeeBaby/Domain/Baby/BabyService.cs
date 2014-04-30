﻿using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using System.Globalization;

namespace Domain.Baby
{
	/// <summary>
	/// Baby service.
	/// </summary>
	public class BabyService: ServiceBase<Baby, IBabyRepository, IUnitOfWork>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Baby.BabyService"/> class.
		/// </summary>
		/// <param name="mainRepository">Main repository.</param>
		/// <param name="unitOfWork">Unit of work.</param>
		public BabyService(IBabyRepository mainRepository, IUnitOfWork unitOfWork)
			: base(mainRepository, unitOfWork)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Domain.Baby.BabyService"/> class.
		/// </summary>
		public BabyService()
		{
		}

		/// <summary>
		/// Saves the baby.
		/// </summary>
		/// <returns>The baby.</returns>
		public Baby SaveBaby(Baby baby)
		{
			MainRepository[baby.Id] = baby;
			UnitOfWork.Commit();

			return baby;
		}

		/// <summary>
		/// Gets the baby.
		/// </summary>
		/// <returns>The baby.</returns>
		public Baby GetBaby()
		{
			return MainRepository.FindFirst();
		}

		/// <summary>
		/// Gets the baby.
		/// </summary>
		/// <returns>The baby.</returns>
		public Baby GetBaby(string id)
		{
			return MainRepository.FindBy(id);
		}
	}
}

