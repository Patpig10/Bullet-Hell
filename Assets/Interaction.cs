using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public UnityEvent interact;
    public string playerTag = "Player"; // tag of the player object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            interact.Invoke();
        }
    }
}
