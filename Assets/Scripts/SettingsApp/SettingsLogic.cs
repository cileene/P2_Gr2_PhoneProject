using System.Collections;
using UnityEngine;

namespace SettingsApp
{
    public class SettingsLogic : MonoBehaviour
    {
        [SerializeField] private GameObject parisPopUp;
        private GameManager _gm;
        
        private void Start()
        {
            _gm = GameManager.Instance;
            
            // if (_gm.currentLevel == 1 && !_gm.parisPopUpSeen)
            // {
            //     StartCoroutine(ShowParisPopUp());
            // }
        }
        
        IEnumerator ShowParisPopUp()
        {
            yield return new WaitForSeconds(2f);
            //parisPopUp.SetActive(true);
            //_gm.parisPopUpSeen = true;
            //_gm.settingsBadge = false;
            //_gm.photosLoading = false; // for linear test build
            //_gm.photosBadge = true; // for linear test build
        }
        
    }
}