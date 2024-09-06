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


    // 미리 패널창을 만들어둔다
    // 만든 패널창은 빨간색에 약 2초에 한번씩 깜빡인다, 
    // 깜빡이는건 알파값을 이용하고 
    // 부드럽게 깜빡이게 하기 위해 lerp함수를 쓴다

    // 캐릭터가 오브젝트에 충돌시 해당 패널 창을 3초간 활성화한다
    // 그리고 다 시 비활성화한다

    // 패널창이 비활성화 되면
    // 화면 정 중앙에 a4페이지 비율의 돌발 퀘스트 설명이 뜬다.
    // 3초 뒤 우측 하단에 작게 변해서 배치된다

    // 하단에 배치된 문서를 클릭하면
    // 다시 화면 정중앙에 크게 뜨고
    // 만약 그 상태에서 esc를 누르면
    // 다시 작게 변해서 하단에 배치된다.


    private void OnTriggerEnter(Collider other)
    {
        // Player 이름의 게임 오브젝트가 닿으면
        if(other.name.Contains("Player"))
        {

        }
    }

}
