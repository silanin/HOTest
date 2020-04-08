using System;

namespace HiddenObject.Api.Requests
{
	[Serializable]
	public class BaseRequest
	{
		public string DeviceId { get; set; }
	}
}