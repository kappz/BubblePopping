using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartEasyMode : MonoBehaviour {

    public void NewGame() {
        SceneManager.LoadScene("HarpoonGame_EASY");
    }
}
