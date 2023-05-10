using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDector : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Replace "Enemy" with the tag of the collider you want to trigger the damage animation
        {
            animator.SetTrigger("Stab Attack");
            // Add your health system code here to decrease the player's health
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
