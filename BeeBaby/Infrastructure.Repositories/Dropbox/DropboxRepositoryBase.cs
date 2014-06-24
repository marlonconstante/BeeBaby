using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BobbyTables;
using HelperSharp;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using Infrastructure.Repositories.Commons;
using System.Collections;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Dropbox
{
	/// <summary>
	/// Repository using Dropbox.
	/// </summary>
	public class DropboxRepositoryBase<TEntity, TRepositoryEntity>: RepositoryBase<TEntity> 
		where TEntity : IAggregateRoot, new()
		where TRepositoryEntity : class, IEntity, new()
	{
		protected IMapper<TEntity, TRepositoryEntity> Mapper { get; private set; }


		#region Fields

		private string m_datastoreId;
		private Datastore m_ds;
		private Table<TRepositoryEntity> m_table;
		private bool m_initialized;

		#endregion

		#region Constructors

		public DropboxRepositoryBase(string datastoreId, IMapper<TEntity, TRepositoryEntity> mapper)
		{
			ExceptionHelper.ThrowIfNull("datastoreId", datastoreId);

			m_datastoreId = datastoreId;
			Mapper = mapper;
		}

		#endregion

		public override long CountAll(Expression<Func<TEntity, bool>> filter)
		{
			Initialize();

			if (filter == null)
			{
				return m_table.Count();
			}
			else
			{
				return MapperHelper.ToDomainEntities(m_table, Mapper).Count(filter.Compile());
			}
		}

		public override IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter)
		{
			Initialize();

			if (filter == null)
			{
				return MapperHelper.ToDomainEntities(m_table, Mapper)
                    .Skip(offset)
                    .Take(limit);
			}
			else
			{
				return MapperHelper.ToDomainEntities(m_table, Mapper)
                    .Where(filter.Compile())
                    .Skip(offset)
                    .Take(limit);
			}
		}

		public override IEnumerable<TEntity> FindAllAscending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			Initialize();
			return FindAll(offset, limit, filter).OrderBy(orderBy.Compile());
		}

		public override IEnumerable<TEntity> FindAllDescending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			Initialize();
			return FindAll(offset, limit, filter).OrderByDescending(orderBy.Compile());
		}

		public override TEntity FindBy(object key)
		{
			Initialize();
			return Mapper.ToDomainEntity(m_table.Get(key as string));
		}

		public void ClearAll()
		{
			Initialize();

			var all = MapperHelper.ToDomainEntities(m_table, Mapper);

			foreach (var r in all)
			{
				m_table.Delete(r.Key.ToString());
			}

			PushToDropbox();
		}

		protected override void PersistDeletedItem(TEntity item)
		{
			Initialize();
			m_table.Delete(item.Key.ToString());
			PushToDropbox();
		}

		protected override void PersistNewItem(TEntity item)
		{
			Initialize();
			m_table.Insert(Mapper.ToRepositoryEntity(item));
			PushToDropbox();
		}

		async private void PushToDropbox()
		{
			Task<bool> task = m_ds.PushAsync();
			task.Wait();

			if (!task.Result)
			{
				throw new InvalidOperationException("Error syncing with DropBox.");
			}
		}

		protected override void PersistUpdatedItem(TEntity item)
		{
			Initialize();
			m_table.Update(Mapper.ToRepositoryEntity(item));
			PushToDropbox();
		}

		async private void Initialize()
		{
			if (!m_initialized)
			{
				// https://www.dropbox.com/1/oauth2/authorize?client_id=wzlvug44evwu7lg&response_type=code
				var manager = new DatastoreManager("cJBboFs_i-sAAAAAAAACwIWnTF0EM1eQAIK4blvvFYiEN6ZRvdcgWM00m1BJyvug");

				var task = manager.GetOrCreateAsync(m_datastoreId.ToLowerInvariant());
				task.Wait();
				m_ds = task.Result;

				m_initialized = true;
			}

			m_table = m_ds.GetTable<TRepositoryEntity>(typeof(TRepositoryEntity).Name);
			var task2 = m_ds.PullAsync();
			task2.Wait();
		}
	}
}
