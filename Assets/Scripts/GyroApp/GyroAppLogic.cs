using UnityEngine;

namespace GyroApp
{
    public class GyroAppLogic : MonoBehaviour
    {
        private void Start()
        {
            if (GameManager.Instance.seenGyroHint)
            {
                GameManager.Instance.playerIsTrapped = true;
            }
        }

    }
}
