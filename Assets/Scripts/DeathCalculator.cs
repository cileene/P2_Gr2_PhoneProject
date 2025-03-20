using System;
using UnityEngine;

public static class DeathCalculator
{
    // Constants used in the algorithm
    private const int BaseExpectancy = 80;
    private const float ModerateAlcoholThreshold = 7f; // moderate threshold (drinks per week)
    private const float AlcoholPenaltyFactor = 0.5f;     // penalty per drink above threshold
    private const int DietBonusThreshold = 5;            // neutral diet rating value
    private const float DietMultiplier = 0.5f;           // bonus years per diet point above threshold
    private const float DietPenaltyMultiplier = 0.5f;      // penalty years per diet point below threshold
    private const float SleepPenaltyFactor = 2f;         // penalty if sleep is below 7 hours
    private const float RiskFactor = 0.5f;               // penalty per risk point above 5
    private const int UrbanPenalty = 2;                  // penalty for living in an urban environment
    private const int SuburbanPenalty = 1;               // penalty for living in a suburban area
    private const int FamilyHistoryPenalty = 3;          // penalty if there is a family history of serious illness

    public static int CalculateDeathYear(UserInput userInput, int currentYear)
    {
        // Start with a base remaining life expectancy (in years)
        float remainingYears = BaseExpectancy - userInput.Age;

        // ----- Lifestyle Adjustments -----
        // Smoking penalty: subtract 5 years for every 10 cigarettes per day
        if (userInput.Smokes)
        {
            remainingYears -= 5f * (userInput.CigarettesPerDay / 10f);
        }

        // Alcohol consumption: subtract years for drinking above the moderate threshold
        if (userInput.AlcoholPerWeek > ModerateAlcoholThreshold)
        {
            remainingYears -= AlcoholPenaltyFactor * (userInput.AlcoholPerWeek - ModerateAlcoholThreshold);
        }

        // Exercise bonus: add bonus years if exercising at least 3 times a week
        if (userInput.ExerciseSessionsPerWeek >= 3)
        {
            remainingYears += 3f;
        }

        // Diet: adjust remaining years based on diet rating (scale 1-10)
        if (userInput.DietRating > DietBonusThreshold)
        {
            remainingYears += (userInput.DietRating - DietBonusThreshold) * DietMultiplier;
        }
        else
        {
            remainingYears -= (DietBonusThreshold - userInput.DietRating) * DietPenaltyMultiplier;
        }

        // Sleep: subtract years if sleep hours are below recommended 7 hours
        if (userInput.SleepHours < 7f)
        {
            remainingYears -= SleepPenaltyFactor;
        }

        // ----- Risk & Environmental Adjustments -----
        // High risk-taking: subtract years for risk ratings above 5
        if (userInput.RiskRating > 5)
        {
            remainingYears -= RiskFactor * (userInput.RiskRating - 5);
        }

        // Living environment adjustments: urban and suburban might add stress factors
        if (userInput.LivingEnvironment.Equals("Urban", StringComparison.OrdinalIgnoreCase))
        {
            remainingYears -= UrbanPenalty;
        }
        else if (userInput.LivingEnvironment.Equals("Suburban", StringComparison.OrdinalIgnoreCase))
        {
            remainingYears -= SuburbanPenalty;
        }

        // Family medical history: subtract years if there is a history of serious illnesses
        if (userInput.HasFamilyHistory)
        {
            remainingYears -= FamilyHistoryPenalty;
        }

        // ----- Random Chance Element -----
        // Introduce a random adjustment to simulate unpredictable events (using UnityEngine.Random)
        int randomAdjustment = UnityEngine.Random.Range(-5, 6); // returns an integer between -5 and 5 (inclusive)
        remainingYears += randomAdjustment;

        // Calculate the predicted death year
        int predictedDeathYear = currentYear + Mathf.RoundToInt(remainingYears);

        return predictedDeathYear;
    }
}