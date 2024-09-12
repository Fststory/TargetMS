using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct Proposal
{
    public string analysis_type;
    public string audience_type;
    public string subject;
    public string project_type;
    public string step;
    public string plan;
    public string target;
    public string purpose;
    public string worker;
    public string budget;
}

public class ProposalMgr : MonoBehaviour
{
    // 기획서 요청 통신 스크립트

    public static ProposalMgr instance;

    // Post 서버 URL
    public string url;

    // 출력받을 TMP_Text
    public TMP_Text text_response;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        PostJson();
    }

    void Update()
    {
        
    }

    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(url));
    }

    IEnumerator PostJsonRequest(string url)
    {
        // 사용자의 입력 정보를 Json 데이터로 변환하기
        Proposal proposal = new Proposal();
        proposal.analysis_type = "3/4C분석";
        proposal.audience_type = "사내대상";
        proposal.subject = "새로운 세대의 사용자 경험을 반영한 모바일 애플리케이션을 통해 시장 점유율을 높이고, 경쟁사 대비 차별화된 기능을 제공하기 위해";
        proposal.project_type = "어플리케이션";
        proposal.step = "컨셉제안";
        proposal.plan = "2월~5월";
        proposal.target = "20대 중반에서 30대 초반의 IT에 익숙한 직장인";
        proposal.purpose = "사용자들이 더 쉽게 상품을 검색하고 구매할 수 있는 통합 쇼핑 플랫폼을 제공하여 사용자 편의성을 극대화하고, 기업 매출 성장을 목표로 함";
        proposal.worker = "";
        proposal.budget = "1억 2000만원";

        string userJsonData = JsonUtility.ToJson(proposal, true);
        byte[] jsonBins = Encoding.UTF8.GetBytes(userJsonData);

        // Post를 하기 위한 준비를 한다.
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBins);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 서버에 Post를 전송하고 응답이 올 때까지 기다린다.
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 다운로드 핸들러에서 텍스트 값을 받아서 UI에 출력한다.
            string response = request.downloadHandler.text;
            text_response.text = response;
            Debug.LogWarning(response);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
    }
}
