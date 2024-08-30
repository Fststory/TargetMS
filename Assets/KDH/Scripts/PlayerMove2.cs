using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Photon.Pun;
using Unity.Mathematics;

public class PlayerMove2 : MonoBehaviourPun, IPunObservable
{
    // �̵� �ӷ�
    public float moveSpeed = 5f;

    // �߷�
    float gravity = -9.81f;
    // y �ӷ�
    float yVelocity;

    // ���� �ʱ� �ӷ�
    public float jumpPower = 3;

    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // ī�޶�
    public GameObject cam;

    // �������� �Ѿ���� ��ġ��
    Vector3 receivePos;
    // �������� �Ѿ���� ȸ����
    quaternion receiveRot;

    // ���� �ӷ�
    public float lerpSpeed = 50;

    // ani
    Animator anim;

    // AD Ű�� �Է� ���� ����
    float h;
    // WS Ű�� �Է� ���� ����
    float v;

    // LookPos
    public Transform lookPos;

    void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ��������
        cc = GetComponent<CharacterController>();

        // �� ���� ���� ī�޶� Ȱ��ȭ
        cam.SetActive(photonView.IsMine);
        //if (photonView.IsMine)
        //{
        //    cam.SetActive(true);
        //}

        if (photonView.IsMine)
        {
            // ���콺 ��ױ�
            Cursor.lockState = CursorLockMode.Locked;
        }
        //�ִϸ����� ��������
        anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        // �� ���� ���� ��Ʋ�� ����!
        if (photonView.IsMine)
        {
            // 1. Ű���� WASD Ű �Է��� ����.
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // 2. ������ ������.
            //Vector3 dir = transform.TransformDirection(new Vector3(h, 0, v).normalized);

            //Vector3 dir = new Vector3(h, 0, v);

            //Vector3 dirH = Vector3.right * h;
            //Vector3 dirV = Vector3.forward * v;
            //Vector3 dir = dirH + dirV;
            //dir.Normalize();

            // �ڽ��� ������ �������� dir ���� 1 (���� ���� �÷��̾� ȸ�� ������� ���� �������� �յڷθ� ������)
            //dir = transform.TransformDirection(dir);

            // �ڽ��� ������ �������� dir ���� 2
            Vector3 dirH = transform.right * h;
            Vector3 dirV = transform.forward * v;
            Vector3 dir = dirH + dirV;
            dir.Normalize();

            // ���࿡ ���� ������ yVelocity  �� 0 ���� �ʱ�ȭ
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

            #region �������� ���� �ƴѰ�
            // dir.y �� yVelocity ���� ����
            dir.y = yVelocity;

            // 3. �� �������� ��������.
            //transform.position += (dir * moveSpeed * Time.deltaTime);
            //transform.position += dir * 5 * Time.deltaTime;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            #endregion

            #region �������� ����
            //dir = dir * moveSpeed;
            //dir.y = yVelocity;
            //cc.Move(dir * Time.deltaTime);
            #endregion

            //// anim �� �̿��ؼ� h, v ���� ���� -- �ƴҶ��� ���� �ϵ��� ������ �̵�
            //anim.SetFloat("DirH", h);
            //anim.SetFloat("DirV", v);
        }
        // ���� Player �ƴ϶��
        else
        {
            // ��ġ ����
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
            // ȸ�� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
        }
        // anim �� �̿��ؼ� h, v ���� ����
        anim.SetFloat("DirH", h);
        anim.SetFloat("DirV", v);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���࿡ ���� �����͸� ���� �� �ִ� ���¶��(�� ���̶��)
        if (stream.IsWriting)
        {
            // ���� ��ġ ���� ������
            stream.SendNext(transform.position);
            // ���� ȸ������ ������
            stream.SendNext(transform.rotation);
            // ���� h ��
            stream.SendNext(h);
            // ���� v ��
            stream.SendNext(v);
            // LookPos �� ��ġ���� ������.
            stream.SendNext(lookPos.position);

        }
        // �����͸� ���� �� �ִ� ���¶�� (�� ���� �ƴ϶��)
        else if (stream.IsReading)
        {
            // ��ġ ���� ����. ���� �߿���. ��ġ ���� �������� ��ġ ����
            //transform.position = (Vector3)stream.ReceiveNext();
            receivePos = (Vector3)stream.ReceiveNext();

            // ȸ�� ���� ����.
            //transform.rotation = (quaternion)stream.ReceiveNext();
            receiveRot = (quaternion)stream.ReceiveNext();
            // �������� ���� �Ǵ� h �� ����
            h = (float)stream.ReceiveNext();
            // �������� ���� �Ǵ� v �� ����
            v = (float)stream.ReceiveNext();
            // LookPos �� ��ġ���� ����.
            lookPos.position = (Vector3)stream.ReceiveNext();
        }
    }
}
