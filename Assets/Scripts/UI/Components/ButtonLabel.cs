using UnityEngine;
using UnityEngine.UI;

namespace HiddenObject.Components
{
    [ExecuteInEditMode]
    public class ButtonLabel : Button
    {
        [Space]
        [SerializeField]
        private Text _text;

        public string Text
        {
            get
            {
                if (_text != null)
                {
                    return _text.text;
                }
                return string.Empty;
            }
            set
            {
                if (_text != null)
                {
                    if (value == null) value = string.Empty;
                    if (_text.text != value)
                    {
                        _text.text = value;
                        _text.CalculateLayoutInputHorizontal();
                    }
                }
            }
        }

        public int TextFontSize
        {
            get
            {
                if (_text != null)
                {
                    return _text.fontSize;
                }
                return 0;
            }
            set
            {
                if (_text != null)
                {
                    _text.fontSize = value;
                }
            }
        }
    }
}