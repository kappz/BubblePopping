using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBubble_Spawn : MonoBehaviour {

	public GameObject bubbleFab;
	//green bubble will spawn after 5 seconds after begin, then every 15
	private float spawnTimer = 5;
	private float timeToDestroy = 30;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0) {
			GameObject bubbleClone = Instantiate (bubbleFab, transform.position, transform.rotation) as GameObject;
			spawnTimer = 10;
			Destroy(bubbleClone, timeToDestroy);
		}
	}
}
