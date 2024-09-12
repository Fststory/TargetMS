using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMove")] 
    public float moveSpeed = 15f;
    public float movePower = 60f;
    public float turnSpeed = 720f;
    private Vector3 movement;
    
    [Header("Reference")]
    private Rigidbody rb;
    private RaycastHit hit;
    private Animator anim;
    
    

// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void Move()
    {
        rb.AddForce( movement * movePower, ForceMode.Acceleration);
        
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }
        
        Vector3 flatSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatSpeed.magnitude >= moveSpeed)
        {
            Vector3 limitSpeed = flatSpeed.normalized * moveSpeed;
            rb.velocity = new Vector3(limitSpeed.x, rb.velocity.y, limitSpeed.z);
        }
    }
    
}
