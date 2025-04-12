using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
public class IdMojiCustomizer : MonoBehaviour
{

public Canvas startCanvas;
public Canvas customizerCanvas;
public Canvas endCanvas;

//system Buttons
public Button createButton;
public Button doneButton;
public Button exitButton;
public Button confirmButton;

//CategoryButtons
public Button faceButton;
public Button eyesButton;
public Button mouthButton;
public Button hairButton;
public Button bodyButton;


//scrollbars
public ScrollRect faceScroll;
public ScrollRect eyesScroll;
public ScrollRect mouthScroll;
public ScrollRect hairScroll;
public ScrollRect bodyScroll;

/*
//elements within each scrollbar
public GridLayoutGroup colorGridFace;
public GridLayoutGroup optionsGridFace;
public GridLayoutGroup colorGridEyes;
public GridLayoutGroup optionsGridEyes;
public GridLayoutGroup colorGridMouth;
public GridLayoutGroup optionsGridMouth;
public GridLayoutGroup colorGridHair;
public GridLayoutGroup optionsGridHair;
*/

public CharacterCustomizer characterCustomizer;

    private void Start()
    {
        // Ensure only the start canvas is visible
        startCanvas.gameObject.SetActive(true);
        customizerCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(false);

        // Hide all scrollrects at the start
        HideAllScrollRects();

        // Assign button click events
        createButton.onClick.AddListener(OpenCustomizer);
        doneButton.onClick.AddListener(FinishCustomization);
        exitButton.onClick.AddListener(ExitApp);
        confirmButton.onClick.AddListener(ExitApp);

       // Assign category button listeners
        faceButton.onClick.AddListener(() => ShowScrollRect(faceScroll));
        eyesButton.onClick.AddListener(() => ShowScrollRect(eyesScroll));
        mouthButton.onClick.AddListener(() => ShowScrollRect(mouthScroll));
        hairButton.onClick.AddListener(() => ShowScrollRect(hairScroll));
        bodyButton.onClick.AddListener(() => ShowScrollRect(bodyScroll));
    }
    public void StartCustomization()
    {   
        startCanvas.gameObject.SetActive(false);  // Hide Start Canvas
        customizerCanvas.gameObject.SetActive(true); // Show Customizer Canvas
    }
    // Function to start customization
    private void OpenCustomizer()
    {
        startCanvas.gameObject.SetActive(false);
        customizerCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
    }
    // Function to finish customization
    private void FinishCustomization()
    {
        startCanvas.gameObject.SetActive(false);
        customizerCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(true);
    }
     // Function to exit app (you can replace this with a proper quit function)
    private void ExitApp()
    {
        SceneHandler.LoadScene("Home");
    }
    public void ShowScrollRect(ScrollRect selectedScroll)
    {
        // Sets the face scroll rect to be default
        faceScroll.gameObject.SetActive(true);
        eyesScroll.gameObject.SetActive(false);
        mouthScroll.gameObject.SetActive(false);
        hairScroll.gameObject.SetActive(false);
        bodyScroll.gameObject.SetActive(false);

        if (selectedScroll != null)
    {
        selectedScroll.gameObject.SetActive(true);
        Debug.Log(selectedScroll.name + " is now active");
    }
    }
    // Hide all scroll rects
    private void HideAllScrollRects()
    {
        faceScroll.gameObject.SetActive(true);
        eyesScroll.gameObject.SetActive(false);
        mouthScroll.gameObject.SetActive(false);
        hairScroll.gameObject.SetActive(false);
        bodyScroll.gameObject.SetActive(false);
    }

    // Function to toggle scroll rects
    private void ToggleScroll(ScrollRect scrollToShow)
    {
        HideAllScrollRects();
        scrollToShow.gameObject.SetActive(true);
    }
}