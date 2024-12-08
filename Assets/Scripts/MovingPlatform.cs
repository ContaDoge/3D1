using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Route Settings")]
    public RouteManager routeManager;     // Shared route manager
    public float speed = 3f;              // Platform movement speed
    public int startingPointIndex = 0;    // Custom starting point for this platform
    private int currentPointIndex;        // Current waypoint index
    private Rigidbody rb;                 // Platform's Rigidbody
    private Transform[] waypoints;        // Local reference to shared waypoints

    private Vector3 previousPosition;     // Previous position for platform
    private Vector3 platformMovement;     // Movement for the platform

    private void Start()
    {
        // Ensure the RouteManager and waypoints exist
        if (routeManager == null || routeManager.waypoints.Length == 0)
        {
            Debug.LogError("RouteManager or waypoints not set!");
            return;
        }

        // Copy shared waypoints
        waypoints = routeManager.waypoints;

        // Set the starting point index
        currentPointIndex = startingPointIndex % waypoints.Length;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Ensure platform doesn't react to physics
        previousPosition = transform.position; // Set initial position
    }

    private void FixedUpdate()
    {
        if (waypoints.Length > 0)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        // Move toward the current waypoint
        Vector3 targetPosition = waypoints[currentPointIndex].position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);

        // Calculate platform movement
        platformMovement = transform.position - previousPosition;
        previousPosition = transform.position;

        // Check if close to the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            UpdateTargetPoint();
        }
    }

    private void UpdateTargetPoint()
    {
        // Move to the next waypoint
        currentPointIndex = (currentPointIndex + 1) % waypoints.Length;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Apply platform movement to the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.position += platformMovement;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the platform.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left the platform.");
        }
    }
}
