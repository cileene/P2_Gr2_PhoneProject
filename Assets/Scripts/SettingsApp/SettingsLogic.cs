using System.Collections;
using UnityEngine;

namespace SettingsApp
{
    public class SettingsLogic : MonoBehaviour
    {
        [SerializeField] private GameObject parisPopUp;
        
        private void Start()
        {
            if (GameManager.Instance.currentLevel == 1 && !GameManager.Instance.parisPopUpSeen)
            {
                StartCoroutine(ShowParisPopUp());
            }

        }
        
        IEnumerator ShowParisPopUp()
        {
            yield return new WaitForSeconds(2f);
            parisPopUp.SetActive(true);
            GameManager.Instance.parisPopUpSeen = true;
            GameManager.Instance.photosBadge = false;
        }
        
    }
}