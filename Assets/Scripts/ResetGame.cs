using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.currentScene = "LockScreenScene";
        GameManager.Instance.currentLevel = 0;
        SaveDataManager.TriggerSave(); // save the current scene
        SceneManager.LoadScene("LockScreenScene");
    }
}
