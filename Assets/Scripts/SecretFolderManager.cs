using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecretFolderManager : MonoBehaviour
{
    public GameObject passwordPanel;
    public TMP_InputField passwordInput;
    public GameObject secretFolderPanel;
    private string correctPassword = "1234"; // Skift koden til hvad du vil

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
