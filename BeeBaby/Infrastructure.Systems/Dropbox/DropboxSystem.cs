using System;
using BobbyTables;
using Infrastructure.Systems.Dropbox.Entities;
using Infrastructure.Systems.Dropbox.Mapper;
using HelperSharp;

namespace Infrastructure.Systems
{
	public class DropboxSystem
	{
		#region Fields

		private string m_datastoreId;
		private Datastore m_ds;
		private Table<MomentData> m_table;
		private bool m_initialized;
		private DropboxMomentMapper m_mapper;

		#endregion


		public DropboxSystem(string datastoreId)
		{
			ExceptionHelper.ThrowIfNull("datastoreId", datastoreId);

			m_datastoreId = datastoreId;
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

			m_table = m_ds.GetTable<MomentData>(typeof(MomentData).Name);
			var task2 = m_ds.PullAsync();
			task2.Wait();
		}
	}
}

