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
			return m_connection.Table<TEntity>().FirstOrDefault(t => t.Key.Equals(key));
		}

		public override IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter)
		{
			return m_connection.Table<TEntity>().Where(filter.Compile()).Skip(offset).Take(limit);
		}

		public override IEnumerable<TEntity> FindAllAscending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			return m_connection.Table<TEntity>().Where(filter.Compile()).OrderBy(orderBy.Compile()).Skip(offset).Take(limit);
		}

		public override IEnumerable<TEntity> FindAllDescending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			return m_connection.Table<TEntity>().Where(filter.Compile()).OrderByDescending(orderBy.Compile()).Skip(offset).Take(limit);
		}

		public override long CountAll(Expression<Func<TEntity, bool>> filter)
		{
			return m_connection.Table<TEntity>().LongCount();
		}

		protected override void PersistNewItem(TEntity item)
		{
			m_connection.Insert(item);
		}

		protected override void PersistUpdatedItem(TEntity item)
		{
			m_connection.Update(item);
		}

		protected override void PersistDeletedItem(TEntity item)
		{
			m_connection.Delete(item);
		}

		#endregion
	}
}

