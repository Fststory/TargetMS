using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTO : MonoBehaviour
{
    // "TUTO1" 오브젝트를 에디터에서 연결할 수 있도록 공개 변수 선언
    public GameObject tuto1Object;

    void Update()
    {
        // 스페이스바가 눌렸는지 확인
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 이 스크립트가 붙어 있는 오브젝트를 비활성화
            gameObject.SetActive(false);

            // "TUTO1" 오브젝트 활성화
            if (tuto1Object != null)
            {
                tuto1Object.SetActive(true);
            }
        }
    }
}
