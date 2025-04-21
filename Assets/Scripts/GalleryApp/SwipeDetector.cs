using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace GalleryApp
{
    public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public ScrollRect scrollRect;

        public GalleryManager
            galleryManager; // Reference to GalleryManager (tried to call by making static but it didn't work)

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

        public void zoomIn()
        {
            Zoom(0.5f);
        }

        public void zoomOut()
        {
            Zoom(-0.5f);
        }

        private void Zoom(float increment) //TODO: smooth the zoom behaviour
        {
            if (_contentRect == null) return;
            // Center pivot so zoom occurs around the center
            _contentRect.pivot = new Vector2(0.5f, 0.5f);

            float currentZoom = _contentRect.localScale.y;
            float newZoom = Mathf.Clamp(currentZoom + increment, _originalZoom, _maxZoom);

            _contentRect.localScale = Vector3.one * newZoom;

            // Disable swipe when zoomed in
            swipeEnabled = Mathf.Approximately(newZoom, _originalZoom);
        }
    }
}