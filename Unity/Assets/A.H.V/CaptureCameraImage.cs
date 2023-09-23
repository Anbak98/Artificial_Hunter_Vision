using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class CaptureCameraImage : MonoBehaviour
{
    public Camera captureCamera; // Assign your camera in the Unity Inspector
    public string savePath = "Img\\"; // Specify the save path and file name
    public float CaptureDelay = 1;
    public byte[] m_imgbyte;
    [SerializeField]public float Counter;
    public void Start(){
        Counter = 0;
    }
    public void Update()
    {
        Counter += Time.deltaTime;
        // Check for user input to trigger the capture (e.g., a key press)
        if (Counter > CaptureDelay)
        {
            CaptureAndSaveImage();
            Counter = 0;
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

        //
        m_imgbyte = screenShot.EncodeToPNG();


        /*
        System.IO.File.WriteAllBytes(savePath + Random.Range(0, 10000) + ".png", m_bytes);

        // Clean up resources
        RenderTexture.active = null;
        captureCamera.targetTexture = null;
        Destroy(renderTexture);

        Debug.Log("Image captured and saved to " + savePath);
        */
    }
    public string serverUrl = "http://localhost:8000/upload"; // 서버 URL을 적절히 변경하세요.

    // 이미지를 업로드하는 함수: 이미지 데이터를 매개변수에
    IEnumerator UploadImage(byte[] imageData)
    {

        Debug.Log("d");
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "image.png", "image/png");

        // POST 요청 보내기
        using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Image upload failed: " + www.error);
            }
            else
            {
                Debug.Log("Image upload successful");
            }
        }
    }
    public void OnUploadButtonClick()
    {        
				// 이미지 업로드 시작
        Debug.Log("h");
        StartCoroutine(UploadImage(m_imgbyte));
    }
}
