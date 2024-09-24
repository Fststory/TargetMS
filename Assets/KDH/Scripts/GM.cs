using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviourPunCallbacks
{
    public static GM instance;

    GameObject playerFactory;
    void Start()
    {
        // RPC 보내는 빈도 설정
        PhotonNetwork.SendRate = 60;
        // OnPhotonSerializeView 보내고 받고 하는 빈도 설정
        PhotonNetwork.SerializationRate = 60;

        // 플레이어를 생성 (현재 Room 에 접속 되어있는 친구들도 보이게)

        // 수정이 필요하면 아래코드를 주석처리할것, 다했으면 다시 돌려놓을것
        PhotonNetwork.Instantiate("Player", new Vector3(-182,4,-3), Quaternion.identity);
        //PhotonNetwork.Instantiate("AA/Player", Vector3.zero, Quaternion.identity); - AA 폴더 안에 플레이어 있을경우 (Resources 안에 있어야함)
    }

    // Update is called once per frame
    void Update()
    {

    }
}
