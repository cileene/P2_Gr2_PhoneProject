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
            case "TestAppScene":
                customEventName = "testAppSceneLoaded";
                break;
            
            case "TestScene":
                customEventName = "testSceneLoaded";
                break;
            
            case "PhoneChatExampleScene":
                customEventName = "phoneChatSceneLoaded";
                break;
            
            case "ScrollingTestScene":
                customEventName = "scrollingTestSceneLoaded";
                break;
            
            case "MessageSelect":
                customEventName = "messageSelectLoaded";
                break;
            
            case "MessageChatBente":
                customEventName = "messageChatBenteLoaded";
                break;
            
            case "MessageChatBobby":
                customEventName = "messageChatBobbyLoaded";
                break;
        }
        AnalyticsService.Instance.RecordEvent(customEventName); // record the custom event
        AnalyticsService.Instance.Flush(); // flush/upload the event to the server
    }
}