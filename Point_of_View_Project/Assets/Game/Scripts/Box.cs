using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody rb;
    public LayerMask groundLayer; // Layer of the ground for detection
    public float checkDistance = 1f; // Distance of the raycast downwards
    public Vector3[] controlPoints; // Relative points for raycasts

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        CheckIfAboveHole();
    }

    private void CheckIfAboveHole()
    {
        bool isAboveGround = false;
        Debug.Log("primo step");
        // Check each control point of the box
        foreach (var point in controlPoints)
        {
            Debug.Log("secondo step");
            Vector3 worldPoint = transform.position + transform.TransformDirection(point);
            if (Physics.Raycast(worldPoint, Vector3.down, checkDistance, groundLayer))
            {
                isAboveGround = true;
                Debug.Log("Above ground");
                break; // Stop checking if any point is over the ground
            }
        }
        
        Debug.Log("terzo step");
        if (!isAboveGround)
        {
            Debug.Log("quarto step");
            // Freeze lateral movement and allow vertical fall
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            Debug.Log("quinto step");
            // Allow normal movement
            // rb.constraints = RigidbodyConstraints.None;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the control points and raycasts
        Gizmos.color = Color.red;
        foreach (var point in controlPoints)
        {
            Vector3 worldPoint = transform.position + transform.TransformDirection(point);
            Gizmos.DrawLine(worldPoint, worldPoint + Vector3.down * checkDistance);
        }
        

    }
}