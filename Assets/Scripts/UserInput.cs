using System;

[Serializable]
public class UserInput
{
    public int Age;
    public bool Smokes;
    public float CigarettesPerDay;
    public float AlcoholPerWeek;
    public int ExerciseSessionsPerWeek;
    public int DietRating;          // Scale: 1 (poor) to 10 (excellent)
    public float SleepHours;
    public int RiskRating;          // Scale: 1 (very cautious) to 10 (very risky)
    public string LivingEnvironment; // e.g., "Urban", "Suburban", "Rural"
    public bool HasFamilyHistory;
}