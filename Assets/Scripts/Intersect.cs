using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersect : MonoBehaviour
{
    private bool flagx = false;
    private bool flagy = false;
    public GameObject explosion;
    public AudioSource explosionSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Destroy the bug when it collides with both beams
        if (flagx == true && flagy == true)
        {
            explosionSound.Play();
            Destroy(this.gameObject);
            Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
        }
    }


    void OnParticleCollision(GameObject beam)
    {
        if (beam.gameObject.CompareTag("Beamx"))
        {
            flagx = true;
        }
        else if (beam.gameObject.CompareTag("Beamy"))
        {
            flagy = true;
        }
    }
}
