using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsAnimation : MonoBehaviour
{
    public InputActionReference gripReference;
    public InputActionReference triggerReference;
    public Animator handAnimator;

    private
    void Update()
    {
        float gripValue = gripReference.action.ReadValue<float>();
        Debug.Log(gripValue);
        handAnimator.SetFloat("Grip", gripValue);

        float triggerValue = triggerReference.action.ReadValue<float>();
        Debug.Log(triggerValue);
        handAnimator.SetFloat("Trigger", triggerValue);
    }
}