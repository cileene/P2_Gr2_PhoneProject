using Unity.Services.Analytics;
using UnityEngine;

public class LockUnlock : MonoBehaviour
{
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject unlocked;
    private bool _alreadySent;

    void Update()
    {
        if (GameManager.PhoneUnlocked)
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
            SendAnalytics();
        }
    }
    
    // Send to analytics
   
    private void SendAnalytics()
    {
        if (_alreadySent) return;
        AnalyticsService.Instance.RecordEvent("lockScreenDone"); // record the custom event
        AnalyticsService.Instance.Flush(); // flush/upload the event to the server
        _alreadySent = true;
    }
}
