using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIUtils
{
    public class SwipeUnlock : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private GameObject popUp;
        public AudioClip homeSound;
        public ScrollRect scrollRect;
        private Vector2 _startDragPosition;
        private float _swipeThreshold = 50f; // Minimum swipe distance to detect

        private void Start()
        {
            if (popUp == null)
            {
                // ignore
                Debug.LogWarning("PopUp GameObject is not assigned in the inspector.");
                if (scrollRect == null)
                {
                    Debug.LogWarning("SwipeUnlock: ScrollRect reference is not assigned.");
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData) // called when drag starts
        {
            _startDragPosition = eventData.position; // Store the initial position of the drag

            scrollRect.OnBeginDrag(eventData); // Call the ScrollRect's OnBeginDrag method to handle the drag event
        }

        public void OnDrag(PointerEventData eventData) // called when dragging
        {
            scrollRect.OnDrag(eventData); // Call the ScrollRect's OnDrag method to handle the drag event
        }

        public void OnEndDrag(PointerEventData eventData) // called when drag ends
        {
            float deltaY = eventData.position.y - _startDragPosition.y; // Calculate the difference in the y-axis

            if (Mathf.Abs(deltaY) > _swipeThreshold) // check swipe is above swipethreshold
            {
                Debug.Log("openPhone");
                SoundManager.Instance.PlaySound(homeSound);
                if (GameManager.Instance.currentScene == "Home")
                {
                    SceneHandler.LoadScene("LockScreen");
                }
                else if (GameManager.Instance.currentScene == "Gyro" && GameManager.Instance. playerIsTrapped)
                {
                    if (popUp != null)
                    {
                        popUp.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("SwipeUnlock: popUp reference not assigned.");
                    }
                }
                else
                {
                    SceneHandler.LoadScene("Home");
                }
            }

            if (scrollRect != null)
            {
                scrollRect.OnEndDrag(eventData);
            }
            else
            {
                Debug.LogWarning("SwipeUnlock: ScrollRect reference not assigned on EndDrag.");
            }
        }
    }
}