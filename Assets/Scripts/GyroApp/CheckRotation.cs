using UnityEngine;

namespace GyroApp
{
    public class CheckRotation : MonoBehaviour
    {
        private void Update()
        {
            //if (GameManager.Instance.phoneUnlocked) return;
            Invoke(nameof(DelayedCheckRotation), 2f);
        }

        private void DelayedCheckRotation()
        {
            // Check if the phone is in a specific rotation
            if (transform.rotation.eulerAngles.x is > 30 and < 150)
            {
                // wait one second, then unlock the phone
                Invoke(nameof(CodeUnlocked), 1f);
            }
        }

        private void CodeUnlocked()
        {
            Debug.Log($"YES!!");
            GameManager.Instance.gyroCodeSeen = true;
            GameManager.Instance.playerIsTrapped = false;
        }
    }
}
