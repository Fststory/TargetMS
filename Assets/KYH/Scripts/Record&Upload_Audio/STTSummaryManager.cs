using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Text;
using TMPro;

[Serializable]
public struct SttSum
{
    public string data;
}

public class STTSummaryManager : MonoBehaviourPunCallbacks
{
    public string summaryUrl;  // summary 주소
    
    public static STTSummaryManager instance;  // 싱글톤

    public SttSum[] sum = new SttSum[5];    // 5문제니까 요약도 5개

    // 출력받을 TMP_Text
    public TMP_Text[] text_response = new TMP_Text[5];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PostJson();
        }
    }

    // STT 결과를 추가하는 함수
    public void AddSTTResult(string sttResult)
    {
        sum[0].data += sttResult;
        //if(현재 푸는 문제 번호 = i)   // 현재 풀고 있는 문제의 번호를 알아야 됨. 변수로 만들어서
        //{
        //    sum[i-1].data += sttResult;
        //}
        Debug.Log("STT 결과 추가: " + sttResult);
    }

    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(summaryUrl));
    }

    IEnumerator PostJsonRequest(string url)
    {
        string proposalJson = JsonUtility.ToJson(sum[0], true);
        byte[] jsonBins = Encoding.UTF8.GetBytes(proposalJson);

        // Post를 하기 위한 준비를 한다.
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 서버에 Post를 전송하고 응답이 올 때까지 기다린다.
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // JSON 파싱
            SttSum response = JsonUtility.FromJson<SttSum>(request.downloadHandler.text);
            string sttSummary = response.data;

            // |n 을 제거하는 코드
            sttSummary = sttSummary.Replace("|n", "");

            text_response[0].text = sttSummary;  // UI에 출력 -> summary_result 게임옵젝
            Debug.LogWarning(response);
        }
        else
        {
            text_response[0].text = request.error;
            Debug.LogError(request.error);
        }
    }
}
