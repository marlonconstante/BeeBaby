using System;
using Domain.Moment;
using Application;
using Domain.Baby;
using System.Collections.Generic;
using System.Linq;
using BeeBaby.ResourcesProviders;
using System.IO;

namespace BeeBaby.Backup
{
	public class MomentBackup : FileBackup<Moment>
	{
		/// <summary>
		/// The name of the file.
		/// </summary>
		public const string FileName = "_moment.backup";

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.MomentBackup"/> class.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public MomentBackup(Moment moment) : this(moment.Id, moment)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.MomentBackup"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="moment">Moment.</param>
		public MomentBackup(string id, Moment moment = null) : base(FileHandle.GetPath(id, FileName), moment)
		{
		}

		/// <summary>
		/// Performs the action.
		/// </summary>
		protected override void PerformAction()
		{
			var moment = GetValue();
			moment.Event = CurrentContext.Instance.AllEvents.Where(ev => ev.Id == moment.EventId).FirstOrDefault();
			moment.Location = new LocationService().SaveLocation(moment.Location);
			moment.Babies = new List<Baby> { CurrentContext.Instance.CurrentBaby };
			moment.MediaCount = new ImageProvider(moment.Id).GetFileNames().Count;

			new MomentService().SaveMoment(moment);

			CurrentContext.Instance.ReloadMoments = true;
		}
	}
}