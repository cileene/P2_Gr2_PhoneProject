using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GalleryApp
{
    public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public ScrollRect scrollRect;
        public GalleryManager galleryManager; // Reference to GalleryManager (tried to call by making static but it didn't work)
        private Vector2 _startDragPosition;
        private bool _isDragging = false;
        private float _swipeThreshold = 50f; // Minimum swipe distance to detect

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startDragPosition = eventData.position;
            _isDragging = true;
            scrollRect.OnBeginDrag(eventData); 
        }

        public void OnDrag(PointerEventData eventData)
        {
            scrollRect.OnDrag(eventData); 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            float deltaX = eventData.position.x - _startDragPosition.x;
        
            if (Mathf.Abs(deltaX) > _swipeThreshold)
            {
                if (deltaX > 0)
                {
                    galleryManager?.PreviousImage(); // Swipe right goes to previous Image
                }
                else
                {
                    galleryManager?.NextImage(); // Swipe left goes to next Image
                }
            }
        
            scrollRect.OnEndDrag(eventData); 
        }
    }
}