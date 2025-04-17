using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public ScrollRect scrollRect;
    private Vector2 _startDragPosition;
    
    private float _swipeThreshold = 50f; // Minimum swipe distance to detect
    
    public void OnBeginDrag(PointerEventData eventData)
    {
            _startDragPosition = eventData.position;
            
            scrollRect.OnBeginDrag(eventData); 
    }
    public void OnDrag(PointerEventData eventData)
    {
            scrollRect.OnDrag(eventData); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        float deltaX = eventData.position.y - _startDragPosition.y;
        
        if (Mathf.Abs(deltaX) > _swipeThreshold)
        {
             Debug.Log("openPhone");
             SceneHandler.LoadScene("Home");
        }
        
        scrollRect.OnEndDrag(eventData); 
    }
}