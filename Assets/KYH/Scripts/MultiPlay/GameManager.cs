using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // �÷��̾ ���� (���� Room �� ���ӵǾ� �ִ� ģ���鵵 ���̰�)
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);     // PhotonNetwork.Instantiate => ������ �������� �ݵ�� Resources ���� �ȿ� �־�� ��
    }

    void Update()
    {
        
    }
}
