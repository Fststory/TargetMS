using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Event_Manager : MonoBehaviour // 돌발이벤트 발생 스크립트
{
    public GameObject AlertPanel;


    void Start()
    {
        
    }

    void Update()
    {
        
    }




    private void OnTriggerEnter(Collider other)
    {
        // Player 이름의 게임 오브젝트가 닿으면
        if(other.name.Contains("Player"))
        {

        }
    }

}
