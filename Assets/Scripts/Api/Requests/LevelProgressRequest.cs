using System;
using HiddenObject.Api.Dto;

namespace HiddenObject.Api.Requests
{
	[Serializable]
	public class LevelProgressRequest : BaseRequest
	{
		public LevelProgressDto LevelProgress { get; set; }
	}
}
