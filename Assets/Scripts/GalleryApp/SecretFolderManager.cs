using TMPro;
using UnityEngine;

namespace GalleryApp
{
    public class SecretFolderManager : MonoBehaviour
    {
        public GameObject passwordPanel;
        public TMP_InputField passwordInput;
        public GameObject secretFolderPanel;
        public string correctPassword = "1234"; //TODO: set to birthyear

        public void OpenPasswordPrompt()
        {
            passwordPanel.SetActive(true);
        }

        public void CheckPassword()
        {
            if (passwordInput.text == correctPassword)
            {
                secretFolderPanel.SetActive(true);
                passwordPanel.SetActive(false);
            }
            else
            {
                passwordInput.text = "";
                passwordPanel.SetActive(false);
            }
        }

        public void CloseSecretFolder()
        {
            secretFolderPanel.SetActive(false);
        }
    }
}
