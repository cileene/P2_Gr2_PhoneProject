using UnityEngine;

public class GyroscopeToRotation : MonoBehaviour
{
    // translate the gyroscope input to rotation
    private void Update()
    {
        // if the device has a gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            // get the gyroscope input
            Quaternion gyro = Input.gyro.attitude;
            // rotate the object
            transform.rotation = new Quaternion(gyro.x, gyro.y, -gyro.z, -gyro.w);
        }
    }
}
