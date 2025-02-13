using UnityEngine;

public class CheckRotation : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.PhoneUnlocked) return;
        Invoke(nameof(DelayedCheckRotation), 2f);
    }

    private void DelayedCheckRotation()
    {
        // Check if the phone is in a specific rotation
        if (transform.rotation.eulerAngles.x is > 345 or < 5)
        {
            // wait one second, then unlock the phone
            Invoke(nameof(UnlockPhone), 1f);
        }
    }

    private void UnlockPhone()
    {
        if (GameManager.PhoneUnlocked) return;
        GameManager.PhoneUnlocked = true;
        Debug.Log($"Phone unlocked! {GameManager.PhoneUnlocked}");
    }
}
