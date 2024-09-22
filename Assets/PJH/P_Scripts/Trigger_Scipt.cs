using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Scipt : MonoBehaviour // 모든 트리거 부모
{  

    void Start()
    {
        
    }

    void Update()
    {
        
    }



    
    public virtual void OnTriggerEnter(Collider other)
    {
        // player move 컴포넌트를 가진 other 가 충돌하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 캐릭터는 동작, 회전을 멈추고

            // 현재 캔버스를 true로
            UI_Manager.instance.currentPanel.SetActive(true);
        }
    }
}
