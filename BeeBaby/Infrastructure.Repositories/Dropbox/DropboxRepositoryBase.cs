using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BobbyTables;
using HelperSharp;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace Infrastructure.Repositories.Dropbox
{
	/// <summary>
	/// Repository using Dropbox.
	/// </summary>
	public class DropboxRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : EntityWithIdBase<string>, IAggregateRoot, new()
	{
		#region Fields

		private string m_datastoreId;
		private Datastore m_ds;
		private Table<TEntity> m_table;
		private bool m_initialized;

		#endregion

		#region Constructors

		public DropboxRepositoryBase(string datastoreId)
		{
			ExceptionHelper.ThrowIfNull("datastoreId", datastoreId);

			m_datastoreId = datastoreId;
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
				return m_table.Count(filter.Compile());
			}
		}

		public override IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter)
		{
			Initialize();

			if (filter == null)
			{
				return m_table
                    .Skip(offset)
                    .Take(limit);
			}
			else
			{
				return m_table
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
			return m_table.Get(key as string);
		}

		public void ClearAll()
		{
			Initialize();

			foreach (var r in m_table)
			{
				m_table.Delete(r.Id);
			}

			PushToDropbox();
		}

		protected override void PersistDeletedItem(TEntity item)
		{
			Initialize();
			m_table.Delete(item.Id);
			PushToDropbox();
		}

		protected override void PersistNewItem(TEntity item)
		{
			Initialize();
			m_table.Insert(item);
			PushToDropbox();
		}

		async private void PushToDropbox()
		{
			var pushAsyncTask = m_ds.PushAsync();
			if (!pushAsyncTask.Result)
			{
				throw new InvalidOperationException("Error syncing with DropBox.");
			}
		}

		protected override void PersistUpdatedItem(TEntity item)
		{
			Initialize();
			m_table.Update(item);
			PushToDropbox();
		}

		async private void Initialize()
		{
			if (!m_initialized)
			{
				// https://www.dropbox.com/1/oauth2/authorize?client_id=5wqvkk9wlx8uhz9&response_type=code&redirect_uri=http://localhost
				var manager = new DatastoreManager("cJBboFs_i-sAAAAAAAACt6z7uYpy-2cOt-NEONBjqQ1spYtW0nCbFn7_X3PQH502");
				var task = manager.GetOrCreateAsync(m_datastoreId.ToLowerInvariant());
				m_ds = task.Result;

				m_table = m_ds.GetTable<TEntity>(typeof(TEntity).Name);
				m_ds.PushAsync();
				m_initialized = true;
			}
		}
	}
}
