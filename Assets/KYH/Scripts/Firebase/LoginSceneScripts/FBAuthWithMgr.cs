using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public class FBAuthWighMgr : MonoBehaviour
{
    // 로그인 패널
    [Header("로그인 패널 변수")]
    public GameObject loginPanel;
    public TextMeshProUGUI loginSuccess_text;
    public TextMeshProUGUI signinSuccess_text;
    public TextMeshProUGUI loginFail_text;
    public TMP_InputField currentInputField;
    bool isLogin; // 로그인 확인

    // 음악
    [Header("음악 변수")]
    private AudioSource audioSource; // 오디오 클립을 드래그해서 인스펙터에 드랍


    FirebaseAuth auth;

    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    void Start()
    {
        isLogin = false;

        // 시작때 로그인 패널 숨기기
        loginPanel.SetActive(false);

        loginSuccess_text.gameObject.SetActive(false);
        loginFail_text.gameObject.SetActive(false);

        // 시작시 음악 재생
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        currentInputField = inputEmail;

        // auth 기능을 전체적으로 관리 실행하는 클래스 담아놓자.
        auth = FirebaseAuth.DefaultInstance;
        // 로그인 / 로그아웃 상태 체크
        auth.StateChanged += OnChangeAuthState;
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
        if (currentInputField == inputEmail)
        {
            inputPassword.Select();
            currentInputField = inputPassword;
        }
        else if (currentInputField == inputPassword)
        {
            inputEmail.Select();
            currentInputField = inputEmail;
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
        if (isLogin)
        {
            // 로그인이 되어있고 시작하기 버튼을 누르면 로비 테스트 씬으로 이동
            SceneManager.LoadScene("connectScene");
        }
        else
        {
            //로그인 패널을 활성화
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

        inputEmail.text = null;
        inputPassword.text = null;
    }

    // 회원가입 성공 텍스트 숨기기
    public void HideSigninSuccessText()
    {
        signinSuccess_text.gameObject.SetActive(false);
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

    void OnChangeAuthState(object sender, EventArgs e)
    {
        // 만약에 유저정보가 있으면
        if (auth.CurrentUser != null)
        {
            // 로그인상태
            print("Email : " + auth.CurrentUser.Email);
            print("UserId : " + auth.CurrentUser.UserId);
            print("로그인 상태");

            // 내 정보 불러오기
            FBDatabase.instance.LoadMyInfo(OnLoadMyInfo);
        }
        // 그렇지 않으면
        else
        {
            // 로그아웃
            print("로그아웃 상태");
        }
    }

    public UserInfo myInfo;
    void OnLoadMyInfo(string strInfo)
    {
        myInfo = JsonUtility.FromJson<UserInfo>(strInfo);
    }

    public void OnClickSignIn()
    {
        StartCoroutine(CoSignIn());
    }

    IEnumerator CoSignIn()
    {
        Debug.Log("Auth: " + auth);
        Debug.Log("Email: " + inputEmail.text);
        Debug.Log("Password: " + inputPassword.text);

        // 회원가입 시도
        var task = auth.CreateUserWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);

        // 통신이 완료될 때까지 기다리자
        yield return new WaitUntil(() => task.IsCompleted);

        // 만약에 예외사항이 없으면
        if (task.Exception == null)
        {
            // 회원 가입 성공
            print("회원 가입 성공");
            // Text를 띄운다            
            signinSuccess_text.gameObject.SetActive(true);
            // 3초뒤에 메세지를 숨긴다.
            Invoke("HideSigninSuccessText", 2f);
        }
        // 그렇지 않으면
        else
        {
            // 회원 가입 실패
            print("회원 가입 실패 : " + task.Exception.Message);
        }
    }

    // 로그인
    public void OnClickLogin()
    {
        StartCoroutine(CoLogin());
    }

    IEnumerator CoLogin()
    {
        // 로그인 시도
        var task = auth.SignInWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);

        // 통신이 완료될 때까지 기다리자
        yield return new WaitUntil(() => task.IsCompleted);

        // 만약에 예외사항이 없다면
        if (task.Exception == null)
        {
            // 로그인 성공
            isLogin = true;

            Debug.Log("로그인 성공");
            // 로그인 패널을 끄고
            loginPanel.SetActive(false);
            // Text를 띄운다            
            loginSuccess_text.gameObject.SetActive(true);
            // 3초뒤에 메세지를 숨긴다.
            Invoke("HideLoginSuccessText", 2f);

        }
        // 그렇지 않으면
        else
        {
            // 로그인 실패
            Debug.Log("로그인 실패 : " + task.Exception.Message);

            // 로그인 실패 텍스트를 띄우고
            loginFail_text.gameObject.SetActive(true);
            // 3초 뒤에 숨긴다.
            Invoke("HideLoginFailText", 2f);
        }
    }

    // 로그아웃
    public void OnClickLogout()
    {
        auth.SignOut();
        isLogin = false;
    }
}