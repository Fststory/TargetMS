//// 마이크 영역에서는 처음에는 움직이고 싶어요
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using TMPro;
//using System;

//public class PlayerMove2 : MonoBehaviourPun, IPunObservable
//{
//    // 캐릭터 컨트롤러
//    CharacterController cc;

//    // 이동 속력
//    public float moveSpeed = 6f;

//    // 중력
//    float gravity = -9.81f;
//    // y 속력
//    float yVelocity;

//    // 점프 초기 속력
//    public float jumpPower = 3;

//    // 카메라
//    public GameObject cam;

//    // 서버에서 넘어오는 위치값
//    Vector3 receivePos;
//    // 서버에서 넘어오는 회전값
//    Quaternion receiveRot; // quaternion -> Quaternion으로 변경

//    // 보정 속력
//    public float lerpSpeed = 50;

//    // 애니메이터
//    Animator anim;

//    // AD 키를 입력 받을 변수
//    float h;
//    // WS 키를 입력 받을 변수
//    float v;

//    // 이동 제어 변수
//    private bool canMove = true; // 플레이어가 이동할 수 있는지 여부

//    // 닉네임 UI
//    public TMP_Text nickName;

//    // MicArea 스크립트 참조
//    private MicArea micArea;

//    void Start()
//    {
//        // 캐릭터 컨트롤러 가져오기
//        cc = GetComponent<CharacterController>();

//        // 내 것일 때만 카메라를 활성화
//        cam.SetActive(photonView.IsMine);

//        if (photonView.IsMine)
//        {
//            // 마우스 잠그기
//            //Cursor.lockState = CursorLockMode.Locked;
//        }

//        // 애니메이터 가져오기
//        anim = GetComponentInChildren<Animator>();

//        // 닉네임 Ui에 해당 캐릭터의 주인의 닉네임 설정
//        nickName.text = photonView.Owner.NickName;

//        // MicArea 스크립트 가져오기
//        micArea = FindObjectOfType<MicArea>();
//    }

//    void Update()
//    {
//        // 내 플레이어인지 확인
//        if (photonView.IsMine)
//        {
//            // ESC 키를 누르면 이동 허용
//            if (!canMove && Input.GetKeyDown(KeyCode.Escape))
//            {
//                canMove = true;
//            }

//            // 이동이 가능한 상태에서만 움직임 처리
//            if (canMove)
//            {
//                // 마우스의 lockmode가 none 이면 (마우스 포인터 활성화 되어있다면) 함수 나가자
//                if (Cursor.lockState == CursorLockMode.None)
//                {
//                    return;
//                }

//                // 1. 키보드 입력 (WASD) 값을 받음
//                h = Input.GetAxis("Horizontal");
//                v = Input.GetAxis("Vertical");

//                // 2. 이동 방향을 계산
//                Vector3 dirH = transform.right * h;  // 좌우 방향
//                Vector3 dirV = transform.forward * v;  // 전후 방향
//                Vector3 dir = dirH + dirV;  // 최종 이동 방향
//                dir.Normalize();  // 방향 벡터를 정규화

//                // 3. 땅에 닿아있는지 확인하여 yVelocity를 초기화
//                if (cc.isGrounded)
//                {
//                    yVelocity = 0;
//                }

//                // 5. 중력 적용
//                yVelocity += gravity * Time.deltaTime;  // 중력에 의해 yVelocity가 감소

//                // 6. 이동 방향에 yVelocity를 포함
//                dir.y = yVelocity;

//                // 7. 캐릭터 컨트롤러를 통해 캐릭터를 이동
//                cc.Move(dir * moveSpeed * Time.deltaTime);

//                // 애니메이터 값을 업데이트 (애니메이션 재생을 위해)
//                anim.SetFloat("DirH", h);  // 좌우 움직임
//                anim.SetFloat("DirV", v);  // 전후 움직임
//            }
//        }
//        else
//        {
//            // 다른 플레이어의 위치 보정
//            if (receivePos != Vector3.zero)
//            {
//                transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
//            }

//            // 다른 플레이어의 회전 보정
//            if (receiveRot != Quaternion.identity)
//            {
//                transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
//            }
//        }
//    }

