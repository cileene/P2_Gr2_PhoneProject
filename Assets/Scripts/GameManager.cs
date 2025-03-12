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

    [Header("Progression")] 
    public bool useSaveData = true;
    public bool phoneUnlocked;
    public int currentLevel;
    public string currentScene; // Default scene

    private string _dataPath;
    public string SaveData { get; private set; }
    private const string FileName = "SaveData.json";

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

    private void Start()
    {
        // Load the current scene from the save data
        if (!string.IsNullOrEmpty(currentScene))
        {
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            Debug.LogWarning("No current scene found in save data.");
        }
    }

    //TODO: we want to track the current open scene to make sure it will be opened on app restart
    //TODO: P2-57 refactor to use json serialization (chapter 12 in the book)
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