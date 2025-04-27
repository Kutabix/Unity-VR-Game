using UnityEngine;

public class ZombieWalker : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    public float walkDistance = 5.0f;

    private Vector3 startPosition;
    private bool isWalking = false;

    void Start()
    {
        startPosition = transform.position;
        StartWalking();
    }

    void Update()
    {
        if (isWalking)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            float distanceWalked = Vector3.Distance(startPosition, transform.position);
            if (distanceWalked >= walkDistance)
            {
                StopWalking();
            }
        }
    }

    void StartWalking()
    {
        isWalking = true;
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    void StopWalking()
    {
        isWalking = false;
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }
}
