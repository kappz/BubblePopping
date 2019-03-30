/*
 * This script controls the state of the game, and is responsible for transmitting
 * and receiving data to and from the server. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMultiplayer : MonoBehaviour, IMultiplayerView {
    private bool startGame;
    private PreferencesManager prefManager;
    private PlayGamesClient playGamesClient;
    private GameText gameText; // Responsible for updating time remaining and score text objects in scene.
    private SpawnSystem spawnSystem;  // Responsible for intantiating all targets.
    private SplatterSystem splatterSystem; // The splatter particle system.
    private HitDetectionSystemMultiplayer hitDetection; // Responsible for interpreting user touch input.
    public AudioClip targetDestroyed; // Audio clip plays whenever a target is destroyed
    public int numSmallTargets, numMediumTargets, numLargeTargets, gameDuration; // User passes in number of each target type to spawn, and how long game should last for.
    public GameObject smallTarget, mediumTarget, largeTarget, endGameMenu, helpMenu, exitMenu;
    public GameObject good, great, perfect, message;

    // Initialize components, hide all UI artifacts, recieve and update player's high score.
    void Start() {
        playGamesClient = PlayGamesClient.GetInstance();
        playGamesClient.AttachMultiplayerView(this);
        startGame = false;
    }

    void Update() {
        if (startGame == true) {
            // Spawn a new set of bubbles if none exist in the current game state.
            if (spawnSystem.numTargetsInPlay == 0) {
                spawnSystem.SpawnTargets(smallTarget, mediumTarget, largeTarget);
            }

            // Check if user destroyed a target this frame.
            if (Time.timeScale != 0.0f) {
                if (Input.touchCount == 2) {
                    if (hitDetection.UserDestroysTarget(Input.GetTouch(0), Input.GetTouch(1), good, great, perfect))
                    {
                        // Play target destroyed sound, update game score, crete and display splatter effect.
                        StartCoroutine(timeToDestroyMessage());
                        gameObject.GetComponent<AudioSource>().PlayOneShot(targetDestroyed, 1.0f);
                        splatterSystem.SetParticleData(hitDetection.target.Location, 2, hitDetection.target.color);
                        splatterSystem.DisplayParticles();
                        spawnSystem.decrNumTargetsInPlay();
                        playGamesClient.OnScoreUpdate(hitDetection.gameScore.score);
                        hitDetection.gameScore.score = 0;
                    }
                }

                // update time remaining and player's score, opponent's score.
                gameText.UpdateCountdownText();

                // Trigger end game if time remaining <= 0, update server with another game played.
                if (gameText.timeRemaining < 0.0f) {
                    endGameMenu.SetActive(true);
                    playGamesClient.LeaveMultiplayerRoom();
                }
            }
        }
    }

    public void UserExitGame()
    {
        playGamesClient.LeaveMultiplayerRoom();
    }

    public void GameStart() {
        startGame = true;
        gameText = new GameText();
        spawnSystem = new SpawnSystem(numSmallTargets, numMediumTargets, numLargeTargets);  // This will spawn (num small targets, num medium targets, num large targets)
        prefManager = new PreferencesManager();
        hitDetection = new HitDetectionSystemMultiplayer();
        splatterSystem = new SplatterSystem(100, 125, 175, 200, 250, 275, 325);;
    }

    public void StartGame() {
        GameStart();
    }

    public void updatePlayerScore(int score) {
        gameText.UpdateSelfScore(score);
    }

    public void ExitGame() {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    public void updateOpponentScore(int score) {
        gameText.UpdateOpponentScore(score);
    }

    IEnumerator timeToDestroyMessage()
    {
        yield return new WaitForSeconds(1.0f);
        message = GameObject.FindGameObjectWithTag("message");
        Destroy(message);
    }
}
