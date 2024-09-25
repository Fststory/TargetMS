using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// 4번째 캔버스에 문서작성중 / 문서작성이 완료되었습니다 문구 출력
/// </summary>
public class Canvas4_Script : MonoBehaviour
{
    public GameObject docING; // 진행중
    public GameObject docDone; // 문서작성 완료
    public GameObject docTextPrefab; // 만약, photonetwork를 쓴다면 resource 폴더안에
    public TMP_Text actualDoc; // 실제 작성될 문서

    void Start()
    {
        // 시작부터 ing text를 띄우는 상태
    }

    void Update()
    {
       // // 만약, 문서가 null이 아니라면
       //if(actualDoc != null)
       // {
       //     // 필요하다면 photonNetwork.instantiate 을쓴다.
       //     //PhotonNetwork.Instantiate
           

       // }
       
    }

    public void DoneText()
    {
        docDone.gameObject.SetActive(true);
        docING.gameObject.SetActive(false);
    }

    
}
