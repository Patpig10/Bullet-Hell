using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrozenObjectsController : MonoBehaviour
{
    public float frozenSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Find all objects with the "frozen" tag
        GameObject[] frozenObjects = GameObject.FindGameObjectsWithTag("frozen");

        // Loop through all frozen objects and adjust their speed
        foreach (GameObject obj in frozenObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply the frozen speed to the object's velocity
                rb.velocity = rb.velocity.normalized * frozenSpeed;
            }
        }
    }
}
