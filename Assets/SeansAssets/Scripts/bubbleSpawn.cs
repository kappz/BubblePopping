using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleSpawn : MonoBehaviour {

	public GameObject bubbleFab;
	public float spawnTimer = 5;
	Vector3 bubbleSpeed;

	// Use this for initialization
	void Start () {
		
		//InvokeRepeating ("spawnBubble", spawnTimer, spawnDelay);
	}

	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0)
		{
			Rigidbody rb = bubbleFab.GetComponent<Rigidbody> ();
			GameObject bubbleClone = Instantiate (bubbleFab, transform.position, transform.rotation);
			spawnTimer = 5;
		}
	}
}
