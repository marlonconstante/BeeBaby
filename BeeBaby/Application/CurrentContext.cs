using System;
using Domain.Moment;

namespace Application
{
	public sealed class CurrentContext
	{
		private static CurrentContext s_instance;
		//private Moment m_moment;
		private CurrentContext()
		{
		}

		public static CurrentContext Instance
		{
			get
			{
				if (s_instance == null)
					s_instance = new CurrentContext();

				return s_instance; 
			}
		}

		/// <summary>
		/// Gets or sets the moment.
		/// </summary>
		/// <value>The moment.</value>
		public Moment Moment
		{
			get;
			set;
		}

		public Event SelectedEvent
		{
			get;
			set;
		}
	}
}