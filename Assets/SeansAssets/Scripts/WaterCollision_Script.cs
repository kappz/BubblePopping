using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterCollision_Script : MonoBehaviour
{
    public AudioSource audiosrc;
    public AudioClip splash;


    void Awake()
    {

        audiosrc = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision col)
    {
        audiosrc.PlayOneShot(splash);
    }
}
