using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public AudioClip homeSound;
    public void LoadScene()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(homeSound);
        }
        else 
        {
            Debug.LogWarning("no sound");
        }
        if (GameManager.Instance.phoneUnlocked) SceneHandler.LoadScene(sceneToLoad);
    }
}
