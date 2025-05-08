using System;
using System.Collections.Generic;
using System.IO;
using GeneralUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    // ------------------ VARIABLES ------------------
    [Header("SAVE DATA")]
    public bool useSaveData = true;
    public bool obfuscateData = true;
    
    [Header("PROGRESSION")] 
    public int currentLevel;
    public string currentScene;
    public int birdHighScore;
    
    [Header("Level 0")]
    public bool phoneUnlocked;
    public bool progressStory;
    public bool iDMojiCreated;
    public bool deathGamePlayed;
    public bool lastSandraMessage;
    
    [Header("Level 1")]
    public bool parisPopUpSeen;
    public bool wasShaken;
    public bool seenGyroHint;
    public bool gyroCodeSeen;
    public bool playerIsTrapped;
    public bool zoomReady;
    
    [Header("FRICTION STRATEGIES")]
    public bool shuffleHomeScreen;
    public bool birdFriction; // Lower brightness and raise game speed linked to score
    public bool textFriction;
    public bool rotationFriction;
    public float rotationChance = 0.1f; // 10% chance to apply rotation friction

    [Header("APP STATES")]
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
    
    [Header("MESSAGE STATE")]
    public int currentChoiceIndex;
    public List<int> playerChoices;
    
    [Header("PLAYER INFO")]
    public string playerName;
    public int playerAge;
    public int playerBirthYear;
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

    [Header("TRACKING")]
    [Header("Apps")]
    public List<string> appNames = new()
    {
        "Messages", "Photos", "Settings", "Death", "HappyBird",
        "Gyro", "oldPhotos", "Notes", "Calendar", "IDMoji"
    };
    public List<int> appCounts = new();

    [Header("Conversations")]
    public List<string> conversationNames = new()
    {
        "Sandra", "Paris"
    };
    public List<int> conversationCounts = new();

    [Header("Photos")]
    public List<string> photoNames = new();
    public List<int> photoCounts = new();
    
    [HideInInspector] public string dataPath;
    [HideInInspector] public string messagesDataPath;
    [HideInInspector] public string selfiePicture;
    [HideInInspector] public FindAndEditTMPElements findAndEditTMPElements;
    public string SaveData { get; private set; }
    private const string FileName = "SaveData.json";

    [Header("Character Customization")]
    public int selectedFaceColorIndex;
    public int selectedFaceVariantIndex;
    public int selectedEyeColorIndex;
    public int selectedEyeVariantIndex;
    public int selectedMouthColorIndex;
    public int selectedMouthVariantIndex;
    public int selectedHairColorIndex;
    public int selectedHairVariantIndex;
    //public int selectedBodyIndex;
    
    
    // ------------------ METHODS ------------------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (findAndEditTMPElements == null)
                findAndEditTMPElements = GetComponent<FindAndEditTMPElements>();
            DontDestroyOnLoad(gameObject);
            
            OpenTrackerUtils.Init(appNames, appCounts);
            OpenTrackerUtils.Init(conversationNames, conversationCounts);
            OpenTrackerUtils.Init(photoNames, photoCounts);
            
            InitFileSystem();
            InitSaveData();
            InitAnalytics();
            
            // subscribe to scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
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
    
    private void InitPhotoNames()
    {
        // initialize photo tracking names and counts
        if (photoNames.Count == 0)
        {
            var gallery = UnityEngine.Object.FindAnyObjectByType<GalleryApp.GalleryManager>();
            if (gallery != null)
            {
                foreach (var sprite in gallery.images)
                    photoNames.Add(sprite.name);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // count app or conversation opens 
        if (OpenTrackerUtils.Register(appNames, appCounts, scene.name))
        {
            if (useSaveData) SaveDataManager.WriteSaveData();
        }
        else if (OpenTrackerUtils.Register(conversationNames, conversationCounts, scene.name))
        {
            if (useSaveData) SaveDataManager.WriteSaveData();
        }
        // when entering the Photos scene, build the photo list
        if (scene.name == "Photos")
        {
            InitPhotoNames();
            OpenTrackerUtils.Init(photoNames, photoCounts);
            if (useSaveData)
                SaveDataManager.WriteSaveData();
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
        dataPath = Application.persistentDataPath + "/system_data/";
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

        if (File.Exists(SaveData) && useSaveData) // load existing file
        {
            Debug.Log($"SaveData found at {SaveData}");
            SaveDataManager.ReadSaveData(SaveData);
            return;
        }

        if (useSaveData) SaveDataManager.WriteSaveData();
        Debug.Log($"New file created at {SaveData}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // also save on application quit
    private void OnApplicationQuit()
    {
        if (useSaveData)
            SaveDataManager.WriteSaveData();
    }
}