using Org.BouncyCastle.Asn1.Mozilla;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CheckList_Panel_Script : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;

    public TMP_InputField inputfield; // 기대효과 적을칸
    public TMP_InputField inputfield2; // 예상 리스크 적을칸

    public GameObject canvas3prefab; // ScrollView_Script 가 달려있는 오브젝트를 넣는다.
    public ScrollView_Script canvas3; //


    void Start()
    {
        //inputfield.onValueChanged.AddListener((string s) => 
        //{
        //    ProposalMgr.instance.proposal.audience_type = s;
        //}); 

        

        //inputfield2.onValueChanged.AddListener((string s) =>
        //{
        //    ProposalMgr.instance.proposal = s;
        //});

        canvas3 = canvas3prefab.GetComponent<ScrollView_Script>();
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

            // 캔버스3에도 전달한다
            canvas3.UdpateAnwer1(text);

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

            // 캔버스3에도 전달한다
            canvas3.UdpateAnwer2(text);

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

            // 캔버스3에도 전달한다
            canvas3.UdpateAnwer3(text);

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


