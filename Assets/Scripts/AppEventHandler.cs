using UnityEngine;
using UnityEngine.UI;
using static System.Enum;

public class AppEventHandler : MonoBehaviour
{
    private enum Apps //TODO: List all apps here
    {
        Messages,
        Photos,
        Settings,
        Death,
        HappyBird,
        Gyro,
        OldPhotos,
        Notes,
        Calendar,
        IDMoji
    }

    private string _name;
    private bool _isLoading;
    private bool _showBadge;
    private GameObject _loadingObject;
    private GameObject _badgeObject;

    private void Start()
    {
        _name = gameObject.name;
        _loadingObject = transform.Find("Loading").gameObject;
        _badgeObject = transform.Find("Badge").gameObject;

        CheckAppBools();
        SetLoadingState();
        SetBadgeState();
    }

    private void SetLoadingState()
    {
        _loadingObject.SetActive(_isLoading);
        GetComponent<Button>().enabled = !_isLoading;
    }

    private void SetBadgeState()
    {
        _badgeObject.SetActive(_showBadge);
    }

    private void CheckAppBools()
    {
        foreach (Apps app in GetValues(typeof(Apps)))
        {
            if (app.ToString() != _name) continue;

            switch (app)
            {
                case Apps.Messages:
                    _isLoading = GameManager.Instance.messagesLoading;
                    _showBadge = GameManager.Instance.messagesBadge;
                    break;
                case Apps.Photos:
                    _isLoading = GameManager.Instance.photosLoading;
                    _showBadge = GameManager.Instance.photosBadge;
                    break;
                case Apps.Settings:
                    _isLoading = GameManager.Instance.settingsLoading;
                    _showBadge = GameManager.Instance.settingsBadge;
                    break;
                case Apps.Death:
                    _isLoading = GameManager.Instance.deathLoading;
                    _showBadge = GameManager.Instance.deathBadge;
                    break;
                case Apps.HappyBird:
                    _isLoading = GameManager.Instance.happyBirdLoading;
                    _showBadge = GameManager.Instance.happyBirdBadge;
                    break;
                case Apps.Gyro:
                    _isLoading = GameManager.Instance.gyroLoading;
                    _showBadge = GameManager.Instance.gyroBadge;
                    break;
                case Apps.OldPhotos:
                    _isLoading = GameManager.Instance.oldPhotosLoading;
                    _showBadge = GameManager.Instance.oldPhotosBadge;
                    break;
                case Apps.Notes:
                    _isLoading = GameManager.Instance.notesLoading;
                    _showBadge = GameManager.Instance.notesBadge;
                    break;
                case Apps.Calendar:
                    _isLoading = GameManager.Instance.calendarLoading;
                    _showBadge = GameManager.Instance.calendarBadge;
                    break;
                case Apps.IDMoji:
                    _isLoading = GameManager.Instance.idMojiLoading;
                    _showBadge = GameManager.Instance.idMojiBadge;
                    break;
                default:
                    _isLoading = false;
                    _showBadge = false;
                    break;
            }
        }
    }
}