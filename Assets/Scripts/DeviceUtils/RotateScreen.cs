using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeviceUtils
{
    public class RotateScreen : MonoBehaviour
    {
        [SerializeField] private float rotationChance = 0.1f; // 25% chance to apply rotation friction
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // apply rotation friction with rotationChance % 
            if (GameManager.Instance.rotationFriction && Random.value < rotationChance)
            {
                // allow only upside-down rotation at runtime
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                Screen.orientation = ScreenOrientation.PortraitUpsideDown;
            }
            else
            {
                // revert to standard portrait orientation
                Screen.autorotateToPortrait = true;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
    }
}