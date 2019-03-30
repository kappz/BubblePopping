using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu_VolumeSlider : MonoBehaviour {
    private AudioSource source;

    private void Start()
    {
        //source = GameObject.FindWithTag("GameSystems").GetComponent<AudioSource>();
        source = GameObject.FindWithTag("Sound").GetComponent<AudioSource>();
    }
    public void ChangeVolume() {
        source.volume = gameObject.GetComponent<Slider>().value;
    }
}
