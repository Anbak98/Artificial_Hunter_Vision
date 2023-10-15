using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ImageUploader : MonoBehaviour
{
    private string serverUrl = "http://localhost:5555/upload"; // 서버 URL을 적절히 변경하세요.

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
                byte[] imageBytes = www.downloadHandler.data;
            }
        }
    }
    public byte[] RenderTexture_To_Byte(RenderTexture _renderImg){
        // Ensure the RenderTexture is active or set it as the active RenderTexture
        RenderTexture.active = _renderImg;

        // Create a Texture2D and read the pixels from the RenderTexture
        Texture2D texture2D = new Texture2D(_renderImg.width, _renderImg.height);
        texture2D.ReadPixels(new Rect(0, 0, _renderImg.width, _renderImg.height), 0, 0);
        texture2D.Apply(); // Applies the changes

        return texture2D.GetRawTextureData();
    }
    // 이미지 업로드 버튼 클릭 시 호출될 함수
    public void OnUploadButtonClick()
    {        
				// 이미지 업로드 시작
        // StartCoroutine(UploadImage());
    }
    public void say(){
        Debug.Log("hi");
    }
}