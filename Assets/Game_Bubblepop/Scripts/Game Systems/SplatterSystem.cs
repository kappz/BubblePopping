/*
This script creates and displays the splash particles that appear when a bubble is destroyed.
This script is attached to the persistent gameobject 'GameSystems'. 
This script works by checking if a bubble was destroyed, collecting information about that bubble,
and creating and displaying a splat particle. 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplatterSystem {
    private int maxNumSplatters; // The maximum number of splatter decals that can be displayed.
    private int smallSplatMinSize; // The minimum size for small bubble splatter particle.
    private int particleDecalDataIndex; // Holds the current index position of the array that contains the initial information of each splatter particle.
    private float smallSplatMaxSize;  // The maximum size for small bubble splatter particle.
    private float mediumSplatMinSize;
    private float mediumSplatMaxSize;
    private float largeSplatMinSize;
    private float largeSplatMaxSize;
    private GameObject gameSystems;
    private ParticleSystem splatSystem;
    private ParticleSplatData[] particleData;  // Array that holds initial splatter decal information for each splatter particle.
    private ParticleSystem.Particle[] particles;  // The array that will store final information about each splatter particle.

    public SplatterSystem(int numSplatters, int  smallMinSize, int smallMaxSize, int mediumMinSize, int mediumMaxSize, int largeMinSize, int largeMaxSize) {
        maxNumSplatters = numSplatters;
        smallSplatMinSize = smallMinSize;
        smallSplatMaxSize = smallMaxSize;
        mediumSplatMinSize = mediumMinSize;
        mediumSplatMaxSize = mediumMaxSize;
        largeSplatMinSize = largeMinSize;
        largeSplatMaxSize = largeMaxSize;
        gameSystems = GameObject.FindWithTag("GameSystems");
        splatSystem = gameSystems.GetComponent<ParticleSystem>();
        particleData = new ParticleSplatData[maxNumSplatters];
        particles = new ParticleSystem.Particle[maxNumSplatters];

        for (int i = 0; i < maxNumSplatters; ++i) {
            particleData[i] = new ParticleSplatData();  // initialize each particle element.
        }
    }

    public void DisplayParticles() {

        for (int i = 0; i < particleData.Length; ++i) { // Copy all current splatter particles to particles array for the purpose of displaying them.
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }
        splatSystem.SetParticles(particles, particles.Length);  // Display the particles using particle system.
    }

    public void SetParticleData(Vector3 location, int score, Color32 color) {
        float size; // the size the particle will take on

        if (particleDecalDataIndex >= maxNumSplatters) {  // If the array is full, wrap around it and overwrite first particle information.
            particleDecalDataIndex = 0; 
        }
        // Set a proper splat size depending on bubble size. 
        if (score == 1) {
            size = Random.Range(smallSplatMinSize, smallSplatMaxSize);
        }
        else if (score == 2) {
            size = Random.Range(mediumSplatMinSize, mediumSplatMaxSize);
        }
        else {
            size = Random.Range(largeSplatMinSize, largeSplatMaxSize);
        }

        location.z = -5;
        // Create and store the splat particle.
        particleData[particleDecalDataIndex].position = location;
        particleData[particleDecalDataIndex].rotation = new Vector3(1, 1, Random.Range(0, 180));
        particleData[particleDecalDataIndex].size = size;
        particleData[particleDecalDataIndex].color = color;
       ++particleDecalDataIndex;
   }
}
