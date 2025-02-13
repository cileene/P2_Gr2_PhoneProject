using Unity.Services.Analytics;

// Static class to notify Analytics when a scene is loaded
public static class UGSSceneTransition
{
    private enum SceneName // enum to store the scene names
    {
        TestAppScene,
        TestScene
    }
    
    public static void HandleSceneCustomEvent(string sceneName)
    {
        var customEventName = ""; // local variable to store the custom event name
        
        switch (sceneName) // switch statement to determine the custom event name based on the scene name
        {
            case nameof(SceneName.TestAppScene):
                customEventName = "testAppSceneLoaded";
                break;
            
            case nameof(SceneName.TestScene):
                customEventName = "testSceneLoaded";
                break;
        }
        AnalyticsService.Instance.RecordEvent(customEventName); // record the custom event
        AnalyticsService.Instance.Flush(); // flush/upload the event to the server
    }
}