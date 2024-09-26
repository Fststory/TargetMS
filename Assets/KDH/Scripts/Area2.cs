using Photon.Voice.Unity.Demos.DemoVoiceUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        // 플레이어에 태그가 "Player"로 설정되어 있다고 가정
        if (enabled && other.CompareTag("Player"))
        {
            
            // 영역에 닿은 오브젝트의 Renderer를 가져온다
            Renderer objectRenderer = GetComponent<Renderer>();

            // Renderer가 있다면 색상을 노랑 변경
            if (objectRenderer != null)
            {
                objectRenderer.material.color = Color.yellow;
            
               
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        // "Player" 태그가 붙은 오브젝트가 나갔을 때만
        if (enabled && other.CompareTag("Player"))
        {
            // 영역에 닿은 오브젝트의 Renderer를 가져온다
            Renderer objectRenderer = GetComponent<Renderer>();

            if (objectRenderer != null)
            {
                // 색상을 원래 색상으로 되돌리기
                objectRenderer.material.color = new Color(0, 0.5f, 0, 1); // 소숫점 단위였음 ㅋㅋ
                //objectRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 1); // 소숫점 단위였음 ㅋㅋ

                
            }
        }
    }
}
