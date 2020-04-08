using UnityEngine;
using UnityEngine.UI;

namespace HiddenObject.UI.Windows
{
    public class FieldItemView : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [SerializeField]
        private Image _image;
        [SerializeField]
        private Image _shineImage;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private RectTransform _rectTransform;

        #endregion

        #region PUBLIC METHODS

        public void SetData(FieldItemEntity data)
        {
            Id = data.Id;
            Sprite = data.Sprite;
            ShowShine = data.ShowShine;

            if (_rectTransform != null)
            {
                _rectTransform.localPosition = new Vector3(data.LocalX, data.LocalY, 0);
                _rectTransform.localScale = new Vector3(data.LocalScale, data.LocalScale, 1);
                _rectTransform.localRotation = Quaternion.Euler(0, 0, data.Rotation);
            }

        }

        public void Clear()
        {
            Id = default;
            Sprite = null;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public int Id { get; set; }

        public Sprite Sprite
        {
            get
            {
                if (_image != null)
                {
                    return _image.sprite;
                }
                return null;
            }
            set
            {
                if (_image != null)
                {
                    _image.sprite = value;
                }
                if(_shineImage != null)
                {
                    _shineImage.sprite = value;
                }
            }
        }

        public bool ShowShine
        {
            get
            {
                if (_shineImage != null)
                {
                    return _shineImage.gameObject.activeSelf;
                }
                return false;
            }
            set
            {
                if (_shineImage != null)
                {
                    _shineImage.gameObject.SetActive(value);
                }
            }
        }

        public Button Button => _button;

        public RectTransform RectTransform => _rectTransform;

        #endregion
    }
}