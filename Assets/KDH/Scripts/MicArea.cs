using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicArea : MonoBehaviour
{
    // 각 영역을 정의
    public GameObject area1;
    public GameObject area2;
    public GameObject area3;
    public GameObject area4;
    public GameObject area5;

    // Area1의 스크립트를 저장할 변수
    private Area area1Script;

    // 각 영역에 플레이어가 들어갔다 나온 상태를 확인하는 플래그
    private bool enteredArea2 = false;
    private bool enteredArea3 = false;
    private bool enteredArea4 = false;
    private bool enteredArea5 = false;

    void Start()
    {
        // Area1의 Area 스크립트를 가져옴
        area1Script = area1.GetComponent<Area>();

        // 시작 시 Area1의 스크립트를 비활성화
        area1Script.enabled = false;
    }

    void Update()
    {
        // 모든 영역을 한 번씩 통과했을 때 Area1의 스크립트를 활성화
        if (enteredArea2 && enteredArea3 && enteredArea4 && enteredArea5 && !area1Script.enabled)
        {
            area1Script.enabled = true;
        }
    }

    // 영역에 플레이어가 들어왔을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 각 영역에 플레이어가 들어갔을 때 플래그를 true로 설정
            if (other.gameObject == area2)
            {
                enteredArea2 = true;
            }
            else if (other.gameObject == area3)
            {
                enteredArea3 = true;
            }
            else if (other.gameObject == area4)
            {
                enteredArea4 = true;
            }
            else if (other.gameObject == area5)
            {
                enteredArea5 = true;
            }
        }
    }

    // 영역에서 플레이어가 나갔을 때 호출되는 함수 (필요하지 않다면 생략 가능)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 다른 특별한 동작은 필요 없으므로 빈 상태로 남겨둘 수 있음
        }
    }
}


//// 아래를 토대로 pt 형한테 맡긴 결과 -- 응 그냥 다 돼~ 실패...
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MicArea : MonoBehaviour
//{
//    // 각 영역을 정의
//    public GameObject area1;
//    public GameObject area2;
//    public GameObject area3;
//    public GameObject area4;
//    public GameObject area5;

//    // Area1의 스크립트를 저장할 변수
//    private Area area1Script;

//    // 플레이어가 들어가 있는 상태를 확인하는 플래그
//    private bool playerInOtherAreas = false;

//    void Start()
//    {
//        // Area1의 Area 스크립트를 가져옴
//        area1Script = area1.GetComponent<Area>();

//        // 시작 시 Area1의 스크립트를 비활성화
//        area1Script.enabled = false;
//    }

//    void Update()
//    {
//        // 플레이어가 다른 영역에서 나왔고, 다른 영역들에 플레이어가 없다면 Area1의 스크립트를 활성화
//        if (!playerInOtherAreas && !area1Script.enabled)
//        {
//            area1Script.enabled = true;
//        }
//    }

//    // 영역에 플레이어가 들어왔을 때 호출되는 함수
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            // area2, area3, area4, area5에 플레이어가 들어가면 area1 스크립트 비활성화
//            if (other.gameObject == area2 || other.gameObject == area3 || other.gameObject == area4 || other.gameObject == area5)
//            {
//                playerInOtherAreas = true;
//                area1Script.enabled = false;
//            }
//        }
//    }

//    // 영역에서 플레이어가 나갔을 때 호출되는 함수
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            // area2, area3, area4, area5에서 플레이어가 나갔는지 확인
//            if (other.gameObject == area2 || other.gameObject == area3 || other.gameObject == area4 || other.gameObject == area5)
//            {
//                playerInOtherAreas = false;
//            }
//        }
//    }
//}



// 내가 직접 써본 식..실패
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MicArea : MonoBehaviour
//{
//    // 일단 밟아야 하는 영역을 가져온다.
//    public GameObject area1;
//    public GameObject area2;
//    public GameObject area3;
//    public GameObject area4;

//    // Start is called before the first frame update
//    void Start()
//    {

//        // 일단 영역에 있는 Area 컴포넌트를 비활성화 한다.
//        Area area = GetComponent<Area>();

//        area.enabled = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // 영역에 닿은 오브젝트의 Renderer를 가져온다
//        Renderer area1 = GetComponent<Renderer>();
//        Renderer area2 = GetComponent<Renderer>();
//        Renderer area3 = GetComponent<Renderer>();
//        Renderer area4 = GetComponent<Renderer>();

//        new Color(0.5f, 0.5f, 0.5f, 1) = color

//        if (area1.material.color(0.5f, 0.5f, 0.5f, 1) && area2.material.color(0.5f, 0.5f, 0.5f, 1) && area3.material.color(0.5f, 0.5f, 0.5f, 1) && area4.material.color(0.5f, 0.5f, 0.5f, 1))
//        {

//        }


//    }
//}
