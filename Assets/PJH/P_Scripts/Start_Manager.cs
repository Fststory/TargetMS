using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Photon.Pun.Demo.Cockpit;

public class Start_Manager : MonoBehaviourPunCallbacks
{

    // 로그인 패널
    [Header("로그인 패널 변수")]
    public GameObject loginPanel;
    public TextMeshProUGUI loginSuccess_text;
    public TextMeshProUGUI loginFail_text;
    public TMP_InputField idInputField; // 아이디 입력칸
    public TMP_InputField passwordInputField; // 비밀번호 입력칸
    public TMP_InputField currentInputField;
    bool isLogin; // 로그인 확인
    


    // 음악
    [Header("음악 변수")]
    private AudioSource audioSource; // 오디오 클립을 드래그해서 인스펙터에 드랍

    // 테스트용 아이디 비밀번호
    private string id = "dkdlel"; // 아이디
    private string psw = "qlalfqjsgh"; // 비밀번호





    void Start()
    {
        isLogin = false;


        // 시작때 로그인 패널 숨기기
        loginPanel.SetActive(false);
        
        loginSuccess_text.gameObject.SetActive(false);
        loginFail_text.gameObject.SetActive(false);

        // 시작시 음악 재생
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();


        currentInputField = idInputField;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchInputField();
        }
        
    }

    public void SwitchInputField()
    {
        if (currentInputField == idInputField)
        {
            passwordInputField.Select();
            currentInputField = passwordInputField;
        }
        else if (currentInputField == passwordInputField)
        {
            idInputField.Select();
            currentInputField = idInputField;
        }
    }

    // 클릭한 필드를 currentinputfield 로 만듬
    public void OnInputFieldSelect(TMP_InputField clickedField)
    {
        currentInputField = clickedField;
    }
    


   // 시작버튼 눌렀을때, 
   public void OnButtonClickedStart()
         {
        if(isLogin)
        {
            // 로그인이 되어있고 시작하기 버튼을 누르면 로비 테스트 씬으로 이동
            SceneManager.LoadScene("connectScene");
        }
        else
        {
            //로그인 패널을 활성화
            loginPanel.SetActive(true);
        }


        //// 로그인이 되어 있으면 , 로비로 이동
        //if (PhotonNetwork.IsConnected)
        //{
        //    SceneManager.LoadScene("Lobby");

        //    //PhotonNetwork.LoadLevel("Lobby"); 포톤사용시
        //}
        //// 로그인이 안되어 있으면
        //else
        //{
        //    // 로그인 패널을 활성화
        //    loginPanel.SetActive(true);           
        //}
    }


    // 시작화면의 종료버튼을 눌렀을때 앱 종료
    public void OnButtonClickedQuit()
    {
        Application.Quit();

        // 유니티 에디터 정지
        //UnityEditor.EditorApplication.isPlaying = false;
    }



    // 로그인 패널의 '나가기'를 눌렀을때
    public void LoginPanelQuitButton()
    {
        loginPanel.SetActive(false);

        idInputField.text = null;
        passwordInputField.text = null;
        
    }

    


    // 테스트를 위한 로그인 비밀번호 상수

    private bool IsValidUser()
    {
        // 만약, 입력된 id와 비밀번호가 사전에 생성된 것들과 같다면
        if (idInputField.text == id && passwordInputField.text == psw)
                    
            {
                // true를 반환해주고
                return true;
            }
            else
            {
                // 아닐경우 false를 반환해라

                return false;


            }
               
    }
       

    // 로그인 패널 '확인'을 눌렀을때
    public void LoinPanelYesButton()
    {
        // 만약, 로그인 정보가 정확하다면
        //if (PhotonNetwork.IsConnected)
        
        // 로그인이 안되어 있고
        if(!isLogin)
        {
            // 아이디 비밀번호가 정확할때
            if (IsValidUser())
            {
                isLogin = true;

                Debug.Log("로그인 됬습니다.");
                // 로그인 패널을 끄고
                loginPanel.SetActive(false);
                // Text를 띄운다            
               // loginSuccess_text.gameObject.SetActive(true);
                // 3초뒤에 메세지를 숨긴다.
                Invoke("HideLoginSuccessText", 2f);

            }
            // 로그인 정보가 부 정확하다면 // 어떻게 부 정확한지 판단하는 스크립트도
            else 
            {
                // 로그인 실패 텍스트를 띄우고
                //loginFail_text.gameObject.SetActive(true);
                // 3초 뒤에 숨긴다.
                Invoke("HideLoginFailText", 2f);


            }
        }
       
    }


    // 로그인 성공 텍스트 숨기기
    public void HideLoginSuccessText()
    {
        loginSuccess_text.gameObject.SetActive(false);
    }

    // 로그인 실패 텍스트 숨기기
    public void HideLoginFailText()
    {
        loginFail_text.gameObject.SetActive(false);
    }

       

}


    


    






