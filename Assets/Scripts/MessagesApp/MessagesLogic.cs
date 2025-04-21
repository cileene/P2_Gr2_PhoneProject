using UnityEngine;

namespace MessagesApp
{
    public class MessagesLogic : MonoBehaviour
    {
        [SerializeField] private GameObject parisMessages;
        
        private void Start()
        {
            if (GameManager.Instance.lastSandraMessage == true)
            {
                parisMessages.SetActive(true);
            }
        }
    }
}