﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement_Speed : MonoBehaviour {

	public float bubbleSpeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (-bubbleSpeed, 0, 0);
	}
}
