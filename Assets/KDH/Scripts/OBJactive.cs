using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJactive : MonoBehaviour
{
    // 오브젝트를 할당받을 변수
    public GameObject targetObject;

    // 키보드 입력을 통해 활성화/비활성화
    void Update()
    {
        // 스페이스바를 눌렀을 때 오브젝트의 활성화 상태를 전환
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 현재 오브젝트의 활성화 상태를 가져와 반전시킴
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);
        }
    }
}
