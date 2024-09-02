using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class AudioUploader : MonoBehaviour
{
    // OutputAudioRecorder 클래스로 생성한 wav 파일을 서버에 업로드하는 기능

    [SerializeField] private string serverUrl = "http://your-fastapi-server-url/uploadfile/";   // 서버 URL, fastapi 주소 받고 수정하면 됨

    public void UploadAudioFile(string filePath)
    {
        StartCoroutine(UploadCoroutine(filePath));
    }

    private IEnumerator UploadCoroutine(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File does not exist: " + filePath);
            yield break;
        }

        WWWForm form = new WWWForm();
        byte[] audioData = File.ReadAllBytes(filePath);
        form.AddBinaryData("file", audioData, Path.GetFileName(filePath), "audio/wav");

        using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error uploading file: " + www.error);
            }
            else
            {
                Debug.Log("File upload complete!");
                Debug.Log("Server response: " + www.downloadHandler.text);
            }
        }
    }
}
