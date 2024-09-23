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

    // 다른 스크립트에서 접근 용이
    /// <summary>
    ///  예시) 체크리스트 작성 씬에서 사용할 때 "3/4C분석" 버튼을 누르면(OnClick()) 발동되는 함수
    ///  void Btn1()
    ///  {
    ///      ProposalMgr.instance.proposal.analysis_type = "3/4C분석";
    ///  }
    /// </summary>
    public static ProposalMgr instance;

    // Post 서버 URL
    public string url;

    // 출력받을 TMP_Text
    public TMP_Text text_response;

    // 기획서 프롬프트
    public Proposal proposal = new Proposal();

    // 싱글턴 패턴
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 프롬프트(체크 리스트) 제출 기능 함수
    // 작성 버튼에 연결시키면 됨!!
    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(url));
    }


    IEnumerator PostJsonRequest(string url)
    {
        // 사용자의 입력 정보(선택한 키워드)를 Json 데이터로 변환하기 (예시)
        //proposal.analysis_type = "3/4C분석";
        //proposal.audience_type = "사내대상";
        //proposal.subject = "새로운 세대의 사용자 경험을 반영한 모바일 애플리케이션을 통해 시장 점유율을 높이고, 경쟁사 대비 차별화된 기능을 제공하기 위해";
        //proposal.project_type = "어플리케이션";
        //proposal.step = "컨셉제안";
        //proposal.plan = "2월~5월";
        //proposal.target = "20대 중반에서 30대 초반의 IT에 익숙한 직장인";
        //proposal.purpose = "사용자들이 더 쉽게 상품을 검색하고 구매할 수 있는 통합 쇼핑 플랫폼을 제공하여 사용자 편의성을 극대화하고, 기업 매출 성장을 목표로 함";
        //proposal.worker = "";
        //proposal.budget = "1억 2000만원";


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

            // "\n"을 실제 개행 문자로 변환 (+ 불필요한 문자 제거)
            response = response.Replace("{\"result\":\"", "");
            response = response.Replace("\\n", "\n");
            response = response.Replace("#", "");
            response = response.Replace("*", "");
            response = response.Replace("}", "");

            text_response.text = response;  // UI에 출력 -> Proposal_result 게임옵젝
            Debug.LogWarning(response);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
    }
}
