/*
This class holds information about each active bubble. 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Target {

    public string type;  // small, medium, large
    public Vector3 Location; // coordinates where midpoint of bubble is located
    public Color32 color;  // color of the bubble
}
