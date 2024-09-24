using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView_Script : MonoBehaviour // 결과 이벤트창이 들어 갈 곳이다
{
    public GameObject content; // 콘텐츠
    public GameObject ccpanel; // 현재 패널
    private VerticalLayoutGroup layoutGroup;

    [Header("질문")]
    public GameObject q1;
    public GameObject q2;
    public GameObject q3;
    public GameObject q4;
    public GameObject q5;
    public GameObject q6;
    public GameObject q7;
    public GameObject q8;
    public GameObject q9;
    public GameObject q10;



    [Header("버튼")]
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button10;



    [Header("패널")]
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

    // 구현방법
    // 자식의 자식이 작동을 안하니까 그냥 버튼을 누르면 버튼 안의 panel을 content의 자식으로 만들고 
    // 다시 버튼을 누르면 panel을 button 안으로 집어넣게
    // 구현하기로 했음 

    public void Awake()
    {
        if(content != null)
        {
        content = transform.Find("Content").gameObject; // Content는 ScrollView의 자식 오브젝트 이름    
        }
        else
        {
            Debug.Log("문제있음");
        }

        layoutGroup = content.GetComponent<VerticalLayoutGroup>();
    }

    // 버튼 눌렀을때
    public void OnClickButton1() => TogglePanel(panel1);
    public void OnClickButton2() => TogglePanel(panel2);
    public void OnClickButton3() => TogglePanel(panel3);
    public void OnClickButton4() => TogglePanel(panel4);
    public void OnClickButton5() => TogglePanel(panel5);
    public void OnClickButton6() => TogglePanel(panel6);
    public void OnClickButton7() => TogglePanel(panel7);
    public void OnClickButton8() => TogglePanel(panel8);
    public void OnClickButton9() => TogglePanel(panel9);
    public void OnClickButton10() => TogglePanel(panel10);



    // 버튼을 눌렀을때 패널이 활성/비활성되고 그에 따른 content의 자식들의 위치변경
    private void TogglePanel(GameObject panel)
    {
        // 현재패널은 이 패널이다
        ccpanel = panel;
        // 버튼 토글을 넣어서 패널 껏다/켯다
        bool isActive = panel.activeSelf;
        panel.SetActive(!isActive);

        // 만약, 패널이 활성화 되어있다면 layout space를 150을 아니라면 50으로
        layoutGroup.spacing = isActive ? 150 : 90;
        // 만약, 패널이 활성화 되어있다면 현재패널=패널, 활성화 안되어 있다면 현재패널= null
        ccpanel = isActive ? panel : null;

    }
          



}




    




   





