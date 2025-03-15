using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour
{
    public GameObject choiceSprite1;
    public GameObject choiceSprite2;
    public List<string> playerMessages;
    public List<string> botMessages;
    public TextMeshProUGUI playerMessagePrefab;
    public TextMeshProUGUI botMessagePrefab;
    public Transform messageContainer;

    private int _currentChoiceIndex = 0;
    private List<int> _playerChoices = new List<int>(); // Keep track of choices made

    void Start()
    {
        LoadConversationState(); // Load the previous state, if any
        DisplayChoices(); // Show the first set of choices
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void DisplayChoices()
    {
        // Show choices, handle index out of bounds
        if (_currentChoiceIndex < playerMessages.Count)
        {
            choiceSprite1.GetComponentInChildren<TextMeshProUGUI>().text = playerMessages[_currentChoiceIndex];
            // Check if there is a second choice before accessing it
            if (_currentChoiceIndex + 1 < playerMessages.Count)
            {
                choiceSprite2.GetComponentInChildren<TextMeshProUGUI>().text = playerMessages[_currentChoiceIndex + 1];
            }
            else
            {
                choiceSprite2.GetComponentInChildren<TextMeshProUGUI>().text = ""; // No second choice, set empty
            }
        }
        else
        {
            choiceSprite1.SetActive(false);
            choiceSprite2.SetActive(false);
        }
    }

    public void OnChoice1Clicked()
    {
        _playerChoices.Add(_currentChoiceIndex); // Save the choice
        DisplayMessage(playerMessages[_currentChoiceIndex]);
        DisplayBotMessage(botMessages[_currentChoiceIndex]);

        _currentChoiceIndex += 2; // Move to the next choices
        SaveConversationState();
        DisplayChoices(); // Show the next set of choices
    }

    public void OnChoice2Clicked()
    {
        _playerChoices.Add(_currentChoiceIndex + 1); // Save the second choice
        DisplayMessage(playerMessages[_currentChoiceIndex + 1]);
        DisplayBotMessage(botMessages[_currentChoiceIndex + 1]);

        _currentChoiceIndex += 2; // Move to the next choices
        SaveConversationState();
        DisplayChoices(); // Show the next set of choices
    }

    private void DisplayMessage(string message)
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
        string sceneName = SceneManager.GetActiveScene().name;
        string saveFileName = $"conversationState_{sceneName}.json";

        SaveData saveData = new SaveData
        {
            CurrentChoiceIndex = _currentChoiceIndex,
            PlayerChoices = _playerChoices
        };

        string json = JsonUtility.ToJson(saveData, true);
        string path = Path.Combine(Application.persistentDataPath + "/Player_Data/", saveFileName);
        File.WriteAllText(path, json);
    }

    // Load the saved conversation state
    private void LoadConversationState()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string saveFileName = $"conversationState_{sceneName}.json";
        string path = Path.Combine(Application.persistentDataPath + "/Player_Data/", saveFileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
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

    // Reset the conversation on pressing R
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetConversation();
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
        DisplayChoices();
    }

    // SaveData class for serializing the conversation state
    [System.Serializable]
    public class SaveData
    {
        public int CurrentChoiceIndex;
        public List<int> PlayerChoices;
    }
}
