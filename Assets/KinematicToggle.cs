using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class SmartChairCollision : MonoBehaviour
{
    public Collider deskCollider;

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    private Collider chairCollider;

    private bool isAwakened = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        chairCollider = GetComponent<Collider>();

        rb.isKinematic = true;

        if (deskCollider != null)
        {
            Physics.IgnoreCollision(chairCollider, deskCollider, true);
        }

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        WakeUp(); 
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        WakeUp(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        WakeUp(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        WakeUp(); 
    }

    private void WakeUp()
    {
        if (isAwakened) return;

        isAwakened = true;
        rb.isKinematic = false;

        if (deskCollider != null)
        {
            Physics.IgnoreCollision(chairCollider, deskCollider, false);
        }
    }
}
