using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_PlayerMove : MonoBehaviour
{

    CharacterController cc;

    float x;
    float v;
    public float moveSpeed;
    public float yVelocity; // 중력가속도
    public float jumpPower; // 점프높이
    float gravity = -9.8f;
    public int jumpCount;
    public int maxjumpCount = 1;
    public Transform lookPos; // 캐릭터가 바라볼 방향





    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        x = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(x, 0, v);

        moveVector = lookPos.TransformDirection(moveVector);

        moveVector.Normalize();

       

        // 중력
        yVelocity += gravity * Time.deltaTime;

        // 바닥에 있으면 중력가속도는 0
        if(cc.isGrounded)
        {
            yVelocity = 0;
            jumpCount = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < maxjumpCount)
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        moveVector.y = yVelocity;

        cc.Move(moveVector * moveSpeed * Time.deltaTime);

    }
    

}


    