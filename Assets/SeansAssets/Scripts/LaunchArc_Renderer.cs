using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;


[RequireComponent(typeof(LineRenderer))]
public class LaunchArc_Renderer : MonoBehaviour {
	LineRenderer lr;

	public float velocity;
	public float angle;
	public int resolution;

	//force of gravity on the Y-Axis (negative value!)
	float grav; 
	float radianAngle;

	// Use this for initialization


	void Awake(){
		lr = GetComponent<LineRenderer> ();
		grav = Mathf.Abs (Physics2D.gravity.y);
	

	}
	//Function to check that 'lr' is NOT null, and game is in play mode
	void OnValidate(){
		//if (lr != null && Application.isPlaying) {
		//	RenderArc ();
		//}
	
	}

	void Start () {
		RenderArc ();
	}

	//This will populate the line renderer with the correct settings.
	void RenderArc(){
		lr.SetVertexCount (resolution + 1);
		lr.SetPositions (CalculateArcArray ());
	}

	//Will Create an ARRAY of type Vector3 positions for arc
	Vector3[] CalculateArcArray(){
		Vector3[] arcArray = new Vector3[resolution + 1];
		radianAngle = Mathf.Deg2Rad * angle;
		float maxSpearDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / grav;
		for (int i = 0; i <= resolution; i++) {
			float t = (float)i / (float)resolution;
			arcArray [i] = CalculateArcPoint (t, maxSpearDistance);
		}
		return arcArray;
	
	}

	Vector3 CalculateArcPoint(float t, float maxDistance){
		float x = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((grav * x * x) / (2 * velocity * velocity * Mathf.Cos (radianAngle) * Mathf.Cos (radianAngle))); 

		return new Vector3 (x, y);
	}


}
