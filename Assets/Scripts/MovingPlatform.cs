using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Route Settings")]
    public RouteManager routeManager;     // Shared route manager
    public float speed = 3f;              // Platform movement speed
    private int currentPointIndex = 0;    // Current waypoint index
    private Rigidbody rb;                 // Platform's Rigidbody
    private Transform[] waypoints;        // Local reference to waypoints

    private Vector3 previousPosition;     // Previous position of the platform
    private Vector3 platformMovement;     // How much the platform moved

    private void Start()
    {
        // Ensure waypoints are assigned
        if (routeManager == null || routeManager.waypoints.Length == 0)
        {
            Debug.LogError("RouteManager or waypoints not set!");
            return;
        }

        // Initialize waypoints and Rigidbody
        waypoints = routeManager.waypoints;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        previousPosition = transform.position; // Initialize previous position
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
        // Move towards the current waypoint
        Vector3 targetPosition = waypoints[currentPointIndex].position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);

        // Calculate platform movement since the last frame
        platformMovement = transform.position - previousPosition;
        previousPosition = transform.position;

        // Check if the platform is close to the waypoint
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the platform.");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Manually apply platform movement to the player 
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.position += platformMovement; // Apply the platform's movement to the player
            }
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
