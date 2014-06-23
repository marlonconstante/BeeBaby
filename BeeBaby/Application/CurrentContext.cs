using System;
using Domain.Moment;
using Domain.Baby;
using System.Collections.Generic;

namespace Application
{
	public sealed class CurrentContext
	{
		private static CurrentContext s_instance;

		private CurrentContext()
		{
			AllEvents = new List<Event>();
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
		/// Gets or sets the current baby.
		/// </summary>
		/// <value>The current baby.</value>
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

		/// <summary>
		/// Gets or sets all events.
		/// </summary>
		/// <value>All events.</value>
		public IList<Event> AllEvents
		{
			get;
			set;
		}
	}
}