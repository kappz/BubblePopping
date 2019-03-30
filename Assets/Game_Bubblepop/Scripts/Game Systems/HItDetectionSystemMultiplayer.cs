/*
 * This script captures and interprets user touch input. The information interpreted by this script 
 * is used for determining if a target should be destroyed. If a target is to be destroyed, the script
 * generates information for the splatter particle effect, and also information for the score computation.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionSystemMultiplayer {
    public Target target { get; set; }  // Stores information about target hit by user input. 
    private Color32 targetColor;  // The target's color
    Vector3 touchOneStartPoint, touchTwoStartPoint;  // The starting location of both touch inputs.
    // Values for length of each pinch, and distance from each pinch end points to target center.
    private float targetRadius, lengthTouchOne, lengthTouchTwo, distanceTouchOneEndTargetCenter, distanceTouchTwoEndTargetCenter;
    public GameScoreMultiplayer gameScore;  // Computes and stores player score, stores opponent's score. 
    public MessageSystem messages;
   
    public HitDetectionSystemMultiplayer() {
        targetRadius = 0.0f;
        gameScore = new GameScoreMultiplayer();
        messages = new MessageSystem();
    }

    // Returns true if touchPosition is within sphere with a midpoint = targetPosition and radius = targetRadius
    public bool InputWithinTargetArea(Vector3 touchPosition, Vector3 targetPosition, float targetRadius) {
        bool result = false;

        if (touchPosition.x >= (targetPosition.x - (targetRadius))
            && (touchPosition.x <= (targetPosition.x + (targetRadius)))
            && (touchPosition.y >= (targetPosition.y - (targetRadius)))
            && (touchPosition.y <= (targetPosition.y + (targetRadius)))) {
            result = true;
        }
        return result;
    }

    // Capture user input, evaluate if target destroy criteria met.
    // If target destroyed, remove from scene, compute and update game score, generate data for splatter particle.


    public bool UserDestroysTarget(Touch touchOne, Touch touchTwo, GameObject good, GameObject great, GameObject perfect) {
        bool result = false;
        Ray inputRayOne = new Ray();  // Ray cast from touch input one into game.
        Ray inputRayTwo = new Ray();  
        RaycastHit rayOneHitData;  // The data received from each ray cast. 
        RaycastHit rayTwoHitData;
        Vector3 spawnLocation;  // The center of a target being destroyed.
        Vector3 touchOneEndPoint = Vector3.zero;  // The location of user touch one end. 
        Vector3 touchTwoEndPoint = Vector3.zero;

        // Get touch start and end point locations, compute each pinch lengths, setup raycasts. 
        if (touchOne.phase == TouchPhase.Began) {
            touchOneStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 883.0f));
        }
        if (touchTwo.phase == TouchPhase.Began) {
            touchTwoStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchTwo.position.x, touchTwo.position.y, 883.0f));
        }
        if (touchOne.phase == TouchPhase.Ended) {
            touchOneEndPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 883.0f)); // Finger one raycast.
            inputRayOne = Camera.main.ScreenPointToRay(touchOne.position);
            lengthTouchOne = Vector3.Distance(touchOneEndPoint, touchOneStartPoint);
        }
        if (touchTwo.phase == TouchPhase.Ended) {
            touchTwoEndPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchTwo.position.x, touchTwo.position.y, 883.0f)); // Finger one raycast.
            inputRayTwo = Camera.main.ScreenPointToRay(touchTwo.position); // Finger one raycast.
            lengthTouchTwo = Vector3.Distance(touchTwoEndPoint, touchTwoStartPoint);
        }

        // Cast rays and inspect 'hit' for target return info if ray hit a target object.
        // Assign corresponding force and offset values based off bubble type return info.
        if (Physics.Raycast(inputRayOne, out rayOneHitData) && Physics.Raycast(inputRayTwo, out rayTwoHitData)) {
            if (rayOneHitData.collider.gameObject.transform.tag == rayTwoHitData.collider.gameObject.transform.tag && lengthTouchOne > 5.0f && lengthTouchTwo > 5.0f) {
                result = true;

                // Generate splatter particle information.
                target = new Target();
                targetColor = rayOneHitData.collider.gameObject.GetComponent<MeshRenderer>().materials[0].color;
                spawnLocation = rayOneHitData.collider.gameObject.transform.position;
                target.type = rayOneHitData.collider.gameObject.tag;
                target.color = targetColor;
                target.Location = spawnLocation;

                // Get the radius of target to pass into gameScore for score compuation. 
                if (rayOneHitData.collider.gameObject.tag == "small")
                {
                    targetRadius = 60.0f;
                }
                else if (rayOneHitData.collider.gameObject.tag == "medium")
                {
                    targetRadius = 70.0f;
                }
                else
                {
                    targetRadius = 90.0f;
                }
                // Compute distance from each touch endpoint from target center and pass to gameScore for score computation.
                // Compute the score.
                distanceTouchOneEndTargetCenter = Vector2.Distance(new Vector2(touchOneEndPoint.x, touchOneEndPoint.y), new Vector2(spawnLocation.x, spawnLocation.y));
                distanceTouchTwoEndTargetCenter = Vector2.Distance(new Vector2(touchTwoEndPoint.x, touchTwoEndPoint.y), new Vector2(spawnLocation.x, spawnLocation.y));
                gameScore.UpdateScore(targetRadius, distanceTouchOneEndTargetCenter, distanceTouchTwoEndTargetCenter, lengthTouchOne,lengthTouchTwo);
                MonoBehaviour.DestroyImmediate(rayOneHitData.transform.gameObject);  // Remove target from scene.
                if (gameScore.tempScore == 1)
                {
                    messages.displayMessage(good, target.Location);
                }
                else if (gameScore.tempScore == 2)
                {
                    messages.displayMessage(great, target.Location);
                }
                else if (gameScore.tempScore == 3)
                {
                    messages.displayMessage(perfect, target.Location);
                }
            }
        }
        return result;
    }

    /*
    public bool UserDestroysTarget(Vector3 position)
    {
        bool result = true;
        Ray inputRayOne = new Ray();  // Ray cast from touch input one into game.
        Ray inputRayTwo = new Ray();
        RaycastHit rayOneHitData;  // The data received from each ray cast. 
        RaycastHit rayTwoHitData;
        Vector3 spawnLocation;  // The center of a target being destroyed.
        Vector3 touchOneEndPoint = Vector3.zero;  // The location of user touch one end. 
        Vector3 touchTwoEndPoint = Vector3.zero;
        inputRayOne = Camera.main.ScreenPointToRay(position);
        // Get touch start and end point locations, compute each pinch lengths, setup raycasts. 
        /*
        if (touchOne.phase == TouchPhase.Began)
        {
            touchOneStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 883.0f));
        }
        if (touchTwo.phase == TouchPhase.Began)
        {
            touchTwoStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchTwo.position.x, touchTwo.position.y, 883.0f));
        }
        if (touchOne.phase == TouchPhase.Ended)
        {
            touchOneEndPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchOne.position.x, touchOne.position.y, 883.0f)); // Finger one raycast.
            inputRayOne = Camera.main.ScreenPointToRay(touchOne.position);
            lengthTouchOne = Vector3.Distance(touchOneEndPoint, touchOneStartPoint);
        }
        if (touchTwo.phase == TouchPhase.Ended)
        {
            touchTwoEndPoint = Camera.main.ScreenToWorldPoint(new Vector3(touchTwo.position.x, touchTwo.position.y, 883.0f)); // Finger one raycast.
            inputRayTwo = Camera.main.ScreenPointToRay(touchTwo.position); // Finger one raycast.
            lengthTouchTwo = Vector3.Distance(touchTwoEndPoint, touchTwoStartPoint);
        }
        

        // Cast rays and inspect 'hit' for target return info if ray hit a target object.
        // Assign corresponding force and offset values based off bubble type return info.
        if (Physics.Raycast(inputRayOne, out rayOneHitData))
        {

                result = true;

                // Generate splatter particle information.
                target = new Target();
                targetColor = rayOneHitData.collider.gameObject.GetComponent<MeshRenderer>().materials[0].color;
                spawnLocation = rayOneHitData.collider.gameObject.transform.position;
                target.type = rayOneHitData.collider.gameObject.tag;
                target.color = targetColor;
                target.Location = spawnLocation;

                // Get the radius of target to pass into gameScore for score compuation. 
                if (rayOneHitData.collider.gameObject.tag == "small")
                {
                    targetRadius = 60.0f;
                }
                else if (rayOneHitData.collider.gameObject.tag == "medium")
                {
                    targetRadius = 70.0f;
                }
                else
                {
                    targetRadius = 90.0f;
                }
                // Compute distance from each touch endpoint from target center and pass to gameScore for score computation.
                // Compute the score.
                distanceTouchOneEndTargetCenter = Vector2.Distance(new Vector2(touchOneEndPoint.x, touchOneEndPoint.y), new Vector2(spawnLocation.x, spawnLocation.y));
                distanceTouchTwoEndTargetCenter = Vector2.Distance(new Vector2(touchTwoEndPoint.x, touchTwoEndPoint.y), new Vector2(spawnLocation.x, spawnLocation.y));
                gameScore.UpdateScore(targetRadius, distanceTouchOneEndTargetCenter, distanceTouchTwoEndTargetCenter, lengthTouchOne, lengthTouchTwo);
                MonoBehaviour.DestroyImmediate(rayOneHitData.transform.gameObject);  // Remove target from scene.
        }
        return result;
    }
    */
}
