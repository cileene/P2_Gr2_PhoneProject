using System;
using CustomUnityAnalytics;
using TMPro;
using UnityEngine;

namespace DeathApp
{
    public class DeathYearTester : MonoBehaviour
    {
        public TextMeshProUGUI resultText;

        private DeathUserInput _userInput;

        public void Awake()
        {
            // Initialize _userInput here after GameManager is likely set up
            _userInput = new DeathUserInput
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
        }

        public void Start()
        {
            int currentYear = DateTime.Now.Year;
            int deathYear = DeathCalculator.CalculateDeathYear(_userInput, currentYear);
            GameManager.Instance.playerDeathYear = deathYear;
            SaveDataManager.TriggerSave();
            Debug.Log($"Predicted Death Year: {deathYear}");

            if (deathYear < currentYear)
            {
                resultText.text = $"In the year of... wait a minute {GameManager.Instance.playerName}. \n" +
                                  $"With your lifestyle you were supposed to die in {deathYear}.\n" +
                                  "Sooo... You win!!! (I guess?)\n" +
                                  "But be careful, you'll probably die any day now.\n" +
                                  $"Thanks for playing {GameManager.Instance.playerName}!\n" +
                                  "This was fun.";
            }
            else if (deathYear == currentYear)
            {
                resultText.text = $"Wow this is exciting {GameManager.Instance.playerName}!\n" +
                                  $"You're gonna die in {deathYear}, also known as this year!\n" +
                                  $"If you wanna win the game, you'll have to start being really careful.\n" +
                                  $"You could seriously die any minute now.\n" +
                                  $"But any ways. Thanks for playing {GameManager.Instance.playerName}!\n";
            }
            else
            {
                resultText.text = $"In the year of {deathYear} you, {GameManager.Instance.playerName}, will die.\n" +
                                  $"But, if you're still alive in {deathYear + 1} then you win the game!\n" +
                                  $"Good luck {GameManager.Instance.playerName}!";
            }

            GameManager.Instance.deathGamePlayed = true;
            GameManager.Instance.progressStory = true;
            //GameManager.Instance.deathBadge = false;
            //GameManager.Instance.messagesBadge = true;
            UGSSnitch();
        }

        private void UGSSnitch()
        {
            PlayedDeathGame playedDeathGame = new PlayedDeathGame
            {
                PlayerName = GameManager.Instance.playerName,
                PlayerAge = GameManager.Instance.playerAge,
                PlayerBirthYear = GameManager.Instance.playerBirthYear,
                PlayerSmokes = GameManager.Instance.playerSmokes,
                PlayerCigarettesPerDay = GameManager.Instance.playerCigarettesPerDay,
                PlayerDrinks = GameManager.Instance.playerDrinks,
                PlayerDrinksPerWeek = GameManager.Instance.playerDrinksPerWeek,
                PlayerExerciseSessionsPerWeek = GameManager.Instance.playerExerciseSessionsPerWeek,
                PlayerDietRating = GameManager.Instance.playerDietRating,
                PlayerSleepHours = GameManager.Instance.playerSleepHours,
                PlayerRiskRating = GameManager.Instance.playerRiskRating,
                PlayerLivingEnvironment = GameManager.Instance.playerLivingEnvironment,
                PlayerFamilyHistory = GameManager.Instance.playerFamilyHistory,
                PlayerDeathYear = GameManager.Instance.playerDeathYear
            };
            Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent(playedDeathGame);
        }
    }
}