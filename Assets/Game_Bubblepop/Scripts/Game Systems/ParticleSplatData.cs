/*
This class holds information about the splatter particles.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParticleSplatData {
    public float size; 
    public Color color;
    public Vector3 position; 
    public Vector3 rotation;

    public ParticleSplatData()
    {
        position = Vector3.zero;
        size = 0.0f;
        rotation = Vector3.zero;
    }

}

