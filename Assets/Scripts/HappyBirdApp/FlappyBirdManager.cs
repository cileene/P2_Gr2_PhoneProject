using CustomUnityAnalytics;
using TMPro;
using UnityEngine;

namespace HappyBirdApp
{
    [DefaultExecutionOrder(-1)]
    public class FlappyBirdManager : MonoBehaviour
    {
        public static FlappyBirdManager Instance { get; private set; }

        [SerializeField] private Player player;
        [SerializeField] private Spawner spawner;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameObject popUp;

        public int Score { get; private set; } = 0;

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f; // This should fix freezing the phone
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            Pause();
            highScoreText.text = GameManager.Instance.birdHighScore.ToString();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            player.enabled = false;
        }

        public void Play()
        {
            Score = 0;
            scoreText.text = Score.ToString();

            playButton.SetActive(false);
            gameOver.SetActive(false);

            Time.timeScale = 1f;
            player.enabled = true;

            // Pipes[] pipes = Object.FindObjectsbyType<Pipes>();
//foreach (Pipes pipe in pipes)
//{
            //   Destroy(pipe.gameObject);
//}
        }

        public void GameOver()
        {
            UpdateHighScore();
            highScoreText.text = GameManager.Instance.birdHighScore.ToString();
            playButton.SetActive(true);
            gameOver.SetActive(true);

            UGSSnitch();

            Pause();
        }

        public void IncreaseScore()
        {
            Score++;
            scoreText.text = Score.ToString();
            if (GameManager.Instance.birdFriction)
            {
                Screen.brightness -= 0.1f;
                Time.timeScale += 0.1f;

                if (Score >= 5) // after winning hardmode go to new messages in paris scene
                {
                    popUp.SetActive(true);
                    GameManager.Instance.progressStory = true;
                }
                
            }
        }

        private void UpdateHighScore() // record the highscore in the GameManager
        {
            int highScore = GameManager.Instance.birdHighScore;
            if (Score > highScore)
            {
                GameManager.Instance.birdHighScore = Score;
            }
        }

        private void UGSSnitch()
        {
            PlayedBirdGame playedBirdGame = new PlayedBirdGame
            {
                HighScore = GameManager.Instance.birdHighScore,
            };
            Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent(playedBirdGame);
        }
    }
}