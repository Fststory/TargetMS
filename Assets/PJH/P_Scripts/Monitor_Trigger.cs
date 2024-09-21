using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor_Trigger : MonoBehaviour
{

    public GameObject checkListCanvas;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // 모니터 박스 안에 있을때
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //checklist 캔버스를 띄운다. 토글
                checkListCanvas.SetActive(!checkListCanvas.activeSelf);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // 박스 안에서 나가면
        if (other.gameObject.CompareTag("Player"))
        {
            checkListCanvas.SetActive(false);
        }
    }


}
