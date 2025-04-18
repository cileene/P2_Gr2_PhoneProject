using System;
using System.Collections.Generic;
using System.IO;
using GeneralUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

// Here we track the game state using a singleton pattern
// And now we also write data to the player's device

[Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    // ------------------ VARIABLES ------------------
    [Header("Progression")] 
    public bool phoneUnlocked;
    public int currentLevel;
    public string currentScene; // Default scene
    public bool progressStory;
    public int birdHighScore;
    
    [Header("Friction Strategies")]
    public bool shuffleHomeScreen;
    public bool birdFriction; // Lower brightness and raise game speed linked to score
    public bool textFriction;

    [Header("Data Paths")]
    public bool useSaveData = true;
    public bool obfuscateData = true;
    
    [Header("App Badges")] 
    public bool messagesBadge;
    public bool photosBadge; 
    public bool settingsBadge;
    public bool deathBadge;
    public bool happyBirdBadge;
    public bool gyroBadge;
    public bool oldPhotosBadge;
    public bool notesBadge;
    public bool calendarBadge;
    public bool idMojiBadge;

    [Header("App Loading")] 
    public bool messagesLoading;
    public bool photosLoading;
    public bool settingsLoading;
    public bool deathLoading;
    public bool happyBirdLoading;
    public bool gyroLoading;
    public bool oldPhotosLoading;
    public bool notesLoading;
    public bool calendarLoading;
    public bool idMojiLoading;
    
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

    
    [HideInInspector] public string dataPath;
    [HideInInspector] public string messagesDataPath;
    [HideInInspector] public string selfiePicture;
    [HideInInspector] public FindAndEditTMPElements findAndEditTMPElements;
    public string SaveData { get; private set; }
    private const string FileName = "SaveData.json";
    
    // ------------------ METHODS ------------------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (findAndEditTMPElements == null)
                findAndEditTMPElements = GetComponent<FindAndEditTMPElements>();
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
    
    public void DestroyGameManager()
    {
        Destroy(gameObject);
    }
    
    

    private void InitAnalytics()
    {
        Instantiate(Resources.Load("Prefabs/UnityAnalytics"));
        Debug.Log("Analytics initialized");
    }

    public void InitFileSystem()
    {
        dataPath = Application.persistentDataPath + "/system_data/"; //TODO: rename to system_data
        Debug.Log(dataPath);

        if (Directory.Exists(dataPath))
        {
            Debug.Log($"Found directory at {dataPath}");
            return;
        }

        Directory.CreateDirectory(dataPath);
        Debug.Log($"New directory created at {dataPath}");
    }

    public void InitSaveData()//TODO: Encrypt this
    {
        SaveData = dataPath + FileName;

        if (File.Exists(SaveData) && useSaveData) // load existing file
        {
            Debug.Log($"SaveData found at {SaveData}");
            SaveDataManager.ReadSaveData(SaveData);
            return;
        }

        if (useSaveData) SaveDataManager.WriteSaveData();
        Debug.Log($"New file created at {SaveData}");
    }
}