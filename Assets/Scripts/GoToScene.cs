using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    public void LoadScene()
    {
        // if the scene to load is not empty, load the scene
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"Loading scene: {sceneToLoad}");
            UGSSceneTransition.HandleSceneCustomEvent(sceneToLoad); // send to Analytics
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified.");
        }
    }
}
