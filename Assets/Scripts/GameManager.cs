using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

// Here we track the game state using a singleton pattern
// And now we also write data to the player's device

[Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool useSaveData = true;

    [Header("Progression")] 
    public bool phoneUnlocked;
    public bool shuffleHomeScreen;
    public int currentLevel;
    public string currentScene; // Default scene
    public bool progressStory;

    public string dataPath;
    public string SaveData { get; private set; }
    private const string FileName = "SaveData.json";
    public string selfiePicture;
    
    [Header("Message State")]
    public int currentChoiceIndex;
    public List<int> playerChoices;
    
    [Header("Player Info")]
    public string playerName;
    public int playerAge;
    public bool playerSmokes;
    public int playerCigarettesPerDay;
    public bool playerDrinks;
    public int playerDrinksPerWeek;
    public int playerExerciseSessionsPerWeek;
    public int playerDietRating;
    public int playerSleepHours;
    public int playerRiskRating;
    public string playerLivingEnvironment;
    public bool playerFamilyHistory;
    public int playerDeathYear;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitFileSystem();
            InitSaveData();
            InitAnalytics();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //TODO: Make a separate class for handling scene management
    private void Start()
    {
        // Load the current scene from the save data
        if (!string.IsNullOrEmpty(currentScene))
        {
            SceneManager.LoadSceneAsync(currentScene);
        }
        else
        {
            Debug.LogWarning("No current scene found in save data.");
        }
    }

    private void InitAnalytics()
    {
        Instantiate(Resources.Load("Prefabs/UnityAnalytics"));
        Debug.Log("Analytics initialized");
    }

    public void InitFileSystem()
    {
        dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(dataPath);

        if (Directory.Exists(dataPath))
        {
            Debug.Log($"Found directory at {dataPath}");
            return;
        }

        Directory.CreateDirectory(dataPath);
        Debug.Log($"New directory created at {dataPath}");
    }

    public void InitSaveData()
    {
        SaveData = dataPath + FileName;

        if (File.Exists(SaveData) & useSaveData) // load existing file
        {
            Debug.Log($"SaveData found at {SaveData}");
            SaveDataManager.ReadSaveData(SaveData);
            return;
        }

        if (useSaveData) SaveDataManager.WriteSaveData();
        Debug.Log($"New file created at {SaveData}");
    }
}