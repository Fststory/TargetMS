using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip officeSound;
    private void Awake()
    {
        audioSource.clip = officeSound;
    }
    void Start()
    {
        audioSource.Play();

    }

    void Update()
    {
        
    }
}
