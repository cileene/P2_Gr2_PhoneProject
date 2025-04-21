using UnityEngine;

//Add to the persistent UI element
namespace UIUtils
{
    public class PersistentUI : MonoBehaviour
    {
        public static PersistentUI Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}