using FlappyBirdScripts;
using UnityEngine;
using UnityEngine.Events;

namespace DeviceUtils
{
    public class ShakePhone : MonoBehaviour
    {
        [Header("Shake Settings")] public float shakeDetectionThreshold = 2.0f;
        public float minShakeInterval = 0.5f;
        private float sqrShakeThreshold;
        private float lastShakeTime;
        [SerializeField] private GameObject background;
        private float _animationSpeed;

        private void Start()
        {
            _animationSpeed = background.GetComponent<Background>().animationSpeed;
            sqrShakeThreshold = shakeDetectionThreshold * shakeDetectionThreshold;
            lastShakeTime = Time.unscaledTime;
        }

        private void Update()
        {
            Vector3 acceleration = Input.acceleration;
            if (acceleration.sqrMagnitude >= sqrShakeThreshold
                && Time.unscaledTime - lastShakeTime >= minShakeInterval && !GameManager.Instance.wasShaken)
            {
                Debug.Log("Phone was shaken!");
                GameManager.Instance.wasShaken = true;
                lastShakeTime = Time.time;
                ToggleBirdHardMode();
            }
        }

        private void ToggleBirdHardMode()
        {
            _animationSpeed = 10f;
            background.transform.rotation = Quaternion.Euler(0, 0, 180);
            GameManager.Instance.birdFriction = true;
        }
    }
}