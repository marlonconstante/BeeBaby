using System;
using System.Net;
using Domain.Moment;

namespace Infrastructure.Systems
{
	public static class RemoteDataSystem
	{
		static public void SendMomentData(Moment moment)
		{
			var request = HttpWebRequest.Create(string.Format(@"http://127.0.0.1:8080"));
			request.ContentType = "application/json";
			request.Method = "GET";

			request.GetResponseAsync();
		}
	}
}

