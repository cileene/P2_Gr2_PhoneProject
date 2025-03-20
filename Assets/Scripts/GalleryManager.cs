using UnityEngine;
using UnityEngine.UI;

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
    }

    public void NextImage()
    {
        _currentIndex = (_currentIndex + 1) % images.Length;
        fullScreenImage.sprite = images[_currentIndex];
    }

    public void PreviousImage()
    {
        _currentIndex = ( _currentIndex - 1 ) % images.Length;
        fullScreenImage.sprite = images[_currentIndex];
    }

    public void CloseImageView()
    {
        imageViewPanel.SetActive(false);
    }
}
