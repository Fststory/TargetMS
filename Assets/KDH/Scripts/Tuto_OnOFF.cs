// 스페이스 눌러도 버튼 안눌리게
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // 포커스 해제를 위해 필요

public class Tuto_OnOFF : MonoBehaviour
{
    public GameObject uiElement;  // UI의 나머지 요소 (토글할 대상)
    public Button toggleButton;   // 토글할 버튼

    private bool isVisible = true;  // UI가 보이는지 상태를 저장

    void Start()
    {
        // 버튼 클릭 이벤트에 토글 함수 추가
        toggleButton.onClick.AddListener(ToggleVisibility);

        // 초기 UI 상태 설정
        SetUIVisibility(isVisible);
    }

    private void Update()
    {
        // F11 키로 토글 가능
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleVisibility();
        }
    }

    public void ToggleVisibility()
    {
        // 현재 상태를 반대로 전환
        isVisible = !isVisible;

        // UI 상태를 설정
        SetUIVisibility(isVisible);

        // 버튼 클릭 후 포커스 해제
        EventSystem.current.SetSelectedGameObject(null);  // 포커스를 없애서 스페이스바로 버튼이 눌리지 않도록 함
    }

    public void SetUIVisibility(bool visible)
    {
        // 나머지 UI 오브젝트만 활성화/비활성화
        uiElement.SetActive(visible);
    }
}

// 기존 식 인데 스페이스바 누르면 자꾸 버튼 눌림
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Tuto_OnOFF : MonoBehaviour
//{
//    public GameObject uiElement;  // UI 요소
//    public Button toggleButton;   // 토글할 버튼

//    private bool isVisible = true;  // UI가 보이는지 상태를 저장

//    void Start()
//    {
//        // 버튼 클릭 이벤트에 토글 함수 추가
//        toggleButton.onClick.AddListener(ToggleVisibility);

//        // 초기 UI 상태 설정
//        SetUIVisibility(isVisible);
//    }

//    public void ToggleVisibility()
//    {
//        // 현재 상태를 반대로 전환
//        isVisible = !isVisible;

//        // UI 상태를 설정
//        SetUIVisibility(isVisible);

//        // 클릭시 마우스 잠금
//        Cursor.lockState = CursorLockMode.Locked;

//    }

//    public void SetUIVisibility(bool visible)
//    {
//        // UI 오브젝트의 활성화/비활성화 설정
//        uiElement.SetActive(visible);
//    }
//}
