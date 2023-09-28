using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ImageUploader : MonoBehaviour
{
    private string serverUrl = "http://localhost:5555/upload_image"; // 서버 URL을 적절히 변경하세요.
    public RenderTexture renderTexture; // Assign your camera in the Unity Inspector
    public Camera CNN_INPUT_Camera; // Assign your camera in the Unity Inspector
    RenderTexture m_CNN_INPUT;
    void Start(){
        m_CNN_INPUT = CNN_INPUT_Camera.targetTexture;
        StartCoroutine(UploadImage(RenderTextureToBytes(m_CNN_INPUT)));
    }
    void Update(){

    }
    // Function to convert a RenderTexture to a byte array
    public byte[] RenderTextureToBytes(RenderTexture rt, TextureFormat format = TextureFormat.RGB24)
    {
        // Set the RenderTexture as the active RenderTexture
        RenderTexture.active = rt;
        
        // Create a new Texture2D
        Texture2D tex = new Texture2D(rt.width, rt.height, format, false);
        
        // Read the pixels from the RenderTexture into the Texture2D
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        
        // Apply changes to the Texture2D
        tex.Apply();
        
        // Encode the Texture2D into a byte array (you can choose the encoding format)
        byte[] bytes = tex.EncodeToPNG(); // Use EncodeToJPG for JPG format
        
        // Cleanup
        Destroy(tex);
        
        return bytes;
    }

    public class JsonData
    {
        public byte[] predict;
    }
    // 이미지를 업로드하는 함수: 이미지 데이터를 매개변수에
    public IEnumerator UploadImage(byte[] imageData)
    {

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
                Texture2D m_texture2D = ((DownloadHandlerTexture)www.downloadHandler).texture;
                m_texture2D.Apply();

                RenderTexture.active = renderTexture;
                Graphics.Blit(m_texture2D, renderTexture);
            }
        }
    }

    // 이미지 업로드 버튼 클릭 시 호출될 함수
    public void OnUploadButtonClick()
    {        
				// 이미지 업로드 시작
        StartCoroutine(UploadImage(RenderTextureToBytes(m_CNN_INPUT)));
    }
}
