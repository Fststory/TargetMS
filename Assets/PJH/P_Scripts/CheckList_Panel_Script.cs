using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckList_Panel_Script : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;

    public InputField inputfield;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 정답 클릭할때
    public void OnClick1(Button selectedBtn) // 유형 답변
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.project_type = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick2(Button selectedBtn) // 읽는 이
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.audience_type = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick3(Button selectedBtn) // 타겟
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.target = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    


    // 오답을 클릭했을때 
    public void OnClikcedWrong()
    {
        // 진행도 오답 표시 추가
    }

    public void Input1() // 입력칸 1
    {
        // input 필드 안의 tmptext 가져온다
        TMP_Text inputtext = inputfield.GetComponent<TMP_Text>();

        if(inputtext != null)
        {
            // 안에 적힐 글을 text 변수 선언을 하고
            string text = inputtext.text;

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.target = text;
        }
    }

    public void Input2() // 입력칸 2
    {

        //inputfield
    }
}


