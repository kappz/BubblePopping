/*
This script controsl the spawn sysem for the game and is attatched to the persistent game object 'GameSystems'.
Each frame the script checks if there are zero bubbles in play and instantiates the predefined amount of new bubbles.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnSystem {
    // Set of possible colors each new target's texture takes on.
    private Color[] colors = new Color[6] {new Color32(242, 224, 144, 255), new Color32(221, 119, 136, 255), 
                                           new Color32(102, 119, 153, 255), new Color32(122, 148, 96, 255),
                                           new Color32(221, 153, 119, 255), new Color32(102, 85, 102, 255)};
    private Vector3 randPos;  // Used to generate a random spawn location
    public int numTargetsInPlay { get; set; } // Tracks how many targets are in the current game state.
    private int numSmallTargets, numMediumTargets, numLargeTargets; // The number of each size target that spawns each wave.

    public SpawnSystem(int numSmall, int numMedium, int numLarge) {
        // instantiate with desired amount per wave spawn for each target size
        numTargetsInPlay = 0;
        numLargeTargets = numLarge;
        numMediumTargets = numMedium;
        numSmallTargets = numSmall;
    }

    public void incrNumTargetsInplay() {
        ++numTargetsInPlay;
    }

    public void decrNumTargetsInPlay() {
        --numTargetsInPlay;
    }

    public void SpawnTargets(GameObject smallTarget, GameObject mediumTarget, GameObject largeTarget) {
        // spawn bubbles
        Spawn(largeTarget, "large", numLargeTargets);
        Spawn(mediumTarget, "medium", numMediumTargets);
        Spawn(smallTarget, "small", numSmallTargets);
    }
    public void Spawn(GameObject targetToSpawn, string targetSize, int numTargetsToSpawn) {
        int numCollisions = 0; // Number of collisions after checking for collisions.
        int targetRadius = 0;
        int targetCount = 0;
        int bubbleSpawnZAxis = 0;
        int randomColor;
        float adjustedRadius = 0;  // used to extend the distance between all targets.

        // set correct target radius information depending on target size.
        if (targetSize == "small") {
            targetRadius = (int)targetToSpawn.transform.localScale.x;
            adjustedRadius = targetRadius * 1.25f;
            bubbleSpawnZAxis = -100;
            }
        else if (targetSize == "medium") {
            targetRadius = (int)targetToSpawn.transform.localScale.x;
            adjustedRadius = targetRadius * 1.25f;
            bubbleSpawnZAxis = -110;
            }
        else {
            targetRadius = (int)targetToSpawn.transform.localScale.x;
            adjustedRadius = targetRadius;
            bubbleSpawnZAxis = -130;
            }

        // Location collision free spawn location for target.
        // Create target, set color, and instantiate into game state.
        while (targetCount < numTargetsToSpawn) {

            // Generate a potential spawn location, check for possible collisions, and repeat until a location is found that doesn't cause collisions.
            do {
                numCollisions = 0;
                // Restrict location bounds of potential spawn location to within game coordinates.
                if (targetSize == "small") {
                    randPos = new Vector3(Random.Range(-280, 280), Random.Range(-135, 60), bubbleSpawnZAxis);
                }
                else if (targetSize == "medium") {
                    randPos = new Vector3(Random.Range(-270, 270), Random.Range(-125, 50), bubbleSpawnZAxis);
                }
                else {
                    randPos = new Vector3(Random.Range(-245,  245), Random.Range(-115, 30), bubbleSpawnZAxis);
                }
                Collider[] hitColliders = Physics.OverlapSphere(randPos, adjustedRadius);  // Scan for collisions.
                for (int j = 0; j < hitColliders.Length; j++) {  // Inspect collision data.
                    if (hitColliders[j].tag == "large" || hitColliders[j].tag == "medium" || hitColliders[j].tag == "small")
                        numCollisions++;
                }
            } while (numCollisions > 0);  // Repeat until not collisions would occur.

            ++targetCount;
            ++numTargetsInPlay;
            randomColor = Random.Range(0, 6); 
            GameObject go = MonoBehaviour.Instantiate(targetToSpawn, randPos, Quaternion.identity);  // Create the target.
            Renderer rend = go.GetComponent<MeshRenderer>(); // Change the target color to a random color from the predefined color set
            rend.materials[0].color = colors[randomColor];
            rend.materials[1].color = colors[randomColor];
            rend.materials[2].color = colors[randomColor];
        }
    }
}