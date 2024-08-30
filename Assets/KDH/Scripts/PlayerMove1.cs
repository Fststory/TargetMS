using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove1 : MonoBehaviourPun
{
    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // �̵� �ӷ�
    public float moveSpeed = 5;

    // �߷�
    float gravity = -9.81f;
    // y �ӷ�
    float yVelocity;
    // ���� �ʱ� �ӷ�
    public float jumpPower = 3;

    // ī�޶�
    public GameObject cam;

    #region ����
    //void Update()
    //{
    //    // ������ �Է��� �޴´�.
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");

    //    Vector3 velocity = new Vector3(h, 0, v);
    //    velocity.Normalize();

    //    // ���� ȸ���ϸ� ���� �չ��⵵ �ٲ��.
    //    velocity = transform.TransformDirection(velocity);

    //    // p = p0 + vt
    //    transform.position += velocity * moveSpeed * Time.deltaTime;
    //}
    #endregion

    private void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ��������.
        cc = GetComponent<CharacterController>();
        // �� ���� ���� ī�޶� Ȱ��ȭ ����
        cam.SetActive(photonView.IsMine);
        //if (photonView.IsMine)
        //{
        //    cam.SetActive(true);
        //}
    }

    private void Update()
    {
        // �� ���� ���� ��Ʈ�� ����!
        if (photonView.IsMine)
        {
            // 1. Ű���� WWASD Ű �Է��� ����
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            // 2. ������ ������.
            Vector3 dirH = Vector3.right * h;
            Vector3 dirV = Vector3.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // ���࿡ ���� ������ yVelocity �� 0 ���� �ʱ�ȭ
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // ���࿡ Space �ٸ� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity �� jumpPower �� ����
                yVelocity = jumpPower;
            }

            // yVelocity ���� �߷¿� ���ؼ� �����Ű��.
            yVelocity += gravity * Time.deltaTime;

            #region �������� ���� �ƴ� �� (���� �̵�[�߷�]���� �̵��ӵ��� �����ϴ� ��)
            // dir.y �� yVelocity ���� ����
            dir.y = yVelocity;

            // �ڽ��� ������ �������� dir ����
            dir = transform.TransformDirection(dir);

            //dirH = transform.right * h;
            //dirV = transform.forward * v;
            // �� �̷��� ������  dir = transform.TransformDirection(dir); <= �� ������ �ʿ� ����

            // 3. �� �������� ��������.
            //transform.position += dir * moveSpeed * Time.deltaTime;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            #endregion

            #region �������� ���� (���� �̵�[�߷�]���� �̵��ӵ��� �������� �ʴ� �� = ������ �߷¸� ����)
            dir = dir * moveSpeed;
            dir.y = yVelocity;
            cc.Move(dir * Time.deltaTime);
            #endregion
        }
    }
}
