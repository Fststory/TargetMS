using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Text;

[Serializable]
public struct SttSum
{
    public string data;
}

public class STTManager : MonoBehaviourPunCallbacks
{
    public string url;
    
    public static STTManager instance;

    public SttSum[] sum = new SttSum[5];

    public string sttResults;

    // STT 결과를 추가하는 함수
    public void AddSTTResult(string sttResult)
    {
        sttResults += sttResult;
        //if(현재 푸는 문제 = i)
        //{
        //    sum[i-1].data += sttResult;
        //}
        Debug.Log("STT 결과 추가: " + sttResult);
    }

    //public void PostJson()
    //{
    //    StartCoroutine(PostJsonRequest(url, i));
    //}

    //IEnumerator PostJsonRequest(string url, int i)
    //{        
    //    string proposalJson = JsonUtility.ToJson(sum[i], true);
    //    byte[] jsonBins = Encoding.UTF8.GetBytes(proposalJson);

    //    // Post를 하기 위한 준비를 한다.
    //    UnityWebRequest request = new UnityWebRequest(url, "POST");
    //    request.SetRequestHeader("Content-Type", "application/json");
    //    request.uploadHandler = new UploadHandlerRaw(jsonBins);
    //    request.downloadHandler = new DownloadHandlerBuffer();

    //    // 서버에 Post를 전송하고 응답이 올 때까지 기다린다.
    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        // 다운로드 핸들러에서 텍스트 값을 받아서 UI에 출력한다.
    //        string response = request.downloadHandler.text;                      

    //        text_response.text = response;  // UI에 출력 -> summary_result 게임옵젝
    //        Debug.LogWarning(response);
    //    }
    //    else
    //    {
    //        text_response.text = request.error;
    //        Debug.LogError(request.error);
    //    }
    //}
}
