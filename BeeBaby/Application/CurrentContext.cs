﻿using System;
using Domain.Moment;
using Domain.Baby;
using System.Collections.Generic;
using Domain.User;

namespace Application
{
	public sealed class CurrentContext
	{
		static CurrentContext s_instance;

		private CurrentContext()
		{
			AllEvents = new List<Event>();
			ReloadMoments = true;
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static CurrentContext Instance
		{
			get
			{
				if (s_instance == null)
				{
					s_instance = new CurrentContext();
				}
				return s_instance; 
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Application.CurrentContext"/> reload moments.
		/// </summary>
		/// <value><c>true</c> if reload moments; otherwise, <c>false</c>.</value>
		public bool ReloadMoments
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		public User User
		{
			get;
			set;
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
		public Baby CurrentBaby
		{
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

		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		/// <value>The scale.</value>
		public float Scale
		{
			get;
			set;
		}
	}
}