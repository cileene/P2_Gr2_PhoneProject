using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour
{
    public GameObject buttonChoice1;
    public GameObject buttonChoice2;
    public List<string> playerButtonMessages;
    public List<string> playerMessages;
    public List<string> botMessages;
    public List<string> loadingDots;
    public TextMeshProUGUI playerMessagePrefab;
    public TextMeshProUGUI botMessagePrefab;
    public TextMeshProUGUI loadingDotsPrefab;
    public Transform messageContainer;
    public List<int> progressMessages;

    private int _currentChoiceIndex;
    private List<int> _playerChoices = new List<int>(); // Keep track of choices made
    private string _saveFileName;
    private string _saveFilePath;

    private void Start()
    {
        _saveFileName = $"conversationState_{GameManager.Instance.currentScene}.json";
        _saveFilePath = Path.Combine(GameManager.Instance.dataPath, _saveFileName);
        LoadConversationState(); // Load the previous state, if any
    }

    private void Update()
    {
        Debug.Log($"Current Choice Index: {_currentChoiceIndex}");
        if (GameManager.Instance.progressStory)
        {
            DisplayChoices(); // Show the first set of choices
        }
        else
        {
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
        DisplayMessage(playerMessages[_currentChoiceIndex + choiceIndex]);
        StartCoroutine(DisplayLoadingDots());
        buttonChoice1.SetActive(false);
        buttonChoice2.SetActive(false);
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
    }
    
    IEnumerator RunChoiceAfterDelay(int choiceIndex)
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
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
    }
    

    private void DisplayMessage(string message) //TODO: refactor multiple repeated methods into one
    {
        TextMeshProUGUI playerMessage = Instantiate(playerMessagePrefab, messageContainer);
        playerMessage.text = message;
    }

    private void DisplayBotMessage(string message)
    {
        TextMeshProUGUI botMessage = Instantiate(botMessagePrefab, messageContainer);
        botMessage.text = message;
    }

    // Save the current conversation state to a JSON file
    private void SaveConversationState()
    {
        SaveData saveData = new SaveData
        {
            CurrentChoiceIndex = _currentChoiceIndex,
            PlayerChoices = _playerChoices
        };

        string json = JsonUtility.ToJson(saveData, true); //TODO: centralize json handling
        File.WriteAllText(_saveFilePath, json);
    }

    // Load the saved conversation state
    private void LoadConversationState()
    {
        if (File.Exists(_saveFilePath))
        {
            string json = File.ReadAllText(_saveFilePath);
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
        string path = Path.Combine(Application.persistentDataPath + "/Player_Data/", saveFileName);

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