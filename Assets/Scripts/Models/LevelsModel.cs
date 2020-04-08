using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObject.Api.Dto;
using UniRx;

namespace HiddenObject.Models
{
    public interface ILevelsModel : ISaveModel
    {
        LevelDto Get(int level);

        void Update(IEnumerable<LevelDto> collection);

        void Reset();

        IList<LevelDto> Levels { get; set; }
    }

    [Serializable]
    public class LevelsModel : ILevelsModel
    {
        public LevelsModel()
        {
            Levels = new List<LevelDto>();
        }

        public LevelDto Get(int level)
        {
            return Levels.FirstOrDefault(x => x.Level == level);
        }

        public void Update(IEnumerable<LevelDto> collection)
        {
            Levels.Clear();
            foreach (var model in collection)
            {
                Levels.Add(model);
            }
        }

        public void Reset()
        {
            Levels.Clear();
        }

        public IList<LevelDto> Levels { get; set; }
    }
}
