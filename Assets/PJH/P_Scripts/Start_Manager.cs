using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Start_Manager : MonoBehaviourPunCallbacks
{

    // 로그인 패널
    [Header("로그인 패널 변수")]
    public GameObject loginPanel;
    public TextMeshProUGUI loginSuccess_text;
    public TextMeshProUGUI loginFail_text;

    // 음악
    [Header("음악 변수")]
    private AudioSource audioSource; // 오디오 클립을 드래그해서 인스펙터에 드랍




    void Start()
    {
        // 시작때 로그인 패널 숨기기
        loginPanel.SetActive(false);
        
        loginSuccess_text.gameObject.SetActive(false);
        loginFail_text.gameObject.SetActive(false);

        // 시작시 음악 재생
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    void Update()
    {

    }

    // 시작버튼 눌렀을때, 
    public void OnButtonClickedStart()
    {
        // 로그인이 되어 있으면 , 로비로 이동
        if (PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Lobby");

            //PhotonNetwork.LoadLevel("Lobby"); 포톤사용시
        }
        // 로그인이 안되어 있으면
        else
        {
            // 로그인 패널을 활성화
            loginPanel.SetActive(true);           
        }
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
    }

    // 로그인 패널 '확인'을 눌렀을때
    public void LoinPanelYesButton()
    {
        // 만약, 로그인 정보가 정확하다면
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("로그인 됬습니다.");
            // 로그인 패널을 끄고
            loginPanel.SetActive(false);
            // Text를 띄운다            
            loginSuccess_text.gameObject.SetActive(true);
            // 3초뒤에 메세지를 숨긴다.
            Invoke("HideLoginSuccessText", 3f);
           
        }
        // 로그인 정보가 부 정확하다면 // 어떻게 부 정확한지 판단하는 스크립트도
        else
        {
            // 로크인 실패 텍스트를 띄우고
            loginFail_text.gameObject.SetActive(true);
            // 3초 뒤에 숨긴다.
            Invoke("HideLoginFailText", 3f);

           
        }


    }


    public void HideLoginSuccessText()
    {
        loginSuccess_text.gameObject.SetActive(false);
    }

    public void HideLoginFailText()
    {
        loginFail_text.gameObject.SetActive(false);
    }

   
    




}


    


    






