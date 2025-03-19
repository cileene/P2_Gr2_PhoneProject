using UnityEngine;
using UnityEngine.UI;

    // Set device brightness
    public class SetBrightness : MonoBehaviour
    {
        private float _brightness; // Default brightness value
        
        private void Start()
        {
            _brightness = Screen.brightness; // Get the current brightness value
            
            // Set slider value to current brightness
            Slider brightnessSlider = GetComponent<Slider>();
            if (brightnessSlider != null) brightnessSlider.value = _brightness;
        }
        
        private void Update()
        {
            // Set the brightness when the script starts
            SetDeviceBrightness(_brightness);
        }

        public void SetDeviceBrightness(float value)
        {
            _brightness = Mathf.Clamp(value, 0f, 1f);
            
            Screen.brightness = _brightness;
        }
    }
