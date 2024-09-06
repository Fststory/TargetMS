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
    
    [Header("btn1을 눌렀을때 나올 변수들")]
    //public GameObject docCanvas1    ;
    public GameObject infoPanel1;
    public Button doc1Exit;
    public GameObject realExitPanel;

    float currentTime;


    [Header("버튼")]
    public List<GameObject> documents; // 문서 버튼은 3개만


    [Header("버튼위치")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;



    void Start()
    {
        Info_Canvas infoCanvas = GetComponent<Info_Canvas>();

        documents = new List<GameObject>(2);

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


    // doucument 버튼을 눌렀을 때 발생할 이벤트 
    //public void OnclickedButton(Button ClickedButton)
    //{
    //    // 누른 버튼안의 패널을 찾고
    //    Transform panel = ClickedButton.transform.Find("Panel");

    //    if(panel != null)
    //    {
    //        // document 안의 document_panel 을 setactive(true)
    //        panel.gameObject.SetActive(true);
    //    }

    //    // 해당 document 버튼 밖에 파란 원이 생김 (setactive) true      

    //}

    public void OnclickedButton()
    {
        infoPanel1.SetActive(true);
    }

    // doucument 사라지면 자리 변경
    void DocumentPlaceSwap()
    {
        for (int i = 0; i < documents.Count; i++)
        {
            GameObject doc = documents[i];

            // 만약, 버튼의 갯수가 2개가 되면
            if (documents.Count == 2)
            {
                if (i == 0)
                {
                    doc.transform.position = pos1.transform.position;
                }
                if (i == 1)
                {
                    doc.transform.position = pos2.transform.position;
                }

            }
            // 만약, 버튼의 갯수가 1개가 되면
            else if (documents.Count == 1)
            {
                if (i == 0)
                {
                    // 남은 버튼의 자리를 옮긴다.
                    doc.transform.position = pos3.transform.position;
                }


            }

        }
    }

    // document 캔버스의 나가기 버튼을 눌렀을때
    public void InfoCanvasExitButton()
    {
        // null refrence는 꼭 넣기
        if(infoPanel1 != null)
        {
            // 패널이 활성화 되어있다면
            if (infoPanel1.activeSelf)
            {
                infoPanel1.SetActive(false);

                // 인포1 의 부모를 찾는다
                if (infoPanel1.transform.parent != null)
                {
                    Transform infoParent1 = infoPanel1.transform.parent;
                    GameObject parentObject = infoParent1.gameObject;
                    // 비활성화 한다. (detroy나)                
                    Destroy(parentObject);

                    // 자리를 재배치한다.
                    DocumentPlaceSwap();
                }

            }
        }
        
        // 패널이 비활성화 되어있다면
        else//if (infoPanel1 == null)
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


}

