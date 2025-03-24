using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Take UI input and assign it to a variable
public class UIInputToVar : MonoBehaviour
{
    public Slider slider;
    public InputField inputField;
    public Dropdown dropdown;
    public Button button;
    public Toggle toggle;
    public TextMeshProUGUI text;

    public string inputObject;

    private enum InputType
    {
        AgeSlider,
        SmokeToggle,
        SmokeSlider,
        DrinkToggle,
        DrinkSlider,
        ExerciseSlider,
        DietSlider,
        SleepSlider,
        RiskSlider,
        EnvironmentDropdown,
        HistoryToggle,
        PlayerName
    }

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
            case "AgeSlider":
                GameManager.Instance.playerAge = (int)slider.value;
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
        }

        SaveDataManager.TriggerSave();
    }
    
    
}