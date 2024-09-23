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


    public void OnClickButton(Button selectedBtn) // 이텍스트는 캔버스 3과 json 문서로 보낸다
    {
        // 버튼의 자식에서 TMP_Text를 가져온다.
        TMP_Text btnText = selectedBtn.GetComponentInChildren<TMP_Text>();

        if (btnText != null)
        {
            // TMP_Text의 텍스트를 가져온다.
            string text = btnText.text;
            //Debug.Log(text);


            ProposalMgr.instance.proposal.analysis_type = text;
        }
        else
        {
            Debug.LogWarning("TMP_Text를 찾을 수 없습니다.");
        }
    }
}


