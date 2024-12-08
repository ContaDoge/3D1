using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform[] points;        // Array of points the platform moves between
    public float speed = 3f;          // Movement speed of the platform
    public int currentPointIndex = 0; // Index of the current target point

    private Transform targetPoint;    // Current target point
    private Rigidbody rb;             // Platform's Rigidbody

    private void Start()
    {
        if (points.Length == 0)
        {
            Debug.LogError("No points assigned to the Moving Platform!");
            return;
        }

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Platform doesn't react to physics
        targetPoint = points[currentPointIndex];
    }

    private void FixedUpdate()
    {
        if (targetPoint != null)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        // Move towards the target point
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

        // Check if the platform is close enough to the target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            UpdateTargetPoint();
        }
    }

    private void UpdateTargetPoint()
    {
        // Cycle to the next point
        currentPointIndex = (currentPointIndex + 1) % points.Length;
        targetPoint = points[currentPointIndex];
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make the player a child of the platform when standing on it
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Remove the player from being a child when leaving the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
