using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject playerFactory;
    
    // 스폰 위치 담을 변수
    public Vector3[] spawnPos;
    public Transform spawnCenter;

    void Start()
    {
        SetSpawnPos();

        // RPC 보내는 빈도 설정
        PhotonNetwork.SendRate = 60;
        // OnPhotonSerializeView 보내고 받고 하는 빈도 설정
        PhotonNetwork.SerializationRate = 60;

        // 내가 위치해야 하는 idx를 알아오자 (현재 방에 들어와 있는 인원 수)
        int idx = PhotonNetwork.CurrentRoom.PlayerCount -1;

        // 플레이어를 생성 (현재 Room 에 접속 되어있는 친구들도 보이게)
        PhotonNetwork.Instantiate("Player", spawnPos[idx], Quaternion.identity);

    }
    void Update()
    {
    }

  
    void SetSpawnPos()
    {
        // maxPlayer 를 현재 방의 최대 인원으로 설정
        int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers; //currentroom에 최대인원. currentroom이 지금 없어서 오류

        // 최대 인원 만큼 spawnPos 의 공간 할당
        spawnPos = new Vector3[maxPlayer];

        // spawnPos 간의 간격(각도)
        float angle = 360.0f / maxPlayer;

        // maxPlayer 만큼 반복
        for (int i = 0; i < maxPlayer; i++)
        {
            // spawnCenter 회전 (i * angle) 만큼
            spawnCenter.eulerAngles = new Vector3(0, i*angle, 0);
            // spawnCenter 앞방향으로 2만큼 떨어진 위치 구하자.
            spawnPos[i] = spawnCenter.position + spawnCenter.forward * 2;
        }

    }
}
