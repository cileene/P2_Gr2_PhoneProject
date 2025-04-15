using Unity.Services.Analytics;

// Static class to notify Analytics when a scene is loaded
namespace CustomUnityAnalytics
{
    public static class UGSSceneTransition
    {
        //TODO: Add all scenes here or automate it somehow
        public static void HandleSceneCustomEvent(string sceneName)
        {
            SceneLoaded sceneLoaded = new SceneLoaded(sceneName)
            {
                SceneName = sceneName
            };
        
            AnalyticsService.Instance.RecordEvent(sceneLoaded);
            AnalyticsService.Instance.Flush();
        }
    }
}