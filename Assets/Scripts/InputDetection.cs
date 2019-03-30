/*
   This class verifies if the user's touches are on the right spot. Notice that both fingers of the user must be
   on the horizontal and vertical bars at the same time. It's main purpose is to get coordinates from where the user
   touches as in (x, y) coordinates. And then, it can calculate the distance from those coordinates and the
   bug's current position.

   For visual effects, the cannons move to the locations touched by the user at a given point.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection {

    private float xposition;
    private float yposition;

    public InputDetection() {

    }

    public bool UserTouches(Touch touchOne, Touch touchTwo) {
        bool touched = false;

        Ray inputRayOne = new Ray();
        Ray inputRayTwo = new Ray();

        RaycastHit rayOneHit;
        RaycastHit rayTwoHit;

        if (touchOne.phase == TouchPhase.Began) {
            inputRayOne = Camera.main.ScreenPointToRay(touchOne.position);
        }
        if (touchTwo.phase == TouchPhase.Began) {
            inputRayTwo = Camera.main.ScreenPointToRay(touchTwo.position);
        }

        if (Physics.Raycast(inputRayOne, out rayOneHit) && Physics.Raycast(inputRayTwo, out rayTwoHit))
        {

            if (rayOneHit.transform.tag == "Horizontal" && rayTwoHit.transform.tag == "Vertical")
            {
                // Convert pixel coordinates to screen coordinates
                Vector3 touchHorizontal = Camera.main.ScreenToWorldPoint(touchOne.position);
                Vector3 touchVertical = Camera.main.ScreenToWorldPoint(touchTwo.position);

                TransformX(touchHorizontal.x);
                TransformY(touchVertical.y);

            }

            if (rayOneHit.transform.tag == "Vertical" && rayTwoHit.transform.tag == "Horizontal") {

                // Convert pixel coordinates to screen coordinates
                Vector3 touchHorizontal = Camera.main.ScreenToWorldPoint(touchTwo.position);
                Vector3 touchVertical = Camera.main.ScreenToWorldPoint(touchOne.position);

                TransformX(touchHorizontal.x);
                TransformY(touchVertical.y);

            }

            touched = true;
        }


        return touched;
    }// End UserTouches

    public void CannonPosition(GameObject cannonX, GameObject cannonY) {
        Transform cannonXT = cannonX.transform;
        Transform cannonYT = cannonY.transform;

        cannonXT.position = new Vector3(xposition, cannonXT.position.y, cannonXT.position.z);
        cannonYT.position = new Vector3(cannonYT.position.x, yposition, cannonYT.position.z);
    }


    // return distance between user's coordinates and bug's current position
    public float Distance(GameObject go) {
        Vector3 touchedPositions = new Vector3(xposition, yposition, -0.3f);

        float distance = Vector3.Distance(touchedPositions, go.transform.position);

        return distance;
    }

    public float GetX() {
        return xposition;
    }

    public float GetY() {
        return yposition;
    }

    void TransformX(float x) {

        if (x < 0)
        {
            xposition = x * 1.5f;
        }
        else if (x > 0)
        {
            xposition = x * 1.5f;
        }
        else
            xposition = x;
    }

    void TransformY(float y) {
        if (y > 0)
        {
            yposition = y * 1.9f;
        }
        else
            yposition = y;
    }
}
