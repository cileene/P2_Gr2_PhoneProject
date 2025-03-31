using UnityEngine.SceneManagement;
using Unity.Services.Analytics;

public static class ResetStaticClass
    {
        public static void ResetGame()
        {
            // Call home
            AnalyticsService.Instance.RecordEvent("playerResetPhone");
            AnalyticsService.Instance.Flush();
        
            // Delete the save data file
            string saveData = GameManager.Instance.SaveData;
            if (System.IO.File.Exists(saveData)) System.IO.File.Delete(saveData);
        
            // Delete the message history
            string messageData = GameManager.Instance.messagesDataPath;
            if (System.IO.File.Exists(messageData)) System.IO.File.Delete(messageData);
       
            GameManager.Instance.DestroyGameManager();
        
            SceneManager.LoadSceneAsync("LockScreen");
        }
        
        
    }
