using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유저 정보    JSON으로 만들 수 있도록 직렬화할 수 있어야 됨(Serializable)
[System.Serializable]
public class UserInfo
{
    public string userName;
    public int age;
    public float height;
    public List<string> favoriteList;
}

public class FBDatabase : MonoBehaviour
{
    public static FBDatabase instance;

    // Firebase Database 를 관리하는 변수
    FirebaseDatabase database;

    // 내 정보
    public UserInfo myInfo;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        database = FirebaseDatabase.DefaultInstance;

        // 내 정보 입력
        myInfo = new UserInfo();
        myInfo.userName = "김영호";
        myInfo.age = 27;
        myInfo.height = 183;
        myInfo.favoriteList = new List<string>();
        myInfo.favoriteList.Add("돼지국밥");
        myInfo.favoriteList.Add("순두부찌개");
        myInfo.favoriteList.Add("갈치구이");
    }

    void Update()
    {
        
    }

    public void SaveMyInfo()
    {
        StartCoroutine(CoSaveMyInfo());
    }

    IEnumerator CoSaveMyInfo()
    {
        // 저장 경로
        string path = "user_info/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // 내 정보를 Json 형태로 바꾸자
        string jsonData = JsonUtility.ToJson(myInfo);

        // 저장을 요청 하자
        var task = database.GetReference(path).SetRawJsonValueAsync(jsonData);

        // 통신이 완료될 때까지 기다리자
        yield return new WaitUntil(() => task.IsCompleted);

        // 예외사항이 없다면
        if (task.Exception == null)
        {
            // 저장 성공
            print("내 정보 저장 성공");
        }
        // 그렇지 않으면
        else
        {
            // 저장 실패
            print("내 정보 저장 실패 : "+ task.Exception.Message);
        }
    }

    public void LoadMyInfo(Action<string> complete)
    {
        StartCoroutine(CoLoadMyInfo(complete));
    }

    IEnumerator CoLoadMyInfo(Action<string> complete)
    {
        string path = "user_info/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        // 불러오기 요청
        var task = database.GetReference(path).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception == null)
        {
            print("불러오기 성공");
            if (complete != null)
            {
                complete(task.Result.GetRawJsonValue());
            }
        }
        else
        {
            print("불러오기 실패");
        }
    }
}
