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
    //public string analysis_type;
    public string audience_type;
    public string subject;
    public string project_type;
    public string step;
    public string plan;
    public string target;
    public string purpose;
    //public string worker;
    public string budget;
    // 아래는 추가된 것 위의 주석은 삭제된 것
    public string expected_outcome;
    public string potential_risk;    
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

    public Canvas4_Script canvas;

    // 싱글턴 패턴
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 4단계 UI 가 활성화 될 떄는 아래부터 작동함 (활성화 -> 비활성화 -> 재활성화 : 첫번째 활성화 단계에서만 awake 와 start 가 발동함)

    // 프롬프트(체크 리스트) 제출 기능 함수
    // 작성 버튼에 연결시키면 됨!!
    public void PostJson()
    {
        StartCoroutine(PostJsonRequest(url));
    }


    IEnumerator PostJsonRequest(string url)
    {
        // 사용자의 입력 정보(선택한 키워드)를 Json 데이터로 변환하기 (예시)
        // ********** 주석 처리 돼있는 부분은 유저의 입력을 받아야 됨 **********

        //proposal.audience_type = "내부 개발자";  // 읽는이
        //proposal.project_type = "프로그램 개발"; // 유형
        proposal.subject = "우주에 대한 지식을 쉽게 알려주어 관심을 올리고 싶다";
        proposal.step = "컨셉제안";
        proposal.plan = "1분기";
        //proposal.target = "특정 연령대"; // 타겟
        proposal.purpose = "미지의 행성을 우주 탐사선으로 탐험한다";
        proposal.budget = "1억 3000만원";
        //proposal.expected_outcome = "";   // 기대 효과
        //proposal.potential_risk = "";     // 예상 리스크


        string proposalJson = JsonUtility.ToJson(proposal, true);
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
            // 다운로드 핸들러에서 텍스트 값을 받아서 UI에 출력한다.
            string response = request.downloadHandler.text;

            // "\n"을 실제 개행 문자로 변환 (+ 불필요한 문자 제거)
            response = response.Replace("{\"result\":\"", "");
            response = response.Replace("\\n", "\n");
            response = response.Replace("#", "");
            response = response.Replace("*", "");
            response = response.Replace("}", "");

            text_response.text = response;  // UI에 출력 -> Proposal_result 게임옵젝
            canvas.DoneText();
            Debug.LogWarning(response);
        }
        else
        {
            text_response.text = request.error;
            Debug.LogError(request.error);
        }
    }
}
