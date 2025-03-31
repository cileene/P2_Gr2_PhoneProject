using UnityEngine;

public class GoToURL : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenURL()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent("playerClickedLink");
            Unity.Services.Analytics.AnalyticsService.Instance.Flush();
            
            // Delete the save data file
            string path = GameManager.Instance.SaveData;
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        
            //TODO: Delete the message history
        
            Destroy(GameManager.Instance.gameObject);
            
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL is empty or null.");
        }
    }

}