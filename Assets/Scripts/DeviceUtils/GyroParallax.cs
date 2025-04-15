using UnityEngine;

namespace DeviceUtils
{
    public class GyroParallax : MonoBehaviour
    {
        [Tooltip("The offset amount for the parallax effect")]
        [SerializeField] private float parallaxIntensity = 0.05f;
        [SerializeField] private float smoothSpeed = 5.0f;

        private bool _gyroAvailable;
        private Vector3 _startPosition;

        private void Start()
        {
            if (SystemInfo.supportsGyroscope)
            {
                Input.gyro.enabled = true;
                _gyroAvailable = true;
                _startPosition = transform.localPosition;
            }
            else
            {
                Debug.LogError("Gyroscope not supported on this device.");
            }
        }

        private void Update()
        {
            if (!_gyroAvailable) return;

            Quaternion gyro = Input.gyro.attitude;
            Vector3 gyroMovement = new Vector3(gyro.y, gyro.x, 0) * parallaxIntensity;

            Vector3 targetPosition = _startPosition + gyroMovement;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }
}