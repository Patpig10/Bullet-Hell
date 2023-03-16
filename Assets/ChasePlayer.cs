using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Transform Player;
    public float MoveSpeed;
    public float detectionDistance;
    public float proximityDistance;
    public float nonChaseHeight;
    public float playerYPosition;
    public float firingRange;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) <= detectionDistance)
        {
            if (Player.position.y < playerYPosition)
            {
                transform.LookAt(Player);
            }
            if (Player.position.y < nonChaseHeight && proximityDistance > Vector3.Distance(Player.position, transform.position))
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, Player.position) <= proximityDistance)
            {
                //Here Call any function U want 
            }

        }
    }
}
