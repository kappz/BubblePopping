using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_Script_Multiplayer : MonoBehaviour
{
    private CannonMode_Controller_Multiplayer cannonModeController_Multi;

    //prefab variables
    public GameObject explosion;
    public GameObject splash;

    //audio variables
    public AudioSource audiosrc;
    public AudioClip cannonBoom;


    public GameObject goodMessage;
    public GameObject greatMessage;
    public GameObject PerfectMessage;
    public Transform messagePos;
    private float messageTimeToDestroy = 2;

    private bool detectedCollision = false;

    void Start()
    {

        cannonModeController_Multi = GameObject.FindObjectOfType<CannonMode_Controller_Multiplayer>();
    }
    void Splash()
    {
        GameObject cannonSplash = Instantiate(splash, transform.position, Quaternion.identity);
        cannonSplash.GetComponent<ParticleSystem>().Play();
    }

    void Explode()
    {
        GameObject hitExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        hitExplosion.GetComponent<ParticleSystem>().Play();
    }

    void DisplayMessage(int score, GameObject g)
    {
        if (score == 1)
        {
            GameObject good = Instantiate(goodMessage, messagePos.transform.position, messagePos.transform.rotation);
            Destroy(g);
            Destroy(good, messageTimeToDestroy);
        }
        if (score == 2)
        {
            GameObject great = Instantiate(greatMessage, messagePos.transform.position, messagePos.transform.rotation);
            Destroy(g);
            Destroy(great, messageTimeToDestroy);
        }
        if (score == 3)
        {
            GameObject perfect = Instantiate(PerfectMessage, messagePos.transform.position, messagePos.transform.rotation);
            Destroy(g);
            Destroy(perfect, messageTimeToDestroy);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "blueBubble")
            if(detectedCollision == false) {
                {
                    detectedCollision = true;
                    Explode();
                    Debug.Log("OUTER");
                    DisplayMessage(1, gameObject);
                    //Destroy(gameObject);
                    cannonModeController_Multi.updatePlayerScore(1);
                }
        }
        if (col.gameObject.tag == "blueInner")
        {
            if (detectedCollision == false)
            {
                detectedCollision = true;
                Explode();
                Debug.Log("INNER");
                Destroy(col.gameObject);  //destroys the bubble hit
                DisplayMessage(2, gameObject);
                // Destroy(gameObject);
                cannonModeController_Multi.updatePlayerScore(2);
            }
       }
        if (col.gameObject.tag == "blueBullseye")
        {
            if (detectedCollision == false)
            {
                detectedCollision = true;
                Explode();
                Debug.Log("BULLSEYE");
                Destroy(col.gameObject);  //destroys the bubble hit
                DisplayMessage(3, gameObject);
                //Destroy(gameObject);
                cannonModeController_Multi.updatePlayerScore(3);
            }
        }
        //Blue bubble conditions end

        //Green Bubble conditions start
        if (col.gameObject.tag == "greenBubble")
        {
            if (detectedCollision == false)
            {
                detectedCollision = true;
                Explode();
                Debug.Log("GREEN OUTER");
                Destroy(col.gameObject);  //destroys the bubble hit
                DisplayMessage(1, gameObject);
                Destroy(gameObject);
                cannonModeController_Multi.updatePlayerScore(1);
            }
        }
        if (col.gameObject.tag == "greenInner")
        {
            if (detectedCollision == false)
            {
                detectedCollision = true;
                Explode();
                Debug.Log("GREEN INNER");
                Destroy(col.gameObject);  //destroys the bubble hit
                DisplayMessage(2, gameObject);
                Destroy(gameObject);
                cannonModeController_Multi.updatePlayerScore(2);
            }
        }
        if (col.gameObject.tag == "greenBullseye")
        {
            if (detectedCollision == false)
            {
                detectedCollision = true;
                Explode();
                Debug.Log("GREEN BULLSEYE");
                Destroy(col.gameObject);  //destroys the bubble hit
                DisplayMessage(3, gameObject);
                Destroy(gameObject);
                cannonModeController_Multi.updatePlayerScore(3);
            }
        }
        //Green bubble conditions end

        if (col.gameObject.tag == "Water")
        {
            Splash();
            Debug.Log("SPLASH!");
            Destroy(gameObject);  //Do not destroy the water stupid
        }



    }

}
