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
        private GameManager _gm;

        private void Start()
        {
            _gm = GameManager.Instance;
        }

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
        
        private void TrackPhotoOpen()
        {
            string photoName = images[_currentIndex].name;
            if (_gm.photoNames.Contains(photoName))
            {
                OpenTrackerUtils.Register(_gm.photoNames, _gm.photoCounts, photoName);
                if (_gm.useSaveData)
                    SaveDataManager.WriteSaveData();
            }
        }
        private void ShowPopup()
        {
            popUp.SetActive(true);
        }

        private void CheckForSandraPhoto() //TODO: NOT FINISHED
        {
            // Check if the current photo is a Sandra photo
            // maybe change to only show the photo when the player has talked to paris
            if (_currentIndex == 6 && _gm.currentLevel == 1)
            {
                Invoke(nameof(ShowPopup), 1f);
                _gm.zoomReady = true;
                _gm.progressStory = true;
                _gm.happyBirdLoading = false; // for linear test build
            }
        }
    }
}