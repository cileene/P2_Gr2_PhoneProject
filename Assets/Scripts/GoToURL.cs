using UnityEngine;

public class GoToURL : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenURL()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL is empty or null.");
        }
    }

}