/*
 * This script displays the help screen for the duration == "helpMenuActiveLength".
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHelpMenu : MonoBehaviour
{
    public float helpMenuActiveLength;  // the duration in which the help screen will stay active
    public GameObject helpMenu;  // the help popup menu

    public void ShowHelpMenu()
    {
        helpMenu.SetActive(true);
        StartCoroutine(WaitSeconds(helpMenuActiveLength, helpMenu));  // wait seconds and hide help screen
    }

    IEnumerator WaitSeconds(float secondsToWait, GameObject helpScreen)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + secondsToWait)
        {
            yield return null;
        }
        helpScreen.SetActive(false);
    }

    public void ShowHelpMenuBug()
    {
        StartCoroutine(WaitSecondsBug(helpMenuActiveLength, helpMenu));
    }

    IEnumerator WaitSecondsBug(float secondsToWait, GameObject helpScreen)
    {
        yield return new WaitForSeconds(secondsToWait);
        helpScreen.SetActive(false);
        Debug.Log("Yolo");      
    }
}
