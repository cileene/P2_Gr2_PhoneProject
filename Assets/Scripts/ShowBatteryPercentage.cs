using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Here we get the battery percentage and display it
public class ShowBatteryPercentage : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] public float batteryLevel = 0.5f; // Default battery level
    private Slider _batterySlider; // Reference to the UI Slider component
    private TextMeshProUGUI _batteryText; // Reference to the UI Text component
    
    private void Start()
    {
        _batterySlider = GetComponent<Slider>();
        _batteryText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        batteryLevel = SystemInfo.batteryLevel;
        
        if (_batterySlider != null) _batterySlider.value = batteryLevel;

        if (_batteryText != null)
        {
            _batteryText.text = (batteryLevel * 100).ToString("F0") + "%"; // Display battery percentage
        }
    }
}