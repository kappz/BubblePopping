using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu_Close : MonoBehaviour {

    public void CloseMenu() {
        GameObject menu = GameObject.FindWithTag("settings");
        Time.timeScale = 1.0f;
        Destroy(menu);
    }
}
