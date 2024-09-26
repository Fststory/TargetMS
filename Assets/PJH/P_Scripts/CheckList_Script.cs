using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
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
   
    public int currentIndex; // 현재 인덱스 확인

    [SerializeField]
    //private float timeLimit = 30f; // 타이머
    public TMP_Text panelTimer; // 타이머를 표시할 텍스트
    public float timer; // 실제 감소될 시간


    void Start()
    {
        panels = new GameObject[5];

        panels[0] = panel1;
        panels[1] = panel2;
        panels[2] = panel3;
        panels[3] = panel4;
        panels[4] = panel5;
        

        ShowPanel(0); // 초기 패널 표시
               

    }

    void Update()
    {
        panelTime();

        //if(currentIndex == 0)
        //{
        //    timer = 30;

           
        //}
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

/// <summary>
/// //////////////////////////////////////////체크리스트2
/// </summary>

    // 체크리스트 2번에 previous클릭
    public void OnClickPre()
    {
        // 만약, 현재 인덱스가 0보다 크다면
        if(currentIndex > 0)
        {
            HidePanel(currentIndex); // 현재 패널 숨기기
            currentIndex--; // 패널 인덱스에서 -1을하고
            ShowPanel(currentIndex); // 현재 패널 보여주기 

            ResetTimer(); // 그리고 타이머 30으로 리셋
        }
    }

    // 체크리스트 2번에 next클릭
    public void OnClickNxt()
    {
        // 만역, 현재 패널이 총 패널의 갯수보다 1 적다면
        if (currentIndex < panels.Length - 1)
        {
            HidePanel(currentIndex); 
            currentIndex++;
            ShowPanel(currentIndex);

            ResetTimer(); // 그리고 타이머 30으로 리셋
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


    void panelTime()
    {
        // 만약, 타이머가 0보다 크다면
        if(timer > 0)
        {
            // 프레임마다 감소시키고
            timer -= Time.deltaTime;
            // 남은 시간을 int로 표시 panel타임에 표시
            panelTimer.text = Mathf.Ceil(timer).ToString();
        }
    }

    void ResetTimer()
    {
        // currenpanel 의 숫자가 바뀔때마다, 즉 버튼을 누를때마다 초기화
        timer = 30;

        panelTimer.text = Mathf.Ceil(timer).ToString();
    }



    /////////////////////////////// 3번째 체크리스트에서 버튼 누르면 2번째 체크리스트 문제띄우기
    ///
    // 3번째 캔버스에 해당 문제 버튼 누르면 2번째 캔버스 해당 문제 패널로 이동하기



    public void ActiveChelistPanel(string typeName)
    {
        // 체크리스트 3을 비활성화하고 
        checkList3.SetActive(false);
        // 체크리스트 2를 활성화하고
        checkList2.SetActive(true);

        // "typeName" 객체를 찾아서 활성화
        Transform typeObject = checkList2.transform.Find(typeName);

        if(typeObject != null)
        {
            typeObject.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Type 없음");
        }
    }


    public void QButton1() => ActiveChelistPanel("Type");   
    public void QButton2() => ActiveChelistPanel("Who");
    public void QButton3() => ActiveChelistPanel("Target");
    public void QButton4() => ActiveChelistPanel("Expect");
    public void QButton5() => ActiveChelistPanel("Risk");


}
