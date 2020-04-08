using System;
using System.Collections.Generic;

namespace HiddenObject.Api.Dto
{
    [Serializable]
    public class LevelDto
    {
        public int Level { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalSeconds { get; set; }

        public string SpriteName { get; set; }

        public IEnumerable<ObjectDto> Objects { get; set; }

    }
}
