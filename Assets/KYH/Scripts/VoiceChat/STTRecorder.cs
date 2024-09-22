using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.EventSystems;

public class STTRecorder : MonoBehaviourPun
{
    // 회의록을 남기는 클래스

    // VoiceChat Prefab
    public GameObject voiceChatFactory;

    // Content 의 Transform
    public RectTransform trContent;

    // ChatView 의 Transform
    public RectTransform trChatView;

    // 채팅이 추가되기 전의 Content 의 H(높이) 값을 가지고 있는 변수
    float prevContentH;

    // 닉네임 색상
    Color nickNameColor;

    void Start()
    {
        // 닉네임 색상 랜덤하게 설정
        nickNameColor = Random.ColorHSV();
    }    

    public void OnSubmit(string s)
    {
        //// 만약에 s 의 길이가 0 이면 함수를 나가자.
        //if (s.Length == 0) return;

        // 채팅 내용을 (NickName : 채팅 내용) 으로 묶기
        // "<color=#ffffff> 원하는 내용 </color>"
        string nick = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" + PhotonNetwork.NickName + "</color>";
        string chat = nick + " : " + s;

        // AddChat Rpc 함수 호출
        photonView.RPC(nameof(AddVoiceChat), RpcTarget.All, chat);
    }

    // 채팅 추가 함수
    [PunRPC]
    void AddVoiceChat(string chat)
    {
        // 새로운 채팅이 추가되기 전의 Content 의 H 값을 저장
        prevContentH = trContent.sizeDelta.y;

        // ChatItem 하나 만들자 (부모를 ChatView 의 Content 로 하자)
        GameObject go = Instantiate(voiceChatFactory, trContent);
        // ChatItem 컴포넌트 가져오자.
        VoiceChat chatItem = go.GetComponent<VoiceChat>();
        // 가져온 컴포넌트의 SetText 함수 실행
        chatItem.SetText(chat);
        // 가져온 컴포넌트의 onAutoScroll 변수에 AutoScrollBottom 을 설정
        chatItem.onAutoScroll = AutoScrollBottom;
    }

    // 채팅 추가 되었을 때 맨 밑으로 Content 위치를 옮기는 함수
    public void AutoScrollBottom()
    {
        // chatView 의 H 보다 content 의 H 값이 크다면 (스크롤이 가능한 상태라면)
        if (trContent.sizeDelta.y > trChatView.sizeDelta.y)
        {
            //trChatView.GetComponent<ScrollRect>().verticalNormalizedPosition - 0;

            // 이전 바닥에 닿아있었다면
            if (prevContentH - trChatView.sizeDelta.y <= trContent.anchoredPosition.y)
            {
                // content 의 y 값을 재설정한다.
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trChatView.sizeDelta.y);
            }
        }
    }
}
