using System;
using System.Numerics;

namespace HiddenObject.Api.Dto
{
    [Serializable]
    public class ObjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SpriteName { get; set; }

        public float LocalX { get; set; }

        public float LocalY { get; set; }

        public float LocalScale { get; set; }

        public float Rotation { get; set; }

    }
}
