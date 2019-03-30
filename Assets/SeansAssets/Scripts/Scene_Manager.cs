using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene_Manager: MonoBehaviour
{
	void Start()
	{
		Debug.Log("LoadSceneA");
	}

	public void LoadA(string scenename)
	{
		Debug.Log("sceneName to load: " + scenename);
		SceneManager.LoadScene(scenename);
	}

  
}