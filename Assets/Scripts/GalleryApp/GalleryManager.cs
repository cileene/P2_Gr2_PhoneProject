using CustomUnityAnalytics;
using UnityEngine;
using UnityEngine.UI;

namespace GalleryApp
{
    public class GalleryManager : MonoBehaviour
    {
        public Image fullScreenImage;
        public GameObject imageViewPanel;
        public Sprite[] images;
        private int _currentIndex = 0;
   
        public void OpenImage(int index)
        {
            _currentIndex = index;
            fullScreenImage.sprite = images[_currentIndex];
            imageViewPanel.SetActive(true);
            UGSSnitch();
        }

        public void NextImage()
        {
            if (_currentIndex < images.Length - 1) // Stop at last image
            {
                _currentIndex++;
                fullScreenImage.sprite = images[_currentIndex];
            }
            UGSSnitch();
        }

        public void PreviousImage()
        {
            if (_currentIndex > 0) // Stop at first image
            {
                _currentIndex--;
                fullScreenImage.sprite = images[_currentIndex];
            }

            UGSSnitch();
        }

        public void CloseImageView()
        {
            imageViewPanel.SetActive(false);
        }
    
        private void UGSSnitch()
        {
            PhotoViewed photoViewed = new PhotoViewed
            {
                PhotoIndex = _currentIndex
            };
            Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent(photoViewed);
        }
    }
}
