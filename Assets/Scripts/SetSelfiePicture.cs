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
        //Load the selfie picture when the script starts
        LoadSelfiePicture();
        SetDimensions();

        // Set the selfie picture as the texture of the RawImage component
        if (selfieTexture != null)
        {
            GetComponent<RawImage>().texture = selfieTexture;
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
        }
    }
    
    private void SetDimensions()
    {
        // Set the dimensions of the RawImage to match the selfie texture
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(selfieTexture.width, selfieTexture.height);
    }
}