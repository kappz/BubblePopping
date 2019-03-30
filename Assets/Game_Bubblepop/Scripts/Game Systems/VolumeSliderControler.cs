using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderControler : MonoBehaviour {
    private AudioSource soundSource;
    private Slider slider;
	// Use this for initialization
	void Start () {
        soundSource = GameObject.FindWithTag("GameSystems").GetComponent<AudioSource>();
        slider = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        soundSource.volume = slider.value;
	}
}
