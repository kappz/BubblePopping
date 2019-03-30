using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public int timeLeft;
    public Text countdownText;
    public GameObject gameOver;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
        countdownText.text = (timeLeft.ToString());

        if (timeLeft <= 0 ) {
            StopCoroutine("LoseTime");
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
	}

    IEnumerator LoseTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