//    // 충돌 감지 함수
//    private void OnTriggerEnter(Collider other)
//    {
//        // 특정 태그 또는 오브젝트와의 충돌을 감지하여 이동을 제한
//        if (other.CompareTag("AREA"))
//        {
//            if (micArea != null && micArea.areaCount != 4 && other.gameObject.name == "Area_monitor")
//            {
//                // "Area_monitor"와 충돌 시 이동 제한
//                canMove = false;  // 이동 제한
//                if (photonView.IsMine)
//                {
//                    // 마우스 잠그기 해제
//                    Cursor.lockState = CursorLockMode.None;
//                }
//            }
//        }
//    }

//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            // 내 위치 값을 보낸다
//            stream.SendNext(transform.position);
//            // 내 회전값을 보낸다
//            stream.SendNext(transform.rotation);
//            // 내 h 값
//            stream.SendNext(h);
//            // 내 v 값
//            stream.SendNext(v);
//        }
//        else if (stream.IsReading)
//        {
//            // 위치 값을 받자
//            receivePos = (Vector3)stream.ReceiveNext();
//            // 회전 값을 받자
//            receiveRot = (Quaternion)stream.ReceiveNext();
//            // 서버에서 전달되는 h 값 받자
//            h = (float)stream.ReceiveNext();
//            // 서버에서 전달되는 v 값 받자
//            v = (float)stream.ReceiveNext();
//        }
//    }

//    internal void SetCollisionRestriction(bool v)
//    {
//        throw new NotImplementedException();
//    }
//}


// 영역 위에선 움직이지 마 + 마우스 자동 생성
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMove2 : MonoBehaviourPun, IPunObservable
{
    // 캐릭터 컨트롤러
    CharacterController cc;

    // 이동 속력
    public float moveSpeed = 6f;

    // 중력
    float gravity = -9.81f;
    // y 속력
    float yVelocity;

    // 점프 초기 속력
    public float jumpPower = 3;

    // 카메라
    public GameObject cam;

    // 서버에서 넘어오는 위치값
    Vector3 receivePos;
    // 서버에서 넘어오는 회전값
    Quaternion receiveRot; // quaternion -> Quaternion으로 변경

    // 보정 속력
    public float lerpSpeed = 50;

    // 애니메이터
    Animator anim;

    // AD 키를 입력 받을 변수
    float h;
    // WS 키를 입력 받을 변수
    float v;

    // 이동 제어 변수
    private bool canMove = true; // 플레이어가 이동할 수 있는지 여부

    // 닉네임 UI
    public TMP_Text nickName;

    // LookPos
    //public Transform lookPos;

    //[PunRPC]
    //void RpcAddPlayer(int order)
    //{
    //    // GameManger 에게 photonView 를 넘겨주자
    //    GM.instance.AddPlayer(photonView, order);
    //}

    void Start()
    {
        // 캐릭터 컨트롤러 가져오기
        cc = GetComponent<CharacterController>();

        // 내 것일 때만 카메라를 활성화
        cam.SetActive(photonView.IsMine);

        if (photonView.IsMine)
        {
            // 마우스 잠그기
            //Cursor.lockState = CursorLockMode.Locked;
        }

        // 애니메이터 가져오기
        anim = GetComponentInChildren<Animator>();

        // 닉네임 Ui에 해당캐릭터의 주인의 닉네임 설정
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // 내 플레이어인지 확인
        if (photonView.IsMine)
        {
            // ESC 키를 누르면 이동 허용
            if (!canMove && Input.GetKeyDown(KeyCode.Escape))
            {
                canMove = true;
            }

            // 이동이 가능한 상태에서만 움직임 처리
            if (canMove)
            {
                // 마우스의 lockmode가 none 이면 (마우스 포인터 활성화 되어있다면) 함수 나가자
                if (Cursor.lockState == CursorLockMode.None)
                {
                    return;
                }

                // 1. 키보드 입력 (WASD) 값을 받음
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");

                // 2. 이동 방향을 계산
                Vector3 dirH = transform.right * h;  // 좌우 방향
                Vector3 dirV = transform.forward * v;  // 전후 방향
                Vector3 dir = dirH + dirV;  // 최종 이동 방향
                dir.Normalize();  // 방향 벡터를 정규화

                // 3. 땅에 닿아있는지 확인하여 yVelocity를 초기화
                if (cc.isGrounded)
                {
                    yVelocity = 0;
                }

                // 4. 점프 입력 처리
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    yVelocity = jumpPower;  // 점프할 때 yVelocity를 점프 파워로 설정
                //}

                // 5. 중력 적용
                yVelocity += gravity * Time.deltaTime;  // 중력에 의해 yVelocity가 감소

                // 6. 이동 방향에 yVelocity를 포함
                dir.y = yVelocity;

                // 7. 캐릭터 컨트롤러를 통해 캐릭터를 이동
                cc.Move(dir * moveSpeed * Time.deltaTime);

                //// 8. 애니메이터 값을 업데이트 (애니메이션 재생을 위해)
                //if (anim != null)
                //{
                //    anim.SetFloat("DirH", h);  // 좌우 움직임
                //    anim.SetFloat("DirV", v);  // 전후 움직임
                //}
            }
        }
        else
        {
            // 다른 플레이어의 위치 보정
            if (receivePos != Vector3.zero)  // 받은 위치가 기본값(Vector3.zero)이 아닌 경우
            {
                transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
            }

            // 다른 플레이어의 회전 보정
            if (receiveRot != Quaternion.identity)  // 받은 회전이 기본값(Quaternion.identity)이 아닌 경우
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
            }
        }
        // anim 을 이용해서 h, v 값을 전달
        anim.SetFloat("DirH", h);
        anim.SetFloat("DirV", v);
    }

    // 충돌 감지 함수
    private void OnTriggerEnter(Collider other)
    {
        // 특정 태그 또는 오브젝트와의 충돌을 감지하여 이동을 제한
        if (other.CompareTag("AREA"))  // 충돌한 오브젝트의 태그가 "AREA"일 경우
        {
            canMove = false;  // 이동 제한
            if (photonView.IsMine)
            {
                // 마우스 잠그기
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 내 위치 값을 보낸다
            stream.SendNext(transform.position);
            // 내 회전값을 보낸다
            stream.SendNext(transform.rotation);
            // 내 h 값
            stream.SendNext(h);
            // 내 v 값
            stream.SendNext(v);
            // LookPos의 위치값을 보낸다
            //stream.SendNext(lookPos.position);
        }
        else if (stream.IsReading)
        {
            // 위치 값을 받자
            receivePos = (Vector3)stream.ReceiveNext();
            // 회전 값을 받자
            receiveRot = (Quaternion)stream.ReceiveNext(); // quaternion -> Quaternion으로 변경
            // 서버에서 전달되는 h 값 받자
            h = (float)stream.ReceiveNext();
            // 서버에서 전달되는 v 값 받자
            v = (float)stream.ReceiveNext();
            // LookPos의 위치값을 받자
            //lookPos.position = (Vector3)stream.ReceiveNext();
        }
    }
}

