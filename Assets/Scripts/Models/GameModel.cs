using System;
using System.Collections.Generic;
using HiddenObject.Api.Dto;
using UniRx;

namespace HiddenObject.Models
{
    public interface IGameModel
    {
        int Level { get; set; }

        IntReactiveProperty Seconds { get; set; }

        int TotalSeconds { get; set; }

        string SpriteName { get; set; }

        List<ObjectModel> Objects { get; set; }

        IntReactiveProperty Stars { get; set; }

        IntReactiveProperty ObjectsCollected { get; }

        BoolReactiveProperty Completed { get; }

        void Update(LevelDto levelData);

        void Reset();
    }

    [Serializable]
    public class GameModel : IGameModel
    {
        public GameModel()
        {
            Seconds = new IntReactiveProperty(0);
            Objects = new List<ObjectModel>();
            Stars = new IntReactiveProperty(0);
            ObjectsCollected = new IntReactiveProperty(0);
            Completed = new BoolReactiveProperty(false);
        }

        public int Level { get; set; }

        public IntReactiveProperty Seconds { get; set; }

        public int TotalSeconds { get; set; }

        public string SpriteName { get; set; }

        public List<ObjectModel> Objects { get; set; }

        public IntReactiveProperty Stars { get; set; }

        public IntReactiveProperty ObjectsCollected { get; }

        public BoolReactiveProperty Completed { get; }

        public void Update(LevelDto levelData)
        {
            Reset();

            Level = levelData.Level;
            TotalSeconds = levelData.TotalSeconds;
            SpriteName = levelData.SpriteName;

            foreach(var dto in levelData.Objects)
            {
                Objects.Add(new ObjectModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    SpriteName = dto.SpriteName,
                    LocalX = dto.LocalX,
                    LocalY = dto.LocalY,
                    LocalScale = dto.LocalScale,
                    Rotation = dto.Rotation,
                    Collected = false
                });
            }
        }

        public void Reset()
        {
            Level = 0;
            Seconds.Value = 0;
            TotalSeconds = 0;
            SpriteName = string.Empty;
            Objects = new List<ObjectModel>();
            Stars.Value = 0;
            ObjectsCollected.Value = 0;
            Completed.Value = false;
        }
    }

    public class ObjectModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SpriteName { get; set; }

        public float LocalX { get; set; }

        public float LocalY { get; set; }

        public float LocalScale { get; set; }

        public float Rotation { get; set; }

        public bool Collected { get; set; }
    }
}
