using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObject.Api.Dto;
using HiddenObject.Constants;
using UniRx;

namespace HiddenObject.Models
{
    public interface IUserProgressModel : ISaveModel
    {
        LevelProgressDto Get(int level);

        void Set(int level, LevelProgressDto progress);

        void Update(IEnumerable<LevelProgressDto> collection);

        void Reset();

        LevelProgressDto GetHighestOpenedLevel { get; }

        IList<LevelProgressDto> Progress { get; set; }
    }

    [Serializable]
    public class UserProgressModel : IUserProgressModel
    {
        public UserProgressModel()
        {
            Progress = new List<LevelProgressDto>();
        }

        public LevelProgressDto Get(int level)
        {
            return Progress.FirstOrDefault(x => x.Level == level);
        }

        public void Set(int level, LevelProgressDto progress)
        {
            var current = Get(level);
            if (current != null)
            {
                if(current.Stars < progress.Stars)
                {
                    current.Stars = progress.Stars;
                    current.Seconds = progress.Seconds;
                    current.ObjectsCollected = progress.ObjectsCollected;
                }
            }
            else
            {
                Progress.Add(progress);
            }
        }

        public void Update(IEnumerable<LevelProgressDto> collection)
        {
            Progress.Clear();
            foreach (var model in collection)
            {
                Progress.Add(model);
            }
        }

        public void Reset()
        {
            Progress.Clear();
        }

        public LevelProgressDto GetHighestOpenedLevel
        {
            get
            {
                if (Progress.Count == 0)
                {
                    return new LevelProgressDto
                    {
                        Level = GameConstants.StartLevel,
                        Seconds = 0,
                        ObjectsCollected = 0,
                        Stars = 0
                    };
                }
                return Progress.FirstOrDefault(x => x.Level == Progress.Max(y => y.Level));
            }
        }

        public IList<LevelProgressDto> Progress { get; set; }

    }
}
