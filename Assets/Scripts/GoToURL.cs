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
            
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL is empty or null.");
        }
    }

}