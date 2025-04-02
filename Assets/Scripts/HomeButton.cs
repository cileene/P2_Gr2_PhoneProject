// Set up a delegate that can be used to notify when the home button is pressed

using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public delegate void HomeButtonPressed();
    public static event HomeButtonPressed OnHomeButtonPressed;

    public void PressButton()
    {
        OnHomeButtonPressed?.Invoke();
    }
    
    private void OnEnable()
    {
        // Subscribe to the event
        OnHomeButtonPressed += HandleHomeButtonPressed;
    }
    private void OnDisable()
    {
        // Unsubscribe from the event
        OnHomeButtonPressed -= HandleHomeButtonPressed;
    }
    private void HandleHomeButtonPressed()
    {
        // Handle the home button press event here
        Debug.Log("Home button pressed!");
    }
}