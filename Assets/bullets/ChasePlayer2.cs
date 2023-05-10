using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer2 : MonoBehaviour
{
    public Transform player;
    public float stoppingDistance = 2f;
    public float retreatDistance = 1f;
    public float detectionRadius = 10f;
    public float hearingDistance = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private bool isPatrolling = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolDestination = transform.position;
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
                // Stop chasing and start patrolling
                isPatrolling = true;
            }
            else
            {
                // Chase the player
                isPatrolling = false;
                agent.speed = chaseSpeed;
                agent.SetDestination(player.position);
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
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolDestination);
        }
    }
}
