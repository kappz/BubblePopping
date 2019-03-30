using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem
{

    public void displayMessage(GameObject message, Vector3 destroyedTargetLocation)
    {
        Vector3 messagePosition = destroyedTargetLocation;
        messagePosition.z = -400.0f;
        MonoBehaviour.Instantiate(message, destroyedTargetLocation, Quaternion.identity);
    }
}
