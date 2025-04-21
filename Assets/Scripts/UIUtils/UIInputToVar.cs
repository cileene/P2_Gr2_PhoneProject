using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Take UI input and assign it to a variable
namespace UIUtils
{
    public class UIInputToVar : MonoBehaviour
    {
        public Slider slider;
        public InputField inputField;
        public TMP_InputField tmpInputField;
        public Dropdown dropdown;
        public Button button;
        public Toggle toggle;
        public TextMeshProUGUI text;

        public string inputObject;

        public void Start()
        {
            slider = GetComponent<Slider>();
            inputField = GetComponent<InputField>();
            text = GetComponent<TextMeshProUGUI>();
            dropdown = GetComponent<Dropdown>();
            button = GetComponent<Button>();
            // Get the gameobjects name
            inputObject = gameObject.name;
        }

        // check the name of the gameobject and assign the value to the correct variable
        public void UpdateVariable()
        {
            switch (inputObject)
            {
                case "BirthYear":
                    // Strip all non-digit characters before parsing
                    string cleanedInput = Regex.Replace(text.text, @"\D+", "");
                    int parsedYear;
                    if (int.TryParse(cleanedInput, out parsedYear))
                    {
                        GameManager.Instance.playerBirthYear = parsedYear;
                        int age = DateTime.Now.Year - parsedYear;
                        GameManager.Instance.playerAge = age;
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid birth year input: '{text.text}'");
                    }
                    break;
                case "SmokeToggle":
                    GameManager.Instance.playerSmokes = toggle.isOn;
                    break;
                case "SmokeSlider":
                    GameManager.Instance.playerCigarettesPerDay = (int)slider.value;
                    break;
                case "DrinkToggle":
                    GameManager.Instance.playerDrinks = toggle.isOn;
                    break;
                case "DrinkSlider":
                    GameManager.Instance.playerDrinksPerWeek = (int)slider.value;
                    break;
                case "ExerciseSlider":
                    GameManager.Instance.playerExerciseSessionsPerWeek = (int)slider.value;
                    break;
                case "DietSlider":
                    GameManager.Instance.playerDietRating = (int)slider.value;
                    break;
                case "SleepSlider":
                    GameManager.Instance.playerSleepHours = (int)slider.value;
                    break;
                case "RiskSlider":
                    GameManager.Instance.playerRiskRating = (int)slider.value;
                    break;
                case "EnvironmentDropdown":
                    GameManager.Instance.playerLivingEnvironment = dropdown.options[dropdown.value].text;
                    break;
                case "HistoryToggle":
                    GameManager.Instance.playerFamilyHistory = toggle.isOn;
                    break;
                case "PlayerName":
                    GameManager.Instance.playerName = text.text;
                    break;
                case "UrbanToggle":
                    GameManager.Instance.playerLivingEnvironment = "Urban";
                    break;
                case "SuburbanToggle":
                    GameManager.Instance.playerLivingEnvironment = "Suburban";
                    break;
                case "RuralToggle":
                    GameManager.Instance.playerLivingEnvironment = "Rural";
                    break;
            }

            SaveDataManager.TriggerSave();
        }
    }
}