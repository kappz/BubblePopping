/*
This script computes and updates the score of the current game being played. 
Each target is treated like a bullseye with three sections. The inner most section 
is worth points, the middle section is worth two points, and the outer most section is worth one point.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreMultiplayer {
    public int score;
    public int tempScore;

    public GameScoreMultiplayer () {
        score = 0;
        tempScore = 0;
    }
    public void UpdateScore(float targetRadius, float distanceTouchOneTargetCenter, float distanceTouchTwoTargetCenter, float lengthTouchOne, float lengthTouchTwo)
    {
        // Assign three points if both user inputs are within inner most section and both pinch lengths are adaquate. 
        if ((distanceTouchOneTargetCenter < targetRadius * 0.40f && distanceTouchTwoTargetCenter < targetRadius * 0.40f) && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4)) {  // check for three points
            score = 3;
            tempScore = 3;
            Handheld.Vibrate();
        }
        // Assign two points if both inputs are withing inner most section, but their lengths are short.
        else if ((distanceTouchOneTargetCenter < targetRadius * 0.40f && distanceTouchTwoTargetCenter < targetRadius * 0.40f)) {
            score = 2;
            tempScore = 2;
        } // Assign two points if one pinch end point is located inner most section and the other is located in the middle section.
        else if ((distanceTouchOneTargetCenter < targetRadius* 0.40f && distanceTouchTwoTargetCenter < targetRadius * 0.66f) && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4)) {
            score = 2;
            tempScore = 2;
        } // Assign two points if one pinch end point is located inner most section and the other is located in the middle section.
        else if ((distanceTouchOneTargetCenter < targetRadius * 0.66f && distanceTouchTwoTargetCenter < targetRadius * 0.40f) && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4)) {
            score = 2;
            tempScore = 2;
        } // Assign two points if both pinch end points are in middle section and are of adaquate length.
        else if (distanceTouchOneTargetCenter < targetRadius * 0.66f || distanceTouchTwoTargetCenter < targetRadius * 0.66f && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4))   // check for two points
        {
            score = 2;
            tempScore = 2;
        }
        else {  // otherwise reward one point
            score = 1;
            tempScore = 1;
        }
    }
}
