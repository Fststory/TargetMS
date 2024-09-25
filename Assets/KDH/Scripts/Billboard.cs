using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform campos;

    private void Start()
    {
        campos = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(campos.position);
    }
}
