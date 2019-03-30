using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartHardGame : MonoBehaviour {

    public void NewGame() {
        SceneManager.LoadScene("HarpoonGame_HARD");
    }
}
