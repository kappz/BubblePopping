using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyParent : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Destroy(transform.parent.gameObject);
        Destroy(col.gameObject);  //destroys the bubble hit
        Destroy(gameObject);
    }
}
