using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using DG.Tweening;
using HiddenObject.Models;
using HiddenObject.Constants;
using HiddenObject.Events;
using HiddenObject.UI.Base;
using HiddenObject.UI.SpriteScriptable;

namespace HiddenObject.UI.Windows
{
	public class GameMediator : BaseMediator
	{
		[Inject]
		public GameView view { get; set; }

		[Inject]
		public IGameModel gameModel { get; set; }

		[Inject]
		public ILevelsModel levels { get; set; }

		[Inject]
		public ISettingsModel settings { get; set; }

		[Inject]
		public BackToMenuSignal backToMenuSignal { get; set; }

        [Inject]
		public CollectObjectSignal collectObjectSignal { get; set; }

		[Inject]
		public ShowGameSignal showGameSignal { get; set; }

		[Inject]
		public HideGameSignal hideGameSignal { get; set; }

		[Inject]
		public NightModeSignal nightModeSignal { get; set; }

		private IDisposable _backButtonSubscription;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		private List<TopItemView> _topObjects = new List<TopItemView>();
		private List<FieldItemView> _fieldObjects = new List<FieldItemView>();
		private List<FlyingObjectView> _flyingObjects = new List<FlyingObjectView>();

		private Queue<int> _animatedItems = new Queue<int>();
		private Queue<Sequence> _sequences = new Queue<Sequence>();

		public override void OnRegister()
		{
			showGameSignal.AddListener(OnShow);
			hideGameSignal.AddListener(OnHide);

			_backButtonSubscription = view.BackButton.OnClickAsObservable()
                .Subscribe(OnBackButtonClick);
		}

        public override void OnRemove()
		{
			showGameSignal.RemoveListener(OnShow);
			hideGameSignal.RemoveListener(OnHide);

			_backButtonSubscription?.Dispose();

			Clear();
		}

		private void OnShow()
		{
			view.Show();
			SetData();
		}

		private void OnHide()
		{
			view.Hide();
			Clear();
		}

		private void Clear()
		{
			view.TimerText = String.Empty;
			view.StarComponent.Stars = 0;
			view.CollectedText = "0/0";

			view.TopPanelPool.ReleaseAllInstances();
			view.GameFieldPool.ReleaseAllInstances();
			view.AnimatedPool.ReleaseAllInstances();

			_subscriptions?.Clear();
			_animatedItems.Clear();

            foreach (var sequence in _sequences)
            {
				sequence.Kill();
			}
			_sequences.Clear();
		}

		private void SetData()
		{
			Clear();

			gameModel.Stars.Subscribe(OnStarsChange).AddTo(_subscriptions);
			gameModel.Seconds.Subscribe(OnSecondTick).AddTo(_subscriptions);
			gameModel.ObjectsCollected.Subscribe(OnItemCollected).AddTo(_subscriptions);
			gameModel.Completed.Subscribe(OnGameCompleted).AddTo(_subscriptions);

			var spriteScriptable = Resources.Load<SpriteScriptableObject>(PathConstants.ScriptableDirectory + "/" + gameModel.SpriteName);

			view.FieldBackground = spriteScriptable.Sprite;

			gameModel.Objects.ForEach(item =>
			{
				var spritePath = PathConstants.ScriptableDirectory + "/" + item.SpriteName;
				var objectSpriteScriptable = Resources.Load<SpriteScriptableObject>(spritePath);

				var topItem = view.TopPanelPool.Get<TopItemView>();
				if (topItem != null)
				{
					topItem.SetData(new TopItemEntity
					{
						Id = item.Id,
						Name = item.Name,
						Sprite = objectSpriteScriptable.Sprite,
						ShowHighlited = false
					});
					_topObjects.Add(topItem);
				}

				var fieldItem = view.GameFieldPool.Get<FieldItemView>();
				if (fieldItem != null)
				{
					fieldItem.SetData(new FieldItemEntity
					{
						Id = item.Id,
						Sprite = objectSpriteScriptable.Sprite,
						ShowShine = false,
						LocalX = item.LocalX,
						LocalY = item.LocalY,
						LocalScale = item.LocalScale,
						Rotation = item.Rotation
					});
					fieldItem.Button.OnClickAsObservable().Select(_ => item.Id).Subscribe(OnObjectClick).AddTo(_subscriptions);
					_fieldObjects.Add(fieldItem);
				}

			});

			view.CollectedText = gameModel.ObjectsCollected + "/" + gameModel.Objects.Count;

			view.ShowNightMode = view.NightModeToggle.isOn = settings.NightMode.Value;
			view.ShowNightModeMask = !gameModel.Completed.Value;
			view.NightModeToggle.OnValueChangedAsObservable().Subscribe(OnNightModeClick).AddTo(_subscriptions);
			settings.NightMode.Subscribe(OnNightModeChange).AddTo(_subscriptions);
		}

		private void OnObjectClick(int objectId)
		{
			var fieldItem = _fieldObjects.FirstOrDefault(x => x.Id == objectId);
			fieldItem.ShowShine = true;

			var target = _topObjects.FirstOrDefault(x => x.Id == objectId);
			var targetPosition = target.RectTransform.position;

			var flyingObjectView = view.AnimatedPool.Get<FlyingObjectView>();
			if (flyingObjectView != null)
			{
				flyingObjectView.Id = objectId;
				flyingObjectView.Sprite = fieldItem.Sprite;
				flyingObjectView.SetPosition(fieldItem.RectTransform);

				var sequence = DOTween.Sequence();

				var moveTween = flyingObjectView.RectTransform.DOMove(new Vector3(targetPosition.x, targetPosition.y, 0f), .5f);
				var scaleTween = flyingObjectView.RectTransform.DOScale(Vector3.one, .5f);
				var rotationTween = flyingObjectView.RectTransform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), .5f);

				_animatedItems.Enqueue(objectId);

				_flyingObjects.Add(flyingObjectView);

				sequence
                    .Append(moveTween)
                    .Join(scaleTween)
                    .Join(rotationTween)
                    .OnComplete(OnAnimationComplete);

				_sequences.Enqueue(sequence);
			}
		}

        private void OnAnimationComplete()
        {
			var sequence = _sequences.Dequeue();
			var targetId = _animatedItems.Dequeue();

			var topItem = _topObjects.FirstOrDefault(x => x.Id == targetId);
			var animatedItem = _flyingObjects.FirstOrDefault(x => x.Id == targetId);

			view.TopPanelPool.Release(topItem);
			view.AnimatedPool.Release(animatedItem);

			sequence.Kill();

			collectObjectSignal.Dispatch(targetId);
		}

		private void OnSecondTick(int seconds)
		{
			var time = TimeSpan.FromSeconds(seconds);
			view.TimerText = time.ToString(@"mm\:ss");
		}

		private void OnStarsChange(int value)
		{
			view.StarComponent.Stars = value;
		}

		private void OnItemCollected(int value)
		{
			view.CollectedText = gameModel.ObjectsCollected + "/" + gameModel.Objects.Count;
		}

		private void OnBackButtonClick(Unit value)
		{
			backToMenuSignal.Dispatch();
		}

        private void OnGameCompleted(bool value)
        {
			view.ShowNightModeMask = !gameModel.Completed.Value;
		}

		private void OnNightModeClick(bool value)
		{
			nightModeSignal.Dispatch(value);
		}

		private void OnNightModeChange(bool value)
		{
			view.ShowNightMode = view.NightModeToggle.isOn = settings.NightMode.Value;
		}
	}
}

