using UnityEngine;
using UnityEngine.UI;
using System.IO;

// Display the selfie picture in the UI
//TODO: PROTOTYPE CODE
public class SetSelfiePicture : MonoBehaviour
{
    public Texture2D selfieTexture;
    
    private void Start()
    {
        LoadSelfiePicture();

        if (selfieTexture != null)
        {
            SetDimensions();
            GetComponent<RawImage>().texture = selfieTexture;
        }
        else
        {
            Debug.LogWarning("Selfie texture not loaded. No selfie file found?");
        }
    }
    
    private void LoadSelfiePicture()
    {
        // Load the selfie picture from the GameManager
        string selfiePath = GameManager.Instance.selfiePicture;

        if (File.Exists(selfiePath))
        {
            byte[] imageData = File.ReadAllBytes(selfiePath);
            selfieTexture = new Texture2D(2, 2);
            selfieTexture.LoadImage(imageData);
            Debug.Log("Trying to load selfie from path: " + selfiePath);
        }
    }
    
    private void SetDimensions()
    {
        // Set the dimensions of the RawImage to match the selfie texture
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(selfieTexture.width, selfieTexture.height);
    }
}