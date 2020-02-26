using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform lookDirection;

    private Vector3 targetPosition;
    private float offset = 1.5f;
    private float distance = 3.5f;

    void Update()
    {
        // Position
        targetPosition = lookDirection.position - transform.forward*distance + transform.up*offset;
        transform.position = Vector3.Lerp(transform.position,targetPosition,0.05f);

        // Rotation
        transform.LookAt(lookDirection.position + Vector3.up*offset);
    }
}
