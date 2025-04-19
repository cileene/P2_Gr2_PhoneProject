using UnityEngine;

namespace DeviceUtils
{
    public class RotateScreen : MonoBehaviour
    {
        private float _spinChance;
        private float _spinSpeed = 30f;    // degrees per second when spinning
        private bool _isSpinning = false;
        private float _spinDirection = 1f; // +1 or -1 for random spin direction
        
        private RectTransform rectTransform;

        private void Start()
        {
            _spinChance = GameManager.Instance.rotationChance / 2;
            
            float roll = Random.value;
            rectTransform = GetComponent<RectTransform>();
            
            // continuous spin chance (smaller chance first)
            if (GameManager.Instance.rotationFriction && roll < _spinChance)
            {
                _isSpinning = true;
                // choose random spin direction
                _spinDirection = Random.value < 0.5f ? 1f : -1f;
            }
            // 180° flip chance
            else if (GameManager.Instance.rotationFriction && roll < GameManager.Instance.rotationChance)
            {
                if (rectTransform != null)
                {
                    rectTransform.Rotate(0, 0, 180);
                }
                else
                {
                    Debug.LogWarning("RectTransform component not found on this GameObject.");
                }
            }
        }

        private void Update()
        {
            if (_isSpinning)
            {
                rectTransform.Rotate(0, 0, _spinDirection * _spinSpeed * Time.deltaTime);
            }
        }
    }
}