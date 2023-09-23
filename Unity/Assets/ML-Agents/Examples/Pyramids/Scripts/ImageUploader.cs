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
            }
        }
    }
}
