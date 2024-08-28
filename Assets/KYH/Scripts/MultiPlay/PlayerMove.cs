using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    // 캐릭터 컨트롤러
    CharacterController cc;

    // 이동 속력
    public float moveSpeed = 5;

    // 중력
    float gravity = -9.81f;
    // y 속력
    float yVelocity;
    // 점프 초기 속력
    public float jumpPower = 3;

    // 카메라
    public GameObject cam;

    #region 내꺼
    //void Update()
    //{
    //    // 유저의 입력을 받는다.
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");

    //    Vector3 velocity = new Vector3(h, 0, v);
    //    velocity.Normalize();

    //    // 내가 회전하면 나의 앞방향도 바뀐다.
    //    velocity = transform.TransformDirection(velocity);

    //    // p = p0 + vt
    //    transform.position += velocity * moveSpeed * Time.deltaTime;
    //}
    #endregion

    private void Start()
    {
        // 캐릭터 컨트롤러 가져오자.
        cc = GetComponent<CharacterController>();
        // 내 것일 때만 카메라를 활성화 하자
        cam.SetActive(photonView.IsMine);
        //if (photonView.IsMine)
        //{
        //    cam.SetActive(true);
        //}
    }

    private void Update()
    {
        // 내 것일 때만 컨트롤 하자!
        if (photonView.IsMine)
        {
            // 1. 키보드 WWASD 키 입력을 받자
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            // 2. 방향을 정하자.
            Vector3 dirH = Vector3.right * h;
            Vector3 dirV = Vector3.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // 만약에 땅에 있으면 yVelocity 를 0 으로 초기화
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // 만약에 Space 바를 누르면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity 를 jumpPower 로 설정
                yVelocity = jumpPower;
            }

            // yVelocity 값을 중력에 의해서 변경시키자.
            yVelocity += gravity * Time.deltaTime;

            #region 물리적인 점프 아닌 것 (상하 이동[중력]에도 이동속도가 개입하는 것)
            // dir.y 에 yVelocity 값을 세팅
            dir.y = yVelocity;

            // 자신의 방향을 기준으로 dir 변경
            dir = transform.TransformDirection(dir);

            //dirH = transform.right * h;
            //dirV = transform.forward * v;
            // ㄴ 이렇게 했으면  dir = transform.TransformDirection(dir); <= 이 과정이 필요 없음

            // 3. 그 방향으로 움직이자.
            //transform.position += dir * moveSpeed * Time.deltaTime;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            #endregion

            #region 물리적인 점프 (상하 이동[중력]에는 이동속도가 개입하지 않는 것 = 오로지 중력만 적용)
            dir = dir * moveSpeed;
            dir.y = yVelocity;
            cc.Move(dir * Time.deltaTime);
            #endregion
        }
    }
}
