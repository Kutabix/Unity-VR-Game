using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = cam.position;
        targetPosition.y = transform.position.y; 
        transform.LookAt(targetPosition);

    }
}
