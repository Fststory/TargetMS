using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using JetBrains.Annotations;
using System;
using Photon.Realtime;

public class Connect_Manager : MonoBehaviourPunCallbacks   // 해당 씬에 들어오자마자 마스터-채널-로비-방생성까지 한번에 되게 설정, 경우에 따라 함수를 스타트에 붙여넣기
{
    void Start()
    {
        
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // 마스터 서버 접속시 호출되는 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 입장했습니다.");
        base.OnConnectedToMaster();

        JoinLobby();

    }

    public void JoinLobby()
    {
        // 기본 로비 입장
        PhotonNetwork.JoinLobby();
    }

    // 로비 접속시 호출되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("로비에 접속했습니다");

        JoinOrCreateRoom();
    }

    public void JoinOrCreateRoom()
    {       
        // 방이 없다면 방 옵션으로
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("TargetMs_Room_1", roomOptions, TypedLobby.Default);
        
    }

    // 방 생성에 성공했을때 호출 될 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    // 방 입장 성공시 호출될 함수 // 일반적으로 방을 생성하면 자동으로 들어가지게 됨.
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 생성 완료");

        // 멀티플레이 컨텐츠 즐길 수 있는 상태

        // 동휘 씨의 PlayScene으로 이동
        // GameScene으로 이동!
        PhotonNetwork.LoadLevel("PlayScene");
    }


    //// 로비 접속시 방을 찾고 없으면 만들기
    //public void JoinOrCreateRoom()
    //{
    //    RoomOptions roomOptions = new RoomOptions();


    //    // 방을 찾기
    //    PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
    //    PhotonNetwork.JoinOrCreateRoom("Room2", roomOptions, TypedLobby.Default);
    //    PhotonNetwork.JoinOrCreateRoom("Room3", roomOptions, TypedLobby.Default);

    //    // 방이 없다면 아래의 정보로 생성
    //    roomOptions.MaxPlayers = 5;
    //    roomOptions.IsVisible = true;
    //    roomOptions.IsOpen = true;

    //}

    //// 방이 있을때
    //public override void OnJoinedRoom()
    //{
    //    Debug.Log("방에 입장합니다.");
    //    base.OnJoinedRoom();

    //    PhotonNetwork.LoadLevel("GameScene");
    //}

    //// 방이 없어서 생성됬을때
    //public override void OnCreatedRoom()
    //{
    //    Debug.Log("방을 만들었습니다.");
    //    base.OnCreatedRoom();
    //}


}
