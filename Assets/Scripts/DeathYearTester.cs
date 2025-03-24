using System;
using TMPro;
using UnityEngine;

public class DeathYearTester : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    
    // You can set these values in the Inspector for testing
    public DeathUserInput testDeathUserInput = new DeathUserInput
    {
        age = GameManager.Instance.playerAge,
        smokes = GameManager.Instance.playerSmokes,
        cigarettesPerDay = GameManager.Instance.playerCigarettesPerDay,
        alcoholPerWeek = GameManager.Instance.playerDrinksPerWeek,
        exerciseSessionsPerWeek = GameManager.Instance.playerExerciseSessionsPerWeek,
        dietRating = GameManager.Instance.playerDietRating,
        sleepHours = GameManager.Instance.playerSleepHours,
        riskRating = GameManager.Instance.playerRiskRating,
        livingEnvironment = GameManager.Instance.playerLivingEnvironment,
        hasFamilyHistory = GameManager.Instance.playerFamilyHistory
    };

    public void Start()
    {
        int currentYear = DateTime.Now.Year;
        int deathYear = DeathCalculator.CalculateDeathYear(testDeathUserInput, currentYear);
        GameManager.Instance.playerDeathYear = deathYear;
        SaveDataManager.TriggerSave();
        Debug.Log($"Predicted Death Year: {deathYear}");
        resultText.text = $"Predicted Death Year: {deathYear}";
    }
}