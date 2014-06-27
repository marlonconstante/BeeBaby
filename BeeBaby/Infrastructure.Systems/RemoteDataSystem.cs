using System;
using System.Net;
using Domain.Moment;
using System.Text;
using Parse;

namespace Infrastructure.Systems
{
	public static class RemoteDataSystem
	{
		static async public void SendMomentData(Moment moment)
		{
			var dateFormat = "yyyyMMddhhmmssff";

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
			momentData["Date"] = moment.Date.ToString(dateFormat);
			momentData["BabyId"] = baby.Id;
			momentData["BabyName"] = baby.Name;
			momentData["BabyBirthDate"] = baby.BirthDateTime.ToString(dateFormat);
			momentData["User"] = baby.Email;

			await momentData.SaveAsync();
		}
	}
}

