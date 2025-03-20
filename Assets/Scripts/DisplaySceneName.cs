using UnityEngine;
using TMPro;


public class DisplaySceneName : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private void Start()
    {
        _textField = GetComponent<TextMeshProUGUI>();

        string currentSceneName = GameManager.Instance.currentScene;
        
        if (_textField != null) _textField.text = currentSceneName;
    }
}