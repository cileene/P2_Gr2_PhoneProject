using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GeneralUtils
{
    public class FindAndEditTMPElements : MonoBehaviour
    {
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
            StartCoroutine(DelayAndEditTMPElements());
        }

        public System.Collections.IEnumerator DelayAndEditTMPElements()
        {
            int waitFrames = 3;
            for (int i = 0; i < waitFrames; i++)
                yield return null;

            if (!GameManager.Instance.textFriction)
                yield break;

            TextMeshProUGUI[] tmpElements = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

            foreach (var tmp in tmpElements)
            {
                tmp.text = Gibberishifier.ToGibberish(tmp.text);
            }
        }
    }
}