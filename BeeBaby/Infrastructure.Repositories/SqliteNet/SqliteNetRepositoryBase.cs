﻿using System;
using System.Linq;
using Skahal.Infrastructure.Framework.PCL.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using SQLite.Net;
using Infrastructure.Repositories.Commons;
using Skahal.Infrastructure.Framework.PCL.Repositories;
using SQLiteNetExtensions.Extensions;
using Infrastructure.Repositories.SqliteNet.Entities;

namespace Infrastructure.Repositories.SqliteNet
{
	public abstract class SqliteNetRepositoryBase<TEntity, TRepositoryEntity>: RepositoryBase<TEntity> 
		where TEntity : IAggregateRoot, new()
		where TRepositoryEntity : IEntity, new()
	{
		protected SQLiteConnection m_connection;

		protected IMapper<TEntity, TRepositoryEntity> Mapper { get; private set; }

		protected int DataVersion = 0;

		public SqliteNetRepositoryBase(SQLiteConnection connection, IMapper<TEntity, TRepositoryEntity> mapper, IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			m_connection = connection;
			Mapper = mapper;
		}

		#region implemented abstract members of RepositoryBase

		public override TEntity FindBy(object key)
		{
			return Mapper.ToDomainEntity(m_connection.Table<TRepositoryEntity>().FirstOrDefault(t => t.Key.Equals(key)));
		}

		public override IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter)
		{
			var table = LoadChildren();

			return InitializeQuery(MapperHelper.ToDomainEntities(table, Mapper).AsQueryable(), filter)
					.Skip(offset).Take(limit);
		}

		public override IEnumerable<TEntity> FindAllAscending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			var table = LoadChildren();

			return InitializeQuery(MapperHelper.ToDomainEntities(table, Mapper).AsQueryable(), filter)
					.OrderBy(orderBy.Compile()).Skip(offset).Take(limit);
		}

		public override IEnumerable<TEntity> FindAllDescending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			var table = LoadChildren();

			return InitializeQuery(MapperHelper.ToDomainEntities(table, Mapper).AsQueryable(), filter)
					.OrderByDescending(orderBy.Compile()).Skip(offset).Take(limit);
		}

		public override long CountAll(Expression<Func<TEntity, bool>> filter)
		{
			return m_connection.Table<TRepositoryEntity>().LongCount();
		}
			
		protected override void PersistNewItem(TEntity item)
		{
			if (item.Key == null)
			{
				item.Key = Guid.NewGuid().ToString(); 
			}
			m_connection.Insert(Mapper.ToRepositoryEntity(item));
		}

		protected override void PersistUpdatedItem(TEntity item)
		{
			m_connection.UpdateWithChildren(Mapper.ToRepositoryEntity(item));
		}

		protected override void PersistDeletedItem(TEntity item)
		{
			m_connection.Delete(Mapper.ToRepositoryEntity(item));
		}

		IQueryable<TEntity> InitializeQuery(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> filter)
		{
			if (filter != null)
			{
				return entities.Where(e => filter.Compile()(e));
			}

			return entities;
		}


		IList<TRepositoryEntity> LoadChildren()
		{

			var table = m_connection.Table<TRepositoryEntity>().ToList();
			foreach (var item in table)
			{
				m_connection.GetChildren(item);
			}
			return table;
		}
		#endregion
	}
}

