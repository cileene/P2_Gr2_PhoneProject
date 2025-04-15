using UnityEngine;
using UnityEngine.UI;

namespace GalleryApp
{
    public class ZoomManager : MonoBehaviour
    {
        public GameObject zoomPanel;
        public Image zoomedImage;
        public Image fullScreenImage;

        public void OpenZoomView()
        {
            zoomedImage.sprite = fullScreenImage.sprite;
            zoomPanel.SetActive(true);
        }

        public void CloseZoomView()
        {
            zoomPanel.SetActive(false);
        }
    }
}
