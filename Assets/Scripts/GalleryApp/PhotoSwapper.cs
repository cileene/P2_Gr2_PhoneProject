using UnityEngine;
using UnityEngine.UI;

namespace GalleryApp
{
    // This changes the images based on the players answers in Death
    public class PhotoSwapper : MonoBehaviour
    {
        [SerializeField] private GameObject bikeImageObject;
        [SerializeField] private GameObject costumeImageObject;
        [SerializeField] private Texture2D bikeTexture;
        [SerializeField] private Texture2D smokeTexture;
        [SerializeField] private Texture2D costumeTexture; 
        [SerializeField] private Texture2D drinkTexture; 
        private GameManager _gm;

        private void Start()
        {
            _gm = GameManager.Instance;
            
            HandleBikeImage();
            HandleCostumeImage();
        }

        private void HandleBikeImage()
        {
            if (bikeImageObject == null || bikeTexture == null || smokeTexture == null) return;
            
            var bikeImage = bikeImageObject.GetComponent<RawImage>();
            if (_gm.playerSmokes)
            {
                bikeImage.texture = smokeTexture;
            }
            else
            {
                bikeImage.texture = bikeTexture;
            }
        }

        private void HandleCostumeImage()
        {
            if (costumeImageObject == null || costumeTexture == null || drinkTexture == null) return;
            
            var costumeImage = costumeImageObject.GetComponent<RawImage>();
            if (_gm.playerDrinks)
            {
                costumeImage.texture = drinkTexture;
            }
            else
            {
                costumeImage.texture = costumeTexture;
            }
            
        }
    }
}