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

    [Header("document를 눌렀을때 나올 변수들")]
    public Canvas docCanvas;
    public GameObject infoPanel1;
    public Button Exit;
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
    }


    void Update()
    {
        if(currentTime >= 30.0f)
        {
            Destroy(docCanvas);

            currentTime = 0;
        }
    }


    // doucument 버튼을 눌렀을 때 발생할 이벤트 
    void OnclickedDocument()
    {
        // 카운트 다운 시작
        currentTime += Time.deltaTime;
        // document 안의 document_panel 을 setactive(true)
        infoPanel1.SetActive(true);
        // 해당 document 버튼 밖에 파란 원이 생김 (setactive) true

        // 

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
    void DocumentCanvasExitButton()
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
            // 패널이 비활성화 되어있다면
            else
            {
                // 정말 나가기 패널을 띄운다.
                realExitPanel.SetActive(true);
            }


        }
    }


     // 오브젝트에 캐릭터가 충돌했을때 사용할 함수 ----> 이건 동휘씨가 playermove나 부딪힐 오브젝트에 별도로 추가
     private void OnTriggerEnter(Collider other)
     {
         // player move 컴포넌트를 가진 other 가 충돌하면
         if (other.gameObject.GetComponent<PlayerMove>())
         {
             // 캐릭터는 동작, 회전을 멈추고

             // info 캔버스를 true로
             info_Canvas.SetActive(true);
         }
     }
}

