using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
   // public Animator hitAnimator;

    public int health = 1;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
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
           // hitAnimator.SetTrigger("shot");

            health--;
            if (health <= 0)
            {
                Die();
            }
        }
        else
  if (other.CompareTag("Ice"))
        {
            //hitAnimator.SetTrigger("shot");

            health--;
            if (health <= 0)
            {
                Die();
            }
        }
        else
              if (other.CompareTag("Fire"))
        {
         //   hitAnimator.SetTrigger("shot");

            health--;
            if (health <= 0)
            {
                Die();
            }
        }
        else
        if (other.CompareTag("Lightening"))
        {
          //  hitAnimator.SetTrigger("shot");

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
