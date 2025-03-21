using System;
using TMPro;
using UnityEngine;

public class DeathYearTester : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    
    // You can set these values in the Inspector for testing
    public DeathUserInput testDeathUserInput = new DeathUserInput
    {
        age = 30,
        smokes = true,
        cigarettesPerDay = 10,
        alcoholPerWeek = 10,
        exerciseSessionsPerWeek = 2,
        dietRating = 4,
        sleepHours = 6,
        riskRating = 7,
        livingEnvironment = "Urban",
        hasFamilyHistory = true
    };

    public void Start()
    {
        int currentYear = DateTime.Now.Year;
        int deathYear = DeathCalculator.CalculateDeathYear(testDeathUserInput, currentYear);
        Debug.Log($"Predicted Death Year: {deathYear}");
        resultText.text = $"Predicted Death Year: {deathYear}";
    }
}