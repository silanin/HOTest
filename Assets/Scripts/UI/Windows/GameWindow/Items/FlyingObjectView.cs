using UnityEngine;
using UnityEngine.UI;

namespace HiddenObject.UI.Windows
{
    public class FlyingObjectView : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [SerializeField]
        private Image _image;
        [SerializeField]
        private RectTransform _rectTransform;

        #endregion

        #region PUBLIC METHODS

        public void SetPosition(RectTransform transform)
        {
            _rectTransform.position = transform.position;
            _rectTransform.localScale = transform.localScale;
            _rectTransform.localRotation = transform.localRotation;
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
            }
        }

        public RectTransform RectTransform => _rectTransform;

        #endregion
    }
}