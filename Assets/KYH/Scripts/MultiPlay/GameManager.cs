using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 플레이어를 생성 (현재 Room 에 접속되어 있는 친구들도 보이게)
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);     // PhotonNetwork.Instantiate => 생성할 프리팹이 반드시 Resources 폴더 안에 있어야 됨
    }

    void Update()
    {
        
    }
}
