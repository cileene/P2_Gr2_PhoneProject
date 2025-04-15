using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Here we get the battery percentage and display it
namespace DeviceUtils
{
    public class ShowBatteryPercentage : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] public float batteryLevel = 0.5f;
        private Slider _batterySlider;
        private TextMeshProUGUI _batteryText;
    
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
                _batteryText.text = (batteryLevel * 100).ToString("F0") + "%";
            }
        }
    }
}