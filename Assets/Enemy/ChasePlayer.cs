using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChasePlayer : MonoBehaviour
{
    public Transform player;
    public float stoppingDistance = 2f;
    public float detectionRadius = 10f;
    public float hearingDistance = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public FrostStatus frostStatus;
    public float aggressionLevel = 1f; // New variable for aggression level

    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private bool isPatrolling = true;
    private bool isFrozen = false;
    private float originalSpeed;
    public Animator animator;

    void Start()
    {
        frostStatus = GetComponent<FrostStatus>();
        agent = GetComponent<NavMeshAgent>();
        patrolDestination = transform.position;
        animator = GetComponent<Animator>();
        originalSpeed = agent.speed;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection radius
        if (distanceToPlayer <= detectionRadius)
        {
            // Check if the player is within the hearing distance
            if (distanceToPlayer <= hearingDistance)
            {
                // Stop patrolling and chase the player
                isPatrolling = false;
                agent.speed = isFrozen ? originalSpeed * 0.5f : chaseSpeed * aggressionLevel; // Adjust the chase speed based on aggression level
                agent.SetDestination(player.position);
                animator.SetBool("Walk Forward", true); // Set the RunForward parameter to true
            }
            else
            {
                // Chase the player while maintaining a stopping distance
                isPatrolling = false;
                agent.speed = isFrozen ? originalSpeed * 0.5f : chaseSpeed * aggressionLevel; // Adjust the chase speed based on aggression level
                Vector3 targetPosition = player.position - (transform.position - player.position).normalized * stoppingDistance;
                agent.SetDestination(targetPosition);
                animator.SetBool("Walk Forward", true); // Set the RunForward parameter to true
            }
        }

        if (isPatrolling)
        {
            // Check if we have reached the patrol destination
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // Choose a new patrol destination
                Vector3 randomDirection = Random.insideUnitSphere * 10f;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
                patrolDestination = hit.position;
            }

            // Move towards the patrol destination
            agent.speed = isFrozen ? originalSpeed * 0.5f : patrolSpeed;
            agent.SetDestination(patrolDestination);
            animator.SetBool("Walk Forward", false); // Set the RunForward parameter to false
        }
    }
}
