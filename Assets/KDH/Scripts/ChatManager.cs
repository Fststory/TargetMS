// 채팅 서로 구분하면서 잘 나오게 해보려고 했는데 안되네 
//using ExitGames.Client.Photon;
//using Photon.Chat;
//using Photon.Pun;
//using Photon.Realtime;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ChatManager : MonoBehaviour, IChatClientListener
//{
//    // 채팅을 총괄하는 객체
//    ChatClient chatClient;

//    // 채팅 입력 UI
//    public TMP_InputField inputChat;

//    // 채팅 채널
//    string currChannel = "메타";

//    // 스크롤 뷰의 Content
//    public RectTransform trContent;
//    // ChatItem Prefab
//    public GameObject chatItemFactory;

//    void Start()
//    {
//        // 채팅내용을 작성하고 엔터를 쳤을때 호출되는 함수 등록
//        inputChat.onSubmit.AddListener(OnSubmit);

//        Connect();

//        // 게임 시작 시 마우스를 잠그고 커서를 보이지 않게 설정
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }

//    void Update()
//    {
//        // 채팅서버에서 오는 응답을 수신하기 위해서 계속 호출 해줘야 한다.        
//        if (chatClient != null)
//        {
//            chatClient.Service();
//        }

//        if (Input.GetKeyDown(KeyCode.F5))
//        {
//            // 채널에서 나가자
//            string[] channels = { currChannel };
//            chatClient.Unsubscribe(channels);
//        }
//        // ESC 키를 눌러 마우스 잠금을 해제
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            Cursor.lockState = CursorLockMode.None;  // 마우스 잠금 해제
//            Cursor.visible = true;  // 마우스 커서 보이게 설정
//        }

//        // 마우스 잠금을 다시 설정하는 예시 (다시 게임에 포커스가 잡혔을 때)
//        if (Input.GetMouseButtonDown(0))  // 예시: 마우스 왼쪽 버튼 클릭 시
//        {
//            Cursor.lockState = CursorLockMode.Locked;  // 마우스 잠금
//            Cursor.visible = false;  // 마우스 커서 숨기기
//        }
//    }

//    void Connect()
//    {
//        // 포톤 설정을 가져오자
//        AppSettings photonSettings = PhotonNetwork.PhotonServerSettings.AppSettings;

//        // 위 설정을 가지고 ChatAppSettings 셋팅
//        ChatAppSettings chatAppSettings = new ChatAppSettings();
//        chatAppSettings.AppIdChat = photonSettings.AppIdChat;
//        chatAppSettings.AppVersion = photonSettings.AppVersion;
//        chatAppSettings.FixedRegion = photonSettings.FixedRegion;
//        chatAppSettings.NetworkLogging = photonSettings.NetworkLogging;
//        chatAppSettings.Protocol = photonSettings.Protocol;
//        chatAppSettings.EnableProtocolFallback = photonSettings.EnableProtocolFallback;
//        chatAppSettings.Server = photonSettings.Server;
//        chatAppSettings.Port = (ushort)photonSettings.Port;
//        chatAppSettings.ProxyServer = photonSettings.ProxyServer;

//        // ChatClinet 만들자.
//        chatClient = new ChatClient(this);
//        // 닉네임
//        chatClient.AuthValues = new Photon.Chat.AuthenticationValues(PhotonNetwork.NickName);
//        // 연결 시도
//        chatClient.ConnectUsingSettings(chatAppSettings);
//    }

//    void OnSubmit(string s)
//    {
//        // 만약에 s 의 길이가 0이면 함수를 나가자.
//        if (s.Length == 0) return;

//        // 귓속말인 판단
//        // /w 아이디 메시지 (/w 김현진 안녕하세요 반갑습니다.)
//        string[] splitChat = s.Split(" ", 3);

//        if (splitChat[0] == "/w")
//        {
//            // 귓속말을 보내자
//            // splitchat[1] : 아이디 , splitChat[2] : 내용
//            chatClient.SendPrivateMessage(splitChat[1], splitChat[2]);
//        }
//        else
//        {
//            // 채팅을 보내자.
//            chatClient.PublishMessage(currChannel, s);
//        }
//        // 채팅 입력란 초기화
//        inputChat.text = "";
//    }

//    void CreateChatItem(string sender, object message, Color color)
//    {
//        // ChatItem 생성 (Content 의 자식으로)
//        GameObject go = Instantiate(chatItemFactory, trContent);
//        // 생성된 게임오브젝트에서 ChatItem 컴포넌트 가져온다.
//        ChatItem1 chatItem = go.GetComponent<ChatItem1>();
//        // 가져온 컴포넌트에서 SetText 함수 실행
//        chatItem.SetText(sender + " : " + message);
//        // TMP_Text 컴포넌트 가져오자
//        TMP_Text text = go.GetComponent<TMP_Text>();
//        // 가져온 컴포넌트를 이용해서 색을 바꾸자
//        text.color = color;
//        // 추가: 스크롤을 항상 아래로 유지시키기 위한 코드
//        Canvas.ForceUpdateCanvases();
//        trContent.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0f;
//    }


//    public void DebugReturn(DebugLevel level, string message)
//    {
//    }

//    public void OnDisconnected()
//    {
//    }

//    // 채팅 서버에 접속이 성공하면 호출되는 함수
//    public void OnConnected()
//    {
//        print("채팅 서버 접속 성공!");
//        // 전체 채널에 들어가자( 구독 )
//        chatClient.Subscribe(currChannel);
//    }

//    public void OnChatStateChange(ChatState state)
//    {
//    }

