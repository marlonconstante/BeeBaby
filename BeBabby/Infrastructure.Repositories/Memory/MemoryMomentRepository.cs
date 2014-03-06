﻿using System;
using Domain.Moment;
using Skahal.Infrastructure.Framework.Repositories;

namespace Infrastructure.Repositories.Memory
{
	/// <summary>
	/// IMomentRepository memory implementation.
	/// </summary>
	public class MemoryMomentRepository : MemoryRepository<Moment>, IMomentRepository
	{
		#region Fields
		private static long s_lastKey;
		#endregion

		public MemoryMomentRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork, u =>
			{
				return ++s_lastKey;
			})
		{
			s_lastKey = 0;
		}
	}
}

