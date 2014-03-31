using System;
using System.Linq;
using Infrastructure.Framework.Repositories;
using Infrastructure.Framework.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using SQLite.Net;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories.SqliteNet
{
	public abstract class SqliteNetRepositoryBase<TEntity, TRepositoryEntity>: RepositoryBase<TEntity> 
		where TEntity : IAggregateRoot, new()
		where TRepositoryEntity : IEntity, new()
	{
		SQLiteConnection m_connection;

		protected IMapper<TEntity, TRepositoryEntity> Mapper { get; private set; }

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
			return MapperHelper.ToDomainEntities(m_connection.Table<TRepositoryEntity>(), Mapper).AsQueryable()
					.Where(filter.Compile()).Skip(offset).Take(limit);
		}

		public override IEnumerable<TEntity> FindAllAscending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			return MapperHelper.ToDomainEntities(m_connection.Table<TRepositoryEntity>(), Mapper).AsQueryable()
					.Where(filter.Compile()).OrderBy(orderBy.Compile()).Skip(offset).Take(limit);
		}

		public override IEnumerable<TEntity> FindAllDescending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			return MapperHelper.ToDomainEntities(m_connection.Table<TRepositoryEntity>(), Mapper).AsQueryable()
					.Where(filter.Compile()).OrderByDescending(orderBy.Compile()).Skip(offset).Take(limit);
		}

		public override long CountAll(Expression<Func<TEntity, bool>> filter)
		{
			return m_connection.Table<TRepositoryEntity>().LongCount();
		}

		protected override void PersistNewItem(TEntity item)
		{
			item.Key = Guid.NewGuid().ToString(); 
			m_connection.Insert(Mapper.ToRepositoryEntity(item));
		}

		protected override void PersistUpdatedItem(TEntity item)
		{
			m_connection.Update(Mapper.ToRepositoryEntity(item));
		}

		protected override void PersistDeletedItem(TEntity item)
		{
			m_connection.Delete(Mapper.ToRepositoryEntity(item));
		}

		#endregion
	}
}

