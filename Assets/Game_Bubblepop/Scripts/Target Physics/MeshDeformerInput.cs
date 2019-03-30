/*
This script monitors and controls all bubble deformity in response to user input. This script is
attatched to the perminate game object 'GameSystems'. During each frame this script checks if there
are two fingers on the screen, verifies if they're in range of a bubble object, deforms that
bubble object with the programed deform values. 
*/

using UnityEngine;

public class MeshDeformerInput : MonoBehaviour {
    public float largeForce, mediumForce, smallForce;  // The force values placed on each different bubble type.
    public float largeOffset, mediumOffset, smallOffset;  // The values bubble vertices move in response to force.
	private float force, forceOffset;  // The final assigned force and offset value.
	
	void Update () {
        // if two fingers placed on screen, verify they're in range of a bubble and deform that ojbect. 
        if (Input.touchCount == 2) {
            HandleInput();
        }
	}
   
    void HandleInput () {
        Ray inputRayOne = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); // Finger one raycast.
        Ray inputRayTwo = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);  // Finger one raycast.
		RaycastHit hit;

        // Cast rays and inspect 'hit' for bubble return info if ray hit a bubble object.
        // Assign corresponding force and offset values based off bubble type return info.

        if (Physics.Raycast(inputRayOne, out hit)) { 
            if (hit.collider.gameObject.tag == "large") { 
                force = largeForce;
                forceOffset = largeOffset;
            }
            if (hit.collider.gameObject.tag == "medium") {
                force = mediumForce;
                forceOffset = mediumOffset;
            }
            if (hit.collider.gameObject.tag == "small") {
                force = smallForce;
                forceOffset = smallOffset;
            }
            // Deform the object
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer) { 
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddDeformingForce(point, force); 
            }
        }

        // repeat for second finger

        if (Physics.Raycast(inputRayTwo, out hit)) {
            if (hit.collider.gameObject.tag == "large") {
                force = largeForce;
                forceOffset = largeOffset;
            }
            if (hit.collider.gameObject.tag == "medium") {
                force = mediumForce;
                forceOffset = mediumOffset;
            }
            if (hit.collider.gameObject.tag == "small") {
                force = smallForce;
                forceOffset = smallOffset;
            }
            // Deform the object
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer) {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        }

    }
}