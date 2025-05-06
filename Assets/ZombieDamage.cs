using UnityEngine;
using System.Collections;

public class ZombieDamageHandler : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private int health = 100;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 30;

            if (health <= 0 )
            {
                isDead = true;
                animator.SetTrigger("IsDeath");
                StartCoroutine(DestroyAfterDeath());
            }
            else
            {
                animator.SetTrigger("TakeDamage");

            }
        }
    }
}
