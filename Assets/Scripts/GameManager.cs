using UnityEngine;

// Here we track the game state using a singleton pattern
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Progression")]
    public bool phoneUnlocked;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instantiate(Resources.Load<GameObject>("Prefabs/UnityAnalytics"));
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}