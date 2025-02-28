using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerExit(Collider objDetected)
    {
        // Check if the object that entered the trigger is tagged as "Player"
        if (objDetected.CompareTag("Obstacule") || objDetected.CompareTag("Reward"))
        {
            // Debug.Log("Obstacule exit the trigger!");
            objDetected.gameObject.SetActive(false);
        }
    }
}
