using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    public void LoadScene()
    {
        if (GameManager.Instance.phoneUnlocked) SceneHandler.LoadScene(sceneToLoad);
    }
}
