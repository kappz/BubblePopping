using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {
    public GameObject settingsMenu;
    public AudioClip buttonSound;
    private AudioSource source;
    private GameObject lookForSettingsMenu;

    public void OpenSettingsMenu()
    {
        lookForSettingsMenu = GameObject.FindWithTag("settings");
        source = gameObject.GetComponent<AudioSource>();
        if (lookForSettingsMenu == null) {
            source.PlayOneShot(buttonSound, 0.5f);
            Instantiate(settingsMenu);
            Time.timeScale = 0.0f;
        }
    }
}
