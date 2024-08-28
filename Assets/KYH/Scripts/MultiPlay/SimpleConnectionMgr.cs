using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon 환경설정을 기반으로 Master Server에 접속을 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // Master Server에 접속이 되면 호출되는 함수
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("Master Server에 접속");

        // Lobby 접속
        JoinLobby();
    }

    public void JoinLobby()
    {
        // 닉네임 설정
        PhotonNetwork.NickName = "Zㅣ타락천사V김영호ㅣZ";
        // 기본 Lobby 입장
        PhotonNetwork.JoinLobby();
    }

    // Lobby에 참여가 성공하면 호출되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("Lobby 입장 완료");

        JoinOrCreateRoom();
    }

    // Room 을 참여하자. 만약에 해당 Room 이 없으면 Room 을 만들겠다.
    public void JoinOrCreateRoom()
    {
        // Room 생성 옵션
        RoomOptions roomOption = new RoomOptions();
        // Room에 들어올 수 있는 최대 인원 설정
        roomOption.MaxPlayers = 20;
        // Lobby에 Room을 보이게 할 것이니?
        roomOption.IsVisible = true;
        // Room에 참여를 할 수 있니?
        roomOption.IsOpen = true;

        // Room 참여 or 생성
        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOption, TypedLobby.Default);
    }

    // Room 생성 성공했을 때 호출되는 함수   -> 생성과 동시에 입장이 이뤄짐
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("Room 생성 완료");
    }

    // Room 입장 성공했을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("Room 입장 완료");

        // 멀티플레이 컨텐츠 즐길 수 있는 상태
        // GameScene으로 이동!
        PhotonNetwork.LoadLevel("GameScene");   // SceneManager에서 하는 거랑 비슷하지만 이동 중 유실될 수 있는 것을 보호해주기에 이렇게 이동해야 된다.
    }
}
