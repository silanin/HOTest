using System.Linq;
using UniRx;
using UnityEngine;
using HiddenObject.Models;
using HiddenObject.Constants;
using HiddenObject.Events;
using HiddenObject.UI.Base;
using HiddenObject.UI.SpriteScriptable;

namespace HiddenObject.UI.Windows
{
	public class GameMenuMediator : BaseMediator
	{
		[Inject]
		public GameMenuView view { get; set; }

		[Inject]
		public IUserProgressModel userProgress { get; set; }

		[Inject]
		public ILevelsModel levels { get; set; }

		[Inject]
		public ISettingsModel settings { get; set; }

        [Inject]
		public ShowMenuSignal showMenuSignal { get; set; }

		[Inject]
		public HideMenuSignal hideMenuSignal { get; set; }

		[Inject]
		public LoadLevelSignal loadLevelSignal { get; set; }

		[Inject]
		public NightModeSignal nightModeSignal { get; set; }

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		public override void OnRegister()
		{
			showMenuSignal.AddListener(OnShow);
			hideMenuSignal.AddListener(OnHide);

			view.PlayButton.Text = "Play";
			view.PlayButton.OnClickAsObservable().Subscribe(OnPlayButtonClick).AddTo(_subscriptions);
			view.NightModeToggle.OnValueChangedAsObservable().Subscribe(OnNightModeClick).AddTo(_subscriptions);
		}

		public override void OnRemove()
		{
			showMenuSignal.RemoveListener(OnShow);
			hideMenuSignal.RemoveListener(OnHide);

			_subscriptions?.Clear();
		}

		private void OnShow()
		{
			view.Show();
			SetData();
		}

		private void OnHide()
		{
			view.Hide();
		}

		private void SetData()
		{
			view.LevelItemView.SetData(LevelEntity);
            view.NightModeToggle.isOn = settings.NightMode.Value;
		}

		private void OnPlayButtonClick(Unit value)
		{
			loadLevelSignal.Dispatch(Level);
		}

		private void OnNightModeClick(bool value)
		{
			nightModeSignal.Dispatch(value);
		}

		private LevelItemEntity LevelEntity
        {
            get
            {
				var levelData = levels.Get(Level);
				var levelProgress = userProgress.Get(Level);
				var spriteScriptable = Resources.Load<SpriteScriptableObject>(PathConstants.ScriptableDirectory + "/" + levelData.SpriteName);

				return new LevelItemEntity
				{
                    Level = levelData.Level,
                    LevelName = levelData.Name,
                    LevelSprite = spriteScriptable.Sprite,
                    Stars = levelProgress != null ? levelProgress.Stars : 0
				};
            }
        }

		private int Level
		{
			get
			{
				return userProgress.Progress.Count > 0 ? userProgress.Progress.Max(x => x.Level) : GameConstants.StartLevel;
			}
		}
	}
}

