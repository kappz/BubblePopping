/*
    This script is attached to the prefab of the bug. Its task is to make to object to move.
    It contains waypoints in Transform[] target. The bug will randomly select a waypoint and move
    towards that location. If the user touches both bars, the bug will stop, and let animation happen.
    If the bug is not killed, then it will resume its movement.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Transform[] target;
    public float speed;
    private int current;

    public GameObject gameController;
    GameController GCScript;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        GCScript = (GameController)gameController.GetComponent(typeof(GameController));
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine("Move");

    }



    IEnumerator Move()
    {
        int point = Random.Range(0, target.Length);

        
        if (GCScript.GetTouchStatus() == true) {
            yield return new WaitForSeconds(1.8f);

        }
        else if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            current = point % target.Length;
        }

        GCScript.DefaultTouchStatus();

    }
}
