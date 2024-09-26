using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Org.BouncyCastle.Asn1.Mozilla;
//using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    


    [Header("캔버스")]
    // 오브젝트 접촉시 띄울 캔버스
    public GameObject cigaCanvas;
    public GameObject phoneCanvas;   
    public GameObject legalCanvas;
    public GameObject coffeeCanvas;
    public GameObject checkListCanvas2;
    public GameObject checkListCanvas3;
    public GameObject AllCheckLISt;
   

    public float timerTime = 30;
    public TMP_Text regaltimer_text;
    public TMP_Text coffeetimer_text;
    public TMP_Text phonetimer_text;
    public TMP_Text cigatimer_text;
    public GameObject checkListCanvas4; // 인스턴스 활성화를 위해 껏다켜야함


    public GameObject RealExitPanel;

    //public GameObject[] infoCanvass; 
   
    //[Header("btn1을 눌렀을때 나올 변수들")]
    ////public GameObject docCanvas1    ;
    //public GameObject infoPanel1;
    //public Button doc1Exit;
    //public LineRenderer lineRenderer;
       


    [Header("버튼")]
    public List<Button> buttonlist = new List<Button>();
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public GameObject currentPanel; // 현재 패널
    public GameObject currentCanvas; // 현재 캔버스
    public Button currentButton; // 현재 활성화된 버튼


    [Header("버튼위치")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4; // 패널 위치
    

    [Header("키워드")]
    public GameObject Keytext;

    [Header("메인UI")]
    [SerializeField]
    private Button uiExitbtn;   
    [SerializeField]
    private Button uiQuestbtn;
    [SerializeField]
    private Transform scrollviewContent; // 스크롤뷰 인벤토리
    public GameObject scrollview; // 스크롤뷰 인벤토리


    float currentTime;
    public GameObject tutorial;
    public PlayerMove2 playermove; // 플레이어 움직임 스크립트
    public TMP_Text countDown_text;
    public TMP_Text currentTimer;
    bool istimerStart = false;


    public GameObject coffee;
    public GameObject smoke;
    public GameObject phone;
    public GameObject regal;
    

    // 싱글톤
    private void Awake()
    {
        if (instance == null) instance = this;

        Transform pt = uiQuestbtn.transform.Find("QuestPanel");
        GameObject panel = pt.gameObject;

        // 처음 시작시 퀘스트 알림말 나오게하기
        panel.SetActive(true);

        // 활성화 되고 1초뒤에 꺼지기
        if (panel.activeSelf)
        {
            Invoke("HideQuestPanel", 2f);
        }
    }

    void Start()
    {
        //Info_Canvas infoCanvas = GetComponent<Info_Canvas>();
        //infoCanvass = new GameObject[] { infoCanvas1, infoCanvas2, infoCanvas3, infoCanvas4, infoCanvas5 };

        //lineRenderer = GetComponent<LineRenderer>();

 
        //checkListCanvas3.SetActive(false);
        //checkListCanvas4.SetActive(false);


        AllCheckLISt.SetActive(false);

        scrollview.SetActive(false);
        currentButton = null;
        RealExitPanel.SetActive(false);


        cigaCanvas.SetActive(false);
        phoneCanvas.SetActive(false);
        legalCanvas.SetActive(false);
        coffeeCanvas.SetActive(false);
    }


    void Update()
    {
        //if (!istimerStart)
        //{
        //    currentTime = 60;

        //    currentTimer.text = currentTime; //내일할거임


        //    if (currentTime > 0) currentTime -= Time.deltaTime;
        //    else GameEnd();

        //}

        Timer(); // 캔버스 타이머

        if (Input.GetKeyDown(KeyCode.I))
        { 
            scrollview.SetActive(!scrollview.activeSelf);
        }

       

    }


    //doucument 버튼을 눌렀을 때 발생할 이벤트
    public void OnclickedButton(Button clickedButton)
    {       
            // 누른 버튼을 현재버튼으로 한다.
            currentButton = clickedButton;

            Cursor.lockState = CursorLockMode.None;

        // 누른 버튼안의 CHECK 텍스트 출력
        clickedButton.transform.Find("Check").gameObject.SetActive(true);


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

    }
      

       

        // 해당 document 버튼 밖에 파란 원이 생김 (setactive) true      

    


    //button이 사라지면 자리 변경
    void ButtonPlaceSwap()
    {        
        for(int i = 0; i < buttonlist.Count; i++)
        {
            // 만약, 버튼의 갯수가 2개가 되면
            if (buttonlist.Count == 2)
            {
                buttonlist[0].transform.position = pos1.transform.position;
                buttonlist[0].transform.Find("Panel").transform.position = pos4.position;
                buttonlist[1].transform.position = pos2.transform.position;
                buttonlist[1].transform.Find("Panel").transform.position = pos4.position;
            }
            // 만약, 버튼의 갯수가 1개가 되면
            else if (buttonlist.Count == 1)
            {
                buttonlist[0].transform.position = pos3.transform.position;
                buttonlist[0].transform.Find("Panel").transform.position = pos4.position;
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
                RealExitPanel.SetActive(true);
            }

        }
        // currnetbutton이 없고 패널이 비활성화 되어있다면
        else
        {
            // 정말 나가기 패널을 띄운다.
            RealExitPanel.SetActive(true);
        }
    }

    // realExit button 에서 나가기 버튼을 눌렀을때 오브젝트 after로 바꾸기
    public void RealExitExitButton()
    {
        if (currentCanvas == cigaCanvas)
        {
            Destroy(currentCanvas);

            timerTime = 0;

            GameObject smoke = GameObject.Find("Smoke_Before");

            smoke.SetActive(false);

            //GameObject smokeArea = GameObject.Find("Area_Smoke");

            //Renderer smokeArea = GetComponent<Renderer>();

            //smokeArea.material.color = new Color(0.5f, 0.5f, 0.5f, 1);
        } 



        if (currentCanvas == coffeeCanvas)
        {
            Destroy(currentCanvas);

            GameObject coffee = GameObject.Find("Coffee_Before");

            coffee.SetActive(false);

        } if (currentCanvas == phoneCanvas)
        {
            Destroy(currentCanvas);

            GameObject tablet = GameObject.Find("Tablet_Before");

            tablet.SetActive(false);

        } if (currentCanvas == legalCanvas)
        {
            Destroy(currentCanvas);

            GameObject rigal = GameObject.Find("rigalpad_Before");

            rigal.SetActive(false);

        }
           

        
        RealExitPanel.SetActive(false);
    
    }

    public void RealExitReturnButton()
    {
        RealExitPanel.SetActive(false);
    }

    // 문서에서 키워드를 눌렀을때 ~키워드를 얻었습니다 출력

    // 키워드 얻고 해당 버튼을 비활성화 하는 코드 추가하기

    



    // 키워드 눌렀을 때
    public void OnClickKeyword(Button keyWord) 
    {
        // keyWord 의 자식중 keytext를 찾음
        Transform findKeyText = keyWord.transform.Find("Keytext");
        // keyWord 버튼의 text 자식을 가져옴
        Transform findBtnText = keyWord.transform.Find("Text");

        // text 안의 tmp_text 컴포넌트를 찾음
        TMP_Text cngFont = findBtnText.GetComponent<TMP_Text>();

        // findKeyText 를 gameobejct 변환함
        Keytext = findKeyText.gameObject;

        if (Keytext != null)
        {          
            // 키텍스트를 활성화
            Keytext.gameObject.SetActive(true);
            // 1초 뒤에 숨기고
            Invoke("HideKeytext", 0.5f);

            // 키워드를 찾는다
            Transform kWord = keyWord.transform.Find("Keyword");    

            if(kWord != null)
            {
                // 키워드를 scrollview의 content의 자식으로 넣는다.
                kWord.transform.SetParent(scrollviewContent, false);

                GameObject kWordgo = kWord.gameObject;

                kWordgo.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("kWord 가 없습니다");
            }
           
        }
        else
        {
            Debug.Log("키워드 안의 Keytext가 없습니다.");
        }


        
        if(cngFont != null)
        {
            // 기존에 있던 text 를 가져오고
            string existingText = cngFont.text;

            // 기존 text 아래에 밑줄을 긋는다
            cngFont.text = $"<u>{existingText}</u>";
        }
        else
        {
            Debug.Log("안됨");
        }
       
        // 키워드 버튼의 상호작용을 멈춤
        keyWord.interactable = false;



    }

    void HideKeytext()
    {
        if (Keytext != null)
        {
            Keytext.gameObject.SetActive(false);
        }
       
    }

    // 아래 것은 start에서 실행 시킬것

    //void GameStart()
    //{
    //    tutorial.SetActive(true);
                    
    //    // 그리고 플레이어 이동 불가
    //    playermove.enabled = false;

    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        tutorial.SetActive(false);
    //        // 카운트 다운
    //        StartCoroutine(CountDown());

    //        // 타이머 시작
    //        istimerStart = true;

    //        playermove.enabled = true;
                       
    //    }
    //}
       


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


    // 플레이어가 각 오브젝트에 닿았을때 출력 될 함수


    public void OnCigarette()    
    {
        cigaCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        currentCanvas = cigaCanvas;
        
        // 작동하면 됬지
        btn1 = currentCanvas.transform.Find("Button1").GetComponent<Button>();       
        btn2 = currentCanvas.transform.Find("Button2").GetComponent<Button>();
        btn3 = currentCanvas.transform.Find("Button3").GetComponent<Button>();

        buttonlist.Add(btn1);
        buttonlist.Add(btn2);
        buttonlist.Add(btn3);

        timerTime = 30f;

        // 타이머 값을 UI에 표시
        cigatimer_text.text = Mathf.Ceil(timerTime).ToString();
        istimerStart = true;

       
    }

    
    public void OnPhone()    
    {
        // 휴대폰 캔버스가 활성화
        phoneCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        // 현재 캔버스를 phonecanvas로
        currentCanvas = phoneCanvas;

        btn1 = currentCanvas.transform.Find("Button1").GetComponent<Button>();
        btn2 = currentCanvas.transform.Find("Button2").GetComponent<Button>();
        btn3 = currentCanvas.transform.Find("Button3").GetComponent<Button>();

        buttonlist.Add(btn1);
        buttonlist.Add(btn2);
        buttonlist.Add(btn3);

        timerTime = 30f;

        // 타이머 값을 UI에 표시
        phonetimer_text.text = Mathf.Ceil(timerTime).ToString();
        istimerStart = true;


    }

  
    public void OnLegalPad()  
    {
        legalCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        currentCanvas = legalCanvas;

        btn1 = currentCanvas.transform.Find("Button1").GetComponent<Button>();
        btn2 = currentCanvas.transform.Find("Button2").GetComponent<Button>();
        btn3 = currentCanvas.transform.Find("Button3").GetComponent<Button>();

        buttonlist.Add(btn1);
        buttonlist.Add(btn2);
        buttonlist.Add(btn3);

        timerTime = 30f;

        // 타이머 값을 UI에 표시
        regaltimer_text.text = Mathf.Ceil(timerTime).ToString();
        istimerStart = true;

    }


    public void OnCoffee()   
    {
        coffeeCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        currentCanvas = coffeeCanvas;


        btn1 = currentCanvas.transform.Find("Button1").GetComponent<Button>();
        btn2 = currentCanvas.transform.Find("Button2").GetComponent<Button>();
        btn3 = currentCanvas.transform.Find("Button3").GetComponent<Button>();

        buttonlist.Add(btn1);
        buttonlist.Add(btn2);
        buttonlist.Add(btn3);

        timerTime = 30f;

        // 타이머 값을 UI에 표시
        coffeetimer_text.text = Mathf.Ceil(timerTime).ToString();
        istimerStart = true;
    }

    void Timer() // 이것도 rpc ismine 체크해야 함 아마도
    {
        // 타이머가 돌아가기 시작하면
        if(istimerStart)
        {
            // 시간을 누적한다.
            timerTime -= Time.deltaTime;

            if(currentCanvas == legalCanvas)
            {
                // 타이머 값을 UI에 표시
                regaltimer_text.text = Mathf.Ceil(timerTime).ToString();
            }
            else if(currentCanvas == coffeeCanvas)
            {
                // 타이머 값을 UI에 표시
                coffeetimer_text.text = Mathf.Ceil(timerTime).ToString();
            }
            else if(currentCanvas == phoneCanvas)
            {
                // 타이머 값을 UI에 표시
                phonetimer_text.text = Mathf.Ceil(timerTime).ToString();
            }
            else if(currentCanvas == cigaCanvas)
            {
                // 타이머 값을 UI에 표시
                cigatimer_text.text = Mathf.Ceil(timerTime).ToString();
            }
            

            // 0초가 되면 캔버스를 제거한다.
            if (timerTime <= 0)
            {
                Destroy(currentCanvas);

                currentCanvas = null;

                istimerStart = false; // 타이머 중지

            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////메인 UI

    public void OnClickuiExitbtn() 
    {
        Application.Quit();
    } 

    //  퀘스트 버튼 /// 1초뒤에 꺼지게할것
    public void OnClickuiQuestbtn() 
    {
        Transform pt = uiQuestbtn.transform.Find("QuestPanel");
        GameObject panel = pt.gameObject;
        // 패널 토글
        panel.SetActive(!panel.activeSelf);

        // 활성화 되고 1초뒤에 꺼지기
        if(panel.activeSelf)
        {
            Invoke("HideQuestPanel", 1f);
        }
       
    }
      

    public void HideQuestPanel()
    {
        Transform pt = uiQuestbtn.transform.Find("QuestPanel");
        GameObject panel = pt.gameObject;
        panel.SetActive(false);
    }





}

