using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHard : MonoBehaviour {

    public Transform[] target;
    public float speed;
    private int current;

    public GameObject gameController;
    GameControllerHard GCScript;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        GCScript = (GameControllerHard)gameController.GetComponent(typeof(GameControllerHard));
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine("Move");

    }



    IEnumerator Move()
    {
        int point = Random.Range(0, target.Length);


        if (GCScript.GetTouchStatus() == true)
        {
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
