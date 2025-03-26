using Unity.Services.Analytics;

// Static class to notify Analytics when a scene is loaded
public static class UGSSceneTransition
{
    //TODO: Add all scenes here or automate it somehow
    public static void HandleSceneCustomEvent(string sceneName)
    {
        var customEventName = ""; // local variable to store the custom event name
        
        switch (sceneName) // switch statement to determine the custom event name based on the scene name
        {
            case "Gyro":
                customEventName = "gyroSceneLoaded";
                break;
            
            case "Home":
                customEventName = "homeSceneLoaded";
                break;
            
            case "Death":
                customEventName = "deathSceneLoaded";
                break;
            
            case "Settings":
                customEventName = "settingsSceneLoaded";
                break;
            
            case "Messages":
                customEventName = "messagesSceneLoaded";
                break;
            
            case "HappyBird":
                customEventName = "happyBirdSceneLoaded";
                break;
            
            case "Sandra":
                customEventName = "sandraSceneLoaded";
                break;
            
            case "Photos":
                customEventName = "photosSceneLoaded";
                break;
            
            case "Notes":
                customEventName = "notesSceneLoaded";
                break;
            
            case "Calendar":
                customEventName = "calendarSceneLoaded";
                break;
        }
        AnalyticsService.Instance.RecordEvent(customEventName); // record the custom event
        AnalyticsService.Instance.Flush(); // flush/upload the event to the server
    }
}