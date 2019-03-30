using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleFeedbackAnimation : MonoBehaviour
{
    private Animator anim;
    private bool hitOuter;
    private bool hitInner;
    private bool hitCenter;
    //Encouraging words for the User as feedback for hitting bubbles
    //public GameObject good;
    // public GameObject great;
    // public GameObject perfect;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collider that was hit: " + col.gameObject.tag);
        if (col.gameObject.tag == "blueBubble")
        {

        }
    }
}
