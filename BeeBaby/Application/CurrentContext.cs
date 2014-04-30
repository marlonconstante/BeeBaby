using System;
using Domain.Moment;
using Domain.Baby;

namespace Application
{
	public sealed class CurrentContext
	{
		private static CurrentContext s_instance;

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

		/// <summary>
		/// Gets or sets the baby.
		/// </summary>
		/// <value>The baby.</value>
		public Baby CurrentBaby {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the selected event.
		/// </summary>
		/// <value>The selected event.</value>
		public Event SelectedEvent
		{
			get;
			set;
		}
	}
}