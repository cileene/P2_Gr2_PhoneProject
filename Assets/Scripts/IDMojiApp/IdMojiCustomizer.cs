using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace IDMojiApp
{
    public class IdMojiCustomizer : MonoBehaviour
    {
        public Canvas startCanvas;
        public Canvas customizerCanvas;
        public GameObject customizerElements;

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
        
        public CharacterCustomizer characterCustomizer;

        private void Start()
        {
            // Ensure only the start canvas is visible
            startCanvas.gameObject.SetActive(true);
            customizerCanvas.gameObject.SetActive(false);
            //Canvas.gameObject.SetActive(false);

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

        // Function to start customization
        private void OpenCustomizer()
        {
            startCanvas.gameObject.SetActive(false);
            customizerCanvas.gameObject.SetActive(true);
            characterCustomizer.InitializeCustomization();
        }

        // Function to finish customization
        private void FinishCustomization()
        {
            startCanvas.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(true);
            customizerElements.SetActive(false);
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

        public void OnEyesButtonClicked()
        {
            characterCustomizer.eyesImage.enabled = true;
            characterCustomizer.UpdateEyeColor("Turqouise");
            characterCustomizer.SelectEyes(0);
        }

        public void OnMouthButtonClicked()
        {
            characterCustomizer.mouthImage.enabled = true;
            characterCustomizer.UpdateMouthColor("Red");
            characterCustomizer.SelectMouth(0);
        }

        public void OnHairButtonClicked()
        {
            characterCustomizer.hairImage.enabled = true;
            characterCustomizer.UpdateHairColor("Blonde");
            characterCustomizer.SelectHair(0);
        }

        public void ExpandCharacterPreview()
        {
            // Ensure the RectTransform of characterPreview is accessed
            RectTransform previewFaceRect = characterCustomizer.faceImage.GetComponent<RectTransform>();
            previewFaceRect.anchoredPosition = new Vector2(-8, -373);

            RectTransform previewEyesRect = characterCustomizer.eyesImage.GetComponent<RectTransform>();
            previewEyesRect.anchoredPosition = new Vector2(-8, -279);

            RectTransform previewMouthRect = characterCustomizer.mouthImage.GetComponent<RectTransform>();
            previewMouthRect.anchoredPosition = new Vector2(-8, -507);
            
            RectTransform previewHairRect = characterCustomizer.hairImage.GetComponent<RectTransform>();
            previewHairRect.anchoredPosition = new Vector2(-8, -225);

            // Scale the individual images
            characterCustomizer.faceImage.transform.localScale = new Vector2(1.8f, 1.8f);
            characterCustomizer.eyesImage.transform.localScale = new Vector2(1.8f, 1.8f);
            characterCustomizer.mouthImage.transform.localScale = new Vector2(1.8f, 1.8f);
            characterCustomizer.hairImage.transform.localScale = new Vector2(1.8f, 1.8f);
        }
    }
}