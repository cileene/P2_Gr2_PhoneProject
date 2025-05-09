using TMPro;
using UnityEngine;

namespace HappyBirdApp
{
    public class SetPopUpText : MonoBehaviour
    {
        private void Start()
        {
            // Set the text of the pop-up to the current score
            TextMeshProUGUI popUpText = GetComponent<TextMeshProUGUI>(); //TODO: Fix this
            popUpText.text = $"Well done {GameManager.Instance.playerName}! you should probably go back to Paris..";
            //GameManager.Instance.photosBadge = true; // for linear test build
        }
    }
}