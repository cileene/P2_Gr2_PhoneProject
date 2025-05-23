using UnityEngine;

// Standard button script to load a scene
namespace UIUtils
{
    public class LoadSceneButton : MonoBehaviour
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
            // set material color to white
            GetComponent<Renderer>().material.color = Color.white;
            Debug.Log($"{_buttonName} released!");
            Activate();
        }

        public void UIClick()
        {
            Activate();
        }

        private void Activate()
        {
            // activate the transition camera
            if (!string.IsNullOrEmpty(sceneToLoad)) _transitionCamera.SetActive(true);

            // wait for transitionTime seconds and then load the scene
            Invoke(nameof(LoadScene), transitionTime);
        }

        private void LoadScene() //TODO: This should be centralized in a scenemanager
        {
            SceneHandler.LoadScene(sceneToLoad);
        }
    }
}