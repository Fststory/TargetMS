using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FBAuth : MonoBehaviour
{
    FirebaseAuth auth;

    public InputField inputEmail;
    public InputField inputPassword;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            // auth 기능을 전체적으로 관리 실행하는 클래스 담아놓자.
            auth = FirebaseAuth.DefaultInstance;
            // 로그인 / 로그아웃 상태 체크
            auth.StateChanged += OnChangeAuthState;
        });
    }

    void Update()
    {
        
    }

    void OnChangeAuthState(object sender, EventArgs e)
    {
        // 만약에 유저정보가 있으면
        if(auth.CurrentUser != null)
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
        // 회원가입 시도
        var task = auth.CreateUserWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);

        // 통신이 완료될 때까지 기다리자
        yield return new WaitUntil(() => task.IsCompleted);

        // 만약에 예외사항이 없으면
        if (task.Exception == null)
        {
            // 회원 가입 성공
            print("회원 가입 성공");
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
            print("로그인 성공");
        }
        // 그렇지 않으면
        else
        {
            // 로그인 실패
            print("로그인 실패 : " + task.Exception.Message);
        }
    }

    // 로그아웃
    public void OnClickLogout()
    {
        auth.SignOut();
    }
}
