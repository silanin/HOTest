using System;
using System.Collections.Generic;
using HiddenObject.Api.Dto;

namespace HiddenObject.Api.Responses
{
	[Serializable]
	public class LevelsResponse : BaseResponse
	{
		public IEnumerable<LevelDto> Levels { get; set; }
	}
}