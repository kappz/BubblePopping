/*
 * This script controls the state of the game, and is responsible for transmitting
 * and receiving data to and from the server. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerSingleplayer : MonoBehaviour, ISingleplayerView {
    private PreferencesManager prefManager;
    private PlayGamesClient playGamesClient;
    private GameText gameText; // Responsible for updating time remaining and score text objects in scene.
    private SpawnSystem spawnSystem;  // Responsible for intantiating all targets.
    private SplatterSystem splatterSystem;  // The splatter particle system.
    private HitDetectionSystem hitDetection; // Responsible for interpreting user touch input.
    public AudioClip targetDestroyed;  // Audio clip plays whenever a target is destroyed
    public int numSmallTargets, numMediumTargets, numLargeTargets; // User passes in number of each target type to spawn, and how long game should last for.
    public GameObject smallTarget, mediumTarget, largeTarget, setupMenu, endGameMenu, helpMenu, exitMenu;  // Objects referencing the target prefabs.
    public GameObject good, great, perfect;
    private GameObject message;

    // Initialize components, hide all UI artifacts, recieve and update player's high score.
    void Start() {
        gameText = new GameText();
        spawnSystem = new SpawnSystem(numSmallTargets, numMediumTargets, numLargeTargets);  // This will spawn (num small targets, num medium targets, num large targets)
        prefManager = new PreferencesManager();
        hitDetection = new HitDetectionSystem();
        splatterSystem = new SplatterSystem(100, 125, 175, 200, 250, 275, 325);
        //gameText.UpdateHighScore(5);
        playGamesClient.AttachSingleplayerView(this);
    }

    void Update() {
        
        // get and display high score
        if (gameText.highScoreText.GetComponent<Text>().text == "0") {
            gameText.UpdateHighScore(prefManager.GetBubblePopHighScore());
        }
       //gameText.UpdateHighScore(5);
        
        // Pause game if player in menu, singleplayer feature only. 
        if (helpMenu.activeSelf == true || setupMenu.activeSelf == true || exitMenu.activeSelf == true || endGameMenu.activeSelf == true) {
            Time.timeScale = 0.0f;
        }
        else {
            Time.timeScale = 1.0f;
        }

        // Check if user destroyed a target this frame.
        if (Time.timeScale != 0.0f) {
            // Spawn a new set of bubbles if none exist in the current game state.
            if (spawnSystem.numTargetsInPlay == 0)
            {
                spawnSystem.SpawnTargets(smallTarget, mediumTarget, largeTarget);
            }
            if (Input.touchCount == 2) {
                if (hitDetection.UserDestroysTarget(Input.GetTouch(0), Input.GetTouch(1), good, great, perfect)) {
                    // update game score, crete splatter effect.
                    StartCoroutine(timeToDestroyMessage());
                    gameObject.GetComponent<AudioSource>().PlayOneShot(targetDestroyed, 1.0f);
                    splatterSystem.SetParticleData(hitDetection.target.Location, hitDetection.gameScore.tempScore, hitDetection.target.color);
                    splatterSystem.DisplayParticles();
                    spawnSystem.decrNumTargetsInPlay();
                    prefManager.UpdateBubblesPopped();
                    hitDetection.gameScore.tempScore = 0;
                }
            }

            // update time remaining and player's score.
            gameText.UpdateCountdownText(); 
            gameText.UpdateSelfScore(hitDetection.gameScore.score);

            // Trigger the end game if time remaining is <= 0.
            if (gameText.timeRemaining <= 0.0f) {
                endGameMenu.SetActive(true);
                prefManager.UpdateBubblePopHighScore(hitDetection.gameScore.score);
                prefManager.UpdateGamesPlayed();  // update server with another games played.
            }
        }
    }

    public void SetGameDuration(int gameLength)
    {
        gameText.timeRemaining = gameLength;
    }

    IEnumerator timeToDestroyMessage()
    {
        yield return new WaitForSeconds(1.0f);
        message = GameObject.FindGameObjectWithTag("message");
        Destroy(message);
    }
}
