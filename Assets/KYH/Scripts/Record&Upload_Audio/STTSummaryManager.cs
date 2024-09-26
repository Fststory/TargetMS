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

[Serializable]
public struct SumResult
{
    public string result;
}

public class STTSummaryManager : MonoBehaviourPunCallbacks
{
    public string summaryUrl;  // summary 주소
    
    public static STTSummaryManager instance;  // 싱글톤

    [Header("현재 문제 번호")]
    public CheckList_Script cls;    // cls 에 체크리스트 캔버스 할당해줘야 됨
    // 현재 풀고 있는 문제 번호 0~4, 총 5개
    // cls.currentIndex;

    [Header("문제별 회의 STT")]
    public SttSum[] sum = new SttSum[5];    // 5문제니까 요약도 5개

    [Header("문제별 회의 STT 요약본")]
    public TMP_Text[] tmpSummary = new TMP_Text[5];  // 출력받을 TMP_Text

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
    //[PunRPC]
    //public void AddSTTResult(string sttResult)
    //{
    //    sum[cls.currentIndex].data += sttResult;
    //    Debug.Log("STT 결과 추가: " + sttResult);
    //}

    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(summaryUrl));
    }

    IEnumerator PostJsonRequest(string url)
    {
        // 해당 문제의 회의록에 작성해야 됨
        string proposalJson = JsonUtility.ToJson(sum[cls.currentIndex], true);
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
            SumResult response = JsonUtility.FromJson<SumResult>(request.downloadHandler.text);
            string sttSummary = response.result;

            // |n 을 제거하는 코드
            //sttSummary = sttSummary.Replace("|n", "");

            tmpSummary[cls.currentIndex].text = sttSummary;  // UI에 출력
            Debug.LogWarning(response);
        }
        else
        {
            tmpSummary[cls.currentIndex].text = request.error;
            Debug.LogError(request.error);
        }
    }
}
