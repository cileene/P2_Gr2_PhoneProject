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

        private string _fileName = "/Selfie.png";

        private void Start()
        {
            // check if there's already a selfie saved
            if (File.Exists(GameManager.Instance.dataPath + _fileName))
            {
                return;
            }

            CaptureSelfieNow();
        }

        public void CaptureSelfieNow()
        {
            _dataPath = GameManager.Instance.dataPath + _fileName;
            StartCoroutine(InitializeCameraAndCapture());
        }

        private void Capture()
        {
            if (_webCamTexture != null)
            {
                // Capture full-resolution selfie
                int width = _webCamTexture.width;
                int height = _webCamTexture.height;
                _selfieTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
                Color[] pixels = _webCamTexture.GetPixels();
                _selfieTexture.SetPixels(pixels);
                _selfieTexture.Apply();

                // Save to file
                byte[] bytes = _selfieTexture.EncodeToPNG();
                File.WriteAllBytes(_dataPath, bytes);

                GameManager.Instance.selfiePicture = _dataPath;
                GameManager.Instance.phoneUnlocked = true;
            }
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