using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace GalleryApp
{
    public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public ScrollRect scrollRect;
        public GalleryManager galleryManager; // Reference to GalleryManager (tried to call by making static but it didn't work)
        public bool swipeEnabled = true;
        private Vector2 _startDragPosition;
        
        private float _swipeThreshold = 50f; // Minimum swipe distance to detect
        private float _zoomSpeed = 0.01f;
        private float _minZoom = 0.5f;
        private float _maxZoom = 4f;
        private RectTransform _contentRect;
        private float? lastPinchDistance = null;
        private float _originalZoom;

        void Start()
        {
            _contentRect = scrollRect.content;
            if (_contentRect != null)
            {
                _originalZoom = _contentRect.localScale.y;
                swipeEnabled = true; // Ensure swiping is enabled at start
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!swipeEnabled) return;
            _startDragPosition = eventData.position;
          
            scrollRect.OnBeginDrag(eventData); 
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!swipeEnabled) return;
            scrollRect.OnDrag(eventData); 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!swipeEnabled) return;
          
            float deltaX = eventData.position.x - _startDragPosition.x;
            float deltaY = eventData.position.y - _startDragPosition.y;
        
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
            if (Mathf.Abs(deltaY) > _swipeThreshold)
            {
                galleryManager?.CloseImageView();
            }
        
            scrollRect.OnEndDrag(eventData); 
        }

        private void Update()
        {
            // Zoom with mouse scroll wheel
            if (Mouse.current != null)
            {
                float scrollValue = Mouse.current.scroll.ReadValue().y;
                if (Mathf.Abs(scrollValue) > 0.01f)
                {
                    Zoom(scrollValue * _zoomSpeed);
                }
            }
            // Zoom using keyboard arrow keys for testing
            if (Keyboard.current != null)
            {
                if (Keyboard.current.upArrowKey.isPressed)
                {
                    Zoom(0.05f);
                }
                if (Keyboard.current.downArrowKey.isPressed)
                {
                    Zoom(-0.05f);
                }
            }
 
            // Pinch zoom on touch devices
            if (Touchscreen.current != null && Touchscreen.current.touches.Count >= 2)
            {
                var touch0 = Touchscreen.current.touches[0];
                var touch1 = Touchscreen.current.touches[1];

                if (touch0.isInProgress && touch1.isInProgress)
                {
                    scrollRect.enabled = false;
                    swipeEnabled = false;
                    Vector2 pos0 = touch0.position.ReadValue();
                    Vector2 pos1 = touch1.position.ReadValue();
                    float currentDistance = Vector2.Distance(pos0, pos1);

                    if (!lastPinchDistance.HasValue)
                    {
                        lastPinchDistance = currentDistance;
                        
                    }
                    else
                    {
                        float deltaDistance = currentDistance - lastPinchDistance.Value;
                        Zoom(deltaDistance * _zoomSpeed);
                        lastPinchDistance = currentDistance;
                        scrollRect.enabled = true;
                    }
                }
            }
            else
            {
                lastPinchDistance = null;
                swipeEnabled = true;
            }
        }

        private void Zoom(float increment)
        {
            if (_contentRect == null) return;

            float currentZoom = _contentRect.localScale.y;
            float newZoom = Mathf.Clamp(currentZoom + increment, _originalZoom, _maxZoom);

            _contentRect.localScale = Vector3.one * newZoom;

            // Disable swipe when zoomed in
            swipeEnabled = Mathf.Approximately(newZoom, _originalZoom);
        }
    }
}