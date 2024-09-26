using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Trigger : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // player move 컴포넌트를 가진 other 가 충돌하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 충돌한 플레이어의 photonview 컴포넌트를 가져온다.
            PhotonView photonview = other.GetComponent<PhotonView>();
            // 캐릭터는 동작, 회전을 멈추고
            if (photonview.IsMine)
            {
                // 현재 캔버스를 true로
                UI_Manager.instance.OnPhone();
            }
        }
    }






    //////////////////////// 포톤 멀티 플레이용 
    //void OnTriggerEnter(Collider other)
    //{
    //    // player move 컴포넌트를 가진 other 가 충돌하면
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        PhotonView photonview = other.GetComponent<PhotonView>();

    //        if (photonview != null && photonview.IsMine)
    //        {
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
    //    // 현재 캔버스를 true로
    //    UI_Manager.instance.OnPhone();
    //}
}

