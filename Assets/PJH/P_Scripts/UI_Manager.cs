using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class UI_Manager : MonoBehaviour
{
    // 오브젝트 접촉시 띄울 캔버스
    public GameObject info_Canvas;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public GameObject currentPanel; // 현재 패널
    
    [Header("btn1을 눌렀을때 나올 변수들")]
    //public GameObject docCanvas1    ;
    public GameObject infoPanel1;
    public Button doc1Exit;
    public GameObject realExitPanel;
    public LineRenderer lineRenderer;
       


    [Header("버튼")]
    public List<Button> buttonlist = new List<Button>();
    public Button currentButton; // 현재 활성화된 버튼


    [Header("버튼위치")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    

    [Header("키워드")]
    public GameObject Keytext;




    float currentTime;
    public GameObject tutorial;
    public PlayerMove2 playermove; // 플레이어 움직임 스크립트
    public TMP_Text countDown_text;
    public TMP_Text currentTimer;
    bool istimerStart = false;




    void Start()
    {
        Info_Canvas infoCanvas = GetComponent<Info_Canvas>();
        lineRenderer = GetComponent<LineRenderer>();

        buttonlist.Add(btn1);
        buttonlist.Add(btn2);
        buttonlist.Add(btn3);


        infoPanel1.SetActive(false);
        realExitPanel.SetActive(false);
    }


    void Update()
    {
        if(!istimerStart)
        {
            currentTime = 60;

           // currentTimer.text = currentTime; 내일할거임
                                    
            if (currentTime>0) currentTime -= Time.deltaTime;
            else GameEnd();

        }

        
    }


    //doucument 버튼을 눌렀을 때 발생할 이벤트
    public void OnclickedButton(Button clickedButton)
    {
        // 누른 버튼을 현재버튼으로 한다.
        currentButton = clickedButton;

        ////////////////////////////////////////////////////////////////////////////////수정필요
        // clicked button 주위로 원을 그린다.
        RectTransform rt = GetComponent<RectTransform>();
        // 버튼 주위의 코너
        Vector3[] corners = new Vector3[4];

        rt.GetWorldCorners(corners);

        // corners 배열의 각 요소는 월드 좌표계에서의 모서리 좌표
        Vector3 bottomLeft = corners[0]; // 왼쪽 하단
        Vector3 bottomRight = corners[1]; // 오른쪽 하단
        Vector3 topRight = corners[2]; // 오른쪽 상단
        Vector3 topLeft = corners[3]; // 왼쪽 상단


        // 누른 버튼안의 패널을 찾고
        Transform panelTransform = clickedButton.transform.Find("Panel");              

        if (panelTransform != null)
        {
            // transform paenl 을 gameObject 형식으로 바꾸고
            GameObject panel = panelTransform.gameObject;

            // 버튼 안의 panel 을 setactive(true)
            panel.gameObject.SetActive(true);

            // 현재 활성화 된 패널을 현재 패널로 한다.
            currentPanel = panel; 
        }
        else
        {
            Debug.Log("자식중에 panel 이 없습니다.");
        }

        // 해당 document 버튼 밖에 파란 원이 생김 (setactive) true      

    }


    //button이 사라지면 자리 변경
    void ButtonPlaceSwap()
    {        
        for(int i = 0; i < buttonlist.Count; i++)
        {
            // 만약, 버튼의 갯수가 2개가 되면
            if (buttonlist.Count == 2)
            {
                buttonlist[0].transform.position = pos1.transform.position;
                buttonlist[1].transform.position = pos2.transform.position;
            }
            // 만약, 버튼의 갯수가 1개가 되면
            else if (buttonlist.Count == 1)
            {
                buttonlist[0].transform.position = pos3.transform.position;
            }
        }
                               
    }
       
    // 나가기 버튼 눌렀을때
    public void OnClickedExitButton()
    {
        // null refrence는 꼭 넣기
        // 만약, 현재버튼이 null이 아니고
        if (currentButton != null)
        {
            // 현채 패널이 활성화 되어있다면 // 비활성화 되어잇다면
            if (currentPanel.activeSelf)
            {
                // 버튼 리스트에서 현재 버튼제거
                buttonlist.Remove(currentButton);       
                // 현재 버튼 제거   
                Destroy(currentButton.gameObject);
                // 버튼 자리를 재배치한다.
                ButtonPlaceSwap();
                
            }
            else if (!currentPanel.activeSelf)
            {
                // 정말 나가기 패널을 띄운다.
                realExitPanel.SetActive(true);
            }

        }
        // 패널이 비활성화 되어있다면
        else
        {
            // 정말 나가기 패널을 띄운다.
            realExitPanel.SetActive(true);
        }
    }

    // realExit button 에서 나가기 버튼을 눌렀을때
    public void RealExitExitButton()
    {
        info_Canvas.SetActive(false);
    }

    public void RealExitReturnButton()
    {
        realExitPanel.SetActive(false);
    }

    // 문서에서 키워드를 눌렀을때 ~키워드를 얻었습니다 출력

    // 키워드 얻고 해당 버튼을 비활성화 하는 코드 추가하기

    
    public void OnClickKeyword(Button keyWord) 
    {
        // keyWord 의 자식중 keytext를 찾음
        Transform findKeyText = keyWord.transform.Find("Keytext");
        // findKeyText 를 gameobejct 변환함
        Keytext = findKeyText.gameObject;

        if (Keytext != null)
        {          

            // 키텍스트를 활성화
            Keytext.gameObject.SetActive(true);
            // 2초 뒤에 숨기고
            Invoke("HideKeytext", 2f);
            // 키워드 버튼의 상호작용을 멈춤
            keyWord.interactable = false;
        }
        else
        {
            Debug.Log("키워드 안의 Keytext가 없습니다.");
        }
       


    }

    void HideKeytext()
    {
        if (Keytext != null)
        {
            Keytext.gameObject.SetActive(false);
        }
       
    }

    // 아래 것은 start에서 실행 시킬것

    void GameStart()
    {
        tutorial.SetActive(true);
                    
        // 그리고 플레이어 이동 불가
        playermove.enabled = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tutorial.SetActive(false);
            // 카운트 다운
            StartCoroutine(CountDown());

            // 타이머 시작
            istimerStart = true;

            playermove.enabled = true;
                       
        }
    }
       


    IEnumerator CountDown()
    {
        countDown_text.text = "3";
        yield return new WaitForSeconds(1f);
        countDown_text.text = "2";
        yield return new WaitForSeconds(1f);
        countDown_text.text = "1";
        yield return new WaitForSeconds(1f);
        countDown_text.text = "START"; // 이건 나중에 여유있으면 이미지로 바꾸던가 다른걸로

        countDown_text.text = "";
    }



    // GameScene 타이머가 끝나면 다음씬으로 이동시킬것
    void GameEnd()
    {
       
        //PhotonNetwork.LoadLevel("CouncilScene");

    }

    
    
   
    // 3 2 1 카운트 다운 후 Start! 
    // / 그리고 플레이어 이동가능

    // GameScence이 시작되면 타이머 시작
    // 카운트 다운 시작
    //currentTime += Time.deltaTime;

    //    if(currentTime >= 30.0f)
    //    {
    //        info_Canvas.SetActive(false);

    //        currentTime = 0;
    //    }







}

