    using UnityEngine;
using UnityEngine.UI;

public class ToggleUIVisibility : MonoBehaviour
{
    public CanvasGroup uiCanvasGroup;  // UI 요소의 CanvasGroup
    public Button toggleButton;        // 토글할 버튼
    public RectTransform uiRectTransform; // UI의 RectTransform

    private bool isVisible = true;     // UI가 보이는지 상태를 저장
    private bool isMoved = false;      // UI의 위치 상태 저장

    // 초기 위치
    private Vector2 initialPosition = new Vector2(12f, -772f);
    // 이동할 위치
    private Vector2 movedPosition = new Vector2(12f, -1377f);

    void Start()
    {
        // 버튼 클릭 이벤트에 토글 함수 추가
        toggleButton.onClick.AddListener(ToggleVisibility);

        // 초기 UI 상태 설정
        SetUIVisibility(!isVisible);
        // 초기 위치 설정
        uiRectTransform.anchoredPosition = initialPosition;
    }

    private void Update()
    {
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

        // 위치를 토글
        TogglePosition();

    }

   public void SetUIVisibility(bool visible)
    {
        if (visible)
        {
            // UI를 보이기 (투명도 1, 인터랙션 가능)
            uiCanvasGroup.alpha = 1f;
            uiCanvasGroup.interactable = true;
            uiCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            // UI를 숨기기 (투명도 0, 인터랙션 불가)
            uiCanvasGroup.alpha = 0f;
            uiCanvasGroup.interactable = false;
            uiCanvasGroup.blocksRaycasts = false;
        }
    }

    // UI의 위치를 변경하는 함수
    public void TogglePosition()
    {
        if (isMoved)
        {
            // 초기 위치로 이동
            uiRectTransform.anchoredPosition = initialPosition;
        }
        else
        {
            // 이동할 위치로 이동
            uiRectTransform.anchoredPosition = movedPosition;
           
           
        }

        // 위치 상태를 토글
        isMoved = !isMoved;
    }
}
