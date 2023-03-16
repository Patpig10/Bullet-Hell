using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public Animator hitAnimator;

    public int health = 1;
    public Rigidbody rb;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage()
    {

        health -= BulletController.damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
           

            health--;
            if (health <= 0)
            {
                Die();
            }
        }

        if (other.CompareTag("Ice"))
        {


            health--;
            if (health <= 0)
            {
                Die();
            }
        }

    }
    void Die()
    {

        Destroy(gameObject);
    }
}
