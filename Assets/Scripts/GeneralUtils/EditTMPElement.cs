using TMPro;
using UnityEngine;

namespace GeneralUtils
{
    public class EditTMPElement : MonoBehaviour
    {
        private void Start()
        {
            TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
            
            if (tmp != null && GameManager.Instance.textFriction)
            {
                tmp.text = Gibberishifier.ToGibberish(tmp.text);
            }
            else if (tmp != null && !GameManager.Instance.textFriction)
            {
                return;
            }
            else
            {
                Debug.LogWarning("TextMeshProUGUI component not found on this GameObject.");
            }
            
        }
    }
}