using UnityEngine;

namespace GyroApp
{
    public class CheckRotation : MonoBehaviour
    {
        [SerializeField] private GameObject endPopupPanel;
        
        private void Update()
        {
            if (!GameManager.Instance.seenGyroHint) return;

            Invoke(nameof(DelayedCheckRotation), 2f);
        }

        private void DelayedCheckRotation()
        {
            // Check if the phone is in a specific rotation
            if (transform.rotation.eulerAngles.x is > 30 and < 150)
            {
                // wait three seconds, then call the method
                Invoke(nameof(CodeUnlocked), 3f);
            }
        }

        private void CodeUnlocked()
        {
            Debug.Log($"YES!!");
            GameManager.Instance.gyroCodeSeen = true;
            GameManager.Instance.playerIsTrapped = false;
            endPopupPanel.SetActive(true);
            Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent("gyroCodeSeen");
            
            enabled = false; // This should fix duplicate analytics events
        }
    }
}
