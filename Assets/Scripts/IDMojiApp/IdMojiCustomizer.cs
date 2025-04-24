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
        public Button cancelButton;

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
        
        // Original preview positions and scale for collapsing later
        private Vector2 faceOriginalPos;
        private Vector2 eyesOriginalPos;
        private Vector2 mouthOriginalPos;
        private Vector2 hairOriginalPos;
        private Vector3 faceOriginalScale;
        private Vector3 eyesOriginalScale;
        private Vector3 mouthOriginalScale;
        private Vector3 hairOriginalScale;

        private void Start()
        {
            if (GameManager.Instance.iDMojiCreated)
            {
                startCanvas.gameObject.SetActive(false);
                customizerCanvas.gameObject.SetActive(true);
            }
            else
            {
                startCanvas.gameObject.SetActive(true);
                customizerCanvas.gameObject.SetActive(false);
                
                // Hide all scrollrects at the start
                HideAllScrollRects();
            }

            // Assign button click events
            createButton.onClick.AddListener(OpenCustomizer);
            doneButton.onClick.AddListener(FinishCustomization);
            exitButton.onClick.AddListener(ExitApp);
            //confirmButton.onClick.AddListener(ExitApp);
            cancelButton.onClick.AddListener(CancelAndGoBack);

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
        
        public void CancelAndGoBack()
        {
            confirmButton.gameObject.SetActive(false);
            customizerElements.SetActive(true);
            cancelButton.gameObject.SetActive(false);
            CollapseCharacterPreview();
        }

        // Function to finish customization
        private void FinishCustomization()
        {
            startCanvas.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(true);
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
            faceOriginalPos = previewFaceRect.anchoredPosition;
            previewFaceRect.anchoredPosition = new Vector2(-8, -373);

            RectTransform previewEyesRect = characterCustomizer.eyesImage.GetComponent<RectTransform>();
            eyesOriginalPos = previewEyesRect.anchoredPosition;
            previewEyesRect.anchoredPosition = new Vector2(-8, -279);

            RectTransform previewMouthRect = characterCustomizer.mouthImage.GetComponent<RectTransform>();
            mouthOriginalPos = previewMouthRect.anchoredPosition;
            previewMouthRect.anchoredPosition = new Vector2(-8, -507);
            
            RectTransform previewHairRect = characterCustomizer.hairImage.GetComponent<RectTransform>();
            hairOriginalPos = previewHairRect.anchoredPosition;
            previewHairRect.anchoredPosition = new Vector2(-8, -225);

            // Scale the individual images
            faceOriginalScale = characterCustomizer.faceImage.transform.localScale;
            characterCustomizer.faceImage.transform.localScale = new Vector2(1.8f, 1.8f);
            
            eyesOriginalScale = characterCustomizer.eyesImage.transform.localScale;
            characterCustomizer.eyesImage.transform.localScale = new Vector2(1.8f, 1.8f);
            
            mouthOriginalScale = characterCustomizer.mouthImage.transform.localScale;
            characterCustomizer.mouthImage.transform.localScale = new Vector2(1.8f, 1.8f);
            
            hairOriginalScale = characterCustomizer.hairImage.transform.localScale;
            characterCustomizer.hairImage.transform.localScale = new Vector2(1.8f, 1.8f);
        }
        
        private void CollapseCharacterPreview()
        {
            // Collapse the character preview
            RectTransform previewFaceRect = characterCustomizer.faceImage.GetComponent<RectTransform>();
            previewFaceRect.anchoredPosition = faceOriginalPos;
            characterCustomizer.faceImage.transform.localScale = faceOriginalScale;

            RectTransform previewEyesRect = characterCustomizer.eyesImage.GetComponent<RectTransform>();
            previewEyesRect.anchoredPosition = eyesOriginalPos;
            characterCustomizer.eyesImage.transform.localScale = eyesOriginalScale;

            RectTransform previewMouthRect = characterCustomizer.mouthImage.GetComponent<RectTransform>();
            previewMouthRect.anchoredPosition = mouthOriginalPos;
            characterCustomizer.mouthImage.transform.localScale = mouthOriginalScale;
            
            RectTransform previewHairRect = characterCustomizer.hairImage.GetComponent<RectTransform>();
            previewHairRect.anchoredPosition = hairOriginalPos;
            characterCustomizer.hairImage.transform.localScale = hairOriginalScale;
        }
    }
}