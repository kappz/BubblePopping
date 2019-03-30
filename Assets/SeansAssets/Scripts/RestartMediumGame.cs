using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMediumGame : MonoBehaviour {

   public void NewGame() {
        SceneManager.LoadScene("HarpoonGame_NORMAL");
    }
}
