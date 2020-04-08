using UnityEngine;
using UnityEngine.EventSystems;

namespace HiddenObject.UI.Windows
{
    public class DraggableMask : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        private Vector3 _startPosition;
        private Vector3 _offsetToMouse;

#if UNITY_STANDALONE || UNITY_EDITOR
        private Vector3 mousePosition;
        public float moveSpeed = 0.1f;
#endif

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.touchCount > 1)
                return;

#if UNITY_IOS || UNITY_ANDROID
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)) + _offsetToMouse;
#endif
        }

#if UNITY_STANDALONE || UNITY_EDITOR
        void Update()
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
#endif
    }
}