using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Legal_Trigger : MonoBehaviour
{
    // 싱글

    void OnTriggerEnter(Collider other)
    {
        // player move 컴포넌트를 가진 other 가 충돌하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 충돌한 플레이어의 photonview 컴포넌트를 가져온다.
            PhotonView photonview = other.GetComponent<PhotonView>();
            // 캐릭터는 동작, 회전을 멈추고
            if(photonview.IsMine)
            {
                // 현재 캔버스를 true로
                UI_Manager.instance.OnLegalPad();
            }
        }
    }


    //////////////// 포톤 멀티플레이 적용
    //void OnTriggerEnter(Collider other)
    //{
    //    // player move 컴포넌트를 가진 other 가 충돌하면
    //    if (other.CompareTag("Player"))
    //    {
    //        {
    //            // 충돌한 플레이어의 photonview 컴포넌트를 가져온다.
    //            PhotonView photonview = other.GetComponent<PhotonView>();

    //            if (photonview != null && photonview.IsMine)
    //            {
    //                photonview.RPC("ShowCanvas", RpcTarget.AllBuffered);
    //            }

    //        }
    //        // 캐릭터는 동작, 회전을 멈추고


    //    }
    //}

    //[PunRPC]
    //public void ShowCanvas()
    //{
    //    // ui매니저 스크립트에서 캔버스 함수를 실행한다.
    //    UI_Manager.instance.OnLegalPad();
    //}
}
