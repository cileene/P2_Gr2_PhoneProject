using System;
using UnityEngine;
using UnityEngine.UI;

namespace GeneralUtils
{
    public class ChangeLanguage : MonoBehaviour
    {
        public Toggle englishToggle;
        public Toggle gibberishToggle;
        private bool isInitializing = false;

        public void Start()
        {
            isInitializing = true;
            // Initialize the toggles based on the current language setting
            if (GameManager.Instance.textFriction)
            {
                gibberishToggle.isOn = true;
                englishToggle.isOn = false;
            }
            else
            {
                englishToggle.isOn = true;
                gibberishToggle.isOn = false;
            }
            isInitializing = false;
            englishToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });
            gibberishToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });
        }

        public void ChangeToEnglish()
        {
            // Set the language to English
            GameManager.Instance.textFriction = false;
            Debug.Log("Language changed to English");
        }

        public void ChangeToGibberish()
        {
            // Set the language to Gibberish
            GameManager.Instance.textFriction = true;
            Debug.Log("Language changed to Gibberish");
        }
        
        public void OnToggleChanged()
        {
            if (isInitializing)
                return;

            // Check which toggle is active and call the appropriate method
            if (englishToggle.isOn)
            {
                ChangeToEnglish();
                SceneHandler.LoadScene(GameManager.Instance.currentScene); // Reload the current scene to apply changes
            }
            else if (gibberishToggle.isOn)
            {
                ChangeToGibberish();
                SceneHandler.LoadScene(GameManager.Instance.currentScene); // Reload the current scene to apply changes
            }
        }
    }
}