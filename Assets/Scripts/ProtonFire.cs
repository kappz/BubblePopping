/*
  This script is to fire the cannons from both sides.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonFire : MonoBehaviour
{

    public GameObject ProtonMainFX;
    public GameObject ProtonExtraFX;
    public AudioSource beamMainAudio;
    public AudioSource beamStartAudio;
    public AudioSource beamStopAudio;

    public ParticleSystem lightningBoltParticles;
    public ParticleSystem protonBeamParticles;

    private int protonBeamFlag = 0;

    private float count = 30;
    private float delay = 3.0f;

    void Start()
    {

        ProtonMainFX.SetActive(false);
        ProtonExtraFX.SetActive(false);

    }

    void Update()
    {
        /* 
            Delay works as a timer to keep everything within the same pace. It is used to pause other activities
            once the cannons fire or the spark appears on screen.
        */
        if (delay <= 3.0f)
        {
            delay += Time.deltaTime;
        }

        if (count < 29)
        {

            //ProtonFiring();
            if (count == 0)
            {
                StartCoroutine("ProtonPackFire");
            }


            count++;
        }
        else if (count == 29)
        {
            ProtonPackStop();
            count = 30;
        }


    }

    public IEnumerator ProtonPackFire()
    {

        ProtonExtraFX.SetActive(true);
        beamStartAudio.Play();
        protonBeamFlag = 0;

        yield return new WaitForSeconds(0.2f);
        // Wait for laser to charge

        if (protonBeamFlag == 0)
        {
            ProtonMainFX.SetActive(true);

            lightningBoltParticles.Play();
            protonBeamParticles.Play();
            beamMainAudio.Play();
        }
    }

    public void ProtonPackStop()
    {

        protonBeamFlag = 1;

        ProtonMainFX.SetActive(false);
        lightningBoltParticles.Stop();
        protonBeamParticles.Stop();

        beamMainAudio.Stop();
        beamStartAudio.Stop();
        beamStopAudio.Play();

    }

    public void StartDelay()
    {
        delay = 0;
    }

    public float GetDelay()
    {
        return delay;
    }

    public void ResetCounter()
    {
        count = 0;
    }
}
