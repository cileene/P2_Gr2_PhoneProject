using CustomUnityAnalytics;
using UnityEngine;
using UnityEngine.SceneManagement;

// This should be a central location for scene handling
public static class SceneHandler
{
    public static void LoadScene(string sceneToLoad)
    {
        // if the scene to load is not empty, load the scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"Loading scene: {sceneToLoad}");
            UGSSceneTransition.HandleSceneCustomEvent(sceneToLoad); // send to Analytics
            GameManager.Instance.currentScene = sceneToLoad; // update the current scene in GameManager
            SaveDataManager.TriggerSave(); // save the current scene
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified.");
        }
    }
}