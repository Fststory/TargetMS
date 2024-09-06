using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Scipt : MonoBehaviour
{
    public UI_Manager uimanager;

    void Start()
    {
        uimanager.GetComponent<UI_Manager>();
    }

    void Update()
    {
        
    }



    
    private void OnTriggerEnter(Collider other)
    {
        // player move 컴포넌트를 가진 other 가 충돌하면
        if (other.gameObject.CompareTag("Player"))
        {
            // 캐릭터는 동작, 회전을 멈추고

            // info 캔버스를 true로
            uimanager.info_Canvas.SetActive(true);
        }
    }
}
