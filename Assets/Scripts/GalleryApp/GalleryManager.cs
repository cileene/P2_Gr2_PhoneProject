using CustomUnityAnalytics;
using UnityEngine;
using UnityEngine.UI;
using GeneralUtils;

namespace GalleryApp
{
    public class GalleryManager : MonoBehaviour
    {
        [SerializeField] private GameObject popUp;
        public Image fullScreenImage;
        public GameObject imageViewPanel;
        public Sprite[] images;
        private int _currentIndex = 0;

        public void OpenImage(int index)
        {
            _currentIndex = index;
            fullScreenImage.sprite = images[_currentIndex];
            imageViewPanel.SetActive(true);
            UGSSnitch(); // remote tracking
            TrackPhotoOpen(); // local tracking
            CheckForSandraPhoto();
        }

        public void NextImage()
        {
            if (_currentIndex < images.Length - 1) // Stop at last image
            {
                _currentIndex++;
                fullScreenImage.sprite = images[_currentIndex];
            }

            UGSSnitch();
            TrackPhotoOpen();
            CheckForSandraPhoto();
        }

        public void PreviousImage()
        {
            if (_currentIndex > 0) // Stop at first image
            {
                _currentIndex--;
                fullScreenImage.sprite = images[_currentIndex];
            }

            UGSSnitch();
            TrackPhotoOpen();
            CheckForSandraPhoto();
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

        /// <summary>Track the currently displayed photo as opened.</summary>
        private void TrackPhotoOpen()
        {
            string photoName = images[_currentIndex].name;
            if (GameManager.Instance.photoNames.Contains(photoName))
            {
                OpenTrackerUtils.Register(GameManager.Instance.photoNames, GameManager.Instance.photoCounts, photoName);
                if (GameManager.Instance.useSaveData)
                    SaveDataManager.WriteSaveData();
            }
        }

        private void CheckForSandraPhoto() //TODO: NOT FINISHED
        {
            // Check if the current photo is a Sandra photo
            // maybe change to only show the photo when the player has talked to paris
            if (_currentIndex == 6 && GameManager.Instance.currentLevel == 1)
            {
                popUp.SetActive(true);
                GameManager.Instance.progressStory = true;
            }
        }
    }
}