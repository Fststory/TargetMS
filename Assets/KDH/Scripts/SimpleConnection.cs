using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SimpleConnection : MonoBehaviourPunCallbacks
{
    public GameObject readyButton; // 준비 완료 버튼을 위한 UI 오브젝트
    private int readyPlayerCount = 0; // 준비 완료한 플레이어 수를 세는 변수

    void Start()
    {
        // Photon 환경설정을 기반으로 마스터 서버에 접속을 시도
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.AutomaticallySyncScene = true;


        // 버튼의 클릭 이벤트에 메서드 추가
        if (readyButton != null)
        {
            Button button = readyButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(OnReadyButtonClicked);
            }
            else
            {
                Debug.LogError("Ready Button does not have a Button component.");
            }
        }
        else
        {
            Debug.LogError("Ready Button is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Update 메서드는 현재 빈 상태로 유지됨
    }

    // 마스터 서버에 접속이 되면 호출되는 함수
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("마스터 서버에 접속");

        // 로비 접속
        JoinLobby();
    }

    public void JoinLobby()
    {
        // 닉네임 설정
        PhotonNetwork.NickName = "김오뷁";
        // 기본 Lobby 입장
        PhotonNetwork.JoinLobby();
    }

    // 로비에 참여가 성공하면 호출되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("로비 입장 완료");

        JoinOrCreateRoom();
    }

    // Room을 참여하자. 만약에 해당 Room이 없으면 Room 만들겠다.
    public void JoinOrCreateRoom()
    {
        // 방 생성 옵션
        RoomOptions roomOption = new RoomOptions();
        // 방에 들어올 수 있는 최대 인원 설정
        roomOption.MaxPlayers = 20;

        // Room 참여 or 생성
        PhotonNetwork.JoinOrCreateRoom("TargetMS_ROOM", roomOption, TypedLobby.Default);
    }

    // 방 생성 성공했을 때 호출되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("방 생성 완료");
    }

    // 방 입장 성공했을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");
        // PhotonNetwork.LoadLevel("PlayScene"); // 씬 전환

        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        PhotonNetwork.LoadLevel("Test_PlayScene");
        // 방에 접속한 플레이어 수 확인
        //if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        //{
        //    // 준비 완료 버튼을 활성화
        //    readyButton.SetActive(true);
        //}
        //else
        //{
        //    print("다른 플레이어를 기다리는 중...");
        //}
    }

    // 플레이어가 준비 버튼을 눌렀을 때 호출될 함수
    public void OnReadyButtonClicked()
    {
        if (photonView != null)
        {
            readyPlayerCount++; // 준비 완료한 플레이어 수 증가
            photonView.RPC("CheckAllPlayersReady", RpcTarget.All); // 모든 클라이언트에서 확인하도록 호출
        }
        else
        {
            Debug.LogError("PhotonView component is missing.");
        }
    }

    // 모든 플레이어가 준비 되었는지 확인하는 함수
    [PunRPC]
    void CheckAllPlayersReady()
    {
        if (readyPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(WaitAndLoadScene(3f)); // 모든 플레이어가 준비되면 3초 대기 후 씬 전환
            }
        }
    }
    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    base.OnPlayerEnteredRoom(newPlayer);
    //    Debug.Log("New player joined: " + newPlayer.NickName);

    //    // 모든 기존 플레이어에게 새 플레이어 정보 전달
    //    foreach (Player player in PhotonNetwork.PlayerList)
    //    {
    //        if (player != newPlayer)
    //        {
    //            // 기존 플레이어의 정보를 새 플레이어에게 전송
    //            // 예를 들어, 모든 플레이어의 상태를 새 플레이어에게 전송
    //            //PhotonView.RPC("UpdatePlayerList", newPlayer);
    //        }
    //    }
    //}


    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // 대기 시간
        PhotonNetwork.LoadLevel("PlayScene"); // 씬 전환
    }
}



// 기존 2초 뒤에 가려고 했던 거
//using System.Collections;
//using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;

//public class SimpleConnection : MonoBehaviourPunCallbacks
//{
//    void Start()
//    {
//        // Photon 환경설정을 기반으로 마스터 서버에 접속을 시도
//        PhotonNetwork.ConnectUsingSettings();
//    }

//    void Update()
//    {

//    }

//    // 마스터 서버에 접속이 되면 호출되는 함수
//    public override void OnConnectedToMaster()
//    {
//        base.OnConnectedToMaster();
//        print("마스터 서버에 접속");

//        // 로비 접속
//        JoinLobby();
//    }

//    public void JoinLobby()
//    {
//        // 닉네임 설정
//        PhotonNetwork.NickName = "김오뷁";
//        // 기본 Lobby 입장
//        PhotonNetwork.JoinLobby();
//    }

//    // 로비에 참여가 성공하면 호출되는 함수
//    public override void OnJoinedLobby()
//    {
//        base.OnJoinedLobby();
//        print("로비 입장 완료");

//        JoinOrCreateRoom();
//    }

//    // Room 을 참여하자. 만약에 해당 Room 이 없으면 Room 만들겠다.
//    public void JoinOrCreateRoom()
//    {
//        // 방 생성 옵션
//        RoomOptions roomOption = new RoomOptions();
//        // 방에 들어 올 수 있는 최대 인원 설정
//        roomOption.MaxPlayers = 20;
//        // 로비에 방을 보이게 할 것이니? -- 기본 true라서 생략 가능
//        roomOption.IsVisible = true;
//        // 방에 참여를 할 수 있냐?? -- 기본 true라서 생략 가능
//        roomOption.IsOpen = true;

//        // Room 참여 or 생성
//        PhotonNetwork.JoinOrCreateRoom("TargetMS_ROOM", roomOption, TypedLobby.Default);
//    }

//    // 방 생성 성공 했을 때 호출되는 함수
//    public override void OnCreatedRoom()
//    {
//        base.OnCreatedRoom();
//        print("방 생성 완료");
//    }

//    // 방 입장 성공 했을 때 호출되는 함수
//    public override void OnJoinedRoom()
//    {
//        base.OnJoinedRoom();
//        print("방 입장 완료");

//        // 방에 접속한 플레이어 수 확인
//        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
//        {
//            // 최소 2명 이상의 플레이어가 접속했을 때 씬을 변경
//            StartCoroutine(WaitAndLoadScene(3f)); // 3초 대기 후 씬 전환
//        }
//        else
//        {
//            print("다른 플레이어를 기다리는 중...");
//        }
//    }

//    private IEnumerator WaitAndLoadScene(float waitTime)
//    {
//        yield return new WaitForSeconds(waitTime); // 대기 시간
//        PhotonNetwork.LoadLevel("PlayScene"); // 씬 전환
//    }
//}
