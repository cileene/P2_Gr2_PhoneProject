using System;
using System.Collections;
using UnityEngine;
using System.IO;

// Use the script to capture a selfie with the device's webcam
//TODO: Fix this
public class CaptureSelfie : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    private Texture2D _selfieTexture;
    private string _dataPath;
    
    public void CaptureSelfieNow()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/Selfie.png";

        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (var device in devices) {
            if (device.isFrontFacing) {
                _webCamTexture = new WebCamTexture(device.name);
                _webCamTexture.Play();
            }
        }
        StartCoroutine(CaptureWithDelay());
    }

    private void Capture()
    {
        if (_webCamTexture != null)
        {
            _selfieTexture = new Texture2D(_webCamTexture.width, _webCamTexture.height);
            _selfieTexture.SetPixels(_webCamTexture.GetPixels());
            _selfieTexture.Apply();

            byte[] bytes = _selfieTexture.EncodeToPNG();
            File.WriteAllBytes(_dataPath, bytes);
            
            GameManager.Instance.selfiePicture = _dataPath;
            GameManager.Instance.phoneUnlocked = true;
        }
    }

    private IEnumerator CaptureWithDelay()
    {
        // Wait until the end of the frame.
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1f);

        // Wait until the webcam has updated its frame data.
        float timeout = 2f; // maximum wait time in seconds
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