/*
    This class is responsible of calculating and holding the total score earned during play time.
    The metric is very simple: when the user's coordinates is closer to the center of the bug, +3 points.
    If its closer to the surface, +1 points.
    And if the coordinate is in the middle of the center and the surface, then +2 points.

    The user can earn bonus point depending how long does the slider charges, and the metrics are the following:
    When it is red (100% charged): +3 additional points.
    Yellow (between 66% and 99%): +2 additional points.
    Green (between 33% and 65%): +1 additional point.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GScore {

    private int currentScore;

    public GScore(int s) {
        currentScore = s;
    }

    public int GetScore() {
        return currentScore;
    }

    public void UpdateScore(float dist, float chargePercentage) {
        if (dist >= 0 && dist <= 0.5f)
        {
            currentScore += 3;
        }
        else if (dist >= 0.51f && dist <= 1.0f)
        {
            currentScore += 2;

        }
        else if (dist >= 1.01f && dist <= 1.05f)
        {
            currentScore += 1;
        }


        if (chargePercentage >= 33 && chargePercentage < 66)
        {
            currentScore += 1;
        }
        else if (chargePercentage >= 66 && chargePercentage < 100) {
            currentScore += 2;
        }
        else if (chargePercentage >= 100) {
            currentScore += 3;
        }
    }
}
