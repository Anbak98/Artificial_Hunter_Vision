using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ImageCapture : MonoBehaviour
{
    public string savePath = "Img\\"; // Specify the save path and file name
    public byte[] m_imgbyte;
    
    public RenderTexture renderTexture;
    public void CaptureAndSaveImage(Camera captureCamera)
    {
        /*
        // Ensure the captureCamera is not null
        if (captureCamera == null)
        {
            Debug.LogError("Capture Camera is not assigned!");
            return;
        }

        // Create a RenderTexture to capture the camera's output
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        captureCamera.targetTexture = renderTexture;

        // Render the camera's view into the RenderTexture
        captureCamera.Render();
        */

        // Create a Texture2D and read the pixels from the RenderTexture
        renderTexture = captureCamera.targetTexture;
        Texture2D screenShot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenShot.Apply();

        //
        m_imgbyte = screenShot.EncodeToPNG();


        
        System.IO.File.WriteAllBytes(savePath + Random.Range(0, 50000) + ".png", m_imgbyte);

        /* Clean up resources
        RenderTexture.active = null;
        captureCamera.targetTexture = null;
        //Destroy(renderTexture);*/

        Debug.Log("Image captured and saved to " + savePath);
        
    }
}