//    // 특정 채널에 다른 사람(나)이 메시지를 보내고 나한테 응답이 올때 호출 되는 함수
//    public void OnGetMessages(string channelName, string[] senders, object[] messages)
//    {
//        for (int i = 0; i < senders.Length; i++)
//        {
//            print(senders[i] + " : " + messages[i]);

//            CreateChatItem(senders[i], messages[i], Color.white);
//        }
//    }

//    // 누군가 나한테 개인메시지를 보냈을 때
//    public void OnPrivateMessage(string sender, object message, string channelName)
//    {
//        CreateChatItem(sender, message, Color.blue);
//    }

//    // 채팅 채널에 접속이 성공했을 때 들어오는 함수
//    public void OnSubscribed(string[] channels, bool[] results)
//    {
//        for (int i = 0; i < channels.Length; i++)
//        {
//            print(channels[i] + " 채널에 접속이 성공 했습니다");
//        }
//    }

//    // 채팅 채널에서 나갔을 때 들어오는 함수
//    public void OnUnsubscribed(string[] channels)
//    {
//        for (int i = 0; i < channels.Length; i++)
//        {
//            print(channels[i] + " 채널에서 나갔습니다");
//        }
//    }

//    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
//    {
//    }

//    public void OnUserSubscribed(string channel, string user)
//    {
//    }

//    public void OnUserUnsubscribed(string channel, string user)
//    {
//    }
//}


// 기존 채팅
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
//using ColorUtility = UnityEngine.ColorUtility; //있어도 없어도 되는거

public class ChatManager : MonoBehaviourPun
{
    // Input Field
    public TMP_InputField inputChat;

    // chatItem prefab
    public GameObject chatItemFactory;

    // content 의 transform
    public RectTransform trContent;

    // Chatview 의 Transform
    public RectTransform trChatView;

    // 채팅이 추가되기 전의 Content 의 H(높이) 값을 가지고 있는 변수
    float prevContentH;

    // 닉네임 색상
    Color nickNameColor;

    void Start()
    {
        // 닉네임 색상 랜덤하게 설정
        nickNameColor = Random.ColorHSV();
        // inputChat 의 내용이 변경될 때 호출되는 함수 등록
        inputChat.onValueChanged.AddListener(OnvalueChanged);
        // inputChat 의 엔터를 쳤을 때 호출되는 함수 등록
        inputChat.onSubmit.AddListener(OnSubmit);
        // inputChat 포커싱을 잃을 때 호출되는 함수 등록
        inputChat.onEndEdit.AddListener(OnEndEdit);
    }

    // Update is called once per frame
    void Update()
    {
        // 만약에 왼쪽 컨트롤키 누르면
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // 마우스 포인터 활성화
            Cursor.lockState = CursorLockMode.None;
        }

        // 만약에 마우스 왼쪽버튼을 눌렀으면
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 포인터가 활성화 되어있다면
            if (Cursor.lockState == CursorLockMode.None)
            {
                // 만약에 UI가 클릭이 되지 않았다면
                //EventSystem.current.IsPointerOverGameObject()
                if (EventSystem.current.IsPointerOverGameObject() == false) // 모바일은 이러면 안됨
                {
                    // 마우스 포인터를 비활성화
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }


    void OnSubmit(string s)
    {
        // 만약에 s 길이가 0  이면 함수를 나가자
        if (s.Length == 0) return;

        // 채팅 내용을 NickName : 채팅 내용
        // "<color=#ffffff"> 원하는 내용 </color>"
        string nick = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" + PhotonNetwork.NickName + "</color>";
        string chat = nick + " : " + s;

        //string chat = PhotonNetwork.NickName + " : " + s;
        //string chat = "<color=#" + ColorUtility.ToHtmlStringRGB(nickNameColor) + ">" + PhotonNetwork.NickName + "</color> : " + s;

        // AddChat RPC 함수 호출
        photonView.RPC(nameof(AddChat), RpcTarget.All, chat);

        print("엔터 침 : " + chat);

        // 강제로 inputchat을 활성화.
        inputChat.ActivateInputField();

        //AutoScrollBottom();
    }

    // 채팅 추가 함수
    [PunRPC]
    void AddChat(string chat)
    {
        // 새 채팅 추가되기 전 content의 H 값 저장
        prevContentH = trContent.sizeDelta.y;

        // chatItem 하나 만들자 (부모를 chatview 의 content로 하자)
        GameObject go = Instantiate(chatItemFactory, trContent);
        // chatItem 컴포넌트 가져오자.
        ChatItem1 chatItem = go.GetComponent<ChatItem1>();
        // 가져온 컴포넌트의 SetText 함수 실행
        chatItem.SetText(chat);
        // 가져온 컴포넌트의 onAutoScroll 변수에 AutoScrollBottom 을 설정
        chatItem.onAutoScroll = AutoScrollBottom;
        // inputchat 에 있는 내용 초기화
        inputChat.text = "";
    }

    // 채팅 추가 되었을 때 맨밑으로 content 위치를 옮기는 함수
    public void AutoScrollBottom()
    {
        // chatView 의 H 보다 content 의 H 값이 크다면 (스크롤이 가능한 상태라면)
        if (trContent.sizeDelta.y > trChatView.sizeDelta.y)
        {
            //// 이거랑 아래 식이랑 똑같이 다 됨.
            //trChatView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

            // 이전 바닥에 닿아있었다면 // 이거랑 위에 식이랑 똑같이 다 됨.
            if (prevContentH - trChatView.sizeDelta.y <= trContent.anchoredPosition.y)
            {
                // content 의 y 값을 재설정한다.
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trChatView.sizeDelta.y);
            }
        }
    }
    void OnvalueChanged(string s)
    {
        //print("변경 중 : " + s);
    }

    void OnEndEdit(string s)
    {
        print("작성 끝 : " + s);
    }
}
