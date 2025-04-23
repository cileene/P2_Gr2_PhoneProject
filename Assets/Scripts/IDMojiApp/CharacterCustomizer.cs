using UnityEngine;
using UnityEngine.UI;

namespace IDMojiApp
{
    public class CharacterCustomizer : MonoBehaviour
    {
        [Header("Character Customization Preview field")]
        public Canvas characterPreview; // Assign your character preview UI Image in Unity
          // Character preview layers
        public Image faceImage;
        public Image eyesImage;
        public Image mouthImage;
        public Image hairImage;
    
        [Header("Face Customization")]
        // Arrays for face shapes of different colors
        public Sprite[] faceShapesPale;
        public Sprite[] faceShapesLight;
        public Sprite[] faceShapesMedium;
        public Sprite[] faceShapesBrown;
        public Sprite[] faceShapesDark;
        private string[] faceColors = { "Pale", "Light", "Medium", "Brown", "Dark" }; 

        //Arrays for face color and shape variant buttons
        public Button[] faceColorVariants;
        public Button[] faceVariants;

        [Header("Eye Customization")]
        //Arrays for eye styles of different colors
        public Sprite[] eyeStylesTurqouise;
        public Sprite[] eyeStylesBlue;
        public Sprite[] eyeStylesGreen;
        public Sprite[] eyeStylesBrown;
        public Sprite[] eyeStylesBlack;
        private string[] eyeColors = { "Turqouise", "Blue", "Green", "Brown", "Black" };

        //Array for eye color and shape variant buttons
        public Button[] eyeColorVariants;
        public Button[] eyeVariants;


        [Header("Mouth Customization")]
        //Arrays for mouth styles of different colors
        public Sprite[] mouthStylesPink;
        public Sprite[] mouthStylesPale;
        public Sprite[] mouthStylesCold;
        public Sprite[] mouthStylesPeach;
        public Sprite[] mouthStylesRed;
        private string[] mouthColors = { "Pink", "Pale", "Cold", "Peach", "Red" };

        //Array for mouth color and shape variant buttons
        public Button[] mouthColorVariants;
        public Button[] mouthVariants;


        [Header("Hair Customization")]
        //Arrays for hair styles
        public Sprite[] hairStylesBlonde;
        public Sprite[] hairStylesDarkBlonde;
        public Sprite[] hairStylesOrange;
        public Sprite[] hairStylesBrown;
        public Sprite[] hairStylesBlack;
        private string[] hairColors = { "Blonde", "Dark Blonde", "Orange", "Brown", "Black" };

        //Array for hair color and style variant buttons
        public Button[] hairColorVariants;
        public Button[] hairVariants;

        //Arrays for body types
        public Sprite[] bodyTypes;        

        // Selected features
        public Sprite selectedFace;
        public Sprite selectedEyes;
        public Sprite selectedMouth;
        public Sprite selectedHair;
        private Sprite selectedBody;

        // Selected color variants
        private Sprite[] selectedFaceColor;
        private Sprite[] selectedEyeColor;
        private Sprite[] selectedMouthColor;    
        private Sprite[] selectedHairColor;

        private void Start()
        {
            // makes sure the correct color of the different elements can be selected and view in the preview
            // Assign button click events for face color variants
            for (int i = 0; i < faceColorVariants.Length; i++)
            {
                int index = i; // Capture index for closure
                faceColorVariants[i].onClick.AddListener(() => UpdateFaceColor(faceColors[index]));
            } 
            for (int i = 0; i < eyeColorVariants.Length; i++)
            {
                int index = i; // Capture index for closure
                eyeColorVariants[i].onClick.AddListener(() => UpdateEyeColor(eyeColors[index]));
            }
            for (int i = 0; i < mouthColorVariants.Length; i++)
            {
                int index = i; // Capture index for closure
                mouthColorVariants[i].onClick.AddListener(() => UpdateMouthColor(mouthColors[index]));
            }
            for (int i = 0; i < hairColorVariants.Length; i++)
            {
                int index = i; // Capture index for closure
                hairColorVariants[i].onClick.AddListener(() => UpdateHairColor(hairColors[index]));
            }
            UpdateCharacterPreview();
        }

        public void SelectFace(int index)
        {
            selectedFace = selectedFaceColor[index];
            UpdateCharacterPreview();
        }

        public void SelectEyes(int index)
        {
            selectedEyes = selectedEyeColor[index];
            UpdateCharacterPreview();
        }

        public void SelectMouth(int index)
        {
            selectedMouth = selectedMouthColor[index];
            UpdateCharacterPreview();
        }

