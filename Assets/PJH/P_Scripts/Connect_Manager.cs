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

        PhotonNetwork JoinOrCreateRoom("TargetMs_Room_1", roomOptions, TypeLobby.Defalut);
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
