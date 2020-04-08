using System;
using UniRx;

namespace HiddenObject.Models
{
    public interface ISettingsModel
    {
        BoolReactiveProperty NightMode { get; }

        void Reset();
    }

    [Serializable]
    public class SettingsModel : ISettingsModel
    {
        public SettingsModel()
        {
            NightMode = new BoolReactiveProperty(false);
        }

        public BoolReactiveProperty NightMode { get; }

        public void Reset()
        {
            NightMode.Value = false;
        }
    }
}
