/*
This script controls the game's countdown timer. This script is attatched to the persistent object 'GameSystems'.
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CountdownTimer : MonoBehaviour {
    public float gameTime;  // total time
    private float timeLeft;  // remaining time
    public Text countdownText;  // text object that displays timer

    void Start()  {
        timeLeft = gameTime;
        InvokeRepeating("decrTimeLeft", 1.0f, 1.0f);  // decrement time by one second for every second of the game
    }

    void LateUpdate() {
        updateCountdownText(); //update the text object with remaining time at the end of each frame
    }

    void updateCountdownText() {
        countdownText.text = ("Time Left: " + (int)timeLeft);  // update text object with new time
    }

    void decrTimeLeft(){
        --timeLeft;  // decrememnt reamaining time by one
    }
}