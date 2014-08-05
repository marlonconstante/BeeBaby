using System;
using System.Net;
using Domain.Moment;
using System.Text;
using Parse;
using Domain.Log;

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
	}
}