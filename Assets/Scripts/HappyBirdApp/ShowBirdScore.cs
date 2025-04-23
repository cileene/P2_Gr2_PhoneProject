using TMPro;
using UnityEngine;

namespace HappyBirdApp
{
    public class ShowBirdScore : MonoBehaviour
    {
        private void Start()
        {
            TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                Debug.LogError("No TextMeshProUGUI component found on the GameObject.");
                return;
            }
            textComponent.text = GameManager.Instance.birdHighScore.ToString();
        }
    }
}