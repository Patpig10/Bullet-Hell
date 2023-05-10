using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1; // Amount of damage to deal

    private void OnTriggerEnter(Collider other)
    {
        HeartSystemNolimit heartSystem = other.GetComponent<HeartSystemNolimit>();
        if (heartSystem != null)
        {
            heartSystem.TakeDamage(damage);
        }
    }
}
