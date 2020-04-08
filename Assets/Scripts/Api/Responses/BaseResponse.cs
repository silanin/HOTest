using HiddenObject.Api.Enums;
using System;

namespace HiddenObject.Api.Responses
{
	[Serializable]
	public class BaseResponse
	{
        public ResponseStatus Status;

		public string Message;
	}
}