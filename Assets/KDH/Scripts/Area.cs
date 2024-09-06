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

            // Renderer가 있다면 색상을 초록색으로 변경
            if (objectRenderer != null)
            {
                objectRenderer.material.color = Color.yellow;
            }
        }
    }
}
