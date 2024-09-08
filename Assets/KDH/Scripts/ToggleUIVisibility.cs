using UnityEngine;
using UnityEngine.UI;

public class ToggleUIVisibility : MonoBehaviour
{
    public CanvasGroup uiCanvasGroup;  // UI 요소의 CanvasGroup
    public Button toggleButton;        // 토글할 버튼

    private bool isVisible = true;     // UI가 보이는지 상태를 저장

    void Start()
    {
        // 버튼 클릭 이벤트에 토글 함수 추가
        toggleButton.onClick.AddListener(ToggleVisibility);

        // 초기 UI 상태 설정
        SetUIVisibility(isVisible);
    }

   public void ToggleVisibility()
    {
        // 현재 상태를 반대로 전환
        isVisible = !isVisible;

        // UI 상태를 설정
        SetUIVisibility(isVisible);
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
}
