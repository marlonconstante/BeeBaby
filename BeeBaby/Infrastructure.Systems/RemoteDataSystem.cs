using System;
using System.Net;
using Domain.Moment;
using System.Text;

namespace Infrastructure.Systems
{
	public static class RemoteDataSystem
	{
		static public void SendMomentData(Moment moment)
		{
			var dateFormat = "yyyyMMddhhmmssff";

			var baby = moment.Babies[0];
			var queryBuilder = new StringBuilder();
			queryBuilder.Append(string.Concat("Id=", moment.Id));
			queryBuilder.Append(string.Concat("&EventId=", Uri.EscapeUriString(moment.Event.Id)));
			queryBuilder.Append(string.Concat("&EventDescription=", Uri.EscapeUriString(moment.Event.Description)));
			queryBuilder.Append(string.Concat("&Description=", Uri.EscapeUriString(moment.Position != null ? moment.Description : string.Empty)));
			queryBuilder.Append(string.Concat("&Longitude=", Uri.EscapeUriString(moment.Position != null ? moment.Position.Longitude.ToString() : string.Empty)));
			queryBuilder.Append(string.Concat("&Latitude=", Uri.EscapeUriString(moment.Position != null ? moment.Position.Latitude.ToString() : string.Empty)));
			queryBuilder.Append(string.Concat("&LocationId=", Uri.EscapeUriString(moment.Location != null ? moment.Location.Id : string.Empty)));
			queryBuilder.Append(string.Concat("&LocationDescription=", Uri.EscapeUriString(moment.Location != null ? moment.Location.Name : string.Empty)));
			queryBuilder.Append(string.Concat("&Date=", moment.Date.ToString(dateFormat)));
			queryBuilder.Append(string.Concat("&BabyId=", baby.Id));
			queryBuilder.Append(string.Concat("&BabyName=", Uri.EscapeUriString(baby.Name)));
			queryBuilder.Append(string.Concat("&BabyBirthDate=", baby.BirthDateTime.ToString(dateFormat)));
			queryBuilder.Append(string.Concat("&User=", Uri.EscapeUriString("teste@grouplighthouse.com")));


			var builder = new UriBuilder("http://127.0.0.1");
			builder.Port = 8080;
			builder.Query = queryBuilder.ToString();
			string url = builder.ToString();

			var request = HttpWebRequest.Create(url);
			request.ContentType = "application/json";
			request.Method = "GET";

			request.GetResponseAsync();
		}
	}
}

