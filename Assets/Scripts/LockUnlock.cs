using Unity.Services.Analytics;
using UnityEngine;

public class LockUnlock : MonoBehaviour
{
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject unlocked;
    private bool _alreadySent;
    
    private void Start()
    {
        UGSSceneTransition.HandleSceneCustomEvent("LockScreen");
    }

    private void Update()
    {
        if (GameManager.Instance.phoneUnlocked)
        {
            locked.SetActive(false);
            unlocked.SetActive(true);
        }
    }
}
