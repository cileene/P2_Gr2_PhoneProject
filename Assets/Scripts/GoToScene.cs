using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public AudioClip homeSound;
    public void LoadScene()
    {
        if (SoundManager.Instance != null && homeSound != null)
        {
            SoundManager.Instance.PlaySound(homeSound);
        }
        else if (SoundManager.Instance == null)
        {
            Debug.LogWarning("SoundManager is missing.");
        }
        else if (homeSound == null)
        {
            Debug.LogWarning("HomeSound clip is not assigned.");
        }

        if (GameManager.Instance.phoneUnlocked)
        {
            SceneHandler.LoadScene(sceneToLoad);
        }
    }
}
