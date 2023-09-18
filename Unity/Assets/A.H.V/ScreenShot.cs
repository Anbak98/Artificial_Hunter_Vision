using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public Camera captureCamera; // Assign ;your camera in the Unity Inspector
    public float ScreenShotDelay;
    public float ScreenShotTimer;
    public string savePath = ".png"; // Specify the save path and file name

    public void Start(){
        ScreenShotDelay = 2;
        ScreenShotTimer = 0;
    }
    private void Update()
    {
        ScreenShotTimer += Time.deltaTime;
        // Check for user input to trigger the capture (e.g., a key press)
        if (ScreenShotTimer > ScreenShotDelay)
        {
            ScreenShotTimer = 0;
            CaptureAndSaveImage();
        }
    }

    private void CaptureAndSaveImage()
    {
        // Ensure the captureCamera is not null
        if (captureCamera == null)
        {
            Debug.LogError("Capture Camera is not assigned!");
            return;
        }

        // Create a RenderTexture to capture the camera's output
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        captureCamera.targetTexture = renderTexture;

        // Render the camera's view into the RenderTexture
        captureCamera.Render();

        // Create a Texture2D and read the pixels from the RenderTexture
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // Save the captured image to a file
        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(Random.Range(0, 10000) + savePath, bytes);

        // Clean up resources
        RenderTexture.active = null;
        captureCamera.targetTexture = null;
        Destroy(renderTexture);

        Debug.Log("Image captured and saved to " + savePath);
    }
}