// 점프 금지 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;

//public class PlayerMove2 : MonoBehaviourPun, IPunObservable
//{
//    // 캐릭터 컨트롤러
//    CharacterController cc;

//    // 이동 속력
//    public float moveSpeed = 6f;

//    // 중력
//    float gravity = -9.81f;
//    // y 속력
//    float yVelocity;

//    // 점프 초기 속력
//    public float jumpPower = 3;

//    // 카메라
//    public GameObject cam;

//    // 서버에서 넘어오는 위치값
//    Vector3 receivePos;
//    // 서버에서 넘어오는 회전값
//    Quaternion receiveRot; // quaternion -> Quaternion으로 변경

//    // 보정 속력
//    public float lerpSpeed = 50;

//    // 애니메이터
//    Animator anim;

//    // AD 키를 입력 받을 변수
//    float h;
//    // WS 키를 입력 받을 변수
//    float v;

//    // LookPos
//    //public Transform lookPos;

//    //[PunRPC]
//    //void RpcAddPlayer(int order)
//    //{
//    //    // GameManger 에게 photonView 를 넘겨주자
//    //    GM.instance.AddPlayer(photonView, order);
//    //}

//    void Start()
//    {
//        // 캐릭터 컨트롤러 가져오기
//        cc = GetComponent<CharacterController>();

//        // 내 것일 때만 카메라를 활성화
//        cam.SetActive(photonView.IsMine);

//        if (photonView.IsMine)
//        {
//            // 마우스 잠그기
//            //Cursor.lockState = CursorLockMode.Locked;
//        }

//        // 애니메이터 가져오기
//        anim = GetComponentInChildren<Animator>();
//    }

//    void Update()
//    {
//        // 내 플레이어인지 확인
//        if (photonView.IsMine)
//        {
//            // 마우스의 lockmode가 none 이면 (마우스 포인터 활성화 되어있다면) 함수 나가자
//            if (Cursor.lockState == CursorLockMode.None)
//            {
//                return;
//            }

