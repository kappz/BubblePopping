using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menuOptions : MonoBehaviour
{

	public void onSinglePlayer ()
	{
		Debug.Log ("You clicked on SINGLEPLAYER!");
		//load the single player options scene
		SceneManager.LoadScene ("gameMode_Single");

	}

	public void onMultiplayer ()
	{
		Debug.Log ("You clicked on MULTIPLAYER!");

		//load the single player options scene
		SceneManager.LoadScene ("gameMode_Multi");
		//SceneManager.LoadScene ("singlePlayerMenu");

	}

	public void onTutorial ()
	{
		Debug.Log ("You clicked on TUTORIAL!");

		//load the single player options scene
		//SceneManager.LoadScene ("singlePlayerMenu");

	}


}
