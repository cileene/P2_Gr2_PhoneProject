using TMPro;
using UnityEngine;

namespace GalleryApp
{
    public class SecretFolderManager : MonoBehaviour
    {
        public GameObject passwordPanel;
        public TMP_InputField passwordInput;
        public GameObject secretFolderPanel;
        private string _correctPassword;
        private GameManager _gm;

        private void Start()
        {
            _gm = GameManager.Instance;
            _correctPassword = _gm.playerBirthYear.ToString();
        }

        public void OpenPasswordPrompt()
        {
            passwordPanel.SetActive(true);
        }

        public void CheckPassword()
        {
            if (passwordInput.text == _correctPassword)
            {
                _gm.seenGyroHint = true;
                
                //_gm.gyroLoading = false; // for linear test build
                //_gm.gyroBadge = true; // for linear test build
                
                secretFolderPanel.SetActive(true);
            }
            else
            {
                passwordInput.text = "";
            }

            passwordPanel.SetActive(false);
        }

        public void CloseSecretFolder()
        {
            secretFolderPanel.SetActive(false);
        }
    }
}
