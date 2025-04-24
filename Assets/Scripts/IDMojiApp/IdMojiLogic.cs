using UnityEngine;

namespace IDMojiApp
{
    public class IdMojiLogic : MonoBehaviour
    {
        [SerializeField] private GameObject popUpPanel;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Confirm()
        {
            if (GameManager.Instance.iDMojiCreated) return;
            
            GameManager.Instance.iDMojiCreated = true;
            GameManager.Instance.progressStory = true;
            popUpPanel.SetActive(true);
        }


    }
}