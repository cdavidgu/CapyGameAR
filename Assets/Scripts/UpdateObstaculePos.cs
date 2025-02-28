using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObstaculePos : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && AppController.SharedInstance.isRunning)
        {
            this.transform.localPosition += Vector3.forward * speed * Time.deltaTime;
        }
    }
}
