using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerColliderDetection : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Controller Started");
    }


    void OnTriggerEnter(Collider objDetected)
    {
        // Check if the object that entered the trigger is tagged as "Player"
        if (objDetected.CompareTag("Reward"))
        {
            // Debug.Log("Obstacule exit the trigger!");
            objDetected.gameObject.SetActive(false);
            AppController.SharedInstance.points += 1;
            AppController.SharedInstance.pointsUIText.text = AppController.SharedInstance.points.ToString();
            return;
        }
        if (objDetected.CompareTag("Obstacule"))
        {
            // Debug.Log("Obstacule exit the trigger!");

            // objDetected.gameObject.SetActive(false);    
            Animator animatorPlayer = this.transform.GetChild(0).GetComponent<Animator>();
            Animator animatorObstacule = objDetected.GetComponent<Animator>();
            animatorPlayer.SetBool("gameRunning", false);
            // animatorPlayer.SetBool("gameOver", true);
            animatorObstacule.SetBool("Attack", true);
            AppController.SharedInstance.startTracking = false;
            AppController.SharedInstance.isRunning = false;
            AppController.SharedInstance.GameOver();
            return;
        }
    }

}
