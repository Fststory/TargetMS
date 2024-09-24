using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciga_Trigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // player move 컴포넌트를 가진 other 가 충돌하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 캐릭터는 동작, 회전을 멈추고

            // 현재 캔버스를 true로
            UI_Manager.instance.OnCigarette();
        }
    }







    //////////////////////// 포톤 멀티 플레이용 
    //void OnTriggerEnter(Collider other)
    //{
    //    // player move 컴포넌트를 가진 other 가 충돌하면
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        PhotonView photonview = other.GetComponent<PhotonView>();

    //        if(photonview != null && photonview.IsMine)
    //        {
    //            photonview.RPC("ShowCanvas", RpcTarget.AllBuffered);
    //        }
    //        // 캐릭터는 동작, 회전을 멈추고


    //    }
    //}

    //[PunRPC]
    //void ShowCanvas()
    //{
    //    // 현재 캔버스를 true로
    //    UI_Manager.instance.OnCigarette();
    //}



}




