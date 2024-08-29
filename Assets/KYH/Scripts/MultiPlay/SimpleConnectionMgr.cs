using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon ȯ�漳���� ������� Master Server�� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        
    }

    // Master Server�� ������ �Ǹ� ȣ��Ǵ� �Լ�
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("Master Server�� ����");

        // Lobby ����
        JoinLobby();
    }

    public void JoinLobby()
    {
        // �г��� ����
        PhotonNetwork.NickName = "Z��Ÿ��õ��V�迵ȣ��Z";
        // �⺻ Lobby ����
        PhotonNetwork.JoinLobby();
    }

    // Lobby�� ������ �����ϸ� ȣ��Ǵ� �Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("Lobby ���� �Ϸ�");

        JoinOrCreateRoom();
    }

    // Room �� ��������. ���࿡ �ش� Room �� ������ Room �� ����ڴ�.
    public void JoinOrCreateRoom()
    {
        // Room ���� �ɼ�
        RoomOptions roomOption = new RoomOptions();
        // Room�� ���� �� �ִ� �ִ� �ο� ����
        roomOption.MaxPlayers = 20;
        // Lobby�� Room�� ���̰� �� ���̴�?
        roomOption.IsVisible = true;
        // Room�� ������ �� �� �ִ�?
        roomOption.IsOpen = true;

        // Room ���� or ����
        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOption, TypedLobby.Default);
    }

    // Room ���� �������� �� ȣ��Ǵ� �Լ�   -> ������ ���ÿ� ������ �̷���
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("Room ���� �Ϸ�");
    }

    // Room ���� �������� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("Room ���� �Ϸ�");

        // ��Ƽ�÷��� ������ ��� �� �ִ� ����
        // GameScene���� �̵�!
        PhotonNetwork.LoadLevel("GameScene");   // SceneManager���� �ϴ� �Ŷ� ��������� �̵� �� ���ǵ� �� �ִ� ���� ��ȣ���ֱ⿡ �̷��� �̵��ؾ� �ȴ�.
    }
}
