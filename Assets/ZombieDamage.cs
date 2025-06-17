using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class ZombieDamageHandler : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isDead = false;
    private int health = 100;

    

    public GameObject healthBarCanvas;
    public Image healthBar;

    private float targetFill;   
    private float currentFill;  

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        targetFill = currentFill = 1f;
        healthBar.fillAmount = 1f;
    }

    void Update()
    {
        currentFill = Mathf.MoveTowards(currentFill, targetFill, Time.deltaTime * 2f);
        healthBar.fillAmount = currentFill;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 25;

            targetFill = (float)health / 100f;

            if (health <= 0)
            {
                isDead = true;

                if (navMeshAgent != null)
                {
                    navMeshAgent.enabled = false;
                }

                GetComponent<ZombieFollowPlayer>().enabled = false;

                if (healthBarCanvas != null)
                {
                    healthBarCanvas.SetActive(false);
                }

                animator.SetTrigger("IsDeath");
                StartCoroutine(DestroyAfterDeath());
            }
            else
            {
                animator.SetTrigger("TakeDamage");

            }
        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
