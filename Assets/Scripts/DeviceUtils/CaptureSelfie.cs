using System.Collections;
using System.IO;
using UnityEngine;

namespace DeviceUtils
{
    public class CaptureSelfie : MonoBehaviour
    {
        private WebCamTexture _webCamTexture;
        private Texture2D _selfieTexture;
        private string _dataPath;

        private void Start()
        {
            // check if there's already a selfie saved
            if (File.Exists(Application.persistentDataPath + "/Player_Data/Selfie.png"))
            {
                return;
            }

            CaptureSelfieNow();
        }

        public void CaptureSelfieNow()
        {
            _dataPath = Application.persistentDataPath + "/Player_Data/Selfie.png";
            StartCoroutine(InitializeCameraAndCapture());
        }

        private void Capture()
        {
            if (_webCamTexture != null)
            {
                // Determine target width and compute target height based on aspect ratio
                int targetWidth = 128; // Desired width for low-res capture
                float aspectRatio = (float)_webCamTexture.height / _webCamTexture.width;
                int targetHeight = Mathf.RoundToInt(targetWidth * aspectRatio);

                // Create a low-res texture
                _selfieTexture = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false, false);

                // Downsample from webcam texture
                Color[] originalPixels = _webCamTexture.GetPixels();
                Color[] resizedPixels = Resample(originalPixels, _webCamTexture.width, _webCamTexture.height, targetWidth, targetHeight);
                for (int i = 0; i < resizedPixels.Length; i++) //TODO: wow
                {
                    resizedPixels[i].r = Mathf.LinearToGammaSpace(resizedPixels[i].r);
                    resizedPixels[i].g = Mathf.LinearToGammaSpace(resizedPixels[i].g);
                    resizedPixels[i].b = Mathf.LinearToGammaSpace(resizedPixels[i].b);
                }
                _selfieTexture.SetPixels(resizedPixels);
                _selfieTexture.Apply();

                // Save to file
                byte[] bytes = _selfieTexture.EncodeToPNG();
                File.WriteAllBytes(_dataPath, bytes);

                GameManager.Instance.selfiePicture = _dataPath;
                GameManager.Instance.phoneUnlocked = true;
            }
        }

        // Helper method to resample pixels
        private Color[] Resample(Color[] original, int originalWidth, int originalHeight, int newWidth, int newHeight)
        {
            Color[] newPixels = new Color[newWidth * newHeight];
            float ratioX = (float)originalWidth / newWidth;
            float ratioY = (float)originalHeight / newHeight;

            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int originalX = Mathf.FloorToInt(x * ratioX);
                    int originalY = Mathf.FloorToInt(y * ratioY);
                    newPixels[y * newWidth + x] = original[originalY * originalWidth + originalX];
                }
            }
            return newPixels;
        }

        private IEnumerator InitializeCameraAndCapture()
        {
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            }

            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.LogWarning("Camera permission not granted.");
                yield break;
            }

            WebCamDevice[] devices = WebCamTexture.devices;
            foreach (var device in devices)
            {
                if (device.isFrontFacing)
                {
                    _webCamTexture = new WebCamTexture(device.name);
                    _webCamTexture.Play();
                }
            }

            if (_webCamTexture == null)
            {
                Debug.LogWarning("No front-facing camera found.");
                yield break;
            }

            float timeout = 2f;
            float timer = 0f;
            while (!_webCamTexture.didUpdateThisFrame && timer < timeout)
            {
                yield return null;
                timer += Time.deltaTime;
            }

            Capture();
            _webCamTexture.Stop();
        }
    }
}