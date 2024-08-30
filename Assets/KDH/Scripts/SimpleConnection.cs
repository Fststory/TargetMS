using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleConnection : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon ȯ�漳���� ������� ������ ������ ������ �õ�
        PhotonNetwork.ConnectUsingSettings();

    }

    void Update()
    {

    }

    // ������ ������ ������ �Ǹ� ȣ��Ǵ� �Լ�
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        print("������ ������ ����");

        // �κ� ����
        JoinLobby();
    }
    public void JoinLobby()
    {
        // �г��� ����
        PhotonNetwork.NickName = "�����";
        // �⺻ Lobby ����
        PhotonNetwork.JoinLobby();
    }

    // �κ� ������ �����ϸ� ȣ��Ǵ� �Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� �Ϸ�");

        JoinOrCreateRoom();
    }

    // Room �� ��������. ���࿡ �ش� Room �� ������ Room ����ڴ�.
    public void JoinOrCreateRoom()
    {
        // �� ���� �ɼ�
        RoomOptions roomOption = new RoomOptions();
        // �濡 ��� �� �� �ִ� �ִ� �ο� ����
        roomOption.MaxPlayers = 20;
        // �κ� ���� ���̰� �� ���̴�? -- �⺻ true�� ���� ����
        roomOption.IsVisible = true;
        // �濡 ������ �� �� �ֳ�?? -- �⺻ true�� ���� ����
        roomOption.IsOpen = true;

        // Room ���� or ����
        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOption, TypedLobby.Default);
    }

    // �� ���� ���� ���� �� ȣ��Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("�� ���� �Ϸ�");
    }

    // �� ���� ���� ���� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�� ���� �Ϸ�");

        // ��Ƽ�÷��� ������ ��� �� �ִ� ����
        // GameScene���� �̵�!
        //SceneManager.LoadScene("");
        PhotonNetwork.LoadLevel("GameScene");
    }

}
