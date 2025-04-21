using TMPro;
using UnityEngine;

namespace HappyBirdApp
{
    public class SetPopUpText : MonoBehaviour
    {
        private void Start()
        {
            // Set the text of the pop-up to the current score
            TextMeshProUGUI popUpText = GetComponent<TextMeshProUGUI>();
            popUpText.text = $"Insert text about {GameManager.Instance.playerName} and then go to Paris!";
        }
    }
}