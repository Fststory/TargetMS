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
    public GameObject[] panels;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;
    public GameObject panel6;
    public GameObject panel7;
    public GameObject panel8;
    public GameObject panel9;
    public GameObject panel10;
    public int currentIndex; // 현재 인덱스 확인


    void Start()
    {
        panels = new GameObject[10];

        panels[0] = panel1;
        panels[1] = panel2;
        panels[2] = panel3;
        panels[3] = panel4;
        panels[4] = panel5;
        panels[5] = panel6;
        panels[6] = panel7;
        panels[7] = panel8;
        panels[8] = panel9;
        panels[9] = panel10;

        ShowPanel(0); // 초기 패널 표시


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


    // 체르리스트 2번 패널들


    // 체크리스트 2번에 previous클릭
    public void OnClickPre()
    {
        if(currentIndex > 0)
        {
            HidePanel(currentIndex); // 현재 패널 숨기기
            currentIndex--; // 패널 인덱스에서 -1을하고
            ShowPanel(currentIndex); // 현재 패널 보여주기 
        }
    }

    // 체크리스트 2번에 next클릭
    public void OnClickNxt()
    {
        if (currentIndex < panels.Length - 1)
        {
            HidePanel(currentIndex); 
            currentIndex++;
            ShowPanel(currentIndex);
        }
    }

    private void ShowPanel(int index)
    {
        panels[index].SetActive(true); // 주어진 인덱스의 패널 활성화
    }

    private void HidePanel(int index)
    {
        panels[index].SetActive(false); // 주어진 인덱스의 패널 비활성화
    }



}
