using UnityEngine;
using UnityEngine.AI;

public class SimpleAILogic : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    [Header("Aggro Settings")]
    public float aggroRange = 12f;
    public float stopDistance = 2.5f;

    [SerializeField] private Transform Target;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("PlayerTarget");

        if (player != null)
            Target = player.transform;
        else
            Debug.LogError("Player not found!");
        //Debug.Log("NPC: " + transform.position + " Player: " + Target.position);
        timer = wanderTimer;
    }

    void Update()
    {
        if (Target == null)
        {
            Debug.LogWarning("NPC has no Target assigned!");
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, Target.position);
        timer += Time.deltaTime;
        //Debug.Log("Distance: " + distanceToTarget);
        //Debug.Log("Target position: " + Target.position);
        // ---- CHASE (continuous update) ----
        if (distanceToTarget <= aggroRange && distanceToTarget > stopDistance)
        {
            Debug.Log("Target found!" +  Target.tag);
            // Always set the destination to the current player position
            agent.SetDestination(Target.position);
        }
        // ---- STOP ----
        else if (distanceToTarget <= stopDistance)
        {
            agent.SetDestination(transform.position);
            combat();
        }
        // ---- TARGET LOST ----
        if (distanceToTarget > aggroRange)
        {
            Debug.Log("Target Lost!");
        }
        // ---- WANDER ----
        else
        {
            Wander();
        }

        // ---- ANIMATION ----
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", speed > 2.5f); 
    }

    void Wander()
    {
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
            agent.SetDestination(newPos);
            timer = 0f;
        }
    }

    void combat()
    {
        
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance + origin;

        NavMeshHit navHit;

        if (NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas))
        {
            return navHit.position;
        }

        return origin; // fallback so we never return invalid positions
    }
}