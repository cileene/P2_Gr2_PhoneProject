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
        private GameManager _gm;

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
        
        private void Start()
        {
            _gm = GameManager.Instance;
            
            Pause();
            highScoreText.text = _gm.birdHighScore.ToString();
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f; // This should fix freezing the phone
            if (Instance == this)
            {
                Instance = null;
            }
        }
        
        public void Pause()
        {
            Time.timeScale = 0f;
            player.enabled = false;
        }

        public void Play()
        {
            Score = 0;
            if (_gm.birdFriction)
            {
                scoreText.text = $"{Score.ToString()}/5";
            }
            else
            {
                scoreText.text = Score.ToString();
            }


            playButton.SetActive(false);
            gameOver.SetActive(false);

            Time.timeScale = 1f;
            player.enabled = true;
        }

        public void GameOver()
        {
            UpdateHighScore();
            highScoreText.text = _gm.birdHighScore.ToString();
            playButton.SetActive(true);
            gameOver.SetActive(true);

            UGSSnitch();

            Pause();
        }

        public void IncreaseScore()
        {
            Score++;
            if (_gm.birdFriction)
            {
                scoreText.text = $"{Score.ToString()}/5";
                Screen.brightness -= 0.1f;
                Time.timeScale += 0.1f;

                if (Score >= 5) // after winning hardmode go to new messages in paris scene
                {
                    popUp.SetActive(true);
                    _gm.progressStory = true;
                }
                
            }
            else
            {
                scoreText.text = Score.ToString();
            }
        }

        private void UpdateHighScore() // record the highscore in the GameManager
        {
            int highScore = _gm.birdHighScore;
            if (Score > highScore)
            {
                _gm.birdHighScore = Score;
            }
        }

        private void UGSSnitch()
        {
            PlayedBirdGame playedBirdGame = new PlayedBirdGame
            {
                HighScore = _gm.birdHighScore,
            };
            
            Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent(playedBirdGame);
        }
    }
}