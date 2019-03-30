using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
   // public string myScene;

    public void LoadSceneByName(string s)
    {
       // s = myScene;
        Debug.Log("Exiting to Main Menu Scene");
        SceneManager.LoadScene(s);
            
     }
}
