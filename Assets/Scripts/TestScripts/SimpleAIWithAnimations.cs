using UnityEngine;
using UnityEngine.AI;

public class SimpleAIWithAnimations : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    [SerializeField] private Transform Target;
    private Animator animator;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(agent.destination, Target.position);

        // --- AI Wandering Logic ---
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
            agent.SetDestination(newPos);
            timer = 0f;
        }

        // --- Animation Logic ---
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        // If you want a run animation:
        animator.SetBool("IsRunning", speed > 2.5f); // adjust threshold
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;
    }
}