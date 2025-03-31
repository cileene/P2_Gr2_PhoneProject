using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: Move to settings app
public class ResetGame : MonoBehaviour
{
    private void Start()
    {
        // Call home
        Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent("playerResetPhone");
        Unity.Services.Analytics.AnalyticsService.Instance.Flush();
        
        // Delete the save data file
        string path = GameManager.Instance.SaveData;
        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        
        //TODO: Delete the message history
        
        Destroy(GameManager.Instance.gameObject);
        
        SceneManager.LoadSceneAsync("LockScreen");
    }
}
