using UnityEngine;
using UnityEngine.UI;

namespace GalleryApp
{
    // This changes the images based on the players answers in Death
    public class PhotoSwapper : MonoBehaviour
    {
        [SerializeField] private GameObject bikeImageObject;
        [SerializeField] private GameObject costumeImageObject;
        [SerializeField] private Sprite bikeTexture;
        [SerializeField] private Sprite smokeTexture;
        [SerializeField] private Sprite costumeTexture; 
        [SerializeField] private Sprite drinkTexture; 
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
            
            var bikeImage = bikeImageObject.GetComponent<Image>();
            if (_gm.playerSmokes)
            {
                bikeImage.overrideSprite = smokeTexture;
            }
            else
            {
                bikeImage.overrideSprite = bikeTexture;
            }
        }

        private void HandleCostumeImage()
        {
            if (costumeImageObject == null || costumeTexture == null || drinkTexture == null) return;
            
            var costumeImage = costumeImageObject.GetComponent<Image>();
            if (_gm.playerDrinks)
            {
                costumeImage.overrideSprite = drinkTexture;
            }
            else
            {
                costumeImage.overrideSprite = costumeTexture;
            }
            
        }
    }
}