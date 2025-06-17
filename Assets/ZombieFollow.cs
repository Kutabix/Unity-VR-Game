using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieFollowPlayer : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    public float stopDistance = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    private bool isAttacking = false;
    private Coroutine attackCoroutine;

    private PlayerHealth playerHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    float GetDistanceToTarget()
    {
        Vector3 zombiePos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPos = new Vector3(target.position.x, 0, target.position.z);
        return Vector3.Distance(zombiePos, playerPos);
    }

    void Update()
    {
        float distance = GetDistanceToTarget();
        Debug.Log($"distance: {distance}, attackRange: {attackRange}");

        if (distance > stopDistance)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
            agent.SetDestination(target.position);

            StopAttack();
        }
        else
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
            }

            if (distance <= attackRange)
            {
                if (!isAttacking)
                {
                    StartAttack();
                }
            }
            else
            {
                StopAttack();
            }
        }

        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
    }

    void StartAttack()
    {
        isAttacking = true;
        attackCoroutine = StartCoroutine(AttackCycle());
    }

    void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackCycle()
    {
        while (isAttacking)
        {
            animator.SetTrigger("Attack");

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(3); 
            }

            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
