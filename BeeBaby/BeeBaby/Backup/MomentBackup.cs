using System;
using Domain.Moment;
using Application;
using Domain.Baby;
using System.Collections.Generic;
using System.Linq;
using BeeBaby.ResourcesProviders;
using System.IO;
using Infrastructure.Systems.Utils;

namespace BeeBaby.Backup
{
	public class MomentBackup : FileBackup<MomentPlan>
	{
		/// <summary>
		/// The name of the file.
		/// </summary>
		public const string FileName = "_moment.backup";

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.MomentBackup"/> class.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public MomentBackup(Moment moment) : this(moment.Id, moment.ToMomentPlan())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BeeBaby.Backup.MomentBackup"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="momentPlan">Moment plan.</param>
		public MomentBackup(string id, MomentPlan momentPlan = null) : base(FileHandle.GetPath(id, FileName), momentPlan)
		{
			RelativeFilePath = Path.Combine(id, FileName);
		}

		/// <summary>
		/// Performs the action.
		/// </summary>
		protected override void PerformAction()
		{
			var moment = GetValue().ToMomentDomain();
			moment.Event = CurrentContext.Instance.AllEvents.Where(ev => ev.Id == moment.EventId).FirstOrDefault();
			moment.Location = new LocationService().SaveLocation(moment.Location);
			moment.Babies = new List<Baby> { CurrentContext.Instance.CurrentBaby };
			moment.MediaCount = new ImageProvider(moment.Id).GetFileNames().Count;

			new MomentService().SaveMoment(moment);

			CurrentContext.Instance.ReloadMoments = true;
		}

		/// <summary>
		/// Gets or sets the relative file path.
		/// </summary>
		/// <value>The relative file path.</value>
		public string RelativeFilePath {
			get;
			set;
		}
	}
}