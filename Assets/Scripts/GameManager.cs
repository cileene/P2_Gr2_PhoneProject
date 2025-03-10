using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

// Here we track the game state using a singleton pattern
// And now we also write data to the player's device
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Progression")]
    public bool phoneUnlocked;
    
    private string _dataPath;
    private string _saveData;
    private const string FileName = "SaveData.txt";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitFileSystem();
            InitTextFile();
            InitAnalytics();
            UpdateSaveData($"Game started: {DateTime.Now}");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //TODO: we want to track the current open scene to make sure it will be opened on app restart
    
    private void InitFileSystem()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);
        
        if(Directory.Exists(_dataPath))
        {
            Debug.Log($"Directory already exists at {_dataPath}");
            return;
        }
        Directory.CreateDirectory(_dataPath);
        Debug.Log($"New directory created at {_dataPath}");
    }
    
    private void InitAnalytics()
    {
        Instantiate(Resources.Load("Prefabs/UnityAnalytics"));
        Debug.Log("Analytics initialized");
    }
    
    private void InitTextFile()
    {
        _saveData = _dataPath + FileName;
        
        if (File.Exists(_saveData))
        {
            Debug.Log($"File already exists at {_saveData}");
            return;
        }
        File.WriteAllText(_saveData, "<SAVE DATA>\n ");
        Debug.Log($"New file created at {_saveData}");
    }

    private void UpdateSaveData(string data)
    {
        if (!File.Exists(_saveData))
        {
            Debug.LogWarning("File does not exist!");
            return;
        }
        File.AppendAllText(_saveData, data + "\n");
    }
}