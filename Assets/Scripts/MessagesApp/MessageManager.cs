using System.Collections;
using System.Collections.Generic;
using System.IO;
using CustomUnityAnalytics;
using TMPro;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MessagesApp
{
    public class MessageManager : MonoBehaviour
    {
        public AudioClip messageSendSound;
        public AudioClip messageReceiveSound;
        
        public GameObject buttonChoice1;
        public GameObject buttonChoice2;
        public List<string> playerButtonMessages;
        public List<string> playerMessages;
        public List<string> botMessages;
        public List<string> loadingDots;
        public List<int> progressMessages;
        public TextMeshProUGUI playerMessagePrefab;
        public TextMeshProUGUI botMessagePrefab;
        public TextMeshProUGUI loadingDotsPrefab;
        public Transform messageContainer;
        public ScrollRect scrollRect;

        private List<int> _playerChoices = new List<int>(); // Keep track of choices made
        private int _currentChoiceIndex;
        private string _saveFileName;
        private string _saveFilePath;

        [SerializeField] private GameObject popUp;
        [SerializeField] private string nextSceneName;

        private void Start()
        {
            _saveFileName = $"conversationState_{GameManager.Instance.currentScene}.json";
            _saveFilePath = Path.Combine(GameManager.Instance.dataPath, _saveFileName);
            GameManager.Instance.messagesDataPath = _saveFilePath;
            if(GameManager.Instance.useSaveData) LoadConversationState(); // Load the previous state, if any
            ScrollToBottom();
        }

        private void Update()
        {
            //Debug.Log($"Current Choice Index: {_currentChoiceIndex}");
            if (GameManager.Instance.progressStory)
            {
                DisplayChoices(); // Show the first set of choices
            }
            else
            {
                GameManager.Instance.deathLoading = false; //TODO: this is a hack
                GameManager.Instance.deathBadge = true; //TODO: more hack
                GameManager.Instance.messagesBadge = false; //TODO: also a hack
            
                buttonChoice1.SetActive(false);
                buttonChoice2.SetActive(false); // Hide buttons if not progressing
            }
        
        }
    
    
        private void CheckProgression()
        {
            if (GameManager.Instance.progressStory)
            {
                // Check if the current choice index is within bounds
                if (_currentChoiceIndex < playerButtonMessages.Count)
                {
                    DisplayChoices();
                }
                else
                {
                    buttonChoice1.SetActive(false);
                    buttonChoice2.SetActive(false); // Hide buttons if no choices left
                }
            }
        }

        private void DisplayChoices()
        {
            if (_currentChoiceIndex < playerButtonMessages.Count)
            {
                buttonChoice1.GetComponentInChildren<TextMeshProUGUI>().text = playerButtonMessages[_currentChoiceIndex];
            }
            else
            {
                buttonChoice1.SetActive(false);
            }
        
            if (_currentChoiceIndex + 1 < playerButtonMessages.Count)
            {
                buttonChoice2.GetComponentInChildren<TextMeshProUGUI>().text = playerButtonMessages[_currentChoiceIndex + 1];
            }
            else
            {
                buttonChoice2.SetActive(false);
            }
        }

        public void OnChoice1Clicked() 
        {
            OnChoiceClicked(0);
            StartCoroutine(RunChoiceAfterDelay(0));
        }

        public void OnChoice2Clicked()
        {
            OnChoiceClicked(1);
            StartCoroutine(RunChoiceAfterDelay(1));
        }

        public void OnChoiceClicked(int choiceIndex)
        {
            _playerChoices.Add(_currentChoiceIndex + choiceIndex); // Save the second choice

            UGSSnitch(choiceIndex); // Call home to UGS
            
            SoundManager.Instance.PlaySound(messageSendSound);
        
            DisplayMessage(playerMessages[_currentChoiceIndex + choiceIndex]);
            StartCoroutine(DisplayLoadingDots());
            buttonChoice1.SetActive(false);
            buttonChoice2.SetActive(false);
            ScrollToBottom();
        }

        private void UGSSnitch(int choiceIndex)
        {
            MessageSent messageSent = new MessageSent
            {
                Conversation = GameManager.Instance.currentScene,
                CurrentChoiceIndex = _currentChoiceIndex,
                ChoiceIndex = choiceIndex
            };
        
            AnalyticsService.Instance.RecordEvent(messageSent);

        }

        IEnumerator DisplayLoadingDots()
        {
            TextMeshProUGUI loadingDotsInstance = Instantiate(loadingDotsPrefab, messageContainer);
            foreach (string dot in loadingDots)
            {
                loadingDotsInstance.text = dot;
                yield return new WaitForSeconds(0.2f);
            }

            Destroy(loadingDotsInstance.gameObject);
            ScrollToBottom();
        }
    
        IEnumerator RunChoiceAfterDelay(int choiceIndex)
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds
            SoundManager.Instance.PlaySound(messageReceiveSound);
            DisplayBotMessage(botMessages[_currentChoiceIndex + choiceIndex]);
            if (progressMessages.Contains(_currentChoiceIndex)) GameManager.Instance.progressStory = false;
            _currentChoiceIndex += 2; // Move to the next choices
            DisplayChoices();
            SaveConversationState();
            if (!GameManager.Instance.progressStory)
            {
                buttonChoice1.SetActive(false);
                buttonChoice2.SetActive(false);
            }
            else if (_currentChoiceIndex < playerButtonMessages.Count)
            {
                buttonChoice1.SetActive(true);
                buttonChoice2.SetActive(true);
            }
            else
            {
                // Wait for 2 seconds
                ScrollToBottom();
                yield return new WaitForSeconds(2f);
                
                popUp.SetActive(true); // Maybe use animation
                GameManager.Instance.currentLevel = 1;

                // Load the next scene if there are no more choices
                //SceneHandler.LoadScene(nextSceneName);
            }
            ScrollToBottom();
        
        }
        private void ScrollToBottom()
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
        private void DisplayMessage(string message) //TODO: refactor multiple repeated methods into one
        {
            TextMeshProUGUI playerMessage = Instantiate(playerMessagePrefab, messageContainer);
            playerMessage.text = message;
            StartCoroutine(GameManager.Instance.findAndEditTMPElements.DelayAndEditTMPElements());
        }

        private void DisplayBotMessage(string message)
        {
            TextMeshProUGUI botMessage = Instantiate(botMessagePrefab, messageContainer);
            botMessage.text = message;
            StartCoroutine(GameManager.Instance.findAndEditTMPElements.DelayAndEditTMPElements());
        }

        // Save the current conversation state to a JSON file
        private void SaveConversationState()
        {
            SaveData saveData = new SaveData
            {
                CurrentChoiceIndex = _currentChoiceIndex,
                PlayerChoices = _playerChoices
            };

            string json = JsonUtility.ToJson(saveData, true);
            if (GameManager.Instance.obfuscateData)
            {
                byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
                json = System.Convert.ToBase64String(jsonBytes);
            }

            File.WriteAllText(_saveFilePath, json);
        }

        // Load the saved conversation state
        private void LoadConversationState()
        {
            if (File.Exists(_saveFilePath))
            {
                string json = File.ReadAllText(_saveFilePath);
                if (GameManager.Instance.obfuscateData)
                {
                    byte[] decodedBytes = System.Convert.FromBase64String(json);
                    json = System.Text.Encoding.UTF8.GetString(decodedBytes);
                }

                SaveData saveData = JsonUtility.FromJson<SaveData>(json);

                _currentChoiceIndex = saveData.CurrentChoiceIndex;
                _playerChoices = new List<int>(saveData.PlayerChoices);

                RestorePreviousMessages();
            }
        }

        // Restore previous messages from saved choices
        private void RestorePreviousMessages()
        {
            foreach (int choiceIndex in _playerChoices)
            {
                if (choiceIndex < playerMessages.Count)
                {
                    DisplayMessage(playerMessages[choiceIndex]);
                    DisplayBotMessage(botMessages[choiceIndex]);
                }
            }
        }

        // Reset the conversation and delete the save file
        public void ResetConversation()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string saveFileName = $"conversationState_{sceneName}.json";
            string path = Path.Combine(GameManager.Instance.dataPath, saveFileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            _currentChoiceIndex = 0;
            _playerChoices.Clear();
            progressMessages.Clear();
            DisplayChoices();
        }

        // SaveData class for serializing the conversation state
        [System.Serializable]
        public class SaveData
        {
            public int CurrentChoiceIndex;
            public List<int> PlayerChoices;
            public List<bool> ProgressMessages;
        }
    }
}