using UnityEngine;

//TODO: Refactor to use new input system

public class GyroscopeToRotation : MonoBehaviour
{
    [Tooltip("The offset rotation around the x axis")]
    [SerializeField] private float xOffsetDegrees;
    private bool _gyroEnabled;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            _gyroEnabled = true;
            Debug.Log("Gyroscope is supported.");
        }
        else
        {
            Debug.LogWarning("Gyroscope not supported.");
        }
    }
    
    private void Update()
    {
        if (!_gyroEnabled) return;
        HandleGyroRotation();
    }

    private void HandleGyroRotation()
    {
        Input.gyro.enabled = true;
        Quaternion gyro = Input.gyro.attitude;
        // Adjust the gyroscope rotation to match Unity's coordinate system
        Quaternion adjustedGyro = new Quaternion(gyro.x, gyro.y, -gyro.z, -gyro.w);
        // Create an offset rotation around the x axis
        Quaternion offsetRotation = Quaternion.Euler(xOffsetDegrees, 0f, 0f);
        // Apply the offset by multiplying the rotations
        transform.rotation = offsetRotation * adjustedGyro;
    }
}
