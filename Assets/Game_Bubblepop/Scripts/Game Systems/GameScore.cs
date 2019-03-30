using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore {

    public int tempScore;
    public int score;
    public GameScore() {
        score = 0;
        tempScore = 0;
    }
    public void UpdateScore(float targetRadius, float distanceTouchOneTargetCenter, float distanceTouchTwoTargetCenter, float lengthTouchOne, float lengthTouchTwo)
    {
        // check for three points
        if ((distanceTouchOneTargetCenter < targetRadius * 0.40f && distanceTouchTwoTargetCenter < targetRadius * 0.40f) && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4)) {  // check for three points
            score += 3;
            tempScore += 3;
            Handheld.Vibrate();
        }
        else if ((distanceTouchOneTargetCenter < targetRadius * 0.40f && distanceTouchTwoTargetCenter < targetRadius * 0.40f)) {
            score += 2;
            tempScore += 2;
        }
        else if ((distanceTouchOneTargetCenter < targetRadius* 0.40f && distanceTouchTwoTargetCenter < targetRadius * 0.66f) && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4)) {
            score += 2;
            tempScore += 2;
        }
        else if ((distanceTouchOneTargetCenter < targetRadius * 0.66f && distanceTouchTwoTargetCenter < targetRadius * 0.40f) && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4)) {
            score += 2;
            tempScore += 2;
        }
        else if (distanceTouchOneTargetCenter < targetRadius * 0.66f || distanceTouchTwoTargetCenter < targetRadius * 0.66f && (lengthTouchOne > targetRadius / 4 && lengthTouchTwo > targetRadius / 4))   // check for two points
        {
            score += 2;
            tempScore += 2;
        }
        else {  // otherwise reward one point
            score += 1;
            tempScore += 1;
        }
    }
}
