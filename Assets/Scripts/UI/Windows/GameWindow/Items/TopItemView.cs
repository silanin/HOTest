using UnityEngine;
using UnityEngine.UI;

namespace HiddenObject.UI.Windows
{
    public class TopItemView : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [SerializeField]
        private Text _name;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private GameObject _highlightedBackground;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private RectTransform _rectTransform;

        #endregion

        #region PUBLIC METHODS

        public void SetData(TopItemEntity data)
        {
            Id = data.Id;
            Name = data.Name;
            Sprite = data.Sprite;
        }

        public void Clear()
        {
            Id = default;
            Name = string.Empty;
            Sprite = null;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public int Id { get; set; }

        public string Name
        {
            get
            {
                if (_name != null)
                {
                    return _name.text;
                }
                return string.Empty;
            }
            set
            {
                if (_name != null)
                {
                    _name.text = value;
                }
            }
        }

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

        public bool ShowHighlited
        {
            get
            {
                if (_highlightedBackground != null)
                {
                    return _highlightedBackground.activeSelf;
                }
                return false;
            }
            set
            {
                if (_highlightedBackground != null)
                {
                    _highlightedBackground.SetActive(value);
                }
            }
        }

        public Button Button => _button;

        public RectTransform RectTransform => _rectTransform;

        #endregion
    }
}