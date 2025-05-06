using UnityEngine;

public class ZombieDamageHandler : MonoBehaviour
{
    private Animator animator;
    private int health = 100;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 30;

            if (health > 0 )
            {
                animator.SetTrigger("TakeDamage");
            } else
            {
                animator.SetBool("IsAlive", false);
                Destroy(gameObject, 2f);
            }
        }
    }
}
