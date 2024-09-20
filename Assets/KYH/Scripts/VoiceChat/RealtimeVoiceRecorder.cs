using Photon.Voice.Unity;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RealtimeVoiceRecorder : MonoBehaviour
{
    public AudioOutCapture audioOutCapture;

    private Dictionary<int, MemoryStream> userAudioStreams = new Dictionary<int, MemoryStream>();

    void Start()
    {
        // 음성 데이터 캡처 이벤트 등록
        audioOutCapture.OnAudioFrame += OnAudioFrameCaptured;
    }

    void OnAudioFrameCaptured(float[] frame, int channels)
    {
        int voiceId = channels; // channels를 voiceId로 사용하려면 다른 방법이 필요합니다.

        if (!userAudioStreams.ContainsKey(voiceId))
        {
            // 새로운 화자의 경우, 메모리 스트림 생성
            userAudioStreams[voiceId] = new MemoryStream();
        }

        // 화자별로 음성 데이터를 저장
        byte[] byteArray = new byte[frame.Length * 4];  // float to byte conversion
        Buffer.BlockCopy(frame, 0, byteArray, 0, byteArray.Length);
        userAudioStreams[voiceId].Write(byteArray, 0, byteArray.Length);
    }

    void OnDestroy()
    {
        foreach (var stream in userAudioStreams.Values)
        {
            stream.Close();  // 모든 메모리 스트림을 닫음
        }
        audioOutCapture.OnAudioFrame -= OnAudioFrameCaptured;
    }

    // 서버에 음성 데이터를 전송하는 코루틴
    public IEnumerator UploadAudioToServer(int voiceId)
    {
        if (!userAudioStreams.ContainsKey(voiceId))
        {
            yield break;
        }

        MemoryStream audioStream = userAudioStreams[voiceId];
        audioStream.Seek(0, SeekOrigin.Begin);  // 스트림의 시작점으로 이동

        // HTTP POST 요청을 위한 폼 데이터 생성
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", audioStream.ToArray(), $"user_{voiceId}_voice.wav", "audio/wav");
        form.AddField("voiceId", voiceId.ToString());  // 화자 구분을 위한 voiceId 추가

        // 서버에 POST 요청 보내기
        using (UnityWebRequest www = UnityWebRequest.Post("https://your-server.com/upload", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Upload failed: " + www.error);
            }
            else
            {
                Debug.Log("Upload successful: " + www.downloadHandler.text);
            }
        }
    }
}