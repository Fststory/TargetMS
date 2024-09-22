using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test_PlayerRotate : MonoBehaviour
{
    float rotX;
    float rotY;
    public float rotateSpeed;

    
    void Start()
    {
        
    }

    void Update()
    {

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        rotX += y * rotateSpeed * Time.deltaTime;
        rotY += x * rotateSpeed * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -60, 60);

        transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }


    public void Rotate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        rotX += y * rotateSpeed * Time.deltaTime;
        rotY += x * rotateSpeed * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -60, 60);

        transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }
}
