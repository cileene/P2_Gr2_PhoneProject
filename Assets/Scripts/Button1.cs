using UnityEngine;
using UnityEngine.SceneManagement;

// Standard button script to load a scene
public class Button1 : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField, Range(0.1f, 1.5f)] private float transitionTime = 0.5f;
    private string _buttonName;
    private GameObject _transitionCamera;
    
    // read all the game manager variables and present them in editor as bools to set gating

    private void Awake()
    {
        _buttonName = gameObject.name;
        
    }

    private void OnMouseDown()
    {
        // set material color to grey
        GetComponent<Renderer>().material.color = Color.grey;
        Debug.Log($"{_buttonName} clicked!");
    }


    
   
}