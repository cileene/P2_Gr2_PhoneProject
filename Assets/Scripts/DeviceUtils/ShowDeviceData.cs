using TMPro;
using UnityEngine;

namespace DeviceUtils
{
    public class ShowDeviceData : MonoBehaviour
    {
        public enum DataType
        {
            DeviceModel,
            DeviceOS,
            BatteryState
        }

        [SerializeField]
        private DataType dataType;

        private void Start()
        {
            TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                Debug.LogError("No TextMeshProUGUI component found on the GameObject.");
                return;
            }

            switch (dataType)
            {
                case DataType.DeviceModel:
                    textComponent.text = SystemInfo.deviceModel;
                    break;
                case DataType.DeviceOS:
                    textComponent.text = SystemInfo.operatingSystem;
                    break;
                case DataType.BatteryState:
                    textComponent.text = SystemInfo.batteryStatus.ToString();
                    break;
            }
        }
    }
}