//            // 1. 키보드 입력 (WASD) 값을 받음
//            h = Input.GetAxis("Horizontal");
//            v = Input.GetAxis("Vertical");

//            // 2. 이동 방향을 계산
//            Vector3 dirH = transform.right * h;  // 좌우 방향
//            Vector3 dirV = transform.forward * v;  // 전후 방향
//            Vector3 dir = dirH + dirV;  // 최종 이동 방향
//            dir.Normalize();  // 방향 벡터를 정규화

//            // 3. 땅에 닿아있는지 확인하여 yVelocity를 초기화
//            if (cc.isGrounded)
//            {
//                yVelocity = 0;
//            }

//            // 4. 점프 입력 처리
//            //if (Input.GetKeyDown(KeyCode.Space))
//            //{
//            //    yVelocity = jumpPower;  // 점프할 때 yVelocity를 점프 파워로 설정
//            //}

//            // 5. 중력 적용
//            yVelocity += gravity * Time.deltaTime;  // 중력에 의해 yVelocity가 감소

//            // 6. 이동 방향에 yVelocity를 포함
//            dir.y = yVelocity;

//            // 7. 캐릭터 컨트롤러를 통해 캐릭터를 이동
//            cc.Move(dir * moveSpeed * Time.deltaTime);

//            //// 8. 애니메이터 값을 업데이트 (애니메이션 재생을 위해)
//            //if (anim != null)
//            //{
//            //    anim.SetFloat("DirH", h);  // 좌우 움직임
//            //    anim.SetFloat("DirV", v);  // 전후 움직임
//            //}
//        }
//        else
//        {
//            // 다른 플레이어의 위치 보정
//            if (receivePos != Vector3.zero)  // 받은 위치가 기본값(Vector3.zero)이 아닌 경우
//            {
//                transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
//            }

//            // 다른 플레이어의 회전 보정
//            if (receiveRot != Quaternion.identity)  // 받은 회전이 기본값(Quaternion.identity)이 아닌 경우
//            {
//                transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
//            }
//        }
//        // anim 을 이용해서 h, v 값을 전달
//        anim.SetFloat("DirH", h);
//        anim.SetFloat("DirV", v);
//    }



//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            // 내 위치 값을 보낸다
//            stream.SendNext(transform.position);
//            // 내 회전값을 보낸다
//            stream.SendNext(transform.rotation);
//            // 내 h 값
//            stream.SendNext(h);
//            // 내 v 값
//            stream.SendNext(v);
//            // LookPos의 위치값을 보낸다
//            //stream.SendNext(lookPos.position);
//        }
//        else if (stream.IsReading)
//        {
//            // 위치 값을 받자
//            receivePos = (Vector3)stream.ReceiveNext();
//            // 회전 값을 받자
//            receiveRot = (Quaternion)stream.ReceiveNext(); // quaternion -> Quaternion으로 변경
//            // 서버에서 전달되는 h 값 받자
//            h = (float)stream.ReceiveNext();
//            // 서버에서 전달되는 v 값 받자
//            v = (float)stream.ReceiveNext();
//            // LookPos의 위치값을 받자
//            //lookPos.position = (Vector3)stream.ReceiveNext();
//        }
//    }
//}

// 기존ㅇ
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;

//public class PlayerMove2 : MonoBehaviourPun, IPunObservable
//{
//    // 이동 속력
//    public float moveSpeed = 5f;

//    // 중력
//    float gravity = -9.81f;
//    // y 속력
//    float yVelocity;

//    // 점프 초기 속력
//    public float jumpPower = 3;

//    // 캐릭터 컨트롤러
//    CharacterController cc;

//    // 카메라
//    public GameObject cam;

//    // 서버에서 넘어오는 위치값
//    Vector3 receivePos;
//    // 서버에서 넘어오는 회전값
//    Quaternion receiveRot; // quaternion -> Quaternion으로 변경

//    // 보정 속력
//    public float lerpSpeed = 50;

//    // 애니메이터
//    Animator anim;

//    // AD 키를 입력 받을 변수
//    float h;
//    // WS 키를 입력 받을 변수
//    float v;

//    // LookPos
//    //public Transform lookPos;

//    void Start()
//    {
//        // 캐릭터 컨트롤러 가져오기
//        cc = GetComponent<CharacterController>();

