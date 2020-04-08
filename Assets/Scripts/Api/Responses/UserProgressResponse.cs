using System;
using System.Collections.Generic;
using HiddenObject.Api.Dto;

namespace HiddenObject.Api.Responses
{
	[Serializable]
	public class UserProgressResponse : BaseResponse
	{
		public IEnumerable<LevelProgressDto> Progress { get; set; }
	}
}
