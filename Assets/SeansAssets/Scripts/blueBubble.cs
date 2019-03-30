using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueBubble : MonoBehaviour {

    public Rigidbody blueBubbleRB;
    public Rigidbody greenBubbleRB;
    public float bubbleSpeed;
    private float spawnTimer = 10;
    private float timeToDestroy = 20;

    public GameObject blueEmitter;
    public GameObject greenEmitter;

    //public GameObject blueBubbleFab;
    //public GameObject greenBubbleFab;


    // Update is called once per frame
    void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer == 5)
        {
            Rigidbody clone;
            clone = Instantiate(blueBubbleRB, transform.position, transform.rotation);
           // spawnTimer = 10;
            clone.velocity = transform.TransformDirection(Vector3.left * bubbleSpeed);
            Destroy(clone, timeToDestroy);
        }
        if (spawnTimer == 0)
        {
            Rigidbody clone;
            clone = Instantiate(blueBubbleRB, transform.position, transform.rotation);
            spawnTimer = 10;
            clone.velocity = transform.TransformDirection(Vector3.right * bubbleSpeed);
            Destroy(clone, timeToDestroy);
        }
        //transform.Translate (-bubbleSpeed, 0, 0);
    }
}



