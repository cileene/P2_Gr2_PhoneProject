using System;
using TMPro;
using UnityEngine;

public class DeathYearTester : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    
    // You can set these values in the Inspector for testing
    public UserInput testUserInput = new UserInput
    {
        Age = 30,
        Smokes = true,
        CigarettesPerDay = 10,
        AlcoholPerWeek = 10,
        ExerciseSessionsPerWeek = 2,
        DietRating = 4,
        SleepHours = 6,
        RiskRating = 7,
        LivingEnvironment = "Urban",
        HasFamilyHistory = true
    };

    public void Start()
    {
        int currentYear = DateTime.Now.Year;
        int deathYear = DeathCalculator.CalculateDeathYear(testUserInput, currentYear);
        Debug.Log($"Predicted Death Year: {deathYear}");
        resultText.text = $"Predicted Death Year: {deathYear}";
    }
}