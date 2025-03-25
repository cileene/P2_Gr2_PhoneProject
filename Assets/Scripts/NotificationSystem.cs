using UnityEngine.Events;

public static class NotificationSystem
{
    // App badges
    public static UnityEvent<bool> MessagesAppBadge;
    public static UnityEvent<bool> SettingsAppBadge;
    public static UnityEvent<bool> PhotosAppBadge;
    
    // App loading
    public static UnityEvent<bool> DeathAppLoading;
    public static UnityEvent<bool> FlappyAppLoading;
}