using Org.BouncyCastle.Asn1.Mozilla;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    [Header("2번 캔버스에서 받아올 답변")]
    public TMP_Text answerSeet1;
    public TMP_Text answerSeet2;
    public TMP_Text answerSeet3;
    public TMP_Text answerSeet4;
    public TMP_Text answerSeet5;
    //bool contentYes = content.SetActive(true);


    [Header("버튼")]
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;



    [Header("패널")]
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;
 
    // 구현방법
    // 자식의 자식이 작동을 안하니까 그냥 버튼을 누르면 버튼 안의 panel을 content의 자식으로 만들고 
    // 다시 버튼을 누르면 panel을 button 안으로 집어넣게
    // 구현하기로 했음 

    public void Start()
    {       

        //if(content != null)
        //{
        //content = transform.Find("Content").gameObject; // Content는 ScrollView의 자식 오브젝트 이름    
        //}
        //else
        //{
        //    Debug.Log("문제있음");
        //}

       // layoutGroup = content.GetComponent<VerticalLayoutGroup>();
    }

    // 버튼 눌렀을때
    public void OnClickButton1() => TogglePanel(panel1);
    public void OnClickButton2() => TogglePanel(panel2);
    public void OnClickButton3() => TogglePanel(panel3);
    public void OnClickButton4() => TogglePanel(panel4);
    public void OnClickButton5() => TogglePanel(panel5);
   

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


    // 2번 캔버스에서 3번 캔버스 answerseet 변경하기

    public void UdpateAnwer1(string text)
    {
        answerSeet1.text = text;
    }         

     public void UdpateAnwer2(string text)
    {
        answerSeet2.text = text;
    } public void UdpateAnwer3(string text)
    {
        answerSeet3.text = text;
    } public void UdpateAnwer4(string text)
    {
        answerSeet4.text = text;
    } public void UdpateAnwer5(string text)
    {
        answerSeet5.text = text;
    }




}


