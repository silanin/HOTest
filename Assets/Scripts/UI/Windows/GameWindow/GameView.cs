using UnityEngine;
using UnityEngine.UI;
using HiddenObject.Components;
using HiddenObject.UI.Base;

namespace HiddenObject.UI.Windows
{
	public class GameView : BaseView
    {
		#region SERIALIZE FIELDS

		[SerializeField]
		private Text _timer;
		[SerializeField]
		private Button _backButton;
        [SerializeField]
        private StarComponent _starComponent;
        [SerializeField]
		private ComponentPoolFactory _topPanelPool;
		[SerializeField]
		private Image _fieldBackground;
        [SerializeField]
        private Text _collected;
        [SerializeField]
        private ComponentPoolFactory _gameFieldPool;
        [SerializeField]
        private ComponentPoolFactory _animatedPool;
        [SerializeField]
        private RectTransform _gameFieldTransform;
        [SerializeField]
        private RectTransform _gameFieldContainerTransform;
        [SerializeField]
        private GameObject _nightModeContainer;
        [SerializeField]
        private GameObject _nightModeMask;
        [SerializeField]
        private Toggle _nightModeToggle;

        #endregion

        private Vector2 rootSize;

        #region MONOBEHAVIOR METHODS

        protected override void Start()
        {
            base.Start();

            rootSize = new Vector2(_gameFieldTransform.rect.width, _gameFieldTransform.rect.height);
            Reposition();
        }

        protected void Update()
        {
            var currentSize = new Vector2(_gameFieldTransform.rect.width, _gameFieldTransform.rect.height);
            if (!rootSize.Equals(currentSize))
            {
                rootSize = currentSize;
                Reposition();
            }
        }

        #endregion

        #region PUBLIC PROPERTIES

        public string TimerText
        {
            get
            {
                if (_timer != null)
                {
                    return _timer.text;
                }
                return string.Empty;
            }
            set
            {
                if (_timer != null)
                {
                    _timer.text = value;
                }
            }
        }

        public string CollectedText
        {
            get
            {
                if (_collected != null)
                {
                    return _collected.text;
                }
                return string.Empty;
            }
            set
            {
                if (_collected != null)
                {
                    _collected.text = value;
                }
            }
        }

        public Sprite FieldBackground
        {
            get
            {
                if (_fieldBackground != null)
                {
                    return _fieldBackground.sprite;
                }
                return null;
            }
            set
            {
                if (_fieldBackground != null)
                {
                    _fieldBackground.sprite = value;
                    _fieldBackground.SetNativeSize();

                    Reposition();
                }
            }
        }

        public bool ShowNightMode
        {
            get
            {
                if (_nightModeContainer != null)
                {
                    return _nightModeContainer.activeSelf;
                }
                return false;
            }
            set
            {
                if (_nightModeContainer != null)
                {
                    _nightModeContainer.SetActive(value);
                }
            }
        }

        public bool ShowNightModeMask
        {
            get
            {
                if (_nightModeMask != null)
                {
                    return _nightModeMask.activeSelf;
                }
                return false;
            }
            set
            {
                if (_nightModeMask != null)
                {
                    _nightModeMask.SetActive(value);
                }
            }
        }

        public Button BackButton => _backButton;

        public StarComponent StarComponent => _starComponent;

        public ComponentPoolFactory TopPanelPool => _topPanelPool;

        public ComponentPoolFactory GameFieldPool => _gameFieldPool;

        public ComponentPoolFactory AnimatedPool => _animatedPool;

        public Toggle NightModeToggle => _nightModeToggle;

        #endregion

        private void Reposition()
        {
            var containerWidth = _gameFieldTransform.rect.width;
            var containerHeight = _gameFieldTransform.rect.height;

            var originalImageWidth = _fieldBackground.sprite.rect.width;
            var originalImageHeight = _fieldBackground.sprite.rect.height;

            var factor = Mathf.Min(containerWidth / originalImageWidth, containerHeight / originalImageHeight);

            _gameFieldContainerTransform.localScale = new Vector3(factor, factor, 1);

        }
    }
}