using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    private MessageManager _choiceManager;  // Reference to the main dialogue manager

    // Start is called before the first frame update
    void Start()
    {
        // Find the main dialogue manager in the scene
        _choiceManager = FindObjectOfType<MessageManager>();
    }

    // This method gets called when the sprite is clicked and the mouse button is released
    void OnMouseUp()
    {
        if (_choiceManager != null)
        {
            // Determine which choice this sprite represents (1 or 2)
            if (gameObject.name == "ChoiceSprite1") // For Sprite 1
            {
                _choiceManager.OnChoice1Clicked();  // Call the method from the main manager
            }
            else if (gameObject.name == "ChoiceSprite2") // For Sprite 2
            {
                _choiceManager.OnChoice2Clicked();  // Call the method from the main manager
            }
        }
    }
}