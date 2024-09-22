﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRot : MonoBehaviour
{
    // 회전 속력
    public float rotSpeed = 200;

    // 회전 값
    float rotX;
    float rotY;


    // 회전 가능 여부
    public bool useRotX;
    public bool useRotY;

    // PhotonView
    public PhotonView pv;

    public Transform Cam;

    void Start()
    {

    }

    void Update()
    {
        // 만약에 내 것이라면
        if (pv.IsMine)
        {
            // 마우스의 lockmode가 none 이면 (마우스 포인터 활성화 되어있다면) 함수 나가자
            if (Cursor.lockState == CursorLockMode.None)

                return;

            // 1. 마우스의 움직임을 받아오자.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            // 2, 회전 값을 변경(누적)
            if (useRotX) rotX += my * rotSpeed * Time.deltaTime;
            if (useRotY) rotY += mx * rotSpeed * Time.deltaTime;

            // rotX 의 값을 제한(최소값, 최대값)
            rotX = Mathf.Clamp(rotX, -80, 80);

            // 3. 구해진 회전 값을 나의 회전값으로 세팅
            Cam.localEulerAngles = new Vector3(-rotX, 0, 0);
            transform.localEulerAngles = new Vector3(0, rotY, 0);
        }
    }
}
