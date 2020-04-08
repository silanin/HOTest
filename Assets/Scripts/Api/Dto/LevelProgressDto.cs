using System;

namespace HiddenObject.Api.Dto
{
    [Serializable]
    public class LevelProgressDto
    {
        public int Level { get; set; }

        public int Stars { get; set; }

        public int ObjectsCollected { get; set; }

        public int Seconds { get; set; }
    }
}
