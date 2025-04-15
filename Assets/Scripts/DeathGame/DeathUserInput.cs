using System;

namespace DeathGame
{
    [Serializable]
    public class DeathUserInput
    {
        public int age;
        public bool smokes;
        public float cigarettesPerDay;
        public float alcoholPerWeek;
        public int exerciseSessionsPerWeek;
        public int dietRating;          // Scale: 1 (poor) to 10 (excellent)
        public float sleepHours;
        public int riskRating;          // Scale: 1 (very cautious) to 10 (very risky)
        public string livingEnvironment; // e.g., "Urban", "Suburban", "Rural"
        public bool hasFamilyHistory;
    }
}