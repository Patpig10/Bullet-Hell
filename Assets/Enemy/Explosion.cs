using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5.0f; // The radius of the explosion
    public float force = 700.0f; // The force of the explosion
    public float damage = 50.0f; // The amount of damage dealt by the explosion
    public GameObject explosionPrefab; // The explosion prefab to instantiate

    // Use this for initialization
    void Start()
    {
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // Check if any colliding object has an Enemy component with isElectric and isBurning both set to true
        bool shouldExplode = false;
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && enemy.isElectric && enemy.isBurning)
            {
                shouldExplode = true;
                break;
            }
        }

        if (shouldExplode)
        {
            // Create a new explosion game object
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Find all colliders within the explosion radius again
            colliders = Physics.OverlapSphere(transform.position, radius);

            // Apply explosion force and damage to all objects within the radius
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage((int)damage);
                }
            }
        }

        // Destroy the explosion object after a short delay
        Destroy(gameObject, 0.5f);
    }
}
