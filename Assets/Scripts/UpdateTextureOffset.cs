using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTextureOffset : MonoBehaviour
{
    // Speed of the texture movement (optional)
    public float offsetStep = 0.5f;
    public float updateInterval = 0.02f;

    // Reference to the material
    private Material material;

    float nextUpdate = 0;

    void Start()
    {
        // Get the material attached to the object's Renderer component
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Update the texture offset every 0.64 seconds

        if (Time.time > nextUpdate && AppController.SharedInstance.isRunning)
        {
            nextUpdate = Time.time + updateInterval;

            // Calculate the new offset based on time
            float offset = Time.time * offsetStep;

            // Update the texture offset on the material (main texture)
            material.mainTextureOffset = new Vector2(0, offset);
        }

        // Optional: Log the new offset
        // Debug.Log("Texture offset updated to: " + material.mainTextureOffset);
    }

}