        public void SelectHair(int index)
        {
            selectedHair = selectedHairColor[index];
            UpdateCharacterPreview();
        }

        public void SelectBody(int index)
        {
            selectedBody = bodyTypes[index];
            UpdateCharacterPreview();
        }

        private void UpdateCharacterPreview()
        {
            // Update face image if a face is selected
            if (faceImage != null && selectedFace != null)
            {
                faceImage.sprite = selectedFace;
            }
            else if (faceImage != null)
            {
                faceImage.sprite = null;
            }

            // Update eyes image if eyes are selected
            if (eyesImage != null && selectedEyes != null)
            {
                eyesImage.sprite = selectedEyes;
            }
            else if (eyesImage != null)
            {
                eyesImage.sprite = null;
            }

            // Update mouth image if a mouth is selected
            if (mouthImage != null && selectedMouth != null)
            {
                mouthImage.sprite = selectedMouth;
            }
            else if (mouthImage != null)
            {
                mouthImage.sprite = null;
            }

            // Update hair image if hair is selected
            if (hairImage != null && selectedHair != null)
            {
                hairImage.sprite = selectedHair;
            }
            else if (hairImage != null)
            {
                hairImage.sprite = null;
            }
        }
        public void UpdateFaceColor(string color)
        {
            switch(color)
            {
                case "Pale":
                    selectedFaceColor = faceShapesPale;
                    break;
                case "Light":
                    selectedFaceColor = faceShapesLight;
                    break;
                case "Medium":
                    selectedFaceColor = faceShapesMedium;
                    break;
                case "Brown":
                    selectedFaceColor = faceShapesBrown;
                    break;
                case "Dark":
                    selectedFaceColor = faceShapesDark;
                    break;
            }
            
        for (int i = 0; i < faceVariants.Length; i++)
            {
                if (i < selectedFaceColor.Length)
                {
                    faceVariants[i].image.sprite = selectedFaceColor[i];
                }
            }
        }
        public void UpdateEyeColor(string color)
        {
            switch(color)
            {
                case "Turqouise":
                    selectedEyeColor = eyeStylesTurqouise;
                    break;
                case "Blue":
                    selectedEyeColor = eyeStylesBlue;
                    break;
                case "Green":
                    selectedEyeColor = eyeStylesGreen;
                    break;
                case "Brown":
                    selectedEyeColor = eyeStylesBrown;
                    break;
                case "Black":
                    selectedEyeColor = eyeStylesBlack;
                    break;
            }  
            for (int i = 0; i < eyeVariants.Length; i++)
            {
                if (i < selectedEyeColor.Length)
                {
                    eyeVariants[i].image.sprite = selectedEyeColor[i];
                }
            }
        }
            public void UpdateMouthColor(string color)
        {
            switch(color)
            {
                case "Pink":
                    selectedMouthColor = mouthStylesPink;
                    break;
                case "Pale":
                    selectedMouthColor = mouthStylesPale;
                    break;
                case "Cold":
                    selectedMouthColor = mouthStylesCold;
                    break;
                case "Peach":
                    selectedMouthColor = mouthStylesPeach;
                    break;
                case "Red":
                    selectedMouthColor = mouthStylesRed;
                    break;
            } 
            for (int i = 0; i < mouthVariants.Length; i++)
            {
                if (i < selectedMouthColor.Length)
                {
                    mouthVariants[i].image.sprite = selectedMouthColor[i];
                }
            }
        }
            public void UpdateHairColor(string color)
        {
            switch(color)
            {
                case "Blonde":
                    selectedHairColor = hairStylesBlonde;
                    break;
                case "Dark Blonde":
                    selectedHairColor = hairStylesDarkBlonde;
                    break;
                case "Orange":
                    selectedHairColor = hairStylesOrange;
                    break;
                case "Brown":
                    selectedHairColor = hairStylesBrown;
                    break;
                case "Black":
                    selectedHairColor = hairStylesBlack;
                    break;
            }  
            for (int i = 0; i < hairVariants.Length; i++)
            {
                if (i < selectedHairColor.Length)
                {
                    hairVariants[i].image.sprite = selectedHairColor[i];
                }
            }   
        }
        public void InitializeCustomization()
        {
            faceImage.sprite = faceShapesPale[0];
            eyesImage.enabled = false;
            mouthImage.enabled = false;
            hairImage.enabled = false;

            UpdateFaceColor("Pale");
            SelectFace(0);
        }
    } 
}