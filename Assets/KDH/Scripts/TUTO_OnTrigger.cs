using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTO_OnTrigger : MonoBehaviour
{
    public GameObject TUTO_Keyword;

    bool isCheckTrue = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // isCheckTrue가 true인 상태에서 스페이스바를 누르면 TUTO_Keyword를 비활성화
        if (isCheckTrue && Input.GetKeyDown(KeyCode.Space))
        {
            TUTO_Keyword.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCheckTrue) return;  // 이미 충돌한 경우 중복 실행 방지

        // 플레이어에 태그가 "Player"로 설정되어 있다고 가정
        if (enabled && other.CompareTag("Player"))
        {
            isCheckTrue = true;

            // TUTO_Keyword 오브젝트를 활성화
            TUTO_Keyword.SetActive(true);
        }
    }
}
