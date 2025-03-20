using UnityEngine;
using UnityEngine.UI;

    // Set device brightness
    public class SetBrightness : MonoBehaviour
    {
        private float _brightness;
        
        private void Start()
        {
            _brightness = Screen.brightness;
            
            Slider brightnessSlider = GetComponent<Slider>();
            if (brightnessSlider != null) brightnessSlider.value = _brightness;
        }
        
        private void Update()
        {
            SetDeviceBrightness(_brightness);
        }

        public void SetDeviceBrightness(float value)
        {
            _brightness = Mathf.Clamp(value, 0f, 1f);
            
            Screen.brightness = _brightness;
        }
    }
