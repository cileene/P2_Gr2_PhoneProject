using UnityEngine;
using UnityEngine.UI;

namespace IDMoji
{
    public class CharacterCustomizer : MonoBehaviour
    {
        public Canvas characterPreview; // Assign your character preview UI Image in Unity

        // Arrays for customization
        public Sprite[] faceShapesPale;
        public Sprite[] faceShapesLight;
        public Sprite[] faceShapesMedium;
        public Sprite[] faceShapesBrown;
        public Sprite[] faceShapesDark;

        //Arrays for eye styles
        public Sprite[] eyeStylesTurqouise;
        public Sprite[] eyeStylesGreen;
        public Sprite[] eyeStylesBlue;
        public Sprite[] eyeStylesBrown;
        public Sprite[] eyeStylesBlack;

        //Arrays for mouth styles
        public Sprite[] mouthStylesPink;
        public Sprite[] mouthStylesPale;
        public Sprite[] mouthStylesCold;
        public Sprite[] mouthStylesPeach;
        public Sprite[] mouthStylesRed;

        //Arrays for hair styles
        public Sprite[] hairStylesBlonde;
        public Sprite[] hairStylesDarkBlonde;
        public Sprite[] hairStylesBrown;
        public Sprite[] hairStylesBlack;
        public Sprite[] hairStylesOrange;

        //Arrays for body types
        public Sprite[] bodyTypes;

        public Button[] faceColorVariants;
        public Button[] eyeColorVariants;
        public Button[] mouthColorVariants;
        public Button[] hairColorVariants;

        // Character preview layers
        public Image faceImage;
        public Image eyesImage;
        public Image mouthImage;
        public Image hairImage;

        // Selected features
        private Sprite selectedFace;
        private Sprite selectedEyes;
        private Sprite selectedMouth;
        private Sprite selectedHair;
        private Sprite selectedBody;



        private void Start()
        {
            // Set default values
            if (faceShapesPale.Length > 0) selectedFace = faceShapesPale[0];
            if (eyeStylesTurqouise.Length > 0) selectedEyes = eyeStylesTurqouise[0];
            if (mouthStylesPink.Length > 0) selectedMouth = mouthStylesPink[0];
            if (hairStylesBlonde.Length > 0) selectedHair = hairStylesBlonde[0];
            if (bodyTypes.Length > 0) selectedBody = bodyTypes[0];

            UpdateCharacterPreview();
        }

        public void SelectFace(int index)
        {
            Debug.Log("Selected face index: " + index);
            selectedFace = faceShapesPale[index];
            UpdateCharacterPreview();
        }

        public void SelectEyes(int index)
        {
            selectedEyes = eyeStylesTurqouise[index];
            UpdateCharacterPreview();
        }

        public void SelectMouth(int index)
        {
            selectedMouth = mouthStylesPink[index];
            UpdateCharacterPreview();
        }

        public void SelectHair(int index)
        {
            selectedHair = hairStylesBlonde[index];
            UpdateCharacterPreview();
        }

        public void SelectBody(int index)
        {
            selectedBody = bodyTypes[index];
            UpdateCharacterPreview();
        }

        private void UpdateCharacterPreview()
        {
            Debug.Log("Updating character preview");

            // Update face image if a face is selected, otherwise clear it
            if (faceImage != null && selectedFace != null)
            {
                faceImage.sprite = selectedFace;
            }
            else if (faceImage != null)
            {
                faceImage.sprite = null;
            }

            // Update eyes image if eyes are selected, otherwise clear it
            if (eyesImage != null && selectedEyes != null)
            {
                eyesImage.sprite = selectedEyes;
            }
            else if (eyesImage != null)
            {
                eyesImage.sprite = null;
            }

            // Update mouth image if a mouth is selected, otherwise clear it
            if (mouthImage != null && selectedMouth != null)
            {
                mouthImage.sprite = selectedMouth;
            }
            else if (mouthImage != null)
            {
                mouthImage.sprite = null;
            }

            // Update hair image if hair is selected, otherwise clear it
            if (hairImage != null && selectedHair != null)
            {
                hairImage.sprite = selectedHair;
            }
            else if (hairImage != null)
            {
                hairImage.sprite = null;
            }
        }
    }

}