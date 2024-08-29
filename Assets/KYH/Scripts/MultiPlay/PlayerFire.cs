using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviourPun
{
    // ť�� Prefab
    public GameObject cubeFactory;

    void Start()
    {
        
    }

    void Update()
    {
        // ���࿡ �� ���� �ƴ϶��
        if (photonView.IsMine == false) return;

        // 1 ��Ű ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ī�޶��� �չ������� 5��ŭ ������ ��ġ�� ������.
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 5;
            // ť����忡�� ť�긦 ����, ��ġ, ȸ��
            PhotonNetwork.Instantiate("Cube", pos, Quaternion.identity);   // 1. Ư�� ��ġ�� �ٷ� �����ϴ� �� �ڵ�� 2. ���� �� ��ġ �����̶��� ���̰� �ִ�.
        }
    }
}
