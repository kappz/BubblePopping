using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeChange : MonoBehaviour {

	private AudioSource music;
	private float musicVolume = 1f;


	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		music.volume = musicVolume;
		
	}

	public void SetVolume(float vol){
		musicVolume = vol;
}
}