//        // 내 것일 때만 카메라를 활성화
//        cam.SetActive(photonView.IsMine);

//        if (photonView.IsMine)
//        {
//            // 마우스 잠그기
//            //Cursor.lockState = CursorLockMode.Locked;
//        }

//        // 애니메이터 가져오기
//        anim = GetComponentInChildren<Animator>();
//    }

//    void Update()
//    {
//        // 내 것일 때만 컨트롤 하자!
//        if (photonView.IsMine)
//        {
//            // 1. 키보드 WASD 키 입력을 받자.
//            h = Input.GetAxis("Horizontal");
//            v = Input.GetAxis("Vertical");

//            // 2. 방향을 정하자.
//            Vector3 dirH = transform.right * h;
//            Vector3 dirV = transform.forward * v;
//            Vector3 dir = dirH + dirV;
//            dir.Normalize();

//            // 만약에 땅에 있으면 yVelocity를 0으로 초기화
//            if (cc.isGrounded)
//            {
//                yVelocity = 0;
//            }

//            // 만약에 Space 바를 누르면
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                // yVelocity를 jumpPower로 설정
//                yVelocity = jumpPower;
//            }

//            // yVelocity 값을 중력에 의해서 변경시키자.
//            yVelocity += gravity * Time.deltaTime;

//            // dir.y에 yVelocity 값을 셋팅
//            dir.y = yVelocity;

//            // 3. 그 방향으로 움직이자.
//            cc.Move(dir * moveSpeed * Time.deltaTime);

//            // 애니메이터 값을 업데이트
//            if (anim != null) // 애니메이터가 null인지 확인
//            {
//                anim.SetFloat("DirH", h);
//                anim.SetFloat("DirV", v);
//            }
//        }
//        else
//        {
//            // 위치 보정
//            if (receivePos != null) // receivePos가 null인지 확인
//            {
//                transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
//            }
//            // 회전 보정
//            if (receiveRot != null) // receiveRot이 null인지 확인
//            {
//                transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
//            }
//        }
//    }


//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        if (stream.IsWriting)
//        {
//            // 내 위치 값을 보낸다
//            stream.SendNext(transform.position);
//            // 내 회전값을 보낸다
//            stream.SendNext(transform.rotation);
//            // 내 h 값
//            stream.SendNext(h);
//            // 내 v 값
//            stream.SendNext(v);
//            // LookPos의 위치값을 보낸다
//            //stream.SendNext(lookPos.position);
//        }
//        else if (stream.IsReading)
//        {
//            // 위치 값을 받자
//            receivePos = (Vector3)stream.ReceiveNext();
//            // 회전 값을 받자
//            receiveRot = (Quaternion)stream.ReceiveNext(); // quaternion -> Quaternion으로 변경
//            // 서버에서 전달되는 h 값 받자
//            h = (float)stream.ReceiveNext();
//            // 서버에서 전달되는 v 값 받자
//            v = (float)stream.ReceiveNext();
//            // LookPos의 위치값을 받자
//            //lookPos.position = (Vector3)stream.ReceiveNext();
//        }
//    }
//}

//// 기존
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Rendering.Universal;
//using Photon.Pun;
//using Unity.Mathematics;

//public class PlayerMove2 : MonoBehaviourPun, IPunObservable
//{
//    // 이동 속력
//    public float moveSpeed = 5f;

//    // 중력
//    float gravity = -9.81f;
//    // y 속력
//    float yVelocity;

//    // 점프 초기 속력
//    public float jumpPower = 3;

//    // 캐릭터 컨트롤러
//    CharacterController cc;

//    // 카메라
//    public GameObject cam;

//    // 서버에서 넘어오는 위치값
//    Vector3 receivePos;
//    // 서버에서 넘어오는 회전값
//    Quaternion receiveRot;

//    // 보정 속력
//    public float lerpSpeed = 50;

//    // ani
//    Animator anim;

//    // AD 키를 입력 받을 변수
//    float h;
//    // WS 키를 입력 받을 변수
//    float v;

//    // LookPos
//    //public Transform lookPos;

//    void Start()
//    {
//        // 캐릭터 컨트롤러 가져오자
//        cc = GetComponent<CharacterController>();

//        // 내 것일 때만 카메라를 활성화
//        cam.SetActive(photonView.IsMine);
//        //if (photonView.IsMine)
//        //{
//        //    cam.SetActive(true);
//        //}

