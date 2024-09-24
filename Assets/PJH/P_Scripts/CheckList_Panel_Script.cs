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


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 정답 클릭할때
    public void OnClick1(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.analysis_type = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick2(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
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
    public void OnClick3(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.subject = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick4(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
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
    public void OnClick5(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.step = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick6(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.plan = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick7(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
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
    public void OnClick8(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.purpose = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick9(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.worker = text;

            // 그리고 진행도에 정답 이미지 추가할것
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
    public void OnClick10(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);

            // 해당 버튼의 inputtext를 proposalMGR로 보낸다.
            ProposalMgr.instance.proposal.budget = text;

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
}


