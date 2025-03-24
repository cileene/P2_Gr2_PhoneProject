using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

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

    private string _dataPath;
    public string SaveData { get; private set; }
    private const string FileName = "SaveData.json";
    public string selfiePicture;
    
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
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);

        if (Directory.Exists(_dataPath))
        {
            Debug.Log($"Found directory at {_dataPath}");
            return;
        }

        Directory.CreateDirectory(_dataPath);
        Debug.Log($"New directory created at {_dataPath}");
    }

    public void InitSaveData()
    {
        SaveData = _dataPath + FileName;

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