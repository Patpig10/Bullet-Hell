using UnityEngine;
using UnityEngine.AI;

public class TestAI : MonoBehaviour
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
    private int nextIndex;
    public GameObject[] waypoints;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        patrolDestination = NextWaypoint(Vector3.zero);
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
                patrolDestination = NextWaypoint(patrolDestination);
            }

            // Move towards the patrol destination
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolDestination);
        }

        // Update the animator parameters
        bool shouldMove = agent.velocity.magnitude > 0.025f && agent.remainingDistance > agent.radius;
        GetComponent<Animator>().SetBool("runBool", shouldMove);
        GetComponent<Animator>().SetBool("runBool", true);
        GetComponent<Animator>().SetBool("idleBool", false);
    }

    private Vector3 NextWaypoint(Vector3 currentPosition)
    {
        if (currentPosition != Vector3.zero)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (currentPosition == waypoints[i].transform.position)
                {
                    nextIndex = (i + 1) % waypoints.Length;
                }
            }
        }
        else
        {
            nextIndex = 0;
        }
        return waypoints[nextIndex].transform.position;
    }
}
