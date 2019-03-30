using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu_Restart : MonoBehaviour {

    public void RestartGame(int sceneANumber)
    {
        GameObject settingsMenu = GameObject.FindWithTag("settings");
        SceneManager.LoadScene(sceneANumber);
        Time.timeScale = 1.0f;
        Destroy(settingsMenu);
    }
}
