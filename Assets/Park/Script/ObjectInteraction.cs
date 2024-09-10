using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObjectInteraction: MonoBehaviour
{
    //public GameObject ButtonUI;
    public Button inputButton; // 버튼 그 자체 
    public float showDistance; // 버튼이 보이는 거리
    private Transform playerposition; // 플레이어 위치 추적
    private CanvasGroup inputF; //캔버스 그룹이 들어있는 오브젝트 => 버튼 그 자체의 캔버스 그룹 컴포넌트
    [FormerlySerializedAs("PhoneScreen")] public GameObject Screen; //클릭이 되었을때 나타나야 하는 화면

    
    void Start()
    {
        // 각각의 버튼에서 CanvasGroup 컴포넌트 찾기
        inputF = inputButton.GetComponent<CanvasGroup>();

        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어를 찾아서 플레이어 트랜스폼에 저장
        if (player != null)
        {
            playerposition = player.transform;
        }
    }

    void Update()
    {
        if (playerposition != null)
        {
            // 플레이어와 오브젝트 사이 거리를 계산
            float distance = Vector3.Distance(playerposition.position, transform.position);

            if (distance <= showDistance)
            {
                ShowSlider(inputF);
            }
            else
            {
                // 거리가 멀어지면 모든 슬라이더 숨기기
                HideSlider(inputF);
                if (Screen.activeSelf)
                {
                    Screen.SetActive(false);
                }
            }

            if (Screen.activeSelf)
            {
                HideSlider(inputF);
            }
        }
    }

    private void ShowSlider(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void HideSlider(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnLaptopScreen()
    {
        if (Screen != null)
        {
            Screen.SetActive(true);
        }
    }
    
    public void OnPhoneScreen()
    {
        if (Screen != null)
        {
            Screen.SetActive(true);
        }
    }

    public void OnBedAction()
    {
        
    }

    public void OnTvScreen()
    {
        
    }
}
