using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizer : MonoBehaviour
{
    public Canvas characterPreview; // Assign your character preview UI Image in Unity

    // Arrays for customization
    public Sprite[] faceShapes;
    public Sprite[] hairStyles;
    public Sprite[] eyeStyles;
    public Sprite[] bodyTypes;

    public Sprite[] faceColorVariants;

   // Character preview layers
    public Image faceImage;
    public Image eyesImage;
    public Image hairImage;

    // Selected features
    private Sprite selectedFace;
    private Sprite selectedHair;
    private Sprite selectedEyes;
    private Sprite selectedBody;



    void Start()
    {
        // Set default values
        if (faceShapes.Length > 0) selectedFace = faceShapes[0];
        if (hairStyles.Length > 0) selectedHair = hairStyles[0];
        if (eyeStyles.Length > 0) selectedEyes = eyeStyles[0];
        if (bodyTypes.Length > 0) selectedBody = bodyTypes[0];

        UpdateCharacterPreview();
    }

    public void SelectFace(int index)
    {
        Debug.Log("Selected face index: " + index);
        selectedFace = faceShapes[index];
        UpdateCharacterPreview();
    }

    public void SelectHair(int index)
    {
        selectedHair = hairStyles[index];
        UpdateCharacterPreview();
    }

    public void SelectEyes(int index)
    {
        selectedEyes = eyeStyles[index];
        UpdateCharacterPreview();
    }

    public void SelectBody(int index)
    {
        selectedBody = bodyTypes[index];
        UpdateCharacterPreview();
    }

 void UpdateCharacterPreview()
    {
        Debug.Log("Updating character preview");
        if (faceImage != null && selectedFace != null)
        {
            faceImage.sprite = selectedFace;
        }
        if (eyesImage != null && selectedEyes != null)
        {
            eyesImage.sprite = selectedEyes;
        }
        if (hairImage != null && selectedHair != null)
        {
            hairImage.sprite = selectedHair;
        }
    }
}
