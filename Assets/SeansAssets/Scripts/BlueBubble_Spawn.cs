using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBubble_Spawn: MonoBehaviour {

	public GameObject bubbleFab;
	private float spawnTimer = 0;
	private float timeToDestroy = 20;

	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0) {
			GameObject bubbleClone = Instantiate (bubbleFab, transform.position, transform.rotation) as GameObject;
			spawnTimer = 10;
			Destroy (bubbleClone, timeToDestroy);
		}
	}
}

