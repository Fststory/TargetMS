using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    
    void Start()
    {
        
    }

    
    void Update()
    {
        // 플레이어가 영역 위로 올라가면 색이 바뀌게 하고싶다.
       

    }
    // 영역에 플레이어가 닿았을 때 실행되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어에 태그가 "Player"로 설정되어 있다고 가정
        if (other.CompareTag("Player"))
        {
            // 영역에 닿은 오브젝트의 Renderer를 가져온다
            Renderer objectRenderer = GetComponent<Renderer>();

            // Renderer가 있다면 색상을 노랑 변경
            if (objectRenderer != null)
            {
                objectRenderer.material.color = Color.yellow;
                //objectRenderer.material.color = new Color(255, 255, 0, 1f);

                //Image image = GetComponent<Image>();
                //image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }

        }
    }
    // 플레이어가 영역에서 나갔을 때 호출되는 함수
    private void OnTriggerExit(Collider other)
    {
        // "Player" 태그가 붙은 오브젝트가 나갔을 때만
        if (other.CompareTag("Player"))
        {
            // 영역에 닿은 오브젝트의 Renderer를 가져온다
            Renderer objectRenderer = GetComponent<Renderer>();

            if (objectRenderer != null)
            {
                // 색상을 원래 색상으로 되돌리기
                //objectRenderer.material.color = new Color(0, 0.5f, 0, 1); // 소숫점 단위였음 ㅋㅋ
                objectRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 1); // 소숫점 단위였음 ㅋㅋ

                //objectRenderer.material.color = new Color(255, 255, 0, 1f); // 이거는 안됨ㅋㅋ
                //objectRenderer.material.color = Color.;
            }
        }
    }
}
