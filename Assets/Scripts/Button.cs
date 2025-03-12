using UnityEngine;
using UnityEngine.SceneManagement;

// Standard button script to load a scene
//TODO: Refactor to use new input system
public class Button : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField, Range(0.1f, 1.5f)] private float transitionTime = 0.5f;
    private string _buttonName;
    private GameObject _transitionCamera;
    
    // read all the game manager variables and present them in editor as bools to set gating

    private void Awake()
    {
        _buttonName = gameObject.name;
        _transitionCamera = transform.Find("TransitionCamera").gameObject;
    }

    private void OnMouseDown()
    {
        // set material color to grey
        GetComponent<Renderer>().material.color = Color.grey;
        Debug.Log($"{_buttonName} clicked!");
    }

    private void OnMouseUp()
    {
        // activate the transition camera
        if (!string.IsNullOrEmpty(sceneToLoad)) _transitionCamera.SetActive(true);

        // set material color to white
        GetComponent<Renderer>().material.color = Color.white;
        Debug.Log($"{_buttonName} released!");

        // wait for transitionTime seconds and then load the scene
        Invoke(nameof(LoadScene), transitionTime);
    }

    private void LoadScene()
    {
        // if the scene to load is not empty, load the scene
        if (!string.IsNullOrEmpty(sceneToLoad) && GameManager.Instance.phoneUnlocked)
        {
            Debug.Log($"Loading scene: {sceneToLoad}");
            UGSSceneTransition.HandleSceneCustomEvent(sceneToLoad); //TODO: add nullcheck
            GameManager.Instance.currentScene = sceneToLoad; // update the current scene in GameManager
            SaveDataManager.TriggerSave(); // save the current scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified or phone is locked.");
        }
    }
}