using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;
using System.Linq.Expressions;
using System.Collections.Generic;
using GDataDB;
using System.Linq;
using HelperSharp;

namespace Infrastructure.Repositories.GoogleDocs
{
	public class GoogleDocsRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : EntityWithIdBase<string>, IAggregateRoot, new()
	{
		#region Fields
		private string m_userName;
		private string m_password;
		private string m_databaseName;
		private ITable<TEntity> m_table;
		#endregion

		#region Constructors
		public GoogleDocsRepositoryBase(string databaseName, string userName, string password)
		{
			m_databaseName = databaseName;
			m_userName = userName;
			m_password = password;
		}
		#endregion

		#region Methods
		public override long CountAll(Expression<Func<TEntity, bool>> filter)
		{
			return FindAll(0, int.MaxValue, filter).Count();
		}

		public override IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter)
		{
			var rows = GetTable().FindAll(offset, limit);
			var elements = rows.Select(t => t.Element);

			return filter == null ? elements : elements.Where(filter.Compile());
		}

		public override IEnumerable<TEntity> FindAllAscending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			return FindAll(offset, limit, filter).OrderBy(orderBy.Compile());
		}

		public override IEnumerable<TEntity> FindAllDescending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
		{
			return FindAll(offset, limit, filter).OrderByDescending(orderBy.Compile());
		}

		public override TEntity FindBy(object key)
		{
			TEntity element = null;
			var row = GetRowById(key.ToString());

			if (row != null)
			{
				element = row.Element;
			}

			return element;
		}

		protected override void PersistDeletedItem(TEntity item)
		{
			var row = GetRowById(item.Id);

			if (row != null)
			{
				row.Delete();
			}
		}

		protected override void PersistNewItem(TEntity item)
		{
			if (String.IsNullOrEmpty(item.Id))
			{
				item.Id = Guid.NewGuid().ToString();
			}

			GetTable().Add(item);
		}

		protected override void PersistUpdatedItem(TEntity item)
		{
			var row = GetRowById(item.Id);

			if (row != null)
			{
				row.Element = item;
				row.Update();
			}
		}
		#endregion

		#region Helpers
		private ITable<TEntity> GetTable()
		{
			if (m_table == null)
			{
				var client = new DatabaseClient(m_userName, m_password);
				var db = client.GetDatabase(m_databaseName) ?? client.CreateDatabase(m_databaseName);

				var tableName = typeof(TEntity).Name;
				m_table = db.GetTable<TEntity>(tableName) ?? db.CreateTable<TEntity>(tableName);
			}

			return m_table;
		}

		private IRow<TEntity> GetRowById(string key)
		{
			return GetTable().FindStructured("id={0}".With(key)).FirstOrDefault();
		}
		#endregion
	}
}

