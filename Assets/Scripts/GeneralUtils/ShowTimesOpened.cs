using TMPro;
using UnityEngine;

namespace GeneralUtils
{
    public class ShowTimesOpened : MonoBehaviour
    {
        private enum OpenedType
        {
            Apps,
            Conversations
        }

        [SerializeField]
        private OpenedType openedType;

        private void Start()
        {
            TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                Debug.LogError("No TextMeshProUGUI component found on the GameObject.");
                return;
            }
            
            switch (openedType)
            {
                case OpenedType.Apps:
                    int openedAppCount = OpenTrackerUtils.GetOpenedCount(GameManager.Instance.appCounts);
                    int totalAppCount = OpenTrackerUtils.GetTotalCount(GameManager.Instance.appNames);
                    textComponent.text = $"{openedAppCount} / {totalAppCount}";
                    break;
                case OpenedType.Conversations:
                    int openedConvCount = OpenTrackerUtils.GetOpenedCount(GameManager.Instance.conversationCounts);
                    int totalConvCount = OpenTrackerUtils.GetTotalCount(GameManager.Instance.conversationNames);
                    textComponent.text = $"{openedConvCount} / {totalConvCount}";
                    break;
            }
        }
    }
}