//        if (photonView.IsMine)
//        {
//            // 마우스 잠그기
//            //Cursor.lockState = CursorLockMode.Locked;
//        }
//        //애니메이터 가져오기
//        anim = GetComponentInChildren<Animator>();

//    }

//    void Update()
//    {
//        // 내 것일 때만 컨틀롤 하자!
//        if (photonView.IsMine)
//        {
//            // 1. 키보드 WASD 키 입력을 받자.
//            h = Input.GetAxis("Horizontal");
//            v = Input.GetAxis("Vertical");


//            // 2. 방향을 정하자.
//            //Vector3 dir = transform.TransformDirection(new Vector3(h, 0, v).normalized);

//            //Vector3 dir = new Vector3(h, 0, v);

//            //Vector3 dirH = Vector3.right * h;
//            //Vector3 dirV = Vector3.forward * v;
//            //Vector3 dir = dirH + dirV;
//            //dir.Normalize();

//            // 자신의 방향을 기준으로 dir 변경 1 (기존 식은 플레이어 회전 관계없이 월드 기준으로 앞뒤로만 움직임)
//            //dir = transform.TransformDirection(dir);

//            // 자신의 방향을 기준으로 dir 변경 2
//            Vector3 dirH = transform.right * h;
//            Vector3 dirV = transform.forward * v;
//            Vector3 dir = dirH + dirV;
//            dir.Normalize();

//            // 만약에 땅에 있으면 yVelocity  를 0 으로 초기화
//            if (cc.isGrounded)
//            {
//                yVelocity = 0;
//            }

//            // 만약에 Space 바를 누르면
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                // yVelocity 를 jumpPower 로 설정
//                yVelocity = jumpPower;
//            }

//            // yVelocity 값을 중력에 의해서 변경시키자.
//            yVelocity += gravity * Time.deltaTime;

//            #region 물리적인 점프 아닌것
//            // dir.y 에 yVelocity 값을 셋팅
//            dir.y = yVelocity;

//            // 3. 그 방향으로 움직이자.
//            //transform.position += (dir * moveSpeed * Time.deltaTime);
//            //transform.position += dir * 5 * Time.deltaTime;
//            cc.Move(dir * moveSpeed * Time.deltaTime);
//            #endregion

//            #region 물리적인 점프
//            //dir = dir * moveSpeed;
//            //dir.y = yVelocity;
//            //cc.Move(dir * Time.deltaTime);
//            #endregion

//            //// anim 을 이용해서 h, v 값을 전달 -- 아닐때도 적용 하도록 밖으로 이동
//            //anim.SetFloat("DirH", h);
//            //anim.SetFloat("DirV", v);
//        }
//        // 나의 Player 아니라면
//        else
//        {
//            // 위치 보정
//            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
//            // 회전 보정
//            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
//        }
//        // anim 을 이용해서 h, v 값을 전달
//        //anim.SetFloat("DirH", h);
//        //anim.SetFloat("DirV", v);
//    }

//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//        // 만약에 내가 데이터를 보낼 수 있는 상태라면(내 것이라면)
//        if (stream.IsWriting)
//        {
//            // 나의 위치 값을 보낸다
//            stream.SendNext(transform.position);
//            // 나의 회전값을 보낸다
//            stream.SendNext(transform.rotation);
//            // 나의 h 값
//            stream.SendNext(h);
//            // 나의 v 값
//            stream.SendNext(v);
//            // LookPos 의 위치값을 보낸다.
//            //stream.SendNext(lookPos.position);

//        }
//        // 데이터를 받을 수 있는 상태라면 (내 것이 아니라면)
//        else if (stream.IsReading)
//        {
//            // 위치 값을 받자. 순서 중요함. 위치 먼저 보냈으면 위치 먼저
//            //transform.position = (Vector3)stream.ReceiveNext();
//            receivePos = (Vector3)stream.ReceiveNext();

//            // 회전 값을 받자.
//            //transform.rotation = (quaternion)stream.ReceiveNext();
//            receiveRot = (quaternion)stream.ReceiveNext();
//            // 서버에서 전달 되는 h 값 받자
//            h = (float)stream.ReceiveNext();
//            // 서버에서 전달 되는 v 값 받자
//            v = (float)stream.ReceiveNext();
//            // LookPos 의 위치값을 받자.
//            //lookPos.position = (Vector3)stream.ReceiveNext();
//        }
//    }
//}
