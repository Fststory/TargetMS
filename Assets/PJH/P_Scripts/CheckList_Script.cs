using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList_Script : MonoBehaviour  // 체크리스트 3번에서 4번으로 넘어가는거부터 
{
    [Header("체크리스트")]
    public GameObject checkList2;
    public GameObject checkList3;
    public GameObject checkList4;
    public GameObject currentCheck;

    [Header("체크리스트2 변수")] // 배열로 해야함
    public GameObject panel1;
    public GameObject panel2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    //체크리스트 2번에서 작성버튼 누르면 3번으로 넘어가게
    public void OnClickCheck2to3()
    {
        checkList2.SetActive(false);

        currentCheck = null;

        checkList3.SetActive(true);

        currentCheck = checkList3;

        

    }


    //체크리스트 3번에서 버튼 누르면 4번으로 넘어가게
    public void OnClickCheck3to4()
    {
        checkList3.SetActive(false);

        currentCheck = null;

        checkList4.SetActive(true);

        currentCheck = checkList4;

        ProposalMgr.instance.PostJson();

    }

    //체크리스트 4번 나가기 버튼
    public void OnClickCheck4ExitButton()
    {
        checkList4.SetActive(false);

        currentCheck = null;

    }

    // 체크리스트 2번에 좌클리했을때
    // 배열로해야함



}
