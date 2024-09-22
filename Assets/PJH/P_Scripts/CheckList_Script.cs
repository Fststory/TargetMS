using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList_Script : MonoBehaviour  // 체크리스트 3번에서 4번으로 넘어가는거부터 
{
    public GameObject checkList2;
    public GameObject checkList3;
    public GameObject checkList4;

    public GameObject currentCheck;
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    //체크리스트 3번에서 버튼 누르면 4번으로 넘어가게
    public void OnClickCheck3to4()
    {
        checkList3.SetActive(false);

        currentCheck = null;

        checkList4.SetActive(true);

        currentCheck = checkList4;

        ProposalMgr.instance.PostJson();

    }

  
}
