using UnityEngine;

public class Button : MonoBehaviour
{
    private string _buttonName;
    
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

    private void OnMouseUp()
    {
        // set material color to white
        GetComponent<Renderer>().material.color = Color.white;
        Debug.Log($"{_buttonName} released!");
    }
}
