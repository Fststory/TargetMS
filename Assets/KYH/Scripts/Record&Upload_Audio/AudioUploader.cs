using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SttResponse
{
    public string result;
}

public class AudioUploader : MonoBehaviour
{
    // OutputAudioRecorder 클래스로 생성한 wav 파일을 서버에 업로드하는 기능
    // OutputAudioRecorder 에서 실행할 것이므로 filePath 도 OutputAudioRecorder 에서 배정하면 된다
    // multipart/form-data 방식, post 메소드, 키-값은 audio_file

    [SerializeField] private string serverUrl = "http://your-fastapi-server-url/uploadfile/";   // 서버 URL, fastapi 주소 받고 수정하면 됨

    public STTRecorder sttRecorder;

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

        WWWForm form = new WWWForm();   // 새로운 폼 생성
        byte[] audioData = File.ReadAllBytes(filePath); // 파일의 데이터 배열 읽어오기
        form.AddBinaryData("audio_file", audioData, Path.GetFileName(filePath), "audio/wav");   // 폼에 오디오 파일 추가

        using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
        {
            www.timeout = 60;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error uploading file: " + www.error);
            }
            else
            {
                Debug.Log("File upload complete!");
                Debug.Log("Server response: " + www.downloadHandler.text);  // www.downloadHandler.text : http 서버에서 주는 STT 응답

                // JSON 파싱
                SttResponse response = JsonUtility.FromJson<SttResponse>(www.downloadHandler.text);
                string sttContent = response.result;

                // |n 을 제거하는 코드
                sttContent = sttContent.Replace("|n", "");
                print(sttContent);

                //sttRecorder.OnSubmit(www.downloadHandler.text); // RPC AddVoiceChat() 실행    => 모두의 화면에 STT(www.downloadHandler.text)를 띄우는 부분
                sttRecorder.OnSubmit(sttContent); // RPC AddVoiceChat() 실행    => 모두의 화면에 정제된 STT 를 띄우는 부분
            }
        }
    }
}
