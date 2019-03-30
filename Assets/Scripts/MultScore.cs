using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultScore {

    public MultScore() {

    }

    public int UpdateScore(float dist, float chargePercentage)
    {

        int temp = 0;

        if (dist >= 0 && dist <= 0.5f)
        {
            temp += 3;
        }
        else if (dist >= 0.51f && dist <= 1.0f)
        {
            temp += 2;

        }
        else if (dist >= 1.01f && dist <= 1.05f)
        {
            temp += 1;
        }


        if (chargePercentage >= 33 && chargePercentage < 66)
        {
            temp += 1;
        }
        else if (chargePercentage >= 66 && chargePercentage < 100)
        {
            temp += 2;
        }
        else if (chargePercentage >= 100)
        {
            temp += 3;
        }

        return temp;
    }
}
