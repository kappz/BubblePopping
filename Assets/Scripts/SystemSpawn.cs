/*
    This class spawns a bug into the screen, and it is only called when no bug is present on screen
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSpawn
{

    public SystemSpawn()
    {

    }


    public void SpawnBug(GameObject insect)
    {
        Vector3 bugPosition = new Vector3(Random.Range(-8, 6), Random.Range(-2.5f, 3), -0.3f);
        MonoBehaviour.Instantiate(insect, bugPosition, Quaternion.identity);
    }

}