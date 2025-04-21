using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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
            SoundManager.Instance.PlaySound(homeSound);
            if (GameManager.Instance.currentScene == "Home")
            {
                SceneHandler.LoadScene("LockScreen");
            }
            else if (GameManager.Instance.currentScene == "Gyro" && GameManager.Instance.playerIsTrapped)
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