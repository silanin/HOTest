using strange.extensions.mediation.impl;

namespace HiddenObject.UI.Base
{
    public class BaseView : View
    {
        public virtual void Show()
        {
            gameObject?.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject?.SetActive(false);
        }
    }
}