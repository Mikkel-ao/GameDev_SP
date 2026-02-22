using UnityEngine;
using UnityEngine.AI;
public class ArtificialIntelligence : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    [Header("Aggro Seittings")] 
    public float aggroRange = 10f; //Distance before AI starts chasing.

    public float stopDistance = 5f; //How close before stopping.
    
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        
        //Player outside aggro range = AI stays idle.
        if (distance > aggroRange)
        {
            agent.SetDestination(transform.position); //Stay where it is
            animator.SetFloat("speed", 0f);
            return;
        }
       
        //Player inside aggro range = AI chases the player
        if (distance < aggroRange)
        {
            agent.SetDestination(target.position);
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
        if (distance <= stopDistance)
        {
            //Player very close = AI stop
            agent.SetDestination(transform.position);
            animator.SetFloat("speed", 0f);
        }
    }
}