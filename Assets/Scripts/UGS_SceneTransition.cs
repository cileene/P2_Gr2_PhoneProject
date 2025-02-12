using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;


public static class UGS_SceneTransition
{
    public static void TestAppSceneCustomEvent(string sceneName)
    {
        if (sceneName == "TestAppScene")
        {
            // The ‘levelCompleted’ event will get cached locally
            //and sent during the next scheduled upload, within 1 minute
            AnalyticsService.Instance.RecordEvent("testAppSceneLoaded");

            // You can call Events.Flush() to send the event immediately
            AnalyticsService.Instance.Flush();
        }
    }
}