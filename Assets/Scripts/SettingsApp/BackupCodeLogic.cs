using UnityEngine;
using TMPro;

namespace SettingsApp
{
    public class BackupCodeLogic : MonoBehaviour
    {
        [SerializeField] private TMP_InputField codeInputField;
        [SerializeField] private GameObject popUp;
        private int inputCode;
        private string fileName;
        private string textToSave;
        
        public void CheckCode()
        {
            // Parse the input and abort if invalid
            if (!ReadValue())
                return;

            // Verify popup reference
            if (popUp == null)
            {
                Debug.LogWarning("BackupCodeLogic: popUp reference not assigned.");
                return;
            }

            // Compare parsed code against stored birth year
            if (inputCode == GameManager.Instance.playerBirthYear)
            {
                popUp.SetActive(true);
                SetFileNameAndText();
                SaveTextFileToDevice.SaveTextFile(fileName, textToSave);
            }
            else
            {
                Debug.Log("BackupCodeLogic: Code did not match.");
            }
        }
        
        private bool ReadValue()
        {
            if (int.TryParse(codeInputField.text, out int codeInt))
            {
                inputCode = codeInt;
                Debug.Log("Parsed value: " + inputCode);
                return true;
            }
            Debug.LogWarning($"Could not parse '{codeInputField.text}' as an integer.");
            return false;
        }

        private void SetFileNameAndText()
        {
            // Set the file name and text to save
            fileName = "BACKUP.txt";
            //TODO: Fill in final narrative here
            textToSave =
                $"Here we'll write all sort of stuff about {GameManager.Instance.playerName} and then go to Paris!";
        }
    }
}