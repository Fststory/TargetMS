using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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




    [Header("버튼")]
    public List<Button> buttonlist = new List<Button>();
    float currentTime;
    public Button currentButton; // 현재 활성화된 버튼


    [Header("버튼위치")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    

    [Header("키워드")]
    public GameObject Keytext;


    void Start()
    {
        Info_Canvas infoCanvas = GetComponent<Info_Canvas>();

        buttonlist.Add(btn1);
        buttonlist.Add(btn2);
        buttonlist.Add(btn3);


        infoPanel1.SetActive(false);
        realExitPanel.SetActive(false);
    }


    void Update()
    {
        // 카운트 다운 시작
        currentTime += Time.deltaTime;

        if(currentTime >= 30.0f)
        {
            info_Canvas.SetActive(false);

            currentTime = 0;
        }
    }


    //doucument 버튼을 눌렀을 때 발생할 이벤트
    public void OnclickedButton(Button clickedButton)
    {
        // 누른 버튼을 현재버튼으로 한다.
        currentButton = clickedButton;

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
}

