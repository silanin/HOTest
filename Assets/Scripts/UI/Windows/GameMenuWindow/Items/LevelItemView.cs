using UnityEngine;
using UnityEngine.UI;
using HiddenObject.Components;

namespace HiddenObject.UI.Windows
{
    public class LevelItemView : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [SerializeField]
        private Text _levelName;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private StarComponent _starComponent;
        [SerializeField]
        private Button _button;

        #endregion

        #region PUBLIC METHODS

        public void SetData(LevelItemEntity data)
        {
            Level = data.Level;
            LevelName = data.LevelName;
            LevelSprite = data.LevelSprite;
            Stars = data.Stars;
        }

        public void Clear()
        {
            Level = default;
            LevelName = string.Empty;
            LevelSprite = null;
            Stars = 0;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public int Level { get; set; }

        public string LevelName
        {
            get
            {
                if (_levelName != null)
                {
                    return _levelName.text;
                }
                return string.Empty;
            }
            set
            {
                if (_levelName != null)
                {
                    _levelName.text = value;
                }
            }
        }

        public Sprite LevelSprite
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

        public int Stars
        {
            get
            {
                if (_starComponent != null)
                {
                    return _starComponent.Stars;
                }
                return 0;
            }
            set
            {
                if (_starComponent != null)
                {
                    _starComponent.Stars = value;
                }
            }
        }

        public Button Button => _button;

        #endregion
    }
}