using System;
using System.Net;
using Domain.Moment;
using System.Text;
using Parse;
using Domain.Log;
using Infrastructure.Systems.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Baby;

namespace Infrastructure.Systems
{
	public static class RemoteDataSystem
	{
		const string s_dateFormat = "yyyyMMddhhmmssff";

		/// <summary>
		/// Sends the moment data.
		/// </summary>
		/// <param name="moment">Moment.</param>
		public static async void SendMomentData(Moment moment)
		{
			var baby = moment.Babies[0];
			var momentData = new ParseObject("Moment");

			momentData["Id"] = moment.Id;
			momentData["EventId"] = moment.Event.Id;
			momentData["EventDescription"] = moment.Event.Description;
			momentData["Description"] = moment.Description;
			momentData["Longitude"] = moment.Position != null ? moment.Position.Longitude.ToString() : string.Empty;
			momentData["Latitude"] = moment.Position != null ? moment.Position.Latitude.ToString() : string.Empty;
			momentData["LocationId"] = moment.Location != null ? moment.Location.Id : string.Empty;
			momentData["LocationDescription"] = moment.Location != null ? moment.Location.Name : string.Empty;
			momentData["Date"] = moment.Date.ToString(s_dateFormat);
			momentData["MediaCount"] = moment.MediaCount;
			momentData["BabyId"] = baby.Id;
			momentData["BabyName"] = baby.Name;
			momentData["BabyGender"] = baby.Gender.ToString();
			momentData["BabyBirthDate"] = baby.BirthDateTime.ToString(s_dateFormat);
			momentData["User"] = baby.Email;
			momentData["Language"] = moment.Language;

			try {
				await momentData.SaveAsync();
			} catch (Exception ex) {
				// Ignora..
			}
		}

		/// <summary>
		/// Sends the flow data.
		/// </summary>
		/// <param name="flow">Flow.</param>
		public static async void SendFlowData(Flow flow)
		{
			var flowData = new ParseObject("Flow");

			flowData["Id"] = flow.Id;
			flowData["DeviceId"] = flow.DeviceId;
			flowData["SessionId"] = flow.SessionId;
			flowData["Name"] = flow.Name;
			flowData["Date"] = flow.Date.ToString(s_dateFormat);

			try {
				await flowData.SaveAsync();
			} catch (Exception ex) {
				// Ignora..
			}
		}

		/// <summary>
		/// Sends the invite.
		/// </summary>
		/// <param name="invite">Invite.</param>
		public static async void SendInvite(FriendshipShared invite)
		{
			try {
				await invite.SaveAsync();
			} catch (Exception ex) {
				// Ignora..
			}
		}

		/// <summary>
		/// Syncs the moment.
		/// </summary>
		/// <param name="moment">Moment.</param>
		/// <param name="thumbnails">Thumbnails.</param>
		/// <param name="fullImages">Full images.</param>
		public static async void SyncMoment(Moment moment, IDictionary<string, byte[]> thumbnails, IDictionary<string, byte[]> fullImages)
		{
			var momentData = new MomentShared(moment);
			momentData.Thumbnails = thumbnails;
			momentData.FullImages = fullImages;

			try {
				await momentData.SaveAsync();

				if (moment.ObjectId == null)
				{
					moment.ObjectId = momentData.ObjectId;
					new MomentService().SaveMoment(moment);
				}
			} catch (Exception ex) {
				// Ignora..
			}
		}

		/// <summary>
		/// Gets all moments.
		/// </summary>
		/// <returns>The all moments.</returns>
		public static async Task<IEnumerable<IMoment>> GetAllMoments(Baby baby)
		{
			var babyMoments = ParseShared.CreateQuery<MomentShared>().WhereEqualTo("UserEmail", baby.Email);
			var friendMoments = ParseShared.CreateQuery<MomentShared>().WhereMatchesKeyInQuery("UserEmail", "UserEmail", FriendshipQuery(baby));

			var query = babyMoments.Or(friendMoments);
			query.OrderByDescending("MomentDate");

			return await ParseShared.FindAsync<MomentShared>(query);
		}

		/// <summary>
		/// Friendships the query.
		/// </summary>
		/// <returns>The query.</returns>
		/// <param name="baby">Baby.</param>
		static ParseQuery<ParseObject> FriendshipQuery(Baby baby)
		{
			var query = from friendship in ParseShared.CreateQuery<FriendshipShared>()
						where friendship.Get<string>("FriendUserEmail") == baby.Email
						select friendship;

			return query;
		}
	}
}