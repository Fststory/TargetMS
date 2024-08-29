using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Start_Manager : MonoBehaviour
{

    // �α��� �г�
    [Header("�α��� �г� ����")]
    public GameObject loginPanel;
    public TextMeshProUGUI loginSuccess_text;
    public TextMeshProUGUI loginFail_text;

    // ����
    [Header("���� ����")]
    private AudioSource audioSource; // ����� Ŭ���� �巡���ؼ� �ν����Ϳ� ���



    void Start()
    {
        // ���۶� �α��� �г� �����
        loginPanel.SetActive(false);
        
        loginSuccess_text.gameObject.SetActive(false);
        loginFail_text.gameObject.SetActive(false);

        // ���۽� ���� ���
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    void Update()
    {

    }

    // ���۹�ư ��������, 
    public void OnButtonClickedStart()
    {
        // �α����� �Ǿ� ������ , �κ�� �̵�
        if (PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Lobby");

            //PhotonNetwork.LoadLevel("Lobby"); �������
        }
        // �α����� �ȵǾ� ������
        else
        {
            // �α��� �г��� Ȱ��ȭ
            loginPanel.SetActive(true);           
        }
    }


    // ����ȭ���� �����ư�� �������� �� ����
    public void OnButtonClickedQuit()
    {
        Application.Quit();

        // ����Ƽ ������ ����
        UnityEditor.EditorApplication.isPlaying = false;
    }

    // �α��� �г��� '������'�� ��������
    public void LoginPanelQuitButton()
    {
        loginPanel.SetActive(false);
    }

    // �α��� �г� 'Ȯ��'�� ��������
    public void LoinPanelYesButton()
    {
        // ����, �α��� ������ ��Ȯ�ϴٸ�
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("�α��� ����ϴ�.");
            // �α��� �г��� ����
            loginPanel.SetActive(false);
            // Text�� ����            
            loginSuccess_text.gameObject.SetActive(true);
            // 3�ʵڿ� �޼����� �����.
            Invoke("HideLoginSuccessText", 3f);
           
        }
        // �α��� ������ �� ��Ȯ�ϴٸ� // ��� �� ��Ȯ���� �Ǵ��ϴ� ��ũ��Ʈ��
        else
        {
            // ��ũ�� ���� �ؽ�Ʈ�� ����
            loginFail_text.gameObject.SetActive(true);
            // 3�� �ڿ� �����.
            Invoke("HideLoginFailText", 3f);

           
        }


    }


    public void HideLoginSuccessText()
    {
        loginSuccess_text.gameObject.SetActive(false);
    }

    public void HideLoginFailText()
    {
        loginFail_text.gameObject.SetActive(false);
    }

   
    




}


    


    






