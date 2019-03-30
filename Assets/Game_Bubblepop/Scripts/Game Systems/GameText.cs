/*
This class updates the game text that displays time remaining, player's current score, player's high score, and opponent's score.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText {

    public float timeRemaining { get; set; }
    GameObject countdownText;  // Text displaying the time remaining.
    GameObject gameScoreText;  // Text displaying the player's score.
    GameObject opponentScore;  // Text display the opponent's score.
    public GameObject highScoreText;
    private string timeToDisplay;

    // Find and assign references to text objects in game scene. 
    public GameText() {
        timeRemaining = 75.0f;
        gameScoreText = GameObject.FindWithTag("scoreText");
        countdownText = GameObject.FindWithTag("countdownText");
        //opponentScore = GameObject.FindWithTag("opponentScore");
        highScoreText = GameObject.FindWithTag("highScore");
    }

    // Update self's score
    public void UpdateSelfScore(int score) {
        gameScoreText.GetComponent<Text>().text = score.ToString();
    }

    // update countdown text
    public void UpdateCountdownText() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining < 10) {
            countdownText.GetComponent<Text>().text = "0" + (int)timeRemaining;
        }
        else {
            countdownText.GetComponent<Text>().text = "" + (int)timeRemaining;
        }
    }

    // update player's high score, for single player only.
    public void UpdateHighScore(int score) {
        highScoreText.GetComponent<Text>().text = score.ToString();
    }

    // update opponent's score for player to see during multiplayer
    public void UpdateOpponentScore(int score) {
        highScoreText.GetComponent<Text>().text = score.ToString();
    }

}
