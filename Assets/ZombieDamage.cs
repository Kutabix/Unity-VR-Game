using UnityEngine;

public class ZombieDamageHandler : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private int health = 100;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            }
            else
            {
                animator.SetTrigger("TakeDamage");
            }
        }
    }
}
