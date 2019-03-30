using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn_Controller : MonoBehaviour {

    public float blueTimer;
    public float greenTimer;   //start timer at 10, counting down.  
                                    //Blue bubble spawns in 5 sec, green in 10, start over. 
    private float timeToDestroy = 20;

    public GameObject blueEmitter;
    public GameObject greenEmitter;

    public GameObject blueBubbleFab;
    public GameObject greenBubbleFab;


    // Update is called once per frame
    void Update () {
        blueTimer -= Time.deltaTime;
        greenTimer -= Time.deltaTime;
        if (blueTimer <= 0)
        {
            GameObject blueClone = Instantiate(blueBubbleFab, blueEmitter.transform.position, blueEmitter.transform.rotation);
            Destroy(blueClone, timeToDestroy);
            blueTimer = 6;
        }

        if (greenTimer <= 0)
        {
            GameObject greenClone = Instantiate(greenBubbleFab, greenEmitter.transform.position, greenEmitter.transform.rotation) as GameObject;
            Destroy(greenClone, timeToDestroy);
            greenTimer = 10;
        }
      
    }
}



