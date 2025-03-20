using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Image fullScreenImage;
    public GameObject ImageViewPanel;
    public Sprite[] images;
    private int currentIndex = 0;
   
    public void OpenImage(int index)
    {
        currentIndex = index;
        fullScreenImage.sprite = images[currentIndex];
        ImageViewPanel.SetActive(true);
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        fullScreenImage.sprite = images[currentIndex];
    }

    public void PreviousImage()
    {
        currentIndex = ( currentIndex - 1 ) % images.Length;
        fullScreenImage.sprite = images[currentIndex];
    }

    public void CloseImageView()
    {
        ImageViewPanel.SetActive(false);
    }
}
