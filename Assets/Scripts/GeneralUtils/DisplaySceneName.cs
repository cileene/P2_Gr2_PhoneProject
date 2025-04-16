using TMPro;
using UnityEngine;

namespace GeneralUtils
{
    public class DisplaySceneName : MonoBehaviour
    {
        private TextMeshProUGUI _textField;

        private void Awake()
        {
            _textField = GetComponent<TextMeshProUGUI>();

            string currentSceneName = GameManager.Instance.currentScene;
        
            if (_textField != null) _textField.text = currentSceneName;
        }
    }
}