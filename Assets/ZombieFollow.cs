using UnityEngine;
using UnityEngine.AI;

public class ZombieFollowPlayer : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    public float stopDistance = 1.5f; // Odleg³oœæ zatrzymania

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > stopDistance)
            {
                if (agent.isStopped)
                {
                    agent.isStopped = false;
                }
                agent.SetDestination(target.position);
            }
            else
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero; // <--- ZERUJEMY velocity!
                }
            }
        }

        float speed = agent.velocity.magnitude;

        if (animator != null)
        {
            animator.SetBool("isWalking", speed > 0.1f);
        }
    }
}
