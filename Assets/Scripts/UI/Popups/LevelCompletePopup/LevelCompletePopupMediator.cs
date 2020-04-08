using UniRx;
using HiddenObject.Models;
using HiddenObject.Events;
using HiddenObject.UI.Base;

namespace HiddenObject.UI.Windows
{
	public class LevelCompletePopupMediator : BaseMediator
	{
		[Inject]
		public LevelCompletePopupView view { get; set; }

		[Inject]
		public IGameModel gameModel { get; set; }

		[Inject]
		public ReplaySignal replaySignal { get; set; }

		[Inject]
		public BackToMenuSignal backToMenuSignal { get; set; }

		[Inject]
		public ShowLevelCompleteSignal showLevelCompleteSignal { get; set; }

		[Inject]
		public HideLevelCompleteSignal hideLevelCompleteSignal { get; set; }

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		public override void OnRegister()
		{
			showLevelCompleteSignal.AddListener(OnShow);
			hideLevelCompleteSignal.AddListener(OnHide);

			view.ReplyButton.Text = "Reply";
			view.CloseButton.Text = "Close";
			view.ReplyButton.OnClickAsObservable().Subscribe(OnReplayButtonClick).AddTo(_subscriptions);
			view.CloseButton.OnClickAsObservable().Subscribe(OnCloseButtonClick).AddTo(_subscriptions);
		}

		public override void OnRemove()
		{
			showLevelCompleteSignal.RemoveListener(OnShow);
			hideLevelCompleteSignal.RemoveListener(OnHide);

			_subscriptions?.Clear();
		}

		private void OnShow()
		{
			view.Show();
			view.StarComponent.Stars = gameModel.Stars.Value;
		}

		private void OnHide()
		{
			view.Hide();
		}

		private void OnReplayButtonClick(Unit value)
		{
			view.Hide();
			replaySignal.Dispatch();
		}

		private void OnCloseButtonClick(Unit value)
		{
			view.Hide();
			backToMenuSignal.Dispatch();
		}
	}